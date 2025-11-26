using nsFigures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    internal class Dessin
    {
        public Dessin(string Nom, List<clsFigures> figures, DateTime date, float version)
        {


        }
        public string Nom_Dessin { get; set; }
        public List<clsFigures> Figures_Dessin { get; set; }


        public DateTime Date_Creation { get; set; }
        public float Version_Dessin
        {
            get; set;


        }

        public void Ajouter_Figure(clsFigures figure)
        {
            Figures_Dessin.Add(figure);
        }

        public void Supprimer_Figure(clsFigures figure)
        {
            Figures_Dessin.Remove(figure);
        }
        public void DessinerFigures()
        {
            foreach (var figure in Figures_Dessin)
            {
                figure.Dessine();
            }
        }
    }
};
