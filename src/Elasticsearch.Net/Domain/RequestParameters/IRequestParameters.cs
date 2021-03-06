using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net
{
	public interface IRequestParameters
	{
		/// <summary>
		/// The querystring that should be appended to the path of the request
		/// </summary>
		IDictionary<string, object> QueryString { get; set; }

		/// <summary>
		/// A method that can be set on the request to take ownership of creating the response object.
		/// When set this will be called instead of the internal .Deserialize();
		/// </summary>
		Func<IElasticsearchResponse, Stream, object> DeserializationState { get; set;  }

		/// <summary>
		/// Configuration for this specific request, i.e disable sniffing, custom timeouts etcetera.
		/// </summary>
		IRequestConfiguration RequestConfiguration { get; set; }

	}
}