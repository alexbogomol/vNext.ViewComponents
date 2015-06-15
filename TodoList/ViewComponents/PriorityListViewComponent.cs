using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.ViewComponents
{
    public class PriorityListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public PriorityListViewComponent(ApplicationDbContext context)
        {
            db = context;
        }
        
        public IViewComponentResult Invoke(int maxPriority)
        {
            var items = db.TodoItems.Where(x => x.IsDone == false
                                             && x.Priority <= maxPriority);

            return View(items);
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            string viewName = "Default";

            // If asking for all completed tasks, render with the "PVC" view.
            if (maxPriority > 3 && isDone == true)
            {
                viewName = "PVC";
            }

            var items = await GetItemsAsync(maxPriority, isDone);

            return View(viewName, items);
        }

        private Task<IQueryable<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
        {
            return Task.FromResult(GetItems(maxPriority, isDone));
        }

        private IQueryable<TodoItem> GetItems(int maxPriority, bool isDone)
        {
            var items = db.TodoItems.Where(x => x.IsDone == isDone
                                             && x.Priority <= maxPriority);

            string msg = $"Priority <= { maxPriority } && isDone == { isDone }";
            ViewBag.PriorityMessage = msg;

            return items;
        }
    }
}
