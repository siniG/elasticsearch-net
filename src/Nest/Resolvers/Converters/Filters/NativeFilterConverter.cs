using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers.Converters.Filters
{
    public class NativeFilterConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Nest.DSL.Filter.NativeAndFilter) == objectType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var f = value as Nest.DSL.Filter.INativeAndFilter;
            if (f == null || (f.IsConditionless && !f.IsVerbatim)) return;

            var contract = serializer.ContractResolver as SettingsContractResolver;
            if (contract == null)
                return;

            string filters;

            using (System.IO.StringWriter sw = new System.IO.StringWriter(new StringBuilder()))
            {
                using (JsonWriter tempWriter = new JsonTextWriter(sw))
                {
                    tempWriter.Formatting = writer.Formatting;
                    tempWriter.WriteStartObject();
                    {
                        tempWriter.WritePropertyName("filters");
                        {
                            serializer.Serialize(tempWriter, f.Filters);
                        }
                    }
                    tempWriter.WriteEndObject();

                    filters = sw.ToString();
                }
            }

            if (!string.IsNullOrWhiteSpace(filters))
            {
                StringBuilder sb = new StringBuilder();
                if (f.NativeFilters != null)
                {
                    foreach (string json in f.NativeFilters)
                    {
                        sb.Append(",");
                        sb.Append(json);
                    }

                }

                string tempFilters = sb.ToString();

                if (!string.IsNullOrWhiteSpace(tempFilters))
                {
                    filters = filters.Insert(filters.LastIndexOf(']'), tempFilters);
                }

                writer.WriteRawValue(filters);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("NativeFilterConverter cannot be read");
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
    }
}
