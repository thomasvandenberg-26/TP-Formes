using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using nsFigures;
namespace TP_Formes_WPF
{
    /// <summary>
    /// Logique d'interaction pour LogEventsWindow.xaml
    /// </summary>
    public partial class LogEventsWindow : Window
    {
        public ObservableCollection<EventViewModel> Events { get; } = new();
        public LogEventsWindow()
        {
            InitializeComponent();
            DataContext = this;

            LogEvents.Instance.Subscribe = OnEventReceived;
        }

        private void OnEventReceived(Event e)
        {
            // IMPORTANT : Flush arrive depuis un thread Timer -> il faut dispatcher vers le thread UI
            Dispatcher.Invoke(() =>
            {
                Events.Add(new EventViewModel
                {
                    Time = DateTime.Now,               // ou e.Date si tu l’as
                    Type = e.Type.ToString(),
                    Message = e.Message
                });

                // plafonner l’affichage (évite de remplir la mémoire de l’UI)
                if (Events.Count > 500)
                    Events.RemoveAt(0);
            });

            Dessin dessin = JsonSerializer.Deserialize(e.Message, typeof(Dessin)) as Dessin;
        }

        protected override void OnClosed(EventArgs e)
        {
            // désinscription propre
            LogEvents.Instance.Unsubscribe = OnEventReceived;
            base.OnClosed(e);
        }

    }


   

    }
