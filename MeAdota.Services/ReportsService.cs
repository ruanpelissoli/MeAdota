using MeAdota.Services.Interfaces;
using System.Threading.Tasks;
using MeAdota.Model;
using MeAdota.Data.Interfaces;

namespace MeAdota.Services
{
    public class ReportsService : BaseService, IReportsService
    {
        private IReportsRepository _reportsRepository;

        public ReportsService(ILoggingService log, IReportsRepository reportsRepository) : base(log)
        {
            _reportsRepository = reportsRepository;
        }

        public async Task<Reports> CreateAsync(Reports model)
        {
            return await TryCatch(async () =>
            {
                var reports = await _reportsRepository.CreateAsync(model);
                return reports;
            });
        }
    }
}
