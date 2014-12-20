namespace SelfHost.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "identity.AppRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "identity.AppUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("identity.AppRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("identity.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "identity.AppUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "identity.AppUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("identity.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "identity.AppUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("identity.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("identity.AppUserRoles", "UserId", "identity.AppUsers");
            DropForeignKey("identity.AppUserLogins", "UserId", "identity.AppUsers");
            DropForeignKey("identity.AppUserClaims", "UserId", "identity.AppUsers");
            DropForeignKey("identity.AppUserRoles", "RoleId", "identity.AppRoles");
            DropIndex("identity.AppUserLogins", new[] { "UserId" });
            DropIndex("identity.AppUserClaims", new[] { "UserId" });
            DropIndex("identity.AppUsers", "UserNameIndex");
            DropIndex("identity.AppUserRoles", new[] { "RoleId" });
            DropIndex("identity.AppUserRoles", new[] { "UserId" });
            DropIndex("identity.AppRoles", "RoleNameIndex");
            DropTable("identity.AppUserLogins");
            DropTable("identity.AppUserClaims");
            DropTable("identity.AppUsers");
            DropTable("identity.AppUserRoles");
            DropTable("identity.AppRoles");
        }
    }
}
