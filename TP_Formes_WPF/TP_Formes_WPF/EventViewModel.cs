using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Formes_WPF
{
    public class EventViewModel
    {
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public override string ToString()
            => $"[{Time:HH:mm:ss}] {Type} - {Message}";

    };
}
