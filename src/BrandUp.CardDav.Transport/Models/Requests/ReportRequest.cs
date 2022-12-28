﻿using BrandUp.CardDav.Transport.Models.Abstract;
using BrandUp.CardDav.Transport.Models.Headers;
using BrandUp.CardDav.Transport.Models.Properties;
using BrandUp.CardDav.Transport.Models.Properties.Filters;
using BrandUp.CardDav.Transport.Models.Requests.Body.Report;
using System.Xml.Serialization;

namespace BrandUp.CardDav.Transport.Models.Requests
{
    public class ReportRequest : ICardDavRequest, IHttpRequestConvertable
    {
        public ReportRequest()
        {
            Headers.Add("Depth", Depth.One.Value);
        }

        #region Static members

        public static ReportRequest CreateQuery(PropList propRequest, AddressData addressData, FilterBody filter, int limit = 0)
            => new ReportRequest()
            {
                Body = new AddresbookQueryBody()
                {
                    PropList = propRequest.Properties.Append(addressData),
                    Filter = filter,
                    Limit = limit
                }
            };

        public static ReportRequest CreateMultiget(PropList propRequest, AddressData addressData, params string[] endpoints)
            => new ReportRequest()
            {
                Body = new MultigetBody()
                {
                    PropList = propRequest.Properties.Append(addressData),
                    VCardEndpoints = endpoints
                }
            };

        #endregion

        #region ICardDavRequest members

        public IDictionary<string, string> Headers { get; init; } = new Dictionary<string, string>();

        IRequestBody ICardDavRequest.Body => Body;

        public IReportBody Body { get; init; }

        #endregion

        #region IHttpRequestConvertable members

        public HttpRequestMessage ToHttpRequest()
        {
            var serializer = new XmlSerializer(Body.GetType());
            var ms = new MemoryStream();

            serializer.Serialize(ms, Body);
            ms.Position = 0;

#if DEBUG
            var debugReader = new StreamReader(ms);
            var debug = debugReader.ReadToEnd();
            ms.Position = 0;
#endif
            HttpRequestMessage request = new()
            {
                Method = new("REPORT"),
                Content = new StreamContent(ms)
            };

            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
            foreach (var header in Headers)
            {
                request.Content.Headers.Add(header.Key, header.Value);
            }

            return request;
        }

        #endregion
    }
}
