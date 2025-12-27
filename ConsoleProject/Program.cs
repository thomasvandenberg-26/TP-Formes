using System;
using System.Drawing;
using System.IO;
using System.Threading;
using nsFigures;
using ConsoleProject;
class Program
{
    static void TestConsumerConsole(Event e)
       => Console.WriteLine($"[CONSOLE] {e.Type} - {e.Message}");

    static void TestConsumerFile(Event e)
        => File.AppendAllText("EventsLog_TEST.txt", $"{e.Type} - {e.Message}{Environment.NewLine}");

    static void TestConsumerThrows(Event e)
    {
        if (e.Type == EventType.Alerte)
            throw new Exception("BOOM (test consumer)");
    }

    static void Main()
    {
        // Nettoyage
        //Console.WriteLine("=== TEST 0: Abonnement consumers ===");
        //LogEvents.Instance.Subscribe = TestConsumerConsole;
        //LogEvents.Instance.Subscribe = TestConsumerFile;
        //Console.WriteLine("OK: consumers abonnés.\n");

        //Console.WriteLine("=== TEST 1: Push 3 events (Info/Alerte/Alarme) ===");
        //LogEvents.Instance.PushEvent(new Event(EventType.Information, "T1 - Info"));
        //LogEvents.Instance.PushEvent(new Event(EventType.Alerte, "T1 - Alerte"));
        //LogEvents.Instance.PushEvent(new Event(EventType.Alarme, "T1 - Alarme"));
        //Thread.Sleep(4200);

        //Console.WriteLine("\n=== TEST FIGURES: création + dessin ===");
        // Création du support

        //clsFigures.SupportDessin = new SupportImprimante_Canon();
        //clsFigures.SupportDessin = new SupportImprimante_Canon();
        // Création du dessin

        Dessin DessindeThomas = new Dessin("Dessin de Thomas du 27", 1.0f);



        // Oui les constucteurs ne sont très propres mais je dois avancer 
        clsCarre cr1 = new clsCarre(new Point(1,1), Color.Aquamarine,"CarreAM", 10);

        clsRectangle rect1 = new clsRectangle(new Point(5,5), Color.Brown, 20, 10, "RectBrown");

        clsCube cb1 = new clsCube(new Point(10,10), Color.Coral, "CubeCoral", 15);

        DessindeThomas.Ajouter_Figure(cr1);
        DessindeThomas.Ajouter_Figure(rect1);
        DessindeThomas.Ajouter_Figure(cb1);
        try
        {
            DessindeThomas.SaveToJson("dessindethomas.json");
        } 
        catch (Exception ex)
        {
            LogEvents.Instance.PushEvent(new Event(EventType.Alerte, $"Erreur lors de la sauvegarde du dessin: {ex.Message}, {DateTime.Now}"));
        }
        //DessindeThomas.DessinerFigures();

        //Console.WriteLine("Attends ~4 secondes pour laisser le timer flusher...");
        //Thread.Sleep(4200);

        //Console.WriteLine("\n=== TEST 3: Unsubscribe du consumer console ===");
        //LogEvents.Instance.Unsubscribe = TestConsumerConsole;
        //LogEvents.Instance.PushEvent(new Event(EventType.Information, "T3 - Info après Unsubscribe Console"));
        //Thread.Sleep(4200);

        //Console.WriteLine("\n=== TEST 4: Consumer qui jette une exception (Flush doit survivre) ===");
        //LogEvents.Instance.Subscribe = TestConsumerThrows;
        //LogEvents.Instance.PushEvent(new Event(EventType.Information, "T4 - Info (OK)"));
        //LogEvents.Instance.PushEvent(new Event(EventType.Alerte, "T4 - Alerte (DOIT déclencher exception)"));
        //LogEvents.Instance.PushEvent(new Event(EventType.Alarme, "T4 - Alarme (OK)"));
        //Thread.Sleep(4200);

        //Console.WriteLine("\n=== FIN ===");
        //Console.WriteLine("Ouvre 'Dessin_CANON.txt' dans bin/Debug/netX.Y/");
        //Console.WriteLine("Appuie sur Entrée pour quitter.");
        //Console.ReadLine();
        // Thread.Sleep(3500);

    }
}
