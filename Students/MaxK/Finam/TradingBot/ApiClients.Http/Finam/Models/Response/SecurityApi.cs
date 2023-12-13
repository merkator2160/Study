using ApiClientsHttp.Finam.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApiClientsHttp.Finam.Models.Response
{
    public class SecurityApi
    {
        public String Code { get; set; }
        public String Board { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Market Market { get; set; }
        public Int32 Decimals { get; set; }
        public Int32 LotSize { get; set; }
        public Int32 MinStep { get; set; }
        public String Currency { get; set; }
        public String InstrumentCode { get; set; }
        public String ShortName { get; set; }
        public Int32 Properties { get; set; }
        public String TimeZoneName { get; set; }

        [JsonProperty("bpCost")]
        public Double Cost { get; set; }
        public Single AccruedInterest { get; set; }
        public String PriceSign { get; set; }
        public String Ticker { get; set; }
        public String LotDivider { get; set; }
    }
}