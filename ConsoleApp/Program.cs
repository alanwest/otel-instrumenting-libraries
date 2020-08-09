using System;
using System.Diagnostics;
using MyLibrary.Instrumentation;
using OpenTelemetry;
using OpenTelemetry.Trace;

namespace ConsoleApp
{
    public class Program
    {
        private const string ActivitySourceName = "console-app";

        public static void Main(string[] args)
        {
            Console.WriteLine("Running legacy example");
            Console.WriteLine("----------------------");
            RunLegacyExample();

            Console.WriteLine("Running ActivitySource example");
            Console.WriteLine("------------------------------");
            RunActivitySourceExample();
        }

        private static void RunLegacyExample()
        {
            using var openTelemetry = Sdk.CreateTracerProvider(
                (builder) => builder
                    .AddInstrumentation((activitySource) => new MyClassInstrumentation(activitySource))
                    .AddActivitySource(ActivitySourceName)
                    .UseConsoleExporter(opt => opt.DisplayAsJson = false));

            var source = new ActivitySource(ActivitySourceName);

            using (var parent = source.StartActivity("Main"))
            {
                var myClass = new LegacyInstrumentedLibrary.MyClass();
                myClass.MyMethod();
            }
        }

        private static void RunActivitySourceExample()
        {
            using var openTelemetry = Sdk.CreateTracerProvider(
                (builder) => builder
                    .AddActivitySource(ActivitySourceInstrumentedLibrary.MyClass.ActivitySourceName)
                    .AddActivitySource(ActivitySourceName)
                    .UseConsoleExporter(opt => opt.DisplayAsJson = false));

            var source = new ActivitySource(ActivitySourceName);

            using (var parent = source.StartActivity("Main"))
            {
                var myClass = new ActivitySourceInstrumentedLibrary.MyClass();
                myClass.MyMethod();
            }
        }
    }
}
