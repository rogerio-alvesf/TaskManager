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

        ValidateData(claim);

        var id_user = int.Parse(claim ?? "0");

        return new OutInformationsession(id_user);
    }

    private static void ValidateData<T>(T data)
    {
        if (data == null)
            throw new NotFoundException("Unable to obtain session data");
    }
}