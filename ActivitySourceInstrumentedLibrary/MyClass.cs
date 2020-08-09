using System;
using System.Diagnostics;
using System.Threading;

namespace ActivitySourceInstrumentedLibrary
{
    public class MyClass
    {
        public const string ActivitySourceName = "MyClass";

        private static readonly ActivitySource ActivitySource = new ActivitySource(ActivitySourceName);

        public void MyMethod()
        {
            Console.WriteLine($"Invoking {nameof(this.MyMethod)}.");

            using (var activity = ActivitySource.StartActivity(nameof(this.MyMethod)))
            {
                activity?.AddTag("CustomTag", "CustomValue");

                // Do something
                Thread.Sleep(100);
            }
        }
    }
}
