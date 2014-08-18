using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.DSL.Filter
{
    [JsonConverter(typeof(NativeFilterConverter))]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface INativeAndFilter : IAndFilter
    {
        IEnumerable<string> NativeFilters { get; set; }
    }

    public class NativeAndFilter : PlainFilter, INativeAndFilter
    {
        protected internal override void WrapInContainer(IFilterContainer container)
        {
            container.And = this;
        }

        public IEnumerable<IFilterContainer> Filters { get; set; }
        public IEnumerable<string> NativeFilters { get; set; }
    }

    public class NativeAndFilterDescriptor : FilterBase, INativeAndFilter
    {
        IEnumerable<IFilterContainer> Nest.IAndFilter.Filters { get; set; }
        IEnumerable<string> Nest.DSL.Filter.INativeAndFilter.NativeFilters { get; set; }


        bool IFilter.IsConditionless
        {
            get
            {
                var nf = ((INativeAndFilter)this);
                return (!nf.Filters.HasAny() || nf.Filters.All(f => f.IsConditionless)) && (!nf.NativeFilters.HasAny());
            }
        }
    }

    [JsonConverter(typeof(NativeFilterConverter))]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public interface INativeOrFilter : IOrFilter
    {
        IEnumerable<string> NativeFilters { get; set; }
    }

    public class NativeOrFilter : PlainFilter, INativeOrFilter
    {
        protected internal override void WrapInContainer(IFilterContainer container)
        {
            container.Or = this;
        }

        public IEnumerable<IFilterContainer> Filters { get; set; }
        public IEnumerable<string> NativeFilters { get; set; }
    }

    public class NativeOrFilterDescriptor : FilterBase, INativeOrFilter
    {
        IEnumerable<IFilterContainer> Nest.IOrFilter.Filters { get; set; }
        IEnumerable<string> Nest.DSL.Filter.INativeOrFilter.NativeFilters { get; set; }


        bool IFilter.IsConditionless
        {
            get
            {
                var nf = ((INativeOrFilter)this);
                return (!nf.Filters.HasAny() || nf.Filters.All(f => f.IsConditionless)) && (!nf.NativeFilters.HasAny());
            }
        }
    }

}
