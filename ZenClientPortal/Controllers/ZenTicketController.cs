using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ZenClientPortal.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ZenTicket")]
    public class ZenTicketController : ApiController
    {
        [Authorize]
        [Route("")]
        public HttpResponseMessage Get()
        {
            using (ZenClientTicketEntities1 entities = new ZenClientTicketEntities1())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entities.TicketEntries.ToList());
            }
        }

        [Route("{status:int}")]
        public HttpResponseMessage Get(int status)
        {
            using (ZenClientTicketEntities1 entities = new ZenClientTicketEntities1())
            {
                switch (status)
                {
                    case 1:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.TicketEntries.Where(e => e.TicStatus == "1").ToList());
                    case 2:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.TicketEntries.Where(e => e.TicStatus == "2").ToList());
                    case 3:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.TicketEntries.Where(e => e.TicStatus == "3").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ticket values must be in the range(1,3)");
                }
            }
        }


        [Route("{username}")]
        public HttpResponseMessage Get(string username)
        {
            username = username.Replace('~', '.');
            username = username.Replace("%40", "@");
            using (ZenClientTicketEntities1 entities = new ZenClientTicketEntities1())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entities.TicketEntries.Where(e => e.Username.ToLower() == username).ToList());
            }
        }

        [Route("{status:int}/{username}")]
        public HttpResponseMessage Get(int status, string username)
        {
            username = username.Replace('~', '.');
            username = username.Replace("%40", "@");
            using (ZenClientTicketEntities1 entities = new ZenClientTicketEntities1())
            {
                switch (status)
                {
                    case 1:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.TicketEntries.Where(e => e.TicStatus == "1" && e.Username.ToLower() == username.ToLower()).ToList());
                    case 2:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.TicketEntries.Where(e => e.TicStatus == "2" && e.Username.ToLower() == username.ToLower()).ToList());
                    case 3:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.TicketEntries.Where(e => e.TicStatus == "3" && e.Username.ToLower() == username.ToLower()).ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ticket values must be in the range(1,3)");
                }
            }
        }

        [Route("")]
        public HttpResponseMessage Post([FromBody]TicketEntry NewTicket)
        {
            try
            {
                using (ZenClientTicketEntities1 entities = new ZenClientTicketEntities1())
                {

                    entities.TicketEntries.Add(NewTicket);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, NewTicket);
                    message.Headers.Location = new Uri(Request.RequestUri + NewTicket.TicketID.ToString());
                    return message;
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            //using (EmployeeDBEntities entities = new EmployeeDBEntities())
            //{
            //   // entities.Employees.Remove(entities.Employees.FirstOrDefault(x => x.ID == id));

            try
            {
                using (ZenClientTicketEntities1 entities = new ZenClientTicketEntities1())
                {

                    var check = entities.TicketEntries.Remove(entities.TicketEntries.FirstOrDefault(x => x.TicketID == id));
                    if (check == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id " + id.ToString() + "Not Found");
                    }
                    else
                    {
                        entities.TicketEntries.Remove(check);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, [FromBody] TicketEntry ChangedTicket)
        {
            try
            {
                using (ZenClientTicketEntities1 entities = new ZenClientTicketEntities1())
                {

                    var check = entities.TicketEntries.FirstOrDefault(x => x.TicketID == id);
                    if (check == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id " + id.ToString() + "Not Found");
                    }
                    else
                    {
                        check.Header = ChangedTicket.Header;
                        check.TicDescription = ChangedTicket.TicDescription;
                        check.TicStatus = ChangedTicket.TicStatus;
                        check.Username= ChangedTicket.Username;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, check);
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }


    }
}


