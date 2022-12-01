using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockKangourou
{
    public interface IDataService
    {
        public ObservableCollection<Kangourou> GetKangourous();
        public void StockKangourou(Kangourou kangourou);
        public void ReleaseKangourou(Kangourou kangourou);
        public void AdoptKangourou( Kangourou kangourou);
        public void DisplayMessage(string message);
    }

    public class DataServiceEF : IDataService
    {
        private readonly MyContext _context;
        private readonly ILogger<DataServiceEF> _log;

        public DataServiceEF(ILogger<DataServiceEF> log, MyContext context)
        {
            _log = log;
            _context = context;
            _context.Database.EnsureCreated();
            _context.Kangourous.Load();
        }

        public void StockKangourou(Kangourou kangourou)
        {
            _context.Kangourous.Add(kangourou);
            _context.SaveChanges();
            _log.LogInformation($"Un kangourou a été stocké");
            return;
        }

        // Paolo
        public void AdoptKangourou(Kangourou kangourou)
        {
            _context.Kangourous.Remove(kangourou);
            _context.SaveChanges();
            _log.LogInformation($"Un kangourou a été adopté, trop la chance");
            _log.LogInformation($"Fuck Clara");
            return;
        }

        public void ReleaseKangourou(Kangourou kangourou)
        {
            _log.LogInformation($"Tentative de relâchement de kangourou en cours... (Espèce de terroriste !!!)");
            return;
        }

        public ObservableCollection<Kangourou> GetKangourous()
        {
            var list = _context.Kangourous.Local.ToObservableCollection();
            _log.LogInformation($"Les australopithèques");
            return list;
        }
        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
