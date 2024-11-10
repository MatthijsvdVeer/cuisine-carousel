using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace CuisineCarousel.Observability;

using Azure.Monitor.OpenTelemetry.AspNetCore;

public static class MonitoringExtensions
{
    public static IHostApplicationBuilder AddOpenTelemetry(this IHostApplicationBuilder builder, string serviceName)
    {
        _ = builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
        });
        
        _ = builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService(serviceName: serviceName,
                    serviceNamespace: "demo",
                    serviceVersion: "1.0",
                    autoGenerateServiceInstanceId: false,
                    serviceInstanceId: serviceName);
            })
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddMeter("Microsoft.SemanticKernel*");
            }).WithTracing(tracing =>
            {
                _ = tracing.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSource("Microsoft.SemanticKernel*")
                    .AddSource();
            });
            
        _ = builder.AddOpenTelemetryExporters();
        
        return builder;
    }
    
    private static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);
        if (useOtlpExporter)
        {
            builder.Services.AddOpenTelemetry()
                .UseOtlpExporter();
        }

        if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
        {
            builder.Services.AddOpenTelemetry()
               .UseAzureMonitor();
        }

        return builder;
    }
}