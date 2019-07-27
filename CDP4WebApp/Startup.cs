// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="RHEA System S.A.">
//    
//  Copyright (c) 2019 RHEA System S.A.
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
    using CDP4WebApp.SessionManagement;
    using Microsoft.AspNetCore.Components.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The purpose of the <see cref="Startup"/> is to configure the web application
    /// and to setup dependency injection
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Setup dependency injection
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISessionAnchor, SessionAnchor>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}