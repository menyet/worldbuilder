namespace Editor.OpenStreetMapImporter
{
    using System.Collections.Generic;

    using Editor.StreetsEditor;

    public class Way
    {
        public string Id { get; set; }

        public List<string> Nodes { get; set; }

        public Street Street { get; set; }
    }
}