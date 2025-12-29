using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsFigures
{
    // Classe représentant un événement dans le système de journalisation

    public class Event
        {
            public EventType Type { get; }
            public string Message { get; }
            public DateTime Timestamp { get; }

            public Event(EventType type, string message)
            {
                Type = type;
                Message = message;
                Timestamp = DateTime.Now;
            }
        }
}

