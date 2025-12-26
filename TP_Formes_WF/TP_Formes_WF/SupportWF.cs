using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using nsFigures;
using ConsoleProject;

namespace TP_Formes_WF
{
     class SupportWF : ISupportDessin
    {

        private Graphics _g;
        private Pen _pen = Pens.Black;

        public SupportWF(Graphics g)
        {
            _g = g;
        }
        int ISupportDessin.Cercle_Trace(int AX_Centre, int AY_Centre, int ARayon)
        {
            throw new NotImplementedException();
        }

        int ISupportDessin.Couleur_Selectionne(Color color)
        {
           if (!color.IsEmpty)
            {
                _pen = new Pen(color);
                return 1;

            }
            return 0;
        }

        int ISupportDessin.Ligne_Trace(int AX_Debut, int AY_Debut, int AX_Fin, int AY_Fin)
        {
            _g.DrawLine(_pen, AX_Debut, AY_Debut, AX_Fin, AY_Fin);
            return 1;
        }
    }
}
