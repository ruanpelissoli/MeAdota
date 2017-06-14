using System.Threading.Tasks;

namespace Adopcat.Mobile.Interfaces
{
    public interface IGeoLocatorService
    {
        Task GetPositionInfos();
    }
}
