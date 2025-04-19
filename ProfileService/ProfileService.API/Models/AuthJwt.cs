namespace ProfileService.API.Models;

public record AuthJwt(string Key, string Issuer, string Audience, int ExpireMinutes);
