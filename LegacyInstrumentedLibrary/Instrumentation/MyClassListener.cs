using System;
using System.Diagnostics;
using LegacyInstrumentedLibrary;
using OpenTelemetry.Instrumentation;
using OpenTelemetry.Trace;

namespace MyLibrary.Instrumentation
{
    internal class MyClassListener : ListenerHandler
    {
        private readonly ActivitySourceAdapter activitySource;

        public MyClassListener(ActivitySourceAdapter activitySource)
            : base(MyClass.SourceName)
        {
            this.activitySource = activitySource;
        }

        public override void OnStartActivity(Activity activity, object payload)
        {
            Console.WriteLine($"Invoking {nameof(MyClassListener)}.{nameof(this.OnStartActivity)}");

            this.activitySource.Start(activity);

            activity.DisplayName = "MyMethod";

            if (activity.IsAllDataRequested)
            {
                activity.AddTag("CustomTag", "CustomValue");
            }
        }

        public override void OnStopActivity(Activity activity, object payload)
        {
            Console.WriteLine($"Invoking {nameof(MyClassListener)}.{nameof(this.OnStopActivity)}");

            this.activitySource.Stop(activity);
        }

        public override void OnException(Activity activity, object payload)
        {
            Console.WriteLine($"Invoking {nameof(MyClassListener)}.{nameof(this.OnException)}");
        }

        public override void OnCustom(string name, Activity activity, object payload)
        {
            Console.WriteLine($"Invoking {nameof(MyClassListener)}.{nameof(this.OnCustom)}");
        }
    }
}
