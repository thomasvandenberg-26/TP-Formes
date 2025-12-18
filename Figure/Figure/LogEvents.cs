using System.Timers;

using nsFigures;

namespace nsFigures
{
    public class LogEvents

    {
        public static readonly Lazy<LogEvents> _instance = new Lazy<LogEvents>(() => new LogEvents());

        public static LogEvents Instance => _instance.Value;
        internal EventService Service { get; } = new EventService();

        private static EventConsumer? _consumers;
        private static readonly object _lockConsumers = new object();

        public enum TypeEvenement
        {
            CREATION_FIGURE,
            CREATION_DESSIN,
            CREATION_COULEUR,
            SUPPRESSION_FIGURE,
            DESSIN_FIGURE,
            DESSIN_LISTE_FIGURE

        }
        public enum events
        {
            NOTIFICATIONS,
            MINEUR,
            MAJEUR
        }

        private readonly System.Timers.Timer _timer;
        private LogEvents()
        {
            _timer = new System.Timers.Timer(3000); // 3000 ms = 3 secondes
            _timer.Elapsed += (s, e) => Flush();
            _timer.AutoReset = true;   // répéter automatiquement
            _timer.Start();
        }
        public EventConsumer Subscribe
        {
            set
            {
                lock (_lockConsumers)
                {
                    _consumers += value;
                }
            }
        }
        public EventConsumer Unsubscribe
        {
            set
            {
                lock (_lockConsumers)
                {
                    _consumers -= value;
                }
            }
        }
        public delegate void EventConsumer(Event e);

        public static void ConsumerConsole(Event e)
        {
            Console.WriteLine(
                $"[{DateTime.Now:HH:mm:ss}] {e.Type} - {e.Message}"
            );
        }

        public static void ConsumerFile(Event e)
        {
            string line =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {e.Type} - {e.Message}";

            File.AppendAllText("EventsLog.txt", line + Environment.NewLine);
        }



        public void PushEvent(Event e)
        {
            Service.pushEvent(e);
        }
        public void Push(string nom, TypeEvenement typeEvenement)


        // Pour préciser de quel type est l'évènement je passe un TypeEvenement en paramètre et le nom de l'objet crée
        {
            DateTime now = DateTime.Now;
            string logMessage = $"[{now}] Création d'un/e {typeEvenement} :[{nom} [Gravité: {events.NOTIFICATIONS}] Opération réussie.";
            System.IO.File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }


        public void Push(string nom, TypeEvenement typeEvenement, string DescriptionErreur)
        {
            DateTime now = DateTime.Now;
            string logMessage = $"[{now}] [Gravité: {events.MINEUR}] {DescriptionErreur}";
            System.IO.File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }

        public void Push(string nom, TypeEvenement typeEvenement, Exception exception, string DescriptionErreur)
        {
            DateTime now = DateTime.Now;
            string logMessage = $"[{now}] {nom} [Gravité: {events.MAJEUR}] {DescriptionErreur} - Exception: {exception}";
            System.IO.File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }

        public void Push(string nom, TypeEvenement typeEvenement, FormatException fe, string DescriptionErreur)
        {
            DateTime now = DateTime.Now;
            string logMessage = $"[{now}] [Gravité: {events.MAJEUR}] {DescriptionErreur} - Exception: {fe.Message}";
            System.IO.File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }
        public void Push(string nom, TypeEvenement typeEvenement, System.NullReferenceException nre, string DescriptionErreur)
        {
            DateTime now = DateTime.Now;
            string logMessage = $"[{now}] [Gravité: {events.MAJEUR}] {DescriptionErreur} - Exception: {nre.Message}";
            System.IO.File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }


        public void Push(string nom, TypeEvenement typeEvenement, System.ArgumentException ae, string DescriptionErreur)
        {
            DateTime now = DateTime.Now;

            string logMessage = $"[{now}] {nom} [Gravité: {events.MAJEUR}] {DescriptionErreur} - Exception: {ae.Message}";

            System.IO.File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }

        public void Push(string nom, TypeEvenement typeEvenement, System.OverflowException oe, string DescriptionErreur)
        {
            DateTime now = DateTime.Now;
            string logMessage = $"[{now}] [Gravité: {events.MAJEUR}] {DescriptionErreur} - Exception: {oe.Message}";
            System.IO.File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }

        public void Flush()
        {
            var events = Service.GetAndClearEvents();

            // S’il n’y a rien à traiter, on sort
            if (events.Count == 0)
                return;

            EventConsumer? consumersCopy;
            lock (_lockConsumers)
            {
                consumersCopy = _consumers;
            }

            if (consumersCopy == null)
                return;

            foreach (var ev in events)
            {
                try
                {
                    consumersCopy(ev);
                }
                catch (Exception ex)
                {
                    // Gérer les exceptions lancées par les consommateurs
                    Console.WriteLine($"Erreur lors du traitement de l'événement : {ex.Message}");
                    Service.CountPerdus++;
                }
            }


        }
    }
}
