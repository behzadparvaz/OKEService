using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OKEService.Infra.Data.ChangeInterceptors.EntityChageInterceptorItems
{
    public interface IEntityChageInterceptorItemRepository
    {
         void Save(List<EntityChageInterceptorItem> entityChageInterceptorItems);
         Task SaveAsync(List<EntityChageInterceptorItem> entityChageInterceptorItems);
    }
}
