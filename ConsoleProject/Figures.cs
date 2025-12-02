using ConsoleProject;
using System.Drawing;
using System.Xml;
using nsFigures;
using System.Runtime.InteropServices.JavaScript;
namespace nsFigures
{
    public abstract class clsFigures

    {

        #region Constructeurs

        //-------------------------

        // je limite la taille des images à 800x480 pixels

        // à l'aide de constantes

        public const int MAX_X = 800;
        public const int MAX_Y = 480;


        internal clsFigures(Point ADepart, Color color, string? ANom = null)

        {
        

            depart = ADepart;

            Angle = 0.0f;

            Nom = ANom ?? ""; // Assure que _Nom n'est jamais null


        }

        #endregion

        #region Liste des figures

        // Champ privé
        public static List<clsFigures> _listeFigures = new List<clsFigures>();

        // Propriété publique
        public List<clsFigures> ListeFigures
        {
            get { return _listeFigures; }
            set { _listeFigures = value ?? new List<clsFigures>(); }
        }


        #endregion

        #region propriété X

        // Accesseur

        // internal const ushort MAX_X = 800;
        // = (value > MAX_X) ? MAX_X : value;

        private Point _depart;

        public Point depart// 0 à 800 pixels

        {

            get

            {

                return _depart;

            }

            set

            {

                _depart = value; 

            }

        }

        #endregion



        #region propriété Y

        //--- Propriété Y

        private Point _Y; // Propriété privée qui contient la valeur (0 à 480 pixels)

        public Point Y // Accesseurs R/W

        {

            get { return _Y; } // Retour directement la valeur

            set { _Y = value; } // Test avec plafonnement si besoin

        }

        #endregion



        #region Angle

        // --- Propriété Couleur
        public Color _Couleur; // Propriété privée qui contient la valeur de la couleur
        public Color Couleur // Accesseurs R/W
        {
            get { return _Couleur; } // Retour directement la valeur
            set { _Couleur = value; } // Test avec plafonnement si besoin
        }

        //--- Propriété Angle


        public const float MAX_ANGLE = 360.0f;

        private float _Angle; // Propriété privée qui contient la valeur (0.0 à 360.0°)

        public float Angle // Accesseurs R/W

        {

            get { return _Angle; } // Retour directement la valeur

            set { _Angle = (value > MAX_ANGLE) ? MAX_ANGLE : (value < 0.0f ? 0.0f : value); } // Test avec plafonnement si besoin

        }

        #endregion


        #region --- Propriété Nom

        //X159#3

        static int i = 0; 
        public string _Nom;   // Propriété privée qui contient le Nom de la figure

        public string Nom // Accesseurs Lecture/Écriture

        {
            
            get { return _Nom ?? ""; } // Retour directement la valeur
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _Nom = value  ;
                else
                {
                    // Génère un nom par défaut basé sur la liste
                    i++;
                    _Nom = $"Figure{i}";

                }
            }
        }

        #endregion


        //#region --- Propriété SupportDessin

        //// static: une seule variable pour tous les objets créés (qqes soit le nombre d’objets)

        static protected ISupportDessin? _SupportDessin; // Propriété  qui contient le support pour dessiner
        static public ISupportDessin? SupportDessin// Accesseurs W

        {

            set { _SupportDessin = value; }

        }

        // #endregion


        #region EventService 
        static protected EventService? _EventService;
         static public EventService EventService
        {
            set { _EventService = value; }
        }
        #endregion

        abstract internal void Dessine();

        virtual internal void Zoom(float ACoeffX, float ACoeffY = 1.0f)

        {

            // Méthode par défaut : rien à traiter dans classe clsFigure !

            Console.WriteLine($"clsFigure.Zoom(CoeffX={ACoeffX} CoeffX={ACoeffY} \"{Nom}\")");

            Dessine();// Redessine figure
            

        }

        public override string ToString()

