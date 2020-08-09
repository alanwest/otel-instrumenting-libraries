# OpenTelemetry .NET - Instrumenting libraries

This repository contains two libraries that have been instrumented with the .NET `Activity` class.

1. `LegacyInstrumentedLibrary` is instrumented using `DiagnosticListener`.
    * Separate OpenTelemetry instrumentation needs to be provided for this library and requires a dependency on the OpenTelemetry API.
2. `ActivitySourceInstrumentedLibrary` is instrumented using the new `ActivitySource` API not yet GA.
    * No dependency on OpenTelemetry is required.
    * It takes a dependency on the preview package of [`System.Diagnostics.DiagnosticSource`](https://www.nuget.org/packages/System.Diagnostics.DiagnosticSource/5.0.0-preview.7.20364.11) which exposes the new `ActivitySource` API.

A console application is provided to demonstrate the instrumentation.

Run it:
```
cd ConsoleApp
dotnet run
```