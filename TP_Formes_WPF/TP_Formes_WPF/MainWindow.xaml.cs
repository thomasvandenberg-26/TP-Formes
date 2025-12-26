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
            Loaded += (_, __) => Dessiner();
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
        private void OpenLogWindow()
        {
            var win = new LogEventsWindow();
            win.Show();
        }
    }
}