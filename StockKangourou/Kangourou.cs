using System;
using PropertyChanged;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Collections.ObjectModel;

namespace StockKangourou
{
    [AddINotifyPropertyChangedInterface]
    public class Kangourou
    {
        public int? Id { get; set; }
        public string codeKangourou { get; set; }
        public string nomKangourou { get; set; }
        public string raceKangourou { get; set; }
        public double? tailleKangourou { get; set; }
        public double? poidsKangourou { get; set; }

        public Kangourou CopyOf()
        {
            string data = JsonConvert.SerializeObject(this);
            var item = JsonConvert.DeserializeObject<Kangourou>(data);
            item.Id = null;
            return item;
        }

        public (bool Success, string Error) Validate(ObservableCollection<Kangourou> voyages)
        {
            Regex regex = new Regex(@"^[0-9]{3}[A-Z]{1}[0-9]{1}$");

            if (codeKangourou == null || !regex.IsMatch(codeKangourou))
            {
                return (false, "Le format de numéro est incorrect");
            }
            if (voyages.Any(x => x.Id == Id))
            {
                return (false, "Il y a déjà un numéro identique");
            }

            return (true, "");
        }
    }
}
