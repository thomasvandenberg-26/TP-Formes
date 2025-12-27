using System.Text;
using System.Windows;
using System.Drawing; 
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using nsFigures;
namespace TP_Formes_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (_, __) => DessinerDepuisJson();
            OpenLogWindow();
        }

        private void Dessiner()
        {
            MainCanvas.Children.Clear();

            clsFigures.SupportDessin = new SupportWPF(MainCanvas);

            var dessin = new Dessin("WPF", 1.0f);

            dessin.Ajouter_Figure(new clsRectangle(
               new System.Drawing.Point(50, 50),
                System.Drawing.Color.Orange,
                100,
                80,"R1"
            ));

            dessin.Ajouter_Figure(new clsCube(
                new System.Drawing.Point(200, 60),
               System.Drawing.Color.Orange,
                "C1",
                60
            ));

            dessin.DessinerFigures();
        }
       private void DessinerDepuisJson()
        {
            try
            {
                // 1) Chemin du fichier JSON
                string filePath = "dessindethomas.json";

                // (Optionnel) tu peux mettre un chemin absolu si besoin
                // string filePath = @"C:\...\dessin.json";

                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"Fichier introuvable : {filePath}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 2) Nettoyer le canvas
                MainCanvas.Children.Clear();

                // 3) Brancher le support WPF
                clsFigures.SupportDessin = new SupportWPF(MainCanvas);

                // 4) Charger le dessin depuis le JSON
                // -> LoadFromJson doit être STATIC et retourner un Dessin
                Dessin dessin = Dessin.LoadFromJson(filePath);

                
                // 5) Dessiner toutes les figures
                dessin.DessinerFigures();

                // 6) Log (optionnel)
                LogEvents.Instance.PushEvent(new Event(EventType.Information,
                    $"Dessin affiché depuis {filePath}"));
            }
            catch (Exception ex)
            {
                LogEvents.Instance.PushEvent(new Event(EventType.Alarme,
                    $"Erreur DessinerDepuisJson : {ex.Message}"));

                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OpenLogWindow()
        {
            var win = new LogEventsWindow();
            win.Show();
        }
    }
}