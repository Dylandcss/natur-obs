using System.Security.Claims;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using AuthService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    //Service d'authentification injecté par DI
    private readonly IAuthService _authService;

    // Constructeur : injecte automatiquement le service
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Inscrit un utilisateur et l'envoi vers la page de login
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var response = _authService.Register(dto);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Authentifie un utilisateur et retourne un JWT
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest dto) 
    {
        //Validation ddu model qui n'est plus obligatoire car APIController
        if(!ModelState.IsValid) return BadRequest(ModelState);

        //Appel au service d'authentification
        //Si succès => retourne le responsedto avec le token
        //Si echec => lance UnauthorizedAccessException => Middleware => 401
        var reponse = _authService.Authenticate(dto);

        return Ok(reponse);
    }
    
    //Retourner les informations de l'utilisateur actuellement connecté
    //Cette route lit les claims stockés dans le JWT
    //Le token est automatiquement décodé par le middleware d'authentification
    [Authorize] //Il faut etre connecté pour accéder à cette route
    [HttpGet("moi")]
    public IActionResult GetCurrentUser()
    {
        // User : C'est une propriété héritée de controllerBase
        // Elle contient les claims extraits du jwt par le middleware

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        //Si aucun claim n'est trouvé c'est qu'il y a un probleme avec le token.
        if(userId == null ||email == null)
        {
            throw new UnauthorizedAccessException("Token invalide ou corrompu.");
        }

        // On retourne un objet anonyme avec les infos de l'utilisateur
        return Ok(new
        {
            Id = int.Parse(userId),
            Email = email,
            Role = role,
            Message = "Vous êtes bien authentifié !"
        });

         

    }

    [HttpGet("/ping")]
    [Authorize]
    public IActionResult Ping()
    {
        return Ok("Token Valide");
    }

}