using Adopcat.Services.Interfaces;
using System.Threading.Tasks;
using Adopcat.Model;
using Adopcat.Data.Interfaces;

namespace Adopcat.Services
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
