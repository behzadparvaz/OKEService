using OKEService.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Core.Domain.Entities
{
    public abstract class Entity : IAuditable
    {
        public long Id { get; protected set; }
        public BusinessId BusinessId { get; protected set; } = BusinessId.FromGuid(Guid.NewGuid());
        protected Entity() { }
    }
}
