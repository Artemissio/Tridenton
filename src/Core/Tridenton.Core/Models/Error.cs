﻿using System.Net;

namespace Tridenton.Core;

public record Error(HttpStatusCode Code, string Key, string Description);

public record ValidationError(string[] Invalidations) : BadRequestError("Common.ValidationError", "One or more validation errors occured");
public record BadRequestError(string Key, string Description) : Error(HttpStatusCode.BadRequest, Key, Description);
public record NotFoundError(string Key, string Description) : Error(HttpStatusCode.NotFound, Key, Description);
public record InternalServerError(string Key, string Description) : Error(HttpStatusCode.InternalServerError, Key, Description);
public record UnauthorizedError(string Key, string Description) : Error(HttpStatusCode.Unauthorized, Key, Description);
public record AccessDeniedError(string Key, string Description) : Error(HttpStatusCode.Forbidden, Key, Description);