namespace Avto1Test.Utils
{
    public class Log
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
#pragma warning restore CA2211 // Non-constant fields should not be visible
    }
}
