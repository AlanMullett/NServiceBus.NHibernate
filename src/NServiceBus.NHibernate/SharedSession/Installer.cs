namespace NServiceBus.Persistence.NHibernate
{
    using System.Collections.Concurrent;
    using global::NHibernate.Cfg;
    using global::NHibernate.Tool.hbm2ddl;
    using Installation;

    class Installer : INeedToInstallSomething
    {
        public static ConcurrentDictionary<string, bool> RunInstaller = new ConcurrentDictionary<string, bool>();

        internal static ConcurrentDictionary<string, Configuration> configuration = new ConcurrentDictionary<string, Configuration>();

        public void Install(string identity, Configure config)
        {
            bool runInstallerValue;
            var localAddress = config.Settings.Get<string>("NServiceBus.LocalAddress");
            if (RunInstaller.TryGetValue(localAddress, out runInstallerValue) && runInstallerValue)
            {
                new SchemaUpdate(configuration[localAddress]).Execute(false, true);
            }
        }
    }
}