        {

            return $"Figure \"{Nom}\": X={depart.X} Y={depart.Y}";

        }

    }//class clsFigures



    //=========================

    internal class clsRectangle : clsFigures

    {

        internal clsRectangle(Point depart, Color color, ushort AHauteur, ushort ALargeur, string ANom = "")

          : base(depart, color, ANom)

        {

            Largeur = ALargeur;

            Hauteur = AHauteur;

            Couleur = color;
        }



        #region propriété Largeur

        //--- Propriété Y

        private ushort _Largeur;

        public ushort Largeur // Accesseurs R/W

        {

            get { return _Largeur; } // Retour directement la valeur

            set { _Largeur = value; } // Test avec plafonnement si besoin;

        }

        #endregion



        #region propriété Hauteur

        //--- Propriété Hauteur

        private ushort _Hauteur;

        public ushort Hauteur // Accesseurs R/W

        {

            get { return _Hauteur; } // Retour directement la valeur

            set { _Hauteur = value; } // Test avec plafonnement si besoin

        }
        internal override void Dessine()
        {

            //Console.WriteLine($"--- clsRectangle.Dessine(X={X} Y={Y} Color={Couleur} L={Largeur} H={Hauteur} \"{Nom}\")");
           

            if(_SupportDessin is null)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Le support de dessin n'est pas défini.");
                return;
            }

            Console.WriteLine("ClsRectangle");
            // D’après ton code tu utilises X.X pour la coordonnée X et Y.Y pour la coordonnée Y
            int x1 = depart.X;
            int y1 = depart.Y;
            int x2 = depart.X + Largeur;
            int y2 = depart.Y + Hauteur;

            // Je vérifie que les coordonnées sont dans les limites de la taille de zone de dessin
            if ((x1 < 0 || y1 < 0) || (x1 > MAX_X || y1 > MAX_Y))
            {
              
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Changement pour une valeur Min 0");
                x1 = 0;
                y1 = 0; 
            }

            if (x2 > MAX_X || y2 > MAX_Y)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Le rectangle dépasse les limites maximales du dessin. Changement pour une valeur Max 800 480");
                x2 = depart.X + MAX_X; 
                y2 = depart.Y + MAX_Y;
            }

            // 4️⃣ Tracé des 4 côtés du rectangle avec l’interface commune
            try { _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B); 
            }
         catch(NullReferenceException nre)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, nre, "Le support n'est pas initialisé.");
                return;
            }
 


            _ = _SupportDessin.Ligne_Trace(x1, y1, x2, y1); // haut
            _ = _SupportDessin.Ligne_Trace(x2, y1, x2, y2); // droite
            _ = _SupportDessin.Ligne_Trace(x2, y2, x1, y2); // bas
            _ = _SupportDessin.Ligne_Trace(x1, y2, x1, y1); // gauche

        }

        override internal void Zoom(float ACoeffX, float ACoeffY = 1.0f)

        {

            // Expansion Largeur & Hauteur

            Console.WriteLine($"clsRectangle.Zoom(CoeffX={ACoeffX} CoeffX={ACoeffY} \"{Nom}\")");



            if (ACoeffX < 0.0f)

                ACoeffX = 1.0f; // Coeff négatif non accepté
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Coefficient de zoom négatif pour X. Valeur par défaut 1.0 utilisée.");



            if (ACoeffY < 0.0f)

                ACoeffY = 1.0f; // Coeff négatif non accepté
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Coefficient de zoom négatif pour Y. Valeur par défaut 1.0 utilisée.");



            Largeur = (ushort)(Largeur * ACoeffX); // Calcul nouvelle Largeur

            Console.WriteLine($"Largeur après zoom: {Largeur}");

            Hauteur = (ushort)(Hauteur * ACoeffY); // Calcul nouvelle Hauteur



            Dessine();// Redessine figure

        }

        public override string ToString()

        {

            return $"Rectangle \"{Nom}\": X={depart.X} Y={depart.Y} Color={Couleur} L={Largeur} H={Hauteur}";

        }

        #endregion

    }//class clsRectangle


    internal class clsCarre : clsFigures
    {
        internal clsCarre(Point depart, Color color ,string ANom, ushort ALargeurHauteur)
            : base(depart, color, ANom)
        {
            LargeurHauteur = ALargeurHauteur;
        }
        public ushort _LargeurHauteur;

        public ushort LargeurHauteur
        {
            get { return _LargeurHauteur; }
            set { _LargeurHauteur = value; }

        }

        internal override void Dessine()
        {
            if(_SupportDessin is null)
            {
                return;
            }

            int x1 = depart.X;
            int y1 = depart.Y;
            int x2 = depart.X + LargeurHauteur;
            int y2 = depart.Y + LargeurHauteur;

            // Je vérifie que les coordonnées sont dans les limites de la taille de zone de dessin
            if ((x1 < 0 || y1 < 0) || (x1 > MAX_X || y1 > MAX_Y))
            {

                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Changement pour une valeur Min 0");
                x1 = 0;
                y1 = 0;
            }

            if (x2 > MAX_X || y2 > MAX_Y)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Le rectangle dépasse les limites maximales du dessin. Changement pour une valeur Max 800 480");
                x2 = depart.X + MAX_X;
                y2 = depart.Y + MAX_Y;
            }

            Console.WriteLine("ClsCarre");

            try
            {
                _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);
            }
            catch (NullReferenceException nre)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, nre, "Le support n'est pas initialisé.");
                return;
            }
            catch (ArgumentException ae)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, ae, "Argument invalide lors de la sélection de la couleur.");
                return;
            }
            catch (FormatException fe)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, fe, "Erreur de format lors de la sélection de la couleur.");
                return;
            }
            _ = _SupportDessin.Ligne_Trace(x1, y1, x2, y1); // haut
            _ = _SupportDessin.Ligne_Trace(x2, y1, x2, y2); // droite
            _ = _SupportDessin.Ligne_Trace(x2, y2, x1, y2); // bas
            _ = _SupportDessin.Ligne_Trace(x1, y2, x1, y1); // gauche
            //Console.WriteLine($"--- clsRectangle.Dessine(X={X} Y={Y} Color={Couleur} C={LargeurHauteur} \"{Nom}\")");
        }

        public override string ToString()

        {

            return $"Carré \"{Nom}\": X={depart.X} Y={depart.Y} Color={Couleur} Coté={LargeurHauteur}";

        }
    }

    internal class clsLigne : clsRectangle
    {
        internal clsLigne(Point depart,Color color,string ANom, ushort longueur,   float angle )
            : base(depart, color, 1, longueur, ANom)
                  
        {
           
        }

        internal override void Dessine()
        {

            if(_SupportDessin is null)
            {
                return;
            }
            int x1 = depart.X;
            int y1 = depart.Y;
            int x2 = depart.X + Largeur;
            int y2 = depart.Y + 1;

            // Je vérifie que les coordonnées sont dans les limites de la taille de zone de dessin
            if ((x1 < 0 || y1 < 0) || (x1 > MAX_X || y1 > MAX_Y))
            {

                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Changement pour une valeur Min 0");
                x1 = 0;
                y1 = 0;
            }

            if (x2 > MAX_X || y2 > MAX_Y)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Le rectangle dépasse les limites maximales du dessin. Changement pour une valeur Max 800 480");
                x2 = depart.X + MAX_X;
                y2 = depart.Y + MAX_Y;
            }
            Console.WriteLine("clsLigne");
            try
            {
                _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);
            }
            catch (NullReferenceException nre)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, nre, "Le support n'est pas initialisé.");
                return;
            }
  
            _ = _SupportDessin.Ligne_Trace(x1, y1, x2, y2); // haut
             
            //Console.WriteLine($"-- clsRectangle.Dessine(X={X} Y={Y} Color={Couleur} C={Largeur} H={1}");
        }

        public override string ToString()

        {

            return $"Ligne \"{Nom}\": X={depart.X} Y={depart.Y} Color={Couleur} L={Largeur}";

        }
    }
    internal class clsCube : clsFigures
    {
        internal clsCube(Point depart, Color color, string? ANom, ushort AProfondeur)
            : base(depart, color,ANom)
        {
            profondeur = AProfondeur;
        }
        public ushort _profondeur;

        public ushort profondeur
        {
            get { return _profondeur; }
            set
            {
                _profondeur = value;
            }
        }

        internal override void Dessine()
        {

            if(_SupportDessin is null )
            {
               return;

            }

            int x1 = depart.X;
            int y1 = depart.Y;
            int x2 = depart.X + profondeur;
            int y2 = depart.Y + profondeur;


            // Je vérifie que les coordonnées sont dans les limites de la taille de zone de dessin
            if ((x1 < 0 || y1 < 0) || (x1 > MAX_X || y1 > MAX_Y))
            {

                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Changement pour une valeur Min 0");
                x1 = 0;
                y1 = 0;
            }

            if (x2 > MAX_X || y2 > MAX_Y)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Le rectangle dépasse les limites maximales du dessin. Changement pour une valeur Max 800 480");
                x2 = depart.X + MAX_X;
                y2 = depart.Y + MAX_Y;
            }
            Console.WriteLine("ClsCube");
            try
            {
                _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);
            }
            catch (NullReferenceException nre)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, nre, "Le support n'est pas initialisé.");
                return;
            }
        
 
            _ = _SupportDessin.Ligne_Trace(x1, y1, x2, y1); // haut
            _ = _SupportDessin.Ligne_Trace(x2, y1, x2, y2); // droite
            _ = _SupportDessin.Ligne_Trace(x2, y2, x1, y2); // bas
            _ = _SupportDessin.Ligne_Trace(x1, y2, x1, y1); // gauche

            //Console.WriteLine($"-- clsCube.Dessine(X={depart.X} Y={depart.Y} Color={Couleur} P={profondeur} ");
            //Console.WriteLine(ToString()); 
        }

        internal override void Zoom(float ACoeffX, float ACoeffY = 1)
        {
            Console.WriteLine($"-- clsCube.Zoom(CoeffX ={ACoeffX} CoeffY={ACoeffY} \"{Nom} ) ");

            if (ACoeffX < 0.0f)

                ACoeffX = 1.0f; // Coeff négatif non accepté
            LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Coefficient de zoom négatif pour X. Valeur par défaut 1.0 utilisée.");



            if (ACoeffY < 0.0f)

                ACoeffY = 1.0f; // Coeff négatif non accepté
            LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Coefficient de zoom négatif pour Y. Valeur par défaut 1.0 utilisée.");




            profondeur = (ushort)(profondeur * ACoeffX);

            Dessine();
        }

        public override string ToString()

        {

            return $"Cube \"{Nom}\": X={depart.X} Y={depart.Y}  Color={Couleur} L={profondeur}";

        }


    }
    internal class clsCercle : clsFigures
    {
        internal clsCercle(Point depart, Color color, string ANom, ushort ARayon)
            : base(depart,color,  ANom)
        {
            Rayon = ARayon;

        }
        public ushort _Rayon;

        public ushort Rayon
        {
            get { return _Rayon; }
            set { _Rayon = value; }
        }

        internal override void Dessine()
        {   
            if(_SupportDessin is null)
            {
                return;
            }

 

            Console.WriteLine($"--- clsCercle.Dessine(X={depart.X} Y={depart.Y} Color={Couleur} R={Rayon} \"{Nom}\")");

            //Console.WriteLine($"    (C={Couleur})");

        }

        override internal void Zoom(float ACoeffX, float ACoeffY = 1.0f)

        {

            // Expansion Largeur & Hauteur

            Console.WriteLine($"clsCercle.Zoom(CoeffX={ACoeffX} CoeffX={ACoeffY} \"{Nom}\")");



            if (ACoeffX < 0.0f)

                ACoeffX = 1.0f; // Coeff négatif non accepté
            LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Coefficient de zoom négatif pour X. Valeur par défaut 1.0 utilisée.");


            Rayon = (ushort)(Rayon * ACoeffX); // Calcul nouvelle Rayon



            Dessine();// Redessine figure

        }

        public override string ToString()

        {

            return $"Cercle \"{Nom}\": X={depart.X} Y={depart.Y} Color={Couleur} R={Rayon}";

        }
    }
    internal class clsCylindre : clsCercle
    {
        internal clsCylindre(Point depart,Color color,  string ANom, ushort ARayon, ushort AProfondeur)
        : base (depart, color, ANom, ARayon)
        {
            profondeur = AProfondeur;
        }

        public ushort _profondeur;

        public ushort profondeur
        {
            get { return _profondeur; }
            set { _profondeur = value; }
        }


        internal override void Dessine()
        {

            Console.WriteLine($"--- clsCylindre.Dessine(X={depart.X} Y={depart.Y} Color={Couleur} R={Rayon} P={profondeur}\"{Nom}\")");
        }

        internal override void Zoom(float ACoeffX, float ACoeffY = 1)
        {
            // Expansion Largeur & Hauteur

            Console.WriteLine($"clsCercle.Zoom(CoeffX={ACoeffX} CoeffX={ACoeffY} \"{Nom}\")");



            if (ACoeffX < 0.0f)

                ACoeffX = 1.0f; // Coeff négatif non accepté


            if (ACoeffX < 0.0f)

                ACoeffX = 1.0f; // Coeff négatif non accepté
            LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, "Coefficient de zoom négatif pour X. Valeur par défaut 1.0 utilisée.");

            try
            {

                profondeur = (ushort)(profondeur * ACoeffX); // Calcul nouvelle Rayon

            }
            catch(OverflowException oe)
            {
                LogEvents.Instance.Push(Nom, LogEvents.TypeEvenement.DESSIN_FIGURE, oe, "Débordement lors du calcul de la nouvelle profondeur.");
                profondeur = ushort.MaxValue; // Définit à la valeur maximale en cas de débordement
            }


            Dessine();
        }

        public override string ToString()

        {
            
            return $"Cercle \"{Nom}\": X={depart.X} Y={depart.Y} Color={Couleur} R={Rayon} P={profondeur}";

        }

    }

    internal class clsPoint : clsFigures

    {

        #region --- ctor

        internal clsPoint(Point depart, Color color, string ANom = "")

          : base( depart, color, ANom) // Appel classe Parent (ici = clsFigure)

        {

            // Init des propriétés de la classe clsFigure -> base(...)

            Console.WriteLine($"clsPoint.ctor(X={depart.X} Y={depart.Y} \"{Nom}\")");

        }

        #endregion



        #region --- Propriété SupportDessin

        // static: une seule variable pour tous les objets créés (qqes soit le nombre d’objets)

        //static protected nsSupportDessin.ISupportDessin? _SupportDessin; // Propriété  qui contient le support pour dessiner

        //static public nsSupportDessin.ISupportDessin? SupportDessin// Accesseurs W

        //{

        //    set { _SupportDessin = value; }

        

        #endregion
        //---------------------------------

        // Dessin du Point

        override internal void Dessine()

        {

            Console.WriteLine($"--- clsPoint.Dessine(X={depart.X} Y={depart.Y} Color={Couleur} \"{Nom}\")");

        }



        //---------------------------------

        public override string ToString()

        {

            return $"Point \"{Nom}\": X={depart.X} Color={Couleur} Y={Y}";

        }

    } //class clsPoint

}
