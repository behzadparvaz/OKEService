﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OKEService.Infra.Events.Outbox;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKEService.Infra.Data.Sql.Commands.OutBoxEventItems
{
    public class OutBoxEventItemConfig : IEntityTypeConfiguration<OutBoxEventItem>
    {
        public void Configure(EntityTypeBuilder<OutBoxEventItem> builder)
        {
            builder.Property(c => c.AccuredByUserId).HasMaxLength(255);
            builder.Property(c => c.EventName).HasMaxLength(255);
            builder.Property(c => c.AggregateName).HasMaxLength(255);
            builder.Property(c => c.EventTypeName).HasMaxLength(500);
            builder.Property(c => c.AggregateTypeName).HasMaxLength(500);
        }
    }
}
