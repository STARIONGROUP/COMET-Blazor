// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticationService.cs" company="RHEA System S.A.">
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

namespace CDP4WebApp.SessionManagement
{
    using System.Threading.Tasks;
    using CDP4Common.SiteDirectoryData;

    /// <summary>
    /// The purpose of the <see cref="IAuthenticationService"/> is to authenticate against
    /// a E-TM-10-25 Annex C.2 data source
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Login (authenticate) with a username and password to an E-TM-10-25 data source
        /// </summary>
        /// <param name="userName">
        /// The username to authenticate with
        /// </param>
        /// <param name="password">
        ///The password to authenticate with
        /// </param>
        /// <returns>
        /// The <see cref="Person"/> object that corresponds to the provided <paramref name="userName"/>
        /// </returns>
        Task<Person> Login(string userName, string password);

        /// <summary>
        /// logout from an E-TM-10-25 data source
        /// </summary>
        /// <returns>
        /// a <see cref="Task"/>
        /// </returns>
        Task LogOut();
    }
}