using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    internal class Support_Wpf : ISupportDessin
    {
        int ISupportDessin.Cercle_Trace(int AX_Centre, int AY_Centre, int ARayon)
        {
            throw new NotImplementedException();
        }

        int ISupportDessin.Couleur_Selectionne(byte ARouge, byte AVert, byte ABleu)
        {
            throw new NotImplementedException();
        }

        int ISupportDessin.Ligne_Trace(int AX_Debut, int AY_Debut, int AX_Fin, int AY_Fin)
        {
            throw new NotImplementedException();
        }
    }
}
