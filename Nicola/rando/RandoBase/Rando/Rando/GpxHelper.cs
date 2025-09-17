using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Rando
{
    static class GpxHelper
    {
        public static List<Trackpoint> ReadGpx(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            XNamespace ns = doc.Root.Name.Namespace; // GPX namespace

            var points = doc.Descendants(ns + "trkpt")
                            .Select(x => new Trackpoint(
                                double.Parse(x.Attribute("lat").Value),
                                double.Parse(x.Attribute("lon").Value),
                                x.Element(ns + "ele") != null ? double.Parse(x.Element(ns + "ele").Value) : 0
                            ))
                            .ToList();

            return points;
        }

       
    }
}
