using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.Services
{
    public class StatisticsService
    {
        private readonly ApplicationDbContext db;

        public StatisticsService(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<int> GetCount()
        {
            return await Task.FromResult(db.TodoItems.Count());
        }

        public async Task<int> GetCompletedCount()
        {
            return await Task.FromResult(db.TodoItems.Count(x => x.IsDone == true));
        }

        public async Task<double> GetAveragePriority()
        {
            if (db.TodoItems.Count() == 0)
            {
                return 0.0;
            }

            return await Task.FromResult(db.TodoItems.Average(x => x.Priority));
        }
    }
}
