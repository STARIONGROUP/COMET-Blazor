// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="RHEA System S.A.">
//    
//  Copyright (c) 2019-2020 RHEA System S.A.
//
//  Author: Sam Gerené
//
//  This file is part of CDP4-Web-Application
//
//  CDP4-Web-Application is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License.
//
//  CDP4-Web-Application is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with CDP4-Web-Application. If not, see<https://www.gnu.org/licenses/>.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CDP4WebApp
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using CDP4Dal;
    using CDP4WebApp.SessionManagement;

    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Radzen;

    /// <summary>
    /// Main entry point of the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the application
        /// </summary>
        /// <param name="args">
        /// not used
        /// </param>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();

            builder.Services.AddTransient(sp => new HttpClient());

            builder.Services.AddSingleton<ISessionAnchor, SessionAnchor>();
            builder.Services.AddSingleton<ISession, Session>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<AuthenticationStateProvider, CDP4AuthenticationStateProvider>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

            builder.RootComponents.Add<App>("app");

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}