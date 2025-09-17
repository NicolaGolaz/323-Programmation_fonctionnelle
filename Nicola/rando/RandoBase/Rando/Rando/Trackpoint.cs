using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rando
{
    class Trackpoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }

        public Trackpoint(double lat, double lon, double ele)
        {
            Latitude = lat;
            Longitude = lon;
            Elevation = ele;
        }
    }
}
