using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nsFigures;
using System.Drawing;
namespace nsFigures;

public class Program
{
    static void Main(string[] args)
    {
        var support = new SupportImprimante_Canon("Dessin_CANON.txt");

        clsFigures.SupportDessin = support;


        var monDessin = new Dessin("Thomas", 1.0f);
        monDessin.Ajouter_Figure(new clsCube(new Point(10, 10), Color.Red, "Cube1", 20));
        monDessin.Ajouter_Figure(new clsRectangle(new Point(20,15), Color.Aqua,30, 15, "Rectangle1"));

      
        monDessin.DessinerFigures();

        Console.WriteLine("Dessin terminé.");
        Console.WriteLine("Fichier attendu : Dessin_CANON.txt");
        Console.WriteLine("Dossier actuel : " + AppDomain.CurrentDomain.BaseDirectory);
        Console.ReadLine();
    }
}
