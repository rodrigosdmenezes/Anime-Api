using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api")]
public class Autenticar : ControllerBase
{
    /// <summary>
    /// Autenticar Usuario na Api
    /// </summary>
    /// <returns>Apresentar Token de Autenticação</returns>
    private readonly IConfiguration _configuration;

    public Autenticar(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        var username = "AnimeApi";

        var token = GenerateJwtToken(username);

        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(string username)
    {
        var jwtConfig = _configuration.GetSection("Jwt");
        var secretKey = jwtConfig.GetValue<string>("SecretKey");
        var issuer = jwtConfig.GetValue<string>("Issuer");
        var audience = jwtConfig.GetValue<string>("Audience");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            Issuer = issuer,
            Audience = audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}    
