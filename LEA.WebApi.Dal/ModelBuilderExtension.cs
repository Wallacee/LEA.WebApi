using LEA.WebApi.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace LEA.WebApi.Dal
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder GlobalConfiguration(this ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    switch (property.Name)
                    {
                        case nameof(Entity.Creation):
                            property.IsNullable = false;
                            property.SetDefaultValue(DateTime.Now);
                            break;
                        default:
                            break;
                    }
                }
            }
            return builder;
        }
    }
}
