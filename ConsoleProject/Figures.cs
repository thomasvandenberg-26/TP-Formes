using System;

using System.Collections.Generic;

using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using System.Threading.Tasks;

using System.Drawing;

namespace nsFigures
{
    internal abstract class clsFigures

    {

        #region Constructeurs

        //-------------------------

        internal clsFigures(Point AX , Point AY,  string ANom = "")

        {

            X = AX;

            Y = AY;

            Angle = 0.0f;

            Nom = ANom ?? string.Empty;

        }

        #endregion



        #region propriété X

        // Accesseur

       // internal const ushort MAX_X = 800;
       // = (value > MAX_X) ? MAX_X : value;

        public Point _X;

        public Point X // 0 à 800 pixels

        {

            get

            {

                return _X;

            }

            set

            {

                _X = value; 

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

        public string Nom // Accesseurs Read Only

        {

            get { return _Nom ?? ""; } // Retour directement la valeur
            set {  _Nom = value; }

        }

        #endregion


        //#region --- Propriété SupportDessin

        //// static: une seule variable pour tous les objets créés (qqes soit le nombre d’objets)

        //static protected nsSupportDessin.ISupportDessin? _SupportDessin; // Propriété  qui contient le support pour dessiner

        //static public nsSupportDessin.ISupportDessin? SupportDessin// Accesseurs W

        //{

        //    set { _SupportDessin = value; }

        //}

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

            return $"Figure \"{Nom}\": X={X} Y={Y}";

        }

    }//class clsFigures



    //=========================

    internal class clsRectangle : clsFigures

    {

        internal clsRectangle(Point AX, Point AY, ushort ALargeur, ushort AHauteur, string ANom = "")

          : base(AX, AY, ANom)

        {

            ///Largeur = ALargeur;

      //            Hauteur = AHauteur;

        }



        #region propriété Largeur

        //--- Propriété Y

        private ushort _Largeur;

        public ushort Largeur // Accesseurs R/W

        {

            get { return _Largeur; } // Retour directement la valeur

            //set { _Largeur = (value > MAX_X) ? MAX_X : value; } // Test avec plafonnement si besoin

        }

        #endregion



        #region propriété Hauteur

        //--- Propriété Hauteur

        private ushort _Hauteur;

        public ushort Hauteur // Accesseurs R/W

        {

            get { return _Hauteur; } // Retour directement la valeur

            set { _Hauteur = (value > MAX_Y) ? MAX_Y : value; } // Test avec plafonnement si besoin

        }

        internal override void Dessine()
        {
            Console.WriteLine($"--- clsRectangle.Dessine(X={X} Y={Y} L={Largeur} H={Hauteur} \"{Nom}\")");

            //            Console.WriteLine($"    (Angle={Angle:0.0} C={Couleur})");
        }

        override internal void Zoom(float ACoeffX, float ACoeffY = 1.0f)

        {

            // Expansion Largeur & Hauteur

            Console.WriteLine($"clsRectangle.Zoom(CoeffX={ACoeffX} CoeffX={ACoeffY} \"{Nom}\")");



            if (ACoeffX < 0.0f)

                ACoeffX = 1.0f; // Coeff négatif non accepté



            if (ACoeffY < 0.0f)

                ACoeffY = 1.0f; // Coeff négatif non accepté



            //Largeur = (ushort)(Largeur * ACoeffX); // Calcul nouvelle Largeur

            Hauteur = (ushort)(Hauteur * ACoeffY); // Calcul nouvelle Hauteur



            Dessine();// Redessine figure

        }

        public override string ToString()

        {

            return $"Rectangle \"{Nom}\": X={X} Y={Y} L={Largeur} H={Hauteur}";

        }

        #endregion

    }//class clsRectangle


    internal class clsCarre : clsFigures
    {
        internal clsCarre(Point AX, Point AY, string ANom, ushort ALargeurHauteur)
            : base(AX, AY, ANom)
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
            Console.WriteLine($"--- clsRectangle.Dessine(X={X} Y={Y} C={LargeurHauteur} \"{Nom}\")");
        }

        public override string ToString()

        {

            return $"Carré \"{Nom}\": X={X} Y={Y} Coté={LargeurHauteur}";

        }
    }

    internal class clsLigne : clsRectangle
    {
        internal clsLigne(Point AX, Point AY, string ANom, ushort ALargeur, ushort AHauteur)
            : base(AX, AY, ALargeur, AHauteur)
        {
            AHauteur = 1;
        }

        internal override void Dessine()
        {
            Console.WriteLine($"-- clsRectangle.Dessine(X={X} Y={Y} C={Largeur} H={1}");
        }

        public override string ToString()

        {

            return $"Ligne \"{Nom}\": X={X} Y={Y} L={Largeur}";

        }
    }
    internal class clsCube : clsFigures
    {
        internal clsCube(Point AX, Point AY, string ANom, ushort AProfondeur)
            : base(AX, AY, ANom)
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
            Console.WriteLine($"-- clsRectangle.Dessine(X={X} Y={Y} P={profondeur} ");
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

            return $"Ligne \"{Nom}\": X={X} Y={Y} L={profondeur}";

        }


    }
    internal class clsCercle : clsFigures
    {
        internal clsCercle(Point AX, Point AY, string ANom, ushort ARayon)
            : base(AX, AY, ANom)
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

            Console.WriteLine($"--- clsCercle.Dessine(X={X} Y={Y} R={Rayon} \"{Nom}\")");

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

            return $"Cercle \"{Nom}\": X={X} Y={Y} R={Rayon}";

        }
    }
    internal class clsCylindre : clsCercle
    {
        internal clsCylindre(Point AX, Point AY, string ANom, ushort ARayon, ushort AProfondeur)
        : base(AX, AY, ANom, ARayon)
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

            Console.WriteLine($"--- clsCylindre.Dessine(X={X} Y={Y} R={Rayon} P={profondeur}\"{Nom}\")");
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

            return $"Cercle \"{Nom}\": X={X} Y={Y} R={Rayon} P={profondeur}";

        }

    }

    internal class clsPoint : clsFigures

    {

        #region --- ctor

        internal clsPoint(Point AX, Point AY, string ANom = "")

          : base(AX, AY, ANom) // Appel classe Parent (ici = clsFigure)

        {

            // Init des propriétés de la classe clsFigure -> base(...)

            Console.WriteLine($"clsPoint.ctor(X={X} Y={Y} \"{Nom}\")");

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

            Console.WriteLine($"--- clsPoint.Dessine(X={X} Y={Y} \"{Nom}\")");



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

            return $"Point \"{Nom}\": X={X} Y={Y}";

        }

    } //class clsPoint

}
