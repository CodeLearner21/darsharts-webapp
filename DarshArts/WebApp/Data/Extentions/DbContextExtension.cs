using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.Data.Extentions
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this ApplicationDbContext context)
        {
            // Seed Data
            var appUser = new AppUser { UserName = "demo@demo.com", NormalizedUserName = "demo@demo.com", NormalizedEmail = "demo@demo.com", Email = "demo@demo.com", PasswordHash = "DemoUser123", EmailConfirmed = true, ConcurrencyStamp = new Guid().ToString() };
            context.Users.Add(appUser);
            context.SaveChanges();

            var appRole = new AppRole { Name = "DemoUser", NormalizedName = "DemoUser", ConcurrencyStamp = new Guid().ToString() };
            context.Roles.Add(appRole);
            context.SaveChanges();

            context.UserRoles.Add(new IdentityUserRole<Guid> { UserId = appUser.Id, RoleId = appRole.Id });
            context.SaveChanges();

            // Seed Default Order Status Types
            List<OrderStatusType> statusTypes = new List<OrderStatusType>();
            statusTypes.Add(new OrderStatusType { Name = "Completed" });
            statusTypes.Add(new OrderStatusType { Name = "Pending" });
            statusTypes.Add(new OrderStatusType { Name = "Cancelled" });
            context.OrderStatusTypes.AddRange(statusTypes);
            context.SaveChanges();
        }
    }
}
