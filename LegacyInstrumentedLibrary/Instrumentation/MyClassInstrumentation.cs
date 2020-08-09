using System;
using OpenTelemetry.Instrumentation;
using OpenTelemetry.Trace;

namespace MyLibrary.Instrumentation
{
    public class MyClassInstrumentation : IDisposable
    {
        private readonly DiagnosticSourceSubscriber diagnosticSourceSubscriber;

        public MyClassInstrumentation(ActivitySourceAdapter activitySource)
        {
            this.diagnosticSourceSubscriber = new DiagnosticSourceSubscriber(new MyClassListener(activitySource), null);
            this.diagnosticSourceSubscriber.Subscribe();
        }

        public void Dispose()
        {
            this.diagnosticSourceSubscriber?.Dispose();
        }
    }
}
