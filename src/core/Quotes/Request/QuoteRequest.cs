using System.Collections.Generic;

namespace core.Quotes.Request
{
    public class QuoteRequest
    {
        public IEnumerable<string> Tickers { get; set; }
        public IEnumerable<QuoteReturnParameter> ReturnParameters { get; set; }

        public QuoteRequest() {}
        public QuoteRequest(IEnumerable<string> tickers, IEnumerable<QuoteReturnParameter> quote_return_parameters)
        {
            Tickers = tickers;
            ReturnParameters = quote_return_parameters;
        }
    }
}