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

            File.AppendAllText("ThomasLog.txt", line + Environment.NewLine);
        }



        public void PushEvent(Event e)
        {
            Service.pushEvent(e);
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
