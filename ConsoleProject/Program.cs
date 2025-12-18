using System;
using System.IO;
using System.Threading;
using nsFigures;

class Program
{
    // Consumer de test : console
    static void TestConsumerConsole(Event e)
    {
        // Affichage simple (sans date pour éviter les différences)
        Console.WriteLine($"[CONSOLE] {e.Type} - {e.Message}");
    }

    // Consumer de test : fichier
    static void TestConsumerFile(Event e)
    {
        File.AppendAllText("EventsLog_TEST.txt", $"{e.Type} - {e.Message}{Environment.NewLine}");
    }

    // Consumer volontairement "cassé" pour tester que Flush ne plante pas
    static void TestConsumerThrows(Event e)
    {
        if (e.Type == EventType.Alerte)
            throw new Exception("BOOM (test)");
    }

    static void Main()
    {
        // Nettoyage des fichiers de test
        if (File.Exists("EventsLog_TEST.txt")) File.Delete("EventsLog_TEST.txt");
        if (File.Exists("DictLog.txt")) File.Delete("DictLog.txt");

        Console.WriteLine("=== TEST 0: Abonnement consumers ===");
        LogEvents.Instance.Subscribe = TestConsumerConsole;
        LogEvents.Instance.Subscribe = TestConsumerFile;

        Console.WriteLine("OK: consumers abonnés.\n");

        Console.WriteLine("=== TEST 1: Push 3 events (Info/Alerte/Alarme) ===");
        LogEvents.Instance.PushEvent(new Event(EventType.Information, "T1 - Info"));
        LogEvents.Instance.PushEvent(new Event(EventType.Alerte, "T1 - Alerte"));
        LogEvents.Instance.PushEvent(new Event(EventType.Alarme, "T1 - Alarme"));

        Console.WriteLine("Events envoyés. Attends ~4 secondes pour laisser le timer flusher...");
        Thread.Sleep(4200);

        Console.WriteLine("\n=== TEST 2: Vérifier que Flush a bien vidé (on attend encore 4s sans push) ===");
        Console.WriteLine("Si ton Flush est correct, il ne doit rien réafficher, ni réécrire dans le fichier.");
        Thread.Sleep(4200);

        Console.WriteLine("\n=== TEST 3: Unsubscribe du consumer console ===");
        LogEvents.Instance.Unsubscribe = TestConsumerConsole;

        LogEvents.Instance.PushEvent(new Event(EventType.Information, "T3 - Info après Unsubscribe Console"));
        Console.WriteLine("Attends ~4 secondes : tu dois avoir une ligne dans le fichier, mais PAS dans la console.");
        Thread.Sleep(4200);

        Console.WriteLine("\n=== TEST 4: Consumer qui jette une exception (Flush doit survivre) ===");
        LogEvents.Instance.Subscribe = TestConsumerThrows;

        LogEvents.Instance.PushEvent(new Event(EventType.Information, "T4 - Info (OK)"));
        LogEvents.Instance.PushEvent(new Event(EventType.Alerte, "T4 - Alerte (DOIT déclencher exception)"));
        LogEvents.Instance.PushEvent(new Event(EventType.Alarme, "T4 - Alarme (OK)"));

        Console.WriteLine("Attends ~4 secondes : ton appli NE doit PAS crash. Tu peux voir un message d'erreur géré.");
        Thread.Sleep(4200);

        Console.WriteLine("\n=== TEST 5: Monitoring compteurs (DictLog) ===");
        // si ta méthode s'appelle comme ça et est accessible:
        // LogEvents.Instance.Service.ImpressionCompteParTypeEvenement();  // souvent impossible (Service internal)
        // => donc idéalement, expose une méthode publique dans LogEvents:
        // LogEvents.Instance.WriteMonitoring();
        //
        // Si tu n'as pas encore cette méthode, ignore ce test.

        Console.WriteLine("\n=== FIN ===");
        Console.WriteLine("Ouvre 'EventsLog_TEST.txt' pour vérifier les lignes écrites.");
        Console.WriteLine("Appuie sur Entrée pour quitter.");
        Console.ReadLine();
    }
}
