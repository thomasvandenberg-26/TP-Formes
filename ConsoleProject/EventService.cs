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

                switch (e.Type)
                {
                    case EventType.Information:
                        CountInfo++;
                        break;
                    case EventType.Alerte:
                        CountAlerte++;
                        break;
                    case EventType.Alarme:
                        CountAlarme++;
                        break;
                }
            }
        }
    }
}
