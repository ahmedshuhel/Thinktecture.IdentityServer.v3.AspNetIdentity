/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

//#define USE_INT_PRIMARYKEY

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SelfHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thinktecture.IdentityManager;
using Thinktecture.IdentityServer.AspNetIdentity;

namespace Thinktecture.IdentityManager.Host
{
    public class AspNetIdentityIdentityManagerFactory
    {
        static AspNetIdentityIdentityManagerFactory()
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.CreateDatabaseIfNotExists<UserDbContext>());
        }

        string connString;
        public AspNetIdentityIdentityManagerFactory(string connString)
        {
            this.connString = connString;
        }
        
        public IIdentityManagerService Create()
        {
            var db = new IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>(connString);
            var store = new UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>(db);
            var usermgr = new UserManager<AppUser, int>(store);
            usermgr.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };

            var rolestore = new RoleStore<AppRole, int, AppUserRole>(db);
            var rolemgr = new RoleManager<AppRole, int>(rolestore);

            var svc = new Thinktecture.IdentityManager.AspNetIdentity.AspNetIdentityManagerService<AppUser, int, AppRole, int>(usermgr, rolemgr);
            var dispose = new DisposableIdentityManagerService(svc, db);
            return dispose;
        }
    }
}