using nsFigures;
namespace TP_Formes_WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            SupportWF support = new SupportWF(e.Graphics);
            clsFigures.SupportDessin = support;
            var dessin = new Dessin("Premier Dessin de Thomas", 1.0f);

         

            dessin.Ajouter_Figure(new clsRectangle(new Point(10, 10), Color.DarkOrange, 10, 20, "RectangleBleu"));
            dessin.Ajouter_Figure(new clsCube(
                new Point(200, 50), Color.Bisque, "C1", 60));

            dessin.DessinerFigures();
        }
    }
}
