using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secret;

    public JwtMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _secret = config["Jwt:Key"] ?? ""; 
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];

        if (!string.IsNullOrEmpty(token))
            AttachUserToContext(context, token);

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken? jwtToken = (JwtSecurityToken)validatedToken;
            string? userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;

            // user id into context
            context.Items["UserId"] = userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            // token invalid
        }
    }
}
