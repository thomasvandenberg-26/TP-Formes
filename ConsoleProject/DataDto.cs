using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsFigures
{
    public class DessinDto
    {
        public string Nom { get; set; } = "";
        public float Version { get; set; }
        public DateTime DateCreation { get; set; }
        public List<FigureDto> Figures { get; set; } = new();
    }
    public class FigureDto
    {
        public string Type { get; set; } = "";  // "clsRectangle", "clsCube", "clsCercle"
        public string Nom { get; set; } = "";

        public int DepartX { get; set; }
        public int DepartY { get; set; }

        public string Couleur{ get; set; }

        // Spécifiques
        public ushort Largeur { get; set; }
        public ushort Hauteur { get; set; }
        public ushort Rayon { get; set; }
        public ushort Profondeur { get; set; }
    }
}
