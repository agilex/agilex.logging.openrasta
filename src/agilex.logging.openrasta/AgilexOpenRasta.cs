using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.Diagnostics;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace agilex.logging.openrasta
{
    public static class AgilexOpenRasta
    {
        public static void ConfigureLogging(string logfilename, string logname)
        {
            BasicConfigurator.Configure(
                new RollingFileAppender
                    {
                        Layout = new PatternLayout("%date [%thread] %-5level %c - %message%newline"),
                        File = logfilename,
                        AppendToFile = true,
                        Name = logname,
                        RollingStyle = RollingFileAppender.RollingMode.Size,
                        MaximumFileSize = "10MB",
                        MaxSizeRollBackups = 1,
                        StaticLogFileName = true
                    }
                );
            ResourceSpace.Uses.Resolver.AddDependencyInstance<ILog>(
                LogManager.GetLogger(logname), DependencyLifetime.Singleton);
            ResourceSpace.Uses.CustomDependency
                <ILogger, Log4NetLogger>(DependencyLifetime.Singleton);
        }
    }
}