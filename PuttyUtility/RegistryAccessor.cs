using Microsoft.Win32;

namespace PuttyUtility
{
    class RegistryAccessor
    {
        private const string REG_KEY_SESSIONS = "Sessions";
        private const string REG_KEY_SOFTWARE = "Software";
        private const string REG_KEY_SIMON = "SimonTatham";
        private const string REG_KEY_PUTTY = "PuTTY";
        private const string REG_KEY_SSHHOSTKEYS = "SshHostKeys";

        public RegistryKey GetSessionsKey()
        {
            RegistryKey puttyKey = getPuTTYKey();

            RegistryKey sessionsKey = null;

            if (puttyKey != null)
            {
                sessionsKey = puttyKey.OpenSubKey(REG_KEY_SESSIONS);
            }

            return sessionsKey;
        }

        public RegistryKey GetSSHHostKeysKey()
        {
            RegistryKey puttyKey = getPuTTYKey();

            RegistryKey sshHostKeysKey = null;

            if (puttyKey != null)
            {
                sshHostKeysKey = puttyKey.OpenSubKey(REG_KEY_SSHHOSTKEYS);
            }

            return sshHostKeysKey;
        }

        private RegistryKey getSoftwareKey()
        {
            return Registry.CurrentUser.OpenSubKey(REG_KEY_SOFTWARE);
        }

        private RegistryKey getSimonTathamKey()
        {
            RegistryKey softwareKey = getSoftwareKey();

            RegistryKey stKey = null;

            if (softwareKey != null)
            {
                stKey = softwareKey.OpenSubKey(REG_KEY_SIMON);
            }

            return stKey;
        }

        private RegistryKey getPuTTYKey()
        {
            RegistryKey stKey = getSimonTathamKey();

            RegistryKey puttyKey = null;

            if (stKey != null)
            {
                puttyKey = stKey.OpenSubKey(REG_KEY_PUTTY);
            }

            return puttyKey;
        }
    }
}
