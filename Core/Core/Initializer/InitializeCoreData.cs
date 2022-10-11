using System;
using System.Collections.Generic;
using System.Linq;
using BaseEntities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Core.Initializer
{
    public static class InitializeCoreData
    {

        public static void ImmutableCreateTimeColumn(this ModelBuilder modelBuilder)
        {
            IEnumerable<Type> exporters = typeof(Entity)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Entity)) && !t.IsAbstract && !t.Name.ToLower().Contains("langentity"))
                .Select(t => t);

            foreach (var exporter in exporters)
                modelBuilder.Entity(exporter)
                    .Property("CreateTime")
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }

}

