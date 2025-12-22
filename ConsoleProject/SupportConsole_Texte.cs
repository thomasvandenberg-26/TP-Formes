using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    // Remplacer "implements" par ":", la syntaxe C# correcte pour l'implémentation d'une interface.
    // S'assurer que l'interface ISupportDessin existe et est accessible dans ce contexte.


    // Je ferais cette classe plus tard quand j'aurai le temps et l'energie.
    internal class SupportConsole_Texte : ISupportDessin
    {
        // Ajouter ici les membres requis par l'interface ISupportDessin si nécessaire.
        public int Cercle_Trace(int AX_Centre, int AY_Centre, int ARayon)
        {
            throw new NotImplementedException();
        }

        public int Couleur_Selectionne(byte ARouge, byte AVert, byte ABleu)
        {
            Console.WriteLine($"Couleur sélectionnée - Rouge: {ARouge}, Vert: {AVert}, Bleu: {ABleu}");
            return 1;

        }

        public int Ligne_Trace(int AX_Debut, int AY_Debut, int AX_Fin, int AY_Fin)
        {
            Console.WriteLine($"Tracé d'une ligne de ({AX_Debut}, {AY_Debut}) à ({AX_Fin}, {AY_Fin})");
            return 1;
        }
    }
}
