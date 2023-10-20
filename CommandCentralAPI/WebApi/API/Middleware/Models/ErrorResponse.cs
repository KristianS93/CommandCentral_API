using Microsoft.AspNetCore.Mvc;

namespace API.Middleware.Models;

/// <summary>
/// This error response model uses the IETF revised RFC 7807, which creates a
/// generalized error-handling schema
/// - Type - A URI identifier that categorizes the error
/// - Title - A brief, human-readable message about the error
/// - Status - The Http response code (optional)
/// - Detail - A human-readable explanation of the error
/// - Instance - A URI that identifies the specific occurence of the error
/// Example:
///     "type": "/errors/incorrect-user-pass",
///     "title": "Incorrect username or password.",
///     "status": 401,
///     "detail": "Authentication failed due to incorrect username or password.",
///     "instance": "/login/log/abc123"
/// the type field categorizes the type of error, while instance identifies a specific
/// occurence of the error in similar fashion to classes and objects.
/// https://datatracker.ietf.org/doc/html/rfc7807
/// </summary>
public class ErrorResponse : ProblemDetails
{
    public IDictionary<string,string[]>? Errors { get; set; }
}