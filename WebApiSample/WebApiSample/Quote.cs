using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eze.WebApiEx;

namespace WebApiSample
{
    /// <summary>
    /// Quotes represent a request to retrieve the price of a security.
    /// </summary>
    public class Quote : Resource
    {
        /// <summary>
        /// The symbol of the security for which to get the quote.
        /// </summary>
        public string Symbol { get; set; }
    }

    public class QuoteController : ResourceController<Quote>
    {
        public override System.Web.Http.IHttpActionResult Post( Quote resource )
        {
            return base.Post( resource );
        }
    }
}
