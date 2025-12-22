using nsFigures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    internal class SupportImprimante_Canon : ISupportDessin
    {
        public int Cercle_Trace(int AX_Centre, int AY_Centre, int ARayon)
        {
            throw new NotImplementedException();
        }

        public int Couleur_Selectionne(byte ARouge, byte AVert, byte ABleu)
        {
            // Combine les valeurs de couleur en un entier
            return ARouge << 16 | AVert << 8 | ABleu;
        }

        public int Ligne_Trace(int AX_Debut, int AY_Debut, int AX_Fin, int AY_Fin)
        {
            // Calculer la différence entre les coordonnées
            int deltaX = AX_Fin - AX_Debut;
            int deltaY = AY_Fin - AY_Debut;

            LogEvents.Instance.PushEvent(new Event(EventType.Information,
               $"Tracé d'une ligne de ({AX_Debut}, {AY_Debut}) à ({AX_Fin}, {AY_Fin})"));


            // Logique pour tracer la ligne (simulation)
            // Ici, vous pouvez ajouter le code pour dessiner la ligne sur un support graphique
            Console.WriteLine($"Tracé d'une ligne de ({AX_Debut}, {AY_Debut}) à ({AX_Fin}, {AY_Fin})");
            return 1;  // Retourner la longueur de la ligne tracée
        }
    }
}
