namespace CarDealer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Repositories;
    using Models;
    using Models.ViewModels;

    public class LogsService
    {
        private readonly IRepository<Log> logs;

        public LogsService(IRepository<Log> logs)
        {
            this.logs = logs;
        }


        public IEnumerable<LogsViewModel> GetLogsVm(string filter)
        {
            var logsDb = this.logs.All();

            if (filter != null)
            {
                logsDb = this.logs.All().Where(l => l.User.Username.Contains(filter));
            }

            return logsDb.Select(l => new LogsViewModel()
            {
                User = l.User.Username,
                ModifiedTable = l.ModifiedTable,
                Operation = l.Operation,
                Id = l.Id,
                Time = l.Time
            })
                                     .OrderBy(l => l.Time);
        }

        public void Log(Log log)
        {
            this.logs.Add(log);
            this.logs.SaveChanges();
        }

        public void DeleteLogById(int id)
        {
            this.logs.Delete(id);
            this.logs.SaveChanges();
        }

        public void ClearAll()
        {
            foreach (var log in this.logs.All())
            {
                this.logs.Delete(log);
            }

            this.logs.SaveChanges();
        }
    }
}
