using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static nsFigures.LogEvents;

namespace nsFigures
{
    internal class EventService
    {

        private const int MAX_EVENTS = 1000;

        private readonly Queue<Event> _events = new Queue<Event>();

        private readonly object _lock = new object();

        public int CountInfo { get; private set; }
        public int CountAlerte { get; private set; }
        public int CountAlarme { get; private set; }

        public int CountPerdus { get; private set; }

        //Taille maximum de la collection d'evenements
        public int TailleMaximumEvents = 1000;
        // Dictionnaire retournant le nombre d'evenements par type



        //public Dictionary<EventType, int> GetEventCounts()
        //{
        //    lock (_lock)
        //    {
        //        return new Dictionary<EventType, int>
        //        {
        //            { EventType.Information, CountInfo },
        //            { EventType.Alerte, CountAlerte },
        //            { EventType.Alarme, CountAlarme }
        //        };
        //    }
        //}

        public Dictionary<EventType, int> EventsDict { get; set; } = new Dictionary<EventType, int>();

     

        // J'ajoute l'evenement dans la collection _events

        public void pushEvent(Event e)
        {
            lock (_lock)
            {
                if (_events.Count >= MAX_EVENTS)
                {
                    CountPerdus++;
                    return;
                }

                _events.Enqueue(e);

                // Suppresion des anciens evenements si la taille du dictionnaire depasse 1000

                if (EventsDict.Count >= 1000)
                {

                    for (int i = 0; i < 800; i++)
                    {
                        EventsDict.Remove((EventType)i);
                    }
                }
                switch (e.Type)


                {
                    // j'ajoute dans mon dictionnaire le type d'evenement et le nombre d'evenements de ce type


                    case EventType.Information:

                        EventsDict.Add(e.Type, CountInfo++);
                        break;
                    case EventType.Alerte:
                        EventsDict.Add(e.Type, CountAlerte++);
                        break;
                    case EventType.Alarme:
                        EventsDict.Add(e.Type, CountAlarme++);
                        break;
                }
                
            }
        }
        // A la fin du processus j'imprime dans un fichier text le contenu du dictionnaire
        public void ImpressionDict()
        {
            foreach(var kvp in EventsDict)
            {
                String logMessage = $"Type d'evenement: {kvp.Key}, Nombre d'evenements: {kvp.Value}";
                System.IO.File.AppendAllText("DictLog.txt", logMessage + Environment.NewLine);
            }
        }


    }
}
