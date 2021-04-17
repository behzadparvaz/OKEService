using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEService.Infra.Data.ChangeInterceptors.EntityChageInterceptorItems
{
    public class FakeEntityChageInterceptorItemRepository : IEntityChageInterceptorItemRepository
    {
        public FakeEntityChageInterceptorItemRepository()
        {

        }
        public void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems)
        {
        }

        public Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems)
        {
            return Task.CompletedTask;
        }
    }
}
