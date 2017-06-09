using Adopcat.Model;
using System.Collections.Generic;

namespace Adopcat.Data.Interfaces
{
    public interface IApplicationParameterRepository : IBaseRepository<ApplicationParameter>
    {
        Dictionary<string, string> LoadParameters();
    }
}
