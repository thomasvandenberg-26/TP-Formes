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

      
            LogEvents.Instance.Push(ANom, LogEvents.TypeEvenement.CREATION_DESSIN);
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
           
                try
                {
                    figures.Add(figure);
                }
                catch (ArgumentNullException ane)
                {
                    LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.CREATION_FIGURE, ane.Message);
                }
                catch (FormatException fe)
                {
                    LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.CREATION_FIGURE, fe.Message);
                }

            catch (Exception ex)
                {
                    LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.CREATION_FIGURE, ex.Message);
                }
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.CREATION_FIGURE);

            
        
        }

                

        public void Supprimer_Figure(clsFigures figure)
        {
            figures.Remove(figure);
            LogEvents.Instance.Push(figure.Nom, LogEvents.TypeEvenement.SUPPRESSION_FIGURE);
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
