using nsFigures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace nsFigures
{
    internal class SupportImprimante_Canon : ISupportDessin
    {
        private readonly string _filePath;
      
        internal void DebutFigure(string type, string nom)
        {
            File.AppendAllText(
                _filePath,
                $"{Environment.NewLine}[FIGURE] {type} \"{nom}\"{Environment.NewLine}"
            );
        }


        public int Cercle_Trace(int AX_Centre, int AY_Centre, int ARayon)
        {
            throw new NotImplementedException();
        }
        public SupportImprimante_Canon(string filePath = "Dessin_CANON.txt")
        {
            _filePath = filePath;

            // On démarre un nouveau "document"
            File.WriteAllText(
                _filePath,
                $"--- Impression Canon ({DateTime.Now}) ---{Environment.NewLine}"
            );
        }
        public int Couleur_Selectionne(Color color)
        {
            // Combine les valeurs de couleur en un entier


           

            File.AppendAllText(
               _filePath,
               $"[CANON] Couleur : {color.Name} , {Environment.NewLine}"
           );


            return 1;
        }

        public int Ligne_Trace(int AX_Debut, int AY_Debut, int AX_Fin, int AY_Fin)
        {
            File.AppendAllText(
             _filePath,
             $"[CANON] Ligne ({AX_Debut},{AY_Debut}) -> ({AX_Fin},{AY_Fin}){Environment.NewLine}"
            );


            return 1;
        }

    }
}
