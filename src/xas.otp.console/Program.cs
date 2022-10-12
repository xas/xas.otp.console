using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xas.Otp.Console;

// Configure
IConfiguration Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

OtpSettings otpSettings = Configuration.GetRequiredSection("OtpSettings").Get<OtpSettings>();

Style redStyle = new Style(foreground: Color.Red);
Style grenStyle = new Style(foreground: Color.Green);

CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    cts.Cancel();
    e.Cancel = true;
};

await AnsiConsole
    .Progress()
    .Columns(new ProgressColumn[]
    {
        new TaskDescriptionColumn(),    // Task description
        new ProgressBarColumn() { CompletedStyle = redStyle, RemainingStyle = grenStyle, Width = 30 },        // Progress bar
        new PercentageColumn(),         // Percentage
        new RemainingTimeColumn(),      // Remaining time
        new SpinnerColumn(Spinner.Known.Clock),            // Spinner
    })
    .StartAsync(async ctx =>
    {
        List<OtpTask> tasks = new List<OtpTask>();
        // Define tasks
        foreach (OtpSetting setting in otpSettings.Settings)
        {
            OtpTask task = new OtpTask(setting);
            task.Initialize(ctx);
            tasks.Add(task);
        }

        while (!cts.IsCancellationRequested)
        {
            tasks.ForEach(x => x.Increment());
            await Task.Delay(1000);
        }
    });
