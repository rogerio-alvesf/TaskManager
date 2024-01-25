using TaskManager.Core.Models;

namespace TaskManager.Core.Interfaces.Service;

public interface IApplicationSessionService
{
    OutInformationsession Obtain();
}