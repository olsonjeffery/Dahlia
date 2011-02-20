using System;
using Dahlia.Models;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Dahlia.Mappings
{
    public class RetreatOverride : IAutoMappingOverride<Retreat>
    {
        public void Override(AutoMapping<Retreat> mapping)
        {
            mapping.IgnoreProperty(x => x.IsFull);
        }
    }
}