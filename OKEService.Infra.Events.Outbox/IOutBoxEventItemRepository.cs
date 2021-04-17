using System;
using System.Collections.Generic;

namespace OKEService.Infra.Events.Outbox
{
    public interface IOutBoxEventItemRepository
    {
        List<OutBoxEventItem> GetOutBoxEventItemsForPublishe(int maxCount = 100);
        void MarkAsRead(List<OutBoxEventItem> outBoxEventItems);
    }
}
