using nsFigures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsFigures
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


            LogEvents.Instance.PushEvent(new Event(EventType.Information, $"Un dessin {Nom} a été crée"));
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

        public DateTime Date_Creation { get; }

        public void Ajouter_Figure(clsFigures figure)
        {

            try
            {
                figures.Add(figure);

                LogEvents.Instance.PushEvent(new Event(EventType.Information, $"Figure : {figure.Nom} ajouté au dessin"));

                return;

            }
            catch (ArgumentNullException ane)
            {

                LogEvents.Instance.PushEvent(new Event(EventType.Alarme, $"Erreur lors de l'ajout de la figure au dessin : {ane.Message}"));

            }
            catch (FormatException fe)
            {

                LogEvents.Instance.PushEvent(new Event(EventType.Alarme, $"Erreur de format lors de l'ajout de la figure au dessin : {fe.Message}"));
            }

            catch (Exception ex)
            {

                LogEvents.Instance.PushEvent(new Event(EventType.Alarme, $"Erreur inconnue lors de l'ajout de la figure au dessin : {ex.Message}"));
            }




        }



        public void Supprimer_Figure(clsFigures figure)
        {
            figures.Remove(figure);
            try
            {

            }
            catch (Exception ex)
            {

                LogEvents.Instance.PushEvent(new Event(EventType.Alarme, $"Erreur inconnue lors de la suppression de la figure du dessin : {ex.Message}"));
            }
        }
        public void DessinerFigures()
        {
            foreach (var figure in figures)
            {
                try
                {
                    figure.Dessine();
                }
                catch (Exception ex)
                {
                    LogEvents.Instance.PushEvent(new Event(EventType.Alarme, $"Erreur inconnue lors du dessin de la figure {figure.Nom} : {ex.Message}"));
                    Console.WriteLine($"Erreur inconnue lors du dessin de la figure {figure.Nom} : {ex.Message}");
                }
            }
        }
    }
}
