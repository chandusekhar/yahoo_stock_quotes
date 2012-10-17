﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace core.Quotes.RequestProcessing
{
    public interface IProcessAQuoteResponse
    {
        IEnumerable<dynamic> Return(QuoteResponse quote_response);
    }

    public interface IParseAYahooQuote
    {
        dynamic Parse(string quote_data, IEnumerable<QuoteReturnParameter> return_parameters);
    }

    public class YahooQuoteParser : IParseAYahooQuote
    {
        public dynamic Parse(string quote_data, IEnumerable<QuoteReturnParameter> return_parameters)
        {
            return new YahooQuote(quote_data.Split(','), return_parameters);
        }
    }

    public class YahooQuote : DynamicObject
    {
        IDictionary<string, string> return_parameter_dictionary;

        public YahooQuote(IEnumerable<string> quote_data, IEnumerable<QuoteReturnParameter> return_parameters)
        {
            return_parameter_dictionary = return_parameters.Zip(quote_data, (x, y) => new Tuple<string, string>(x.ToString(), y)).ToDictionary(x => x.Item1, x => x.Item2);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if(return_parameter_dictionary.ContainsKey(binder.Name))
            {
                result = return_parameter_dictionary[binder.Name];
                return true;
            }

            result = null;
            return false;
        }
    }
}