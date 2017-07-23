using Microsoft.Win32;
using System.Collections.Generic;

namespace PuttyUtility
{
    class Controller
    {
        private RegistryAccessor m_registryAccessor;

        public Controller(RegistryAccessor registryAccessor)
        {
            m_registryAccessor = registryAccessor;
        }

        public List<Session> GetSessions()
        {
            List<Session> sessions = new List<Session>();

            RegistryKey sessionsKey = m_registryAccessor.GetSessionsKey();

            string[] sessionNames = sessionsKey.GetSubKeyNames();

            foreach (string sessionName in sessionNames)
            {
                RegistryKey sessionKey = sessionsKey.OpenSubKey(sessionName);

                sessions.Add(new Session(sessionName, sessionKey));
            }

            return sessions;
        }

        public List<HostKey> GetHostKeys()
        {
            List<HostKey> hostKeys = new List<HostKey>();

            RegistryKey hostKeysKey = m_registryAccessor.GetSSHHostKeysKey();

            string[] hostKeyNames = hostKeysKey.GetValueNames();

            foreach (string hostKeyName in hostKeyNames)
            {
                hostKeys.Add(new HostKey(hostKeyName, hostKeysKey.GetValue(hostKeyName)));
            }

            return hostKeys;
        }
    }
}
