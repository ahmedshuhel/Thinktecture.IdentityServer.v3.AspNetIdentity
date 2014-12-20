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

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace SelfHost
{
    public class UserDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public UserDbContext(string connString)
            : base(connString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUserRole>()
                .ToTable("AppUserRoles");
            modelBuilder.Entity<AppUserClaim>()
                .ToTable("AppUserClaims");
             modelBuilder.Entity<AppUserLogin>()
                .ToTable("AppUserLogins");
           modelBuilder.Entity<AppUser>()
                .ToTable("AppUsers");
            modelBuilder.Entity<AppRole>()
                .ToTable("AppRoles");

            modelBuilder.HasDefaultSchema("identity");

        }
    }
    public class AppUserStore : UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(UserDbContext ctx)
            : base(ctx)
        {
        }
    }
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim> { }
    public class AppRole : IdentityRole<int, AppUserRole> { }
    public class AppUserLogin : IdentityUserLogin<int> { }
    public class AppUserRole : IdentityUserRole<int> { }
    public class AppUserClaim : IdentityUserClaim<int> { }

    public class AppUserManager : UserManager<AppUser, int>
    {
        public AppUserManager(AppUserStore store)
            : base(store)
        {
        }
    }

    public class UserDbContextFactory : IDbContextFactory<UserDbContext>
    {

        public UserDbContext Create()
        {
            var connString = ConfigurationManager.ConnectionStrings["AspId"].ConnectionString;
            return new UserDbContext(connString);
        }

    }
}
