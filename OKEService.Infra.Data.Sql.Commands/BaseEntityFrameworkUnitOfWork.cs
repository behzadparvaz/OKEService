using OKEService.Core.Domain.Data;
using OKEService.Utilities;
using System.Threading.Tasks;

namespace OKEService.Infra.Data.Sql.Commands
{
    public abstract class BaseEntityFrameworkUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : BaseCommandDbContext
    {
        protected readonly TDbContext _dbContext;
        protected readonly OKEServiceServices _hamoonApplicationContext;

        public BaseEntityFrameworkUnitOfWork(TDbContext dbContext, OKEServiceServices hamoonApplicationContext)
        {
            _dbContext = dbContext;
            _hamoonApplicationContext = hamoonApplicationContext;
        }

        public void BeginTransaction()
        {
            _dbContext.BeginTransaction();
        }

        public int Commit()
        {
            var result = _dbContext.SaveChanges();
            return result;
        }

        public async Task<int> CommitAsync()
        {
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public void CommitTransaction()
        {
            _dbContext.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.RollbackTransaction();
        }
    }
}
