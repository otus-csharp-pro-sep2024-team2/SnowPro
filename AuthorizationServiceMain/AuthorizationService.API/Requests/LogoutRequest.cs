using System;
namespace AuthorizationService.API.Requests
{
	public class LogoutRequest
	{
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}

