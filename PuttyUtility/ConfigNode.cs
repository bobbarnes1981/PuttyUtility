using System.Collections.Generic;

namespace PuttyUtility
{
    public class ConfigNode
    {
        public string Name { get; set; }

        public ConfigNodeType Type { get; set; }

        public List<ConfigNode> Nodes { get; set; }

        public ConfigNode()
        {
            Nodes = new List<ConfigNode>();
        }
    }
}
