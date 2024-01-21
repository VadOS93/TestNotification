using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace TestNotification.Handlers;

/// <summary>
/// Basic Authentication handler
/// </summary>
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
	public BasicAuthenticationHandler(
		IOptionsMonitor<AuthenticationSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder,
		ISystemClock clock)
		: base(options, logger, encoder, clock)
	{
	}

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		if (!Request.Headers.ContainsKey("Authorization"))
			return AuthenticateResult.Fail("Authorization header not found");

		try
		{
			var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
			var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
			var credentials = System.Text.Encoding.UTF8.GetString(credentialBytes).Split(':');
			var username = credentials[0];
			var password = credentials[1];

			// Replace this with your actual authentication logic
			if (IsAuthenticated(username, password))
			{
				var claims = new[] { new Claim(ClaimTypes.Name, username) };
				var identity = new ClaimsIdentity(claims, Scheme.Name);
				var principal = new ClaimsPrincipal(identity);
				var ticket = new AuthenticationTicket(principal, Scheme.Name);

				return AuthenticateResult.Success(ticket);
			}
			else
			{
				return AuthenticateResult.Fail("Invalid username or password");
			}
		}
		catch (Exception ex)
		{
			return AuthenticateResult.Fail("Error during authentication: " + ex.Message);
		}
	}

	private bool IsAuthenticated(string username, string password)
	{
		// Replace this with your actual authentication logic
		return username == "testuser" && password == "testpassword";
	}
}