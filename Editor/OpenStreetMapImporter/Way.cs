namespace Editor.OpenStreetMapImporter
{
    using System.Collections.Generic;

    class Way
    {
        public string Id { get; set; }

        public List<string> Nodes { get; set; }
    }
}