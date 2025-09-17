using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Rando
{
    public partial class Rando : Form
    {
        private List<Trackpoint> _trackpoints;
        private Bitmap _carte;

        private Color[] gradient = new Color[]
        {
            Color.FromArgb(255, 144, 238, 144), // vert clair
            Color.FromArgb(162, 216, 128),
            Color.FromArgb(180, 194, 112),
            Color.FromArgb(198, 172, 96),
            Color.FromArgb(216, 150, 80),
            Color.FromArgb(234, 128, 64),
            Color.FromArgb(244, 106, 48),
            Color.FromArgb(248,  84, 36),
            Color.FromArgb(252,  62, 24),
            Color.FromArgb(254,  48, 18),
            Color.FromArgb(255,  32, 12),
            Color.FromArgb(255,  16, 6),
            Color.FromArgb(255,   0, 0)   // rouge vif
        };

        public Rando()
        {
            InitializeComponent();

            // Charger l'image de la carte
            _carte = new Bitmap("map.png"); // chemin relatif au dossier bin

            // Lire le fichier GPX
            _trackpoints = GpxHelper.ReadGpx("gemmikandersteg.gpx");

            // Relier l'événement Paint
            this.Paint += Rando_Form_Paint;
        }

        private void Rando_Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Dessiner la carte en arrière-plan
            if (_carte != null)
            {
                g.DrawImage(_carte, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }

            // Dessiner le tracé GPX coloré selon l'altitude
            if (_trackpoints != null && _trackpoints.Count >= 2)
            {
                DrawColoredTrack(g, _trackpoints, this.ClientSize.Width, this.ClientSize.Height);
            }
        }

        private void DrawColoredTrack(Graphics g, List<Trackpoint> trackpoints, int width, int height)
        {
            double minLat = trackpoints.Min(tp => tp.Latitude);
            double maxLat = trackpoints.Max(tp => tp.Latitude);
            double minLon = trackpoints.Min(tp => tp.Longitude);
            double maxLon = trackpoints.Max(tp => tp.Longitude);

            double minEle = trackpoints.Min(tp => tp.Elevation);
            double maxEle = trackpoints.Max(tp => tp.Elevation);

            for (int i = 0; i < trackpoints.Count - 1; i++)
            {
                var tp1 = trackpoints[i];
                var tp2 = trackpoints[i + 1];

                // Transformer les coordonnées GPS en coordonnées écran
                int x1 = (int)((tp1.Longitude - minLon) / (maxLon - minLon) * width);
                int y1 = (int)((maxLat - tp1.Latitude) / (maxLat - minLat) * height);
                int x2 = (int)((tp2.Longitude - minLon) / (maxLon - minLon) * width);
                int y2 = (int)((maxLat - tp2.Latitude) / (maxLat - minLat) * height);

                // Calculer le niveau de couleur selon l'altitude avec échelle dynamique
                int level = (int)((tp1.Elevation - minEle) / (maxEle - minEle) * (gradient.Length - 1));
                if (level < 0) level = 0;
                if (level >= gradient.Length) level = gradient.Length - 1;

                using (Pen pen = new Pen(gradient[level], 2))
                {
                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }
    }
}
