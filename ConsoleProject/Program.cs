using nsFigures;
using System.Drawing;
using System.Runtime.CompilerServices;
 
namespace ConsoleProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // bool majeur = false;

            //Console.WriteLine("Quel age as tu ? ");
            //int age = int.Parse(Console.ReadLine());


            //if(age > 17)
            //{
            //    majeur = true;

            //}

            //Console.WriteLine("Vous êtes majeur : " + majeur);
            //Console.ReadLine();


            //float vitesse = 42.5F;
            //double temperature = 32.3;

            //decimal toto = 23.8M;


            //Console.WriteLine("toto est de type : " + toto.GetType());


            //string chaine = "ABC";
            //char car = chaine[0];
            //Console.WriteLine(chaine[0]);
            //Console.WriteLine(chaine[1]);
            //Console.WriteLine(chaine[2]);
            //Console.WriteLine(chaine[3]);

            //int i = 12;

            //Console.WriteLine(i.ToString());
            //string s = "ABC" + 12.ToString() + "_" + 1.23f.ToString() + true;

            //int x = 123;
            //byte b = 2;
            //s = "add(" + x + "+" + b + ")=" + (x + b);
            //Console.WriteLine(s);

            //s = string.Format("add({0}+{1})={2)", i, b, i + b);
            //Console.WriteLine(s);

            //s = $"add({x}+{b})={x+b}";
            //Console.WriteLine(s);


            //int nombre = 127;

            //string rep = $"nbr={nombre:x8}";
            //Console.WriteLine(rep);


            //DateTime dateTime = DateTime.Now;
            //s = $"{dateTime:gg}";
            //s = $"{dateTime:}";


            //Console.WriteLine(s);

            //float f = 123.33f;

            //int j = 12;

            //float k = (float)f;


            //int test = int.TryParse()

            //Console.WriteLine("Saisir un jour : entre 1 et 7 ");
            //string njour = Console.ReadLine();
            //int intJour = int.Parse(njour);

            //Console.WriteLine($"{intJour}");

            //switch (intJour)
            //{
            //    case 1:
            //        rep = "vous êtes un Lundi";
            //        break;
            //    case 2:
            //        rep = "vous êtes un Mardi";
            //        break;
            //    case 3:
            //        rep = "vous êtes un Mercredi";
            //        break;


            //}

            //while (true)
            //{

            //    Console.WriteLine(">");
            //    string strRep = Console.ReadLine();

            //    strRep = strRep.Trim();

            //    if (s == "")
            //        continue;

            //    switch(s.ToLower())
            //    {
            //        case "*":
            //            Formation();
            //            break;

            //        case "q":
            //            return;

            //        default:
            //            Console.WriteLine("Erreur !!!");
            //            break;

            //    }
            //}
            Formation();

            //int var1 = 6;
            //int var2 = 8;

            //swap(var1, var2); 

       
        }

        //struct Personne
        //{

        //  public  string Nom;

        //  public  byte Age;
        //  public  float Taille;

        //    public Adresse Lieu;

        //};

        //struct Adresse
        //{
        //    public string adresse1;
        //    public string adresse2;
        //    public uint codePostal;
        //    public string Ville;
        //    public byte Etage;
        //}
        //static void valeur(int val)
        //{
        //    Console.WriteLine($"Valeur={val}");
        //    val = 0;
        //    Console.WriteLine($"Valeur={val}");
        //}


        public static void Formation()
        {
            //Personne individu;
            //individu.Nom = "Gilles";
            //individu.Age = 24;
            //individu.Taille = 1.63f;

            //individu.Lieu.adresse1 = "33 rue challs";

            //int data = 12;
            //valeur(data);
            //Console.WriteLine($"Valeur={data}"); 

           // int i;
            // var fig = new clsFigures(); 
            // clsFigures fig = new clsFigures(); 

            //fig.X = 2;
            //fig.Y = 90;
            //fig.Nom = "carré";


            //clsFigures fig2 = new()
            //{
            //    X = 2,
            //    Y = 3,
            //    Nom = "",
            //    Angle = 0.0f

            //};

            //clsFigures fig2 = new clsFigures(200, 200, "carre");
            //clsRectangle rectangle = new clsRectangle(1, 2, 200, 500, "rectangle");


            //clsFigures fig3 = new clsRectangle(900, 20, 1, 2, "recta"); 



            //clsRectangle clsRectangle = new clsRectangle(
            //    new Point(50, 50),
            //    new Point(100, 100),
            //    Color.Aqua,
            //    10,
            //    200,
            //    "Rectangle1"

            //);


            //Console.WriteLine($"Avant Zoom : Largeur = {clsRectangle.Largeur}, Hauteur = {clsRectangle.Hauteur}");
            //  clsRectangle.Zoom(0.5f, 0.5f);



            clsCube clsCube1 = new clsCube(
                new Point(10, 50)
                ,
                Color.Yellow,
                null,
                 20
            );

            //  clsCube clsCube2 = new clsCube(
            //    new Point(50, 50),
            //    new Point(100, 100),
            //    Color.Yellow,
            //    null,
            //     300
            //);
            //  clsCube clsCube3 = new clsCube(
            // new Point(50, 50),
            // new Point(100, 100),
            // Color.Yellow,
            // null,
            //  300

            //Console.WriteLine("Liste figures taille : {0} ",  clsCube2.ListeFigures.Count.ToString());
            EventService serviceEvents = new EventService();

            Event demarrage = new Event(EventType.Information, "Démarrage de l'application");

            serviceEvents.pushEvent(demarrage);

            Console.WriteLine("--------------- STATISTIQUES EVENTS -----------------");
            Console.WriteLine($"Informations : {serviceEvents.CountInfo}");
            Console.WriteLine($"Alertes      : {serviceEvents.CountAlerte}");
            Console.WriteLine($"Alarmes      : {serviceEvents.CountAlarme}");
            Console.WriteLine($"Perdus       : {serviceEvents.CountPerdus}");
            Console.WriteLine("------------------------------------------------------");
            clsFigures.SupportDessin = new SupportImprimante_Canon();
            Dessin? dessin1 = null;
            try {
                dessin1.Version = 1.0f; 
            }
            
            catch(Exception ex)
            {
                LogEvents.Instance.Push("La creation du dessin n'a pas fonctionné ", LogEvents.TypeEvenement.CREATION_DESSIN, ex, ex.Message);
            }

            clsRectangle? r = null;
            try
            {
                Color color = Color.FromArgb(300, 300, 300);
               r = new clsRectangle(new Point(10, 10), color, 20, 40, "R1");
         
            }
            catch(ArgumentException ae)
            {
                LogEvents.Instance.Push("La creation d'une couleur n'a pas fonctionné ", LogEvents.TypeEvenement.CREATION_COULEUR, ae, ae.Message);
                
            }
            finally
            {
             
                dessin1.Ajouter_Figure(r);
            }
           
            //  r.Dessine();

            // clsCube1.Dessine();

            clsCarre clsCarre = new clsCarre(new Point(50, 30),
                Color.FromArgb(125, 125, 125),
                "MonCarre",
                40);

         //clsCarre.Dessine();
           clsLigne clsLigne = new clsLigne( new Point(70, 30),Color.Green, "MaLigne", 20, 30.0f);

            clsCercle clsCercle = new clsCercle( new Point(20,30), Color.Purple, "MonCercle", 25);
           // clsCercle.Dessine();

            //clsLigne.Dessine();
            //  clsCylindre clsCylindre = new clsCylindre(
            // new Point(50, 50),
            // new Point(100, 100),
            // Color.Brown,
            //"MonCylindre",
            //  20,
            //  150);
            //  clsCylindre.Zoom(0.5f);

            // Toutes les figures sont dans la liste commune
            // clsCube3.Zoom(0.5f, 0.5f);
         
            dessin1.Ajouter_Figure(clsCube1);
            dessin1.Ajouter_Figure(clsCarre);
            dessin1.Ajouter_Figure(clsLigne);
            dessin1.DessinerFigures();

        }
        public static void swap(ref int var1,ref int var2)
        {
            int tempVar = var1;
            var1 = var2;
            var2 = var1;
            Console.WriteLine($"Après swap : var1 = {var1}, var2 = {var2}");
        }
   

     
    }

    public static class ClsExtension
    {
        public static bool EstPair(this int AValue)
        {
            return (AValue % 2 == 0) ? true : false; 
        }

    }
}
