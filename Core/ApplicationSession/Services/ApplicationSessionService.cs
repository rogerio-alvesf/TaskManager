using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services;

public class ApplicationSessionService : IApplicationSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationSessionService(IHttpContextAccessor httpContextAccessor)
    => _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    public OutInformationsession Obtain()
    {
        if (_httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == false)
            throw new UnauthorizedException("User without permission");

        var claimsPrincipal = _httpContextAccessor.HttpContext?.User;

        var claim = claimsPrincipal?.FindFirst("id")?.Value;
        var claimEmail = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

        ValidateData(claim);
        ValidateData(claimEmail);

        var id_user = int.Parse(claim ?? "0");
        var email_user = claimEmail ?? "0";

        return new OutInformationsession(id_user, email_user);
    }

    private static void ValidateData<T>(T data)
    {
        if (data == null)
            throw new NotFoundException("Unable to obtain session data");
    }
}