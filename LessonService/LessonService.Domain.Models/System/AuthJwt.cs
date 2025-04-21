namespace LessonService.Domain.Models.System;

public record AuthJwt(string Key, string Issuer, string Audience, int ExpireMinutes);
