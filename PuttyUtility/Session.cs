using Microsoft.Win32;
using System.Collections.Generic;

namespace PuttyUtility
{
    class Session
    {
        public string Name { get; set; }

        /// <summary>
        /// temporary
        /// </summary>
        public Dictionary<string, object> Data { get; set; }

        public Session(string name, RegistryKey registryKey)
        {
            Name = name;

            Data = new Dictionary<string, object>();

            string[] valueNames = registryKey.GetValueNames();
            foreach (string valueName in valueNames)
            {
                Data.Add(valueName, registryKey.GetValue(valueName));
            }
        }
    }
}
