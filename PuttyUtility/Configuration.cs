namespace PuttyUtility
{
    public class Configuration
    {
        public string PuttyPath { get; set; }

        public ConfigNode Sessions { get; set; }

        public static Configuration Default
        {
            get
            {
                return new Configuration
                {
                    PuttyPath = "C:\\Program Files (x86)\\PuTTY\\putty.exe",
                    Sessions = new ConfigNode { Name = "Sessions", Type = ConfigNodeType.Folder }
                };
            }
        }
    }
}
