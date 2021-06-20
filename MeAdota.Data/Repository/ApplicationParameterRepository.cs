using MeAdota.Data.Interfaces;
using MeAdota.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeAdota.Data.Repository
{
    public class ApplicationParameterRepository : BaseRepository<ApplicationParameter>, IApplicationParameterRepository
    {
        public ApplicationParameterRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public Dictionary<string, string> LoadParameters()
        {
            try
            {
                return DbSet.Select(p => new { p.Key, p.Value }).AsQueryable().ToDictionary(k => k.Key, v => v.Value);
            }
            catch (Exception ex)
            {
                Exception exNew = new Exception("Erro ao carregar os parametros da aplicação : " + ex.Message, ex.InnerException);
                throw exNew;
            }
        }
    }
}
