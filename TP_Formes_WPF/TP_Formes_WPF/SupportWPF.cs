using ConsoleProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TP_Formes_WPF
{
    internal class SupportWPF : ISupportDessin
    {
        private readonly Canvas _canvas;
        private SolidColorBrush _brush = Brushes.Black;
        public SupportWPF(Canvas canvas)
        {
            _canvas = canvas;
        }
        int ISupportDessin.Cercle_Trace(int AX_Centre, int AY_Centre, int ARayon)
        {
            throw new NotImplementedException();
        }

        int ISupportDessin.Couleur_Selectionne(System.Drawing.Color color)
        {
            _brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A,color.R,color.G,color.B));
            return 1; 
        }

        int ISupportDessin.Ligne_Trace(int AX_Debut, int AY_Debut, int AX_Fin, int AY_Fin)
        {
            var line = new Line
            {
                X1 = AX_Debut,
                Y1 = AY_Debut,
                X2 = AX_Fin,
                Y2 = AY_Fin,
                Stroke = _brush,
                StrokeThickness = 2
            };
            _canvas.Children.Add(line);
            return 1;
        }
    }
}
