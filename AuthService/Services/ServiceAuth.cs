using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.DTOs;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services;

public class ServiceAuth : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    
    public ServiceAuth(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    public RegisterResponse Register(RegisterRequest dto)
    {
        var user = _userRepository.GetByEmail(dto.Email);
        if (user != null)
        {
            throw new Exception("Email ou mot de passe incorrect.");
        }
        
        var pass = PasswordService.HashPassword(dto.Password);
        
        var newUser = new User(dto.Username, dto.Email, pass)
        {
            Username = dto.Username,
            PasswordHash = pass,
            Email = dto.Email,
        };
        
        _userRepository.Create(newUser);
        
        return new RegisterResponse()
        {
            Username = newUser.Username,
            Email = newUser.Email,
            Password = newUser.PasswordHash,
        };
    }

    public AuthResponse Authenticate(LoginRequest dto)
    {
            var user = _userRepository.GetByUsername(dto.Username);
            
            if(user == null || !PasswordService.VerifyPassword(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Username ou mot de passe incorrect");
            }

            // Recupération de la configuration JWT
            //On lit la clé secrère depuis appsettings.json
            // Si elle es=xiste pas , on envoie une exception avec un message clair
            var secretKey = _config["jwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("jwtSettings:SecretKey manque dans appsettings.json");

            // duree de validité du token , par defaut un jours si non configuré
            var expirationDays = int.Parse(_config["jwtSettings:ExpirationInDays"] ?? "1");

            // Generation du token

            //L'outil qui sait créer et lire des jwt
            var tokenHandler = new JwtSecurityTokenHandler();

            // pour l'algo de signature
            // on a besoin que clé secrete soit converti en tableau de bytes
            var key = Encoding.ASCII.GetBytes(secretKey);

            //Date d'expiration du token
            var expiration = DateTime.Now.AddDays(expirationDays);

            // Recette du token : ce qu'il contient et comment le signer
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //On va mettre les infos de la "carte magnétique"
                Subject = new ClaimsIdentity([
                    // CLaim 1 : L'id de l'utilisateur 
                    // Utile pour savori qui fait la requete coté server
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                    //Claim 2 : Le role de l'utilisateur
                    //Utilisé pour vérifier les droits [authorize(Roles="Admin")]
                    new Claim(ClaimTypes.Role, user.Role.ToString()),

                    //Claime 3 : L'email de l'utilisateur
                    //utile pour afficher coté client ou dans les logs
                    new Claim(ClaimTypes.Email, user.Email)

                ]),

                //Date d'expiration
                Expires = expiration,

                //Donner une signature numérique
                // Empeche la falsification du token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)

            };

            // Fabrication du token à partir de la recette
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse()
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.Username,
                Role = user.Role.ToString(),
                ExpiresIn = expiration.Ticks,
            };
        }
}