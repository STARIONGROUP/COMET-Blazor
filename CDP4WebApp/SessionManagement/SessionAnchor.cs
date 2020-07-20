// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionAnchor" company="RHEA System S.A.">
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
    using CDP4Dal;

    /// <summary>
    /// The purpose of the <see cref="SessionAnchor"/> is to provide access to
    /// an instance of <see cref="ISession"/>
    /// </summary>
    public class SessionAnchor : ISessionAnchor
    {
        /// <summary>
        /// Gets or sets the <see cref="ISession"/>
        /// </summary>
        public ISession Session { get; set;}

        /// <summary>
        /// Gets a value determining whether the session is open or not
        /// </summary>
        public bool IsOpen
        { 
            get { 
                
                if (this.Session == null || this.Session.Credentials == null)
                { 
                    return false;
                }

                return true; 
            }
         }
    }
}