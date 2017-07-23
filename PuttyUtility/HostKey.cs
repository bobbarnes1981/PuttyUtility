namespace PuttyUtility
{
    class HostKey
    {
        public string Name { get; set; }

        public object Key { get; set; }

        public HostKey(string name, object key)
        {
            Name = name;
            Key = key;
        }
    }
}
