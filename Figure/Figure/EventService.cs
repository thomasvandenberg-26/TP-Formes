using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static nsFigures.LogEvents;

namespace nsFigures
{
    public class EventService
    {

        private const int MAX_EVENTS = 1000;

        private readonly object _lock = new object();

        public int CountInfo { get;  set; }
        public int CountAlerte { get;  set; }
        public int CountAlarme { get; set; }

        public int CountPerdus { get; set; }

        //Taille maximum de la collection d'evenements
        public int TailleMaximumEvents = 1000;
        // Collection d'evenements
        public List<Event> Events = new List<Event>();  

        // J'ajoute l'evenement dans la collection _events


        public List<Event> GetAndClearEvents()
        {
            List<Event> eventsCopy;

            lock (_lock)
            {
                eventsCopy = new List<Event>(Events);
                Events.Clear();
            }
            return eventsCopy;
        }
        public void pushEvent(Event e)
        {
            lock (_lock)
            {
               

                // Suppresion des anciens evenements si la taille du dictionnaire depasse 1000

                if (Events.Count >= 1000)
                {
                    Events.RemoveRange(0, 800);
                    CountPerdus += 800;
                }
                switch (e.Type)


                {
                    // j'ajoute dans mon dictionnaire le type d'evenement et le nombre d'evenements de ce type

                    
                    case EventType.Information:
                        Events.Add(e);
                        CountInfo++;
                        break;
                    case EventType.Alerte:
                        Events.Add(e);
                        CountAlerte++;
                        break;
                    case EventType.Alarme:
                        Events.Add(e);
                        CountAlarme++;
                        break;
                }
                
            }
        }
        // A la fin du processus j'imprime dans un fichier text le contenu du dictionnaire
        public void ImpressionCompteParTypeEvenement()
        {
            String logMessage = $"Evenement d'information : {CountInfo}" +
                $" \n Evenement Alerte : {CountAlerte} " +
                $"\n Evenement Alarme : {CountAlarme} " +
                $"\n Evenement Perdus : {CountPerdus} " ;
            File.WriteAllText("DictLog.txt", logMessage + Environment.NewLine);
        }


    }
}
