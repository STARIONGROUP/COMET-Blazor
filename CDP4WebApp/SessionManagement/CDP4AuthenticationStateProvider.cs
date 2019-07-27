// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CDP4AuthenticationStateProvider.cs" company="RHEA System S.A.">
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
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CDP4Common.SiteDirectoryData;
    using CDP4Dal;
    using Microsoft.AspNetCore.Components;

    public class CDP4AuthenticationStateProvider : AuthenticationStateProvider
    {
        /// <summary>
        /// The <see cref="ISessionAnchor"/> used to get access to the <see cref="ISession"/>
        /// </summary>
        private readonly ISessionAnchor sessionAnchor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CDP4AuthenticationStateProvider"/>
        /// </summary>
        /// <param name="sessionAnchor">
        /// The (injected) <see cref="ISessionAnchor"/> used to get access to the <see cref="ISession"/>
        /// </param>
        public CDP4AuthenticationStateProvider(ISessionAnchor sessionAnchor)
        { 
            this.sessionAnchor = sessionAnchor;
        }

        /// <summary>
        /// Asynchronously gets an <see cref="AuthenticationState"/> that describes the current user.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that, when resolved, gives an <see cref="AuthenticationState"/> instance that describes the current user.
        /// </returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;

            if (this.sessionAnchor.Session == null || this.sessionAnchor.Session.Credentials == null)
            {
                identity = new ClaimsIdentity();
            }
            else
            {
                var person = this.sessionAnchor.Session.ActivePerson;

                identity = this.CreateClaimsIdentity(person);
            }

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        /// <summary>
        /// Creates a <see cref="ClaimsIdentity"/> based on a <see cref="Person"/>
        /// </summary>
        /// <param name="person">
        /// The <see cref="Person"/> on the basis of which the <see cref="ClaimsIdentity"/> is created
        /// </param>
        /// <returns>
        /// an instance of <see cref="ClaimsIdentity"/>
        /// </returns>
        /// <remarks>
        /// When the <paramref name="person"/> is null an anonymous <see cref="ClaimsIdentity"/> is returned
        /// </remarks>
        private ClaimsIdentity CreateClaimsIdentity(Person person)
        {
            ClaimsIdentity identity;

            if (person == null)
            {
                identity = new ClaimsIdentity();
            }
            else
            { 
                EmailAddress email;
                if (person.DefaultEmailAddress != null)
                {
                    email = person.DefaultEmailAddress;
                }
                else
                {
                    email = person.EmailAddress.FirstOrDefault();
                }

                identity = new ClaimsIdentity("10-25 Authenticated");
                identity.AddClaim(new Claim(ClaimTypes.Name, person.Name));
                if (email != null)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Email, email.Value));
                }
            }

            return identity;
        }

        /// <summary>
        /// force the <see cref="NotifyAuthenticationStateChanged"/> event to be raised
        /// </summary>
        public void NotifyAuthenticationStateChanged()
        {
            this.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}