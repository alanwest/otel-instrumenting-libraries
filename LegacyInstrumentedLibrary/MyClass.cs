using System;
using System.Diagnostics;
using System.Threading;

namespace LegacyInstrumentedLibrary
{
    public class MyClass
    {
        public static readonly string SourceName = $"{nameof(MyClass)}";

        private static readonly string StartActivityName = $"{SourceName}.Start";
        private static readonly string StopActivityName = $"{SourceName}.Stop";

        private readonly DiagnosticListener diagnosticListener;

        public MyClass(DiagnosticListener diagnosticListener)
            => this.diagnosticListener = diagnosticListener;

        public MyClass()
            : this(new DiagnosticListener(SourceName))
        {
        }

        public void MyMethod()
        {
            Console.WriteLine($"Invoking {nameof(this.MyMethod)}.");

            var activity = this.StartActivity(null);

            try
            {
                // Do something
                Thread.Sleep(100);
            }
            finally
            {
                this.StopActivity(activity, null);
            }
        }

        private Activity StartActivity(object payload)
        {
            var activity = new Activity(SourceName);

            this.diagnosticListener.OnActivityExport(activity, payload);

            if (this.diagnosticListener.IsEnabled(StartActivityName, payload))
            {
                this.diagnosticListener.StartActivity(activity, payload);
            }
            else
            {
                activity.Start();
            }

            return activity;
        }

        private void StopActivity(Activity activity, object payload)
        {
            if (this.diagnosticListener.IsEnabled(StopActivityName, payload))
            {
                this.diagnosticListener.StopActivity(activity, payload);
            }
            else
            {
                activity.Stop();
            }
        }
    }
}
