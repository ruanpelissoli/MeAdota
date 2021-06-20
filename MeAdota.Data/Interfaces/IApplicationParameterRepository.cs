using MeAdota.Model;
using System.Collections.Generic;

namespace MeAdota.Data.Interfaces
{
    public interface IApplicationParameterRepository : IBaseRepository<ApplicationParameter>
    {
        Dictionary<string, string> LoadParameters();
    }
}
