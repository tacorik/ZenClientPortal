using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZenClientPortal.Controllers
{
    public class ZenTicketController : ApiController
    {
       // [Authorize]
        public IEnumerable<TicketEntry> Get()
            {
        using(ZenClientTicketEntities1 entities=new ZenClientTicketEntities1())
            {
                return entities.TicketEntries.ToList();
            }
    }
    }
}
