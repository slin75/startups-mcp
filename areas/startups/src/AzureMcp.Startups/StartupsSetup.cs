// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Startups.Commands;
using AzureMcp.Startups.Commands.Guidance;
using AzureMcp.Startups.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Startups
{
    public class StartupsSetup : IAreaSetup
    {
        public string Name => "startups";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStartupsService, StartupsService>();
        }

        public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
        {
            var startupsGroup = new CommandGroup("startups", "Interact with Microsoft for Startups through " +
            "two commands: guidance command to learn about what the program offers and deploy command to " +
            "deploy static web resources and web apps to a storage account in the Azure portal");
            rootGroup.AddSubGroup(startupsGroup);

            // Register guidance command
            startupsGroup.AddCommand("guidance", new StartupsGuidanceCommand(loggerFactory.CreateLogger<StartupsGuidanceCommand>()));

            // Register deploy command
            startupsGroup.AddCommand("deploy", new StartupsDeployCommand(loggerFactory.CreateLogger<StartupsDeployCommand>()));
        }
    }
}
