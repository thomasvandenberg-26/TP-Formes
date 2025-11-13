using System;

using System.Collections.Generic;

using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using System.Threading.Tasks;

namespace nsFigures
{
    internal abstract class clsFigures

    {

        #region Constructeurs

        //-------------------------

        internal clsFigures(ushort AX, ushort AY, string ANom = "")

        {

            X = AX;

            Y = AY;

            Angle = 0.0f;

            Nom = ANom ?? string.Empty;

        }

        #endregion



        #region propriété X

        // Accesseur

        internal const ushort MAX_X = 800;

        public ushort _X;

        public ushort X // 0 à 800 pixels

        {

            get

            {

                return _X;

            }

            set

            {

                _X = (value > MAX_X) ? MAX_X : value;

            }

        }

        #endregion



        #region propriété Y

        //--- Propriété Y

        public const ushort MAX_Y = 480;

        private ushort _Y; // Propriété privée qui contient la valeur (0 à 480 pixels)

        public ushort Y // Accesseurs R/W

        {

            get { return _Y; } // Retour directement la valeur

            set { _Y = (value > MAX_Y) ? MAX_Y : value; } // Test avec plafonnement si besoin

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



        public string Nom;

        abstract internal void Dessine(); 

    }//class clsFigures



    //=========================

    internal class clsRectangle : clsFigures

    {

        internal clsRectangle(ushort AX, ushort AY, ushort ALargeur, ushort AHauteur, string ANom = "")

          : base(AX, AY, ANom)

        {

            Largeur = ALargeur;

            Hauteur = AHauteur;

        }

        

        #region propriété Largeur

        //--- Propriété Y

        private ushort _Largeur;

        public ushort Largeur // Accesseurs R/W

        {

            get { return _Largeur; } // Retour directement la valeur

            set { _Largeur = (value > MAX_X) ? MAX_X : value; } // Test avec plafonnement si besoin

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

        #endregion

    }//class clsRectangle


    internal class clsCarre : clsFigures
    {
        internal clsCarre(ushort AX, ushort AY, string ANom, ushort ALargeurHauteur)
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
            throw new NotImplementedException();
        }
    }
    
    internal class clsLigne : clsRectangle
    {
        internal clsLigne(ushort AX, ushort AY, string ANom,ushort  ALargeur, ushort AHauteur )
            :base(AX , AY, ALargeur, AHauteur)
        {
            AHauteur = 1; 
        }
    }
    internal class clsCube : clsFigures
    {
        internal clsCube(ushort AX, ushort AY, string ANom, float AProfondeur)
            : base(AX, AY, ANom)
        {
            profondeur = AProfondeur;
        }
        public float _profondeur;

        public float profondeur
        {
            get { return _profondeur; }
            set
            {
                _profondeur = value;
            }
        }
        internal class clsCercle : clsFigures
        {
            internal clsCercle(ushort AX, ushort AY, string ANom, float ARayon)
                : base(AX, AY, ANom)
            {
                Rayon = ARayon;

            }
            public float _Rayon;

            public float Rayon
            {
                get { return _Rayon; }
                set { _Rayon = value; }
            }

            internal override void Dessine()
            {
                throw new NotImplementedException();
            }
        }

        internal class clsCylindre : clsCercle
        {
            internal clsCylindre(ushort AX, ushort AY, string ANom, float ARayon, float AProfondeur)
            : base(AX, AY, ANom, ARayon)
            {
                profondeur = AProfondeur;
            }

            public float _profondeur;

            public float profondeur
            {
                get { return _profondeur; }
                set { _profondeur = value; }
            }
        }

        internal override void Dessine()
        {
            throw new NotImplementedException();
        }
    }
}

