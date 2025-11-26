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

        private List<clsFigures> figures;
        public Dessin(string ANom, float AVersion)
        {

            Nom = ANom;
            DateTime dateCreation = DateTime.Now; 
            Version = AVersion;
            figures = new List<clsFigures>();

        }

        private string _Nom;

        public string Nom // Accesseurs R/W

        {

            get { return _Nom; } // Retour directement la valeur

            set { _Nom = value; } // Test avec plafonnement si besoin;

        }

        private DateTime _Date;

        private float _Version;

        public float Version // Accesseurs R/W

        {

            get { return _Version; } // Retour directement la valeur

            set { _Version = value; } // Test avec plafonnement si besoin;

        }
  
        public DateTime Date_Creation { get;}
     
        public void Ajouter_Figure(clsFigures figure)
        {
           figures.Add(figure);
        }

        public void Supprimer_Figure(clsFigures figure)
        {
            figures.Remove(figure);
        }
        public void DessinerFigures()
        {
            foreach (var figure in figures)
            {
                figure.Dessine();
            }
        }
    }
};
