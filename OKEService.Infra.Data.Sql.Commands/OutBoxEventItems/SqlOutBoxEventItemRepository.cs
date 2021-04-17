using Dapper;
using OKEService.Infra.Events.Outbox;
using OKEService.Utilities.Configurations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OKEService.Infra.Data.Sql.Commands.OutBoxEventItems
{
    public class SqlOutBoxEventItemRepository : IOutBoxEventItemRepository
    {
        private readonly OKEServiceConfigurations _configurations;

        public SqlOutBoxEventItemRepository(OKEServiceConfigurations configurations)
        {
            _configurations = configurations;
        }
        public List<OutBoxEventItem> GetOutBoxEventItemsForPublishe(int maxCount = 100)
        {
            using var connection = new SqlConnection(_configurations.PoolingPublisher.SqlOutBoxEvent.ConnectionString);
            string query = string.Format(_configurations.PoolingPublisher.SqlOutBoxEvent.SelectCommand, maxCount);
            var result = connection.Query<OutBoxEventItem>(query).ToList();
            return result;
        }
        public void MarkAsRead(List<OutBoxEventItem> outBoxEventItems)
        {
            string idForMark = string.Join(',', outBoxEventItems.Where(c => c.IsProcessed).Select(c => c.OutBoxEventItemId).ToList());
            if (!string.IsNullOrWhiteSpace(idForMark))
            {
                using var connection = new SqlConnection(_configurations.PoolingPublisher.SqlOutBoxEvent.ConnectionString);
                string query = string.Format(_configurations.PoolingPublisher.SqlOutBoxEvent.UpdateCommand, idForMark);
                connection.Execute(query);
            }
        }
    }
}
