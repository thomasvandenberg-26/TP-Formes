
using System.Runtime.InteropServices;

namespace nsFigures
{
    public sealed class LogEvents

    {
        public static readonly Lazy<LogEvents> _instance = new Lazy<LogEvents>(() => new LogEvents());

        public static LogEvents Instance => _instance.Value;

        private LogEvents()
        {

        }
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
    }
}
