using AuthService.Middlewares;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using AuthService.Data;
using AuthService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //Information de base de l'API
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Articles",
        Version = "v1",
        Description = "Api de gestion des articles avec authentification JWT"
    });

    // configuration du bouton AUthorize dans swagger
    //Permet de tester les routes prot�g�es directement dans swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        //Nom du header HTTP o� envoyer le token
        Name = "Authorization",

        // Type Http = on utilise le sch�ma bearer (standart OAuth2/JWT)
        Type = SecuritySchemeType.Http,

        // "Bearer" = le prefixe du token dans le header
        Scheme = "Bearer",

        // Format du token (informatif)
        BearerFormat = "JWT",

        // Le token dasn le Header Http (pas dans l'url ou dans le body)
        In = ParameterLocation.Header,

        //Description affich�e dans la popup Swagger
        Description = "Entrez votre token JWT. \nExemple : eyJHjhfgjhdfOSDGJ...."
    });

    // Applique cette s�curit� � TOUTES les routes par d�faut
    // (les routes sans [Authorize] fonctionneront quand m�me sans token)
    // AddSecurityRequirement dit � Swagger :
    //"Pour TOUTES les routes, ajoute un cadenas qui demande Bearer token"
    // Cette configuration permet d'afficher le bouton "Authorize" dans Swagger
    // et d'ajouter automatiquement le cadenas sur les routes prot�g�es ([Authorize])
    options.AddSecurityRequirement(document =>
    {
        // OpenApiSecurityRequirement = r�gle de s�curit� appliqu�e aux routes Swagger
        // C'est une sorte de dictionnaire :
        //  - CL�   : le sch�ma de s�curit� utilis� (ici Bearer / JWT)
        //  - VALEUR: les scopes requis (vide car on n'utilise pas OAuth avec scopes)

        var requirement = new OpenApiSecurityRequirement
        {
            // OpenApiSecuritySchemeReference fait r�f�rence au sch�ma "Bearer"
            // d�fini plus haut avec AddSecurityDefinition("Bearer", ...)
            // 
            // "Bearer" : nom exact du sch�ma
            // document : permet � Swagger de retrouver ce sch�ma au moment du rendu
            [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
        };

        // On retourne cette r�gle pour l'appliquer � TOUTES les routes Swagger
        return requirement;
    });

});

string ConnectionString = builder.Configuration.GetConnectionString("default");

// enregistrer les repo
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString)));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, ServiceAuth>();

// Configuration de l'authentification JWT
//--------------------

//R�cup�ration de la cl� secr�te depuis appsettings
//C'est la MEME cl� que celle utilis� pour signer les tokens
var secretKey = builder.Configuration["jwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("jwtSettings:SecretKey manquant.");

//conversion de la cl� en tableau de bytes
var key = Encoding.ASCII.GetBytes(secretKey);

//Configuration du systeme d'authentification ASP.NEt
builder.Services.AddAuthentication(options =>
{
    //Jwtbearer = on utilise des tokens jwt dans le header Authorization
    // C'est le schema par d�faut pour authentifier et pour "Challenger" (demander auth)
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // false : accepter les requete http(pas https)
    // en prod : toujours true
    options.RequireHttpsMetadata = false;

    //stocker le token dans le httpcontext pour y acceder si besoin plus tard
    options.SaveToken = true;

    //PARAMETRE DE VALIDATION DU TOKEN
    //A chque requete avec [Authorize] ASP.NET v�rifie automatiquement
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //verifie que la signature correspond � notre cl� secr�te
        // Si quelqu'un modifie le token, la signature ne correspondra plus => rejet�
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        //On ne v�rifie pas l'emmetru pour simplifier le cours
        // En prod ; validateissuer = true + ValidIssuer = "MONAPI"
        //L'issuer garantit que le token vient bien du bon serveur d'authentification
        ValidateIssuer = false,

        //On ne verifie par l'audience pour simplifier le cours
        // en prod : ValidateAudience = true + ValideAUdience = "Mes clients"
        // Token pour app mobile est diff�rent du token pour app web
        //Chacun verifie son audience
        ValidateAudience = false,

        // On veut pas de tol�rance sur l'expiration du token
        // Par defaut : 5minutes de marge
        //Ici on met 0 pour dire que une fois le token expir� alors il est rejet� imm�diatement
        ClockSkew = TimeSpan.Zero

    };

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Sp�cifie EXACTEMENT o� Swagger doit chercher le JSON
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API NaturObs V1");
    });

    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
// Qui est tu ?
// Lit le header Authorization , valide le token JWT
// Remplire HttpContext.User avec les claims du token 
app.UseAuthentication(); 

// As tu le droit?
//Verifie les attributs [Authorize] et [Authorize(Roles="...")]
app.UseAuthorization();

app.MapControllers();

app.Run();
