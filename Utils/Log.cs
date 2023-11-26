namespace Avto1Test.Utils
{
    public class Log
    {
       public static ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    }
}
