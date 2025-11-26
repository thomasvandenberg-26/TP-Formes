using ConsoleProject;
using System.Drawing;
using System.Xml;

namespace nsFigures
{
    internal abstract class clsFigures

    {

        #region Constructeurs

        //-------------------------

        internal clsFigures(Point ADepart, Color color, string? ANom = null)

        {

            depart = ADepart;

            Angle = 0.0f;

            Nom = ANom ?? ""; // Assure que _Nom n'est jamais null

            ListeFigures.Add(this);
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

        public const ushort MAX_Y = 480;

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
                    int i = ListeFigures.Count + 1;
                    _Nom = "Figure" + i.ToString();

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
            

            //            Console.WriteLine($"    (Angle={Angle:0.0} C={Couleur})");

            if(_SupportDessin is null)
            {
                return;
            }

            Console.WriteLine("ClsRectangle");
            // D’après ton code tu utilises X.X pour la coordonnée X et Y.Y pour la coordonnée Y
            int x1 = depart.X;
            int y1 = depart.Y;
            int x2 = depart.X + Largeur;
            int y2 = depart.Y + Hauteur;

            // 4️⃣ Tracé des 4 côtés du rectangle avec l’interface commune
            _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);
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



            if (ACoeffY < 0.0f)

                ACoeffY = 1.0f; // Coeff négatif non accepté



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
            Console.WriteLine("ClsCarre");

            _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);
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
            Console.WriteLine("clsLigne");
            _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);
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

            Console.WriteLine("ClsCube");
            _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);
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


            if (ACoeffY < 0.0f)

                ACoeffY = 1.0f; // Coeff négatif non accepté


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

            //int xCenter = depart.X;
            //int yCenter = depart.Y;
            //int radius = Rayon;
            //int points = 100; // Nombre de points pour dessiner le cercle
            //double angleStep = 2 * Math.PI / points;
            //_ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);
            //for (int i = 0; i < points; i++)
            //{
            //    double angle1 = i * angleStep;
            //    double angle2 = (i + 1) * angleStep;
            //    int x1 = xCenter + (int)(radius * Math.Cos(angle1));
            //    int y1 = yCenter + (int)(radius * Math.Sin(angle1));
            //    int x2 = xCenter + (int)(radius * Math.Cos(angle2));
            //    int y2 = yCenter + (int)(radius * Math.Sin(angle2));
            //    _ = _SupportDessin.Ligne_Trace(x1, y1, x2, y2);
            //}


            Console.WriteLine($"--- clsCercle.Dessine(X={depart.X} Y={depart.Y} Color={Couleur} R={Rayon} \"{Nom}\")");

            //Console.WriteLine($"    (C={Couleur})");

        }

        override internal void Zoom(float ACoeffX, float ACoeffY = 1.0f)

        {

            // Expansion Largeur & Hauteur

            Console.WriteLine($"clsCercle.Zoom(CoeffX={ACoeffX} CoeffX={ACoeffY} \"{Nom}\")");



            if (ACoeffX < 0.0f)

                ACoeffX = 1.0f; // Coeff négatif non accepté



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



            profondeur = (ushort)(profondeur * ACoeffX); // Calcul nouvelle Rayon
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



            //// Si SupportDessin non défini (null), pas de traitement

            //if (_SupportDessin != null)

            //{

            //  _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);

            //  _ = _SupportDessin.Ligne_Trace(X, Y, X, Y);

            //}



            // Si SupportDessin null, pas de traitement

            //_ = _SupportDessin?.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);

            //_ = _SupportDessin?.Ligne_Trace(X, Y, X, Y);



            //// Si SupportDessin non de type interface ISupportDessin (null ou autre type), pas de traitement

            //if (_SupportDessin is nsSupportDessin.ISupportDessin)

            //{

            //  _ = _SupportDessin.Couleur_Selectionne(Couleur.R, Couleur.G, Couleur.B);

            //  _ = _SupportDessin.Ligne_Trace(X, Y, X, Y);

            //}

        }



        //---------------------------------

        public override string ToString()

        {

            return $"Point \"{Nom}\": X={depart.X} Color={Couleur} Y={Y}";

        }

    } //class clsPoint

}
