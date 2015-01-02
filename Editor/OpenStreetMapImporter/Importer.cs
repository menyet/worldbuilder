﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Editor.OpenStreetMapImporter
{
    using System.Globalization;
    using System.Xml.Linq;

    using Editor.StreetsEditor;

    class Importer
    {
        const double OffsetX = 17.21;
        const double OffsetY = 48.12;

        const double Scale = 50000;

        public Map Import(string filePath)
        {

            var nodes = new Dictionary<string, Tuple<Node, Point>>();


            var ways = new List<Way>();

            XElement xelement = XElement.Load(filePath);
            IEnumerable<XElement> employees = xelement.Elements("node");
            // Read the entire XML
            foreach (var employee in employees)
            {
                    var idElement = employee.Attribute("id");
                    var lonElement = employee.Attribute("lon");
                    var latElement = employee.Attribute("lat");

                    var longitude = double.Parse(lonElement.Value, CultureInfo.InvariantCulture);
                    var latitude = double.Parse(latElement.Value, CultureInfo.InvariantCulture);
                    
                    var n = new Node()
                    {
                        Id = idElement.Value,
                        Longitude = longitude,
                        Latitude = latitude,
                    };


                var point = new Point((n.Longitude - OffsetX)*Scale, (n.Latitude - OffsetY)*Scale, 0.0);

                nodes.Add(idElement.Value, new Tuple<Node, Point>(n, point));

                //Console.WriteLine(n.Id);
                //Console.WriteLine(employee);
            }


            IEnumerable<XElement> wayNodes = xelement.Elements("way");
            // Read the entire XML
            foreach (var way in wayNodes)
            {
                var wayId = way.Attribute("id").Value;

                var nds = way.Elements("nd");

                var w = new Way()
                {
                    Id = wayId,
                    
                    Nodes = nds.Select(nd => nd.Attribute("ref").Value).ToList()
                };

                //Console.WriteLine(w.Id);


                ways.Add(w);

                //Console.WriteLine(employee);
            }


            var map = new Map();

            

            foreach (var way in ways.Take(100))
            {
                string lastNodeId = null;

                foreach (var node in way.Nodes)
                {
                    if (lastNodeId != null)
                    {
                        map.StreetList.Add(
                            new Street(
                                nodes[lastNodeId].Item2,
                                nodes[node].Item2,
                                "XYZ"));
                    }

                    lastNodeId = node;

                }
            }

            return map;
        }
    }
}
