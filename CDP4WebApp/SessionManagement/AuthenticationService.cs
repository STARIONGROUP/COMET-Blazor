// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationService.cs" company="RHEA System S.A.">
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

namespace CDP4WebApp.SessionManagement
{
    using System;
    using System.Threading.Tasks;
    using CDP4Common.SiteDirectoryData;
    using CDP4Dal;
    using CDP4ServicesDal;
    using Microsoft.AspNetCore.Components;

    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// The (injected) <see cref="ISessionAnchor"/> that provides access to the <see cref="ISession"/>
        /// </summary>
        private readonly ISessionAnchor sessionAnchor;

        /// <summary>
        /// The (injected) <see cref="AuthenticationStateProvider"/>
        /// </summary>
        private readonly AuthenticationStateProvider authenticationStateProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="sessionAnchor">
        /// The (injected) <see cref="ISessionAnchor"/> that provides access to the <see cref="ISession"/>
        /// </param>
        /// <param name="authenticationStateProvider">
        /// The (injected) <see cref="AuthenticationStateProvider"/>
        /// </param>
        public AuthenticationService(ISessionAnchor sessionAnchor, AuthenticationStateProvider authenticationStateProvider)
        {
            this.sessionAnchor = sessionAnchor;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Person> Login(string userName, string password)
        {
            var uri = new Uri("http://localhost:5000");
            var credentials = new CDP4Dal.DAL.Credentials(userName, password, uri);
            var dal = new CdpServicesDal();

            var session = new Session(dal, credentials);
            await session.Open();

            this.sessionAnchor.Session = session;

            ((CDP4AuthenticationStateProvider)this.authenticationStateProvider).NotifyAuthenticationStateChanged();

            Console.WriteLine($"user:{session.ActivePerson.Name}");

            return session.ActivePerson;
        }

        public async Task LogOut() 
        {
            if (this.sessionAnchor.Session != null)
            {
                await this.sessionAnchor.Session.Close();
                this.sessionAnchor.Session = null;
            }

            ((CDP4AuthenticationStateProvider)this.authenticationStateProvider).NotifyAuthenticationStateChanged();
        }
    }
}