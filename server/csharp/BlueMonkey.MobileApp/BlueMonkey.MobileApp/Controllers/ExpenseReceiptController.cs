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
    public class ExpenseReceiptController : TableController<ExpenseReceipt>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<ExpenseReceipt>(context, Request);
        }

        // GET tables/ExpenseReceipt
        public IQueryable<ExpenseReceipt> GetAllExpenseReceipt()
        {
            var sid = this.GetSid();
            return Query()
                .Where(x => x.UserId == sid); 
        }

        // GET tables/ExpenseReceipt/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ExpenseReceipt> GetExpenseReceipt(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ExpenseReceipt/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ExpenseReceipt> PatchExpenseReceipt(string id, Delta<ExpenseReceipt> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/ExpenseReceipt
        public async Task<IHttpActionResult> PostExpenseReceipt(ExpenseReceipt item)
        {
            item.UserId = this.GetSid();
            ExpenseReceipt current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ExpenseReceipt/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteExpenseReceipt(string id)
        {
             return DeleteAsync(id);
        }
    }
}
