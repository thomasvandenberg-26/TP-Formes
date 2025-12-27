using nsFigures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace nsFigures
{
    public class Dessin
    {

        private List<clsFigures> figures;
        string jsonString =" ";
     
        public Dessin(string? ANom, float AVersion)
        {

            Nom = ANom;
            DateTime dateCreation = DateTime.Now; 
            Version = AVersion;
            figures = new List<clsFigures>();

            LogEvents.Instance.PushEvent(new Event(EventType.Information, $"Un dessin {Nom} a été crée"));

        }

        private string? _Nom;

        public string? Nom // Accesseurs R/W

        {

            get { return _Nom; } // Retour directement la valeur


            set { if (!string.IsNullOrEmpty(value))
                { _Nom = value; } else
                {
                    _Nom = "Dessin_Sans_Nom";
                }// Test avec plafonnement si besoin;

            }
        }

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

                LogEvents.Instance.PushEvent(new Event(EventType.Information, $"Figure Ajouté dans le json : {jsonString}"));

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

            SaveToJson("dessin.json");



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

        public void SaveToJson(string filePath)
        {
            try
            {
                var dessinData = new
                {
                    Nom = this.Nom,
                    Version = this.Version,
                    DateCreation = this.Date_Creation,
                    Figures = this.figures.Select(f =>
                    {
                        var fig =  new FigureDto
                        {
                            Type = f.GetType().Name,
                            Nom = f.Nom,
                            Couleur = f.Couleur.Name,
                            
                        };
                        if (f is clsRectangle rect)
                        {
                            fig.DepartX = rect.depart.X;
                            fig.DepartY = rect.depart.Y;
                            fig.Largeur = rect.Largeur;
                            fig.Hauteur = rect.Hauteur;
                        }

                        if( f is clsCarre carre)
                        {
                            fig.DepartX = carre.depart.X;
                            fig.DepartY = carre.depart.Y;
                            fig.Largeur = carre.LargeurHauteur;
                            fig.Hauteur = carre.LargeurHauteur;

                        }
                        if( f is clsLigne ligne)
                        {
                            fig.DepartX = ligne.depart.X;
                            fig.DepartY = ligne.depart.Y;
                            fig.Largeur = (ushort)(ligne.depart.X + ligne.depart.Y);
                        }
                        if (f is clsCube cube)
                        {   fig.DepartX = cube.depart.X;
                            fig.DepartY = cube.depart.Y;
                            fig.Profondeur = cube.profondeur;
                        }

                        return fig;
                    }).ToList()

                    
                };
                

                      
                        // Ajouter d'autres propriétés spécifiques aux figures si nécessaire
                 
                jsonString = JsonSerializer.Serialize(dessinData, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(filePath, jsonString);
                LogEvents.Instance.PushEvent(new Event(EventType.Information, $"Dessin sauvegardé en JSON : {filePath}"));
            }
            catch (Exception ex)
            {
                LogEvents.Instance.PushEvent(new Event(EventType.Alarme, $"Erreur lors de la sauvegarde du dessin en JSON : {ex.Message}"));
            }
        }

        public static Dessin LoadFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var dto = JsonSerializer.Deserialize<DessinDto>(json)
                      ?? throw new Exception("JSON invalide (DessinDto null)");

            var dessin = new Dessin(dto.Nom, dto.Version);

            foreach (var f in dto.Figures)
            {
                var point = new System.Drawing.Point(f.DepartX, f.DepartY);

                var color = System.Drawing.Color.FromName(f.Couleur);
                if (!color.IsKnownColor && !color.IsNamedColor)
                    color = System.Drawing.Color.Black;


                clsFigures fig = f.Type switch
                {
                    nameof(clsRectangle) => new clsRectangle(point, color, f.Hauteur, f.Largeur, f.Nom),
                    nameof(clsCarre) => new clsCarre(point, color,f.Nom, f.Largeur),
                    nameof(clsLigne) => new clsLigne(point, color, f.Nom, f.Largeur),
                    nameof(clsCube) => new clsCube(point, color, f.Nom, f.Profondeur),
                    nameof(clsCercle) => new clsCercle(point, color, f.Nom, f.Rayon),
                    _ => throw new Exception($"Type de figure inconnu: {f.Type}")
                };

                dessin.Ajouter_Figure(fig);
            }

            return dessin;
        }
    }
}
