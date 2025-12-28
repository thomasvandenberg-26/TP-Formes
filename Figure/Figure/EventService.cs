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

        private readonly object _lock = new object();

        public int CountInfo { get;  set; }
        public int CountAlerte { get;  set; }
        public int CountAlarme { get; set; }

        public int CountPerdus { get; set; }

        // Collection d'evenements
        public Queue< Event> _events = new Queue<Event>();  

        // J'ajoute l'evenement dans la collection _events


        public List<Event> GetAndClearEvents()
        {
           

            lock (_lock)
            {
              var copy = _events.ToList();
                _events.Clear();
                return copy;
            }
          
        }
        public void pushEvent(Event e)
        {
            lock (_lock)
            {

                _events.Enqueue(e);

                if (_events.Count >= MAX_EVENTS)
                { 
                     _events.Dequeue();
                    CountPerdus++;
                }
                switch (e.Type)

                { 
                    // Incremente le compteur selon le type d'evenement
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
        // A la fin du processus j'imprime dans un fichier text le contenu du dictionnaire

    }
}
