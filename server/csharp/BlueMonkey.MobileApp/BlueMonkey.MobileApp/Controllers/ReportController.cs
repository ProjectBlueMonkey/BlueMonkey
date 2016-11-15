using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using BlueMonkey.MobileApp.DataObjects;
using BlueMonkey.MobileApp.Models;
using BlueMonkey.MobileApp.Commons;

namespace BlueMonkey.MobileApp.Controllers
{
    [Authorize]
    public class ReportController : TableController<Report>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Report>(context, Request);
        }

        // GET tables/Report
        public IQueryable<Report> GetAllReport()
        {
            var sid = this.GetSid();
            return Query()
                .Where(x => x.UserId == sid); 
        }

        // GET tables/Report/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Report> GetReport(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Report/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Report> PatchReport(string id, Delta<Report> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Report
        public async Task<IHttpActionResult> PostReport(Report item)
        {
            item.UserId = this.GetSid();
            Report current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Report/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteReport(string id)
        {
             return DeleteAsync(id);
        }
    }
}
