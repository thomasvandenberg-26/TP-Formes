using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    public interface ISupportDessin
    {

        int Couleur_Selectionne(Color color);
        int Ligne_Trace( int AX_Debut, int AY_Debut, int AX_Fin, int AY_Fin);

        int Cercle_Trace(int AX_Centre, int AY_Centre, int ARayon);
    }
}
