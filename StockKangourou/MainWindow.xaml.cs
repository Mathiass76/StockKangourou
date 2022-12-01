using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Windows.Markup;
using System.Media;
using System.Diagnostics;

namespace StockKangourou
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IDataService _dataService;
        public Kangourou TheKangourou { get; set; }
        public Kangourou SelectedKangourou { get; set; }
        public List<string> RaceKangourou { get; set; }
        public ObservableCollection<Kangourou> Kangourous { get; set; }

        public MainWindow(IDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            TheKangourou = new Kangourou();
            RaceKangourou = new List<string> { "Wallaby", "Grand Kangourou", "Pétrogale", "Kangourou Antilope", "Clara", "Rat Kangourou", "Stilobic" };
            Kangourous = _dataService.GetKangourous();
            DataContext = this;
        }

        public void Stocker_Click(object sender, RoutedEventArgs e)
        {
            var kangourou = TheKangourou.CopyOf();
            (bool Success, string Error) = kangourou.Validate(Kangourous);
            if (!Success)
            {
                _dataService.DisplayMessage(Error);
                return;
            }
            _dataService.StockKangourou(kangourou);
        }

        public void Adopter_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Adoptez ce kangourou ?", "Adoption", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                SoundPlayer yaa = new SoundPlayer(@"Son\ia.wav");
                yaa.Load();
                yaa.Play();
                _dataService.AdoptKangourou(SelectedKangourou);
            }
        }


        public void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedKangourou != null)
            {
                TheKangourou.codeKangourou = SelectedKangourou.codeKangourou;
                TheKangourou.nomKangourou = SelectedKangourou.nomKangourou;
                TheKangourou.raceKangourou = SelectedKangourou.raceKangourou;
                TheKangourou.tailleKangourou = SelectedKangourou.tailleKangourou;
                TheKangourou.poidsKangourou = SelectedKangourou.poidsKangourou;
            }
        }

        public void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            File.WriteAllText(
                @"\Kangourou.txt",
                JsonConvert.SerializeObject(Kangourous));
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Kangourous.Clear();
            var kangourous = JsonConvert.DeserializeObject<ObservableCollection<Kangourou>>(File.ReadAllText(@"\Kangourou.txt"));
            foreach (var item in kangourous)
            {
                Kangourous.Add(item);
            }
        }

        private void KangouMaxiBruit(object sender, MouseButtonEventArgs e)
        {
            SoundPlayer miaou = new SoundPlayer(@"Son\miaou.wav");
            miaou.Load();
            miaou.Play();
        }

        private void Relâcher_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer commentca = new SoundPlayer(@"Son\commentca.wav");
            commentca.Load();
            commentca.Play();
            var result = MessageBox.Show("Êtes-vous sûr ?", "Supprimer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                SoundPlayer ctsur = new SoundPlayer(@"Son\ctsur.wav");
                ctsur.Load();
                ctsur.Play();
                var result2 = MessageBox.Show("Attend répète ?", "Répète", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result2 == MessageBoxResult.Yes)
                {
                    SoundPlayer stepbro = new SoundPlayer(@"Son\stepbro.wav");
                    stepbro.Load();
                    stepbro.Play();
                    var result3 = MessageBox.Show("T'es vrmt sur ?", "Répète", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result3 == MessageBoxResult.Yes)
                         {
                        SoundPlayer encore = new SoundPlayer(@"Son\encore.wav");
                        encore.Load();
                        encore.Play();
                    }
                    else
                    {
                        SoundPlayer letsgo = new SoundPlayer(@"Son\letsgo.wav");
                        letsgo.Load();
                        letsgo.Play();
                    }
                }
                else
                {
                    SoundPlayer okletsgo = new SoundPlayer(@"Son\okletsgo.wav");
                    okletsgo.Load();
                    okletsgo.Play();
                }
            }
            else
            {
                SoundPlayer ouiouioui = new SoundPlayer(@"Son\ouiouioui.wav");
                ouiouioui.Load();
                ouiouioui.Play();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer lalalala = new SoundPlayer(@"Son\lalalala.wav");
            lalalala.Load();
            lalalala.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "https://berserkscan.fr/manga/scan-berserk-1-vf/", UseShellExecute = true });
        }
    }
}
