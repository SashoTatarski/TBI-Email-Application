using EMS.Data.dbo_Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMS.Data.Seed
{
    public static class AccountSeeder
    {
        public static async Task Init(SystemDataContext context, UserManager<UserDomain> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            await SeedRoles(context, roleManager);
            await SeedManagers(context, userManager);
            await SeedOperators(context, userManager);
        }

        private static async Task SeedRoles(SystemDataContext context, RoleManager<IdentityRole> roleManager)
        {
            if (!context.Roles.Any(role => role.Name == Constants.RoleOperator))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = Constants.RoleOperator
                });
            }
            if (!context.Roles.Any(role => role.Name == Constants.RoleManager))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = Constants.RoleManager
                });
            }
        }

        private static async Task SeedManagers(SystemDataContext context, UserManager<UserDomain> userManager)
        {
            if (!context.Users.Any(user => user.UserName == Constants.DefaultManagerUsername))
            {
                var newManager = new UserDomain
                {
                    UserName = Constants.DefaultManagerUsername,
                    Email = Constants.DefaultManagerUsername,
                    CreatedOn = DateTime.Now,
                    IsPasswordChanged = true
                };

                var result = await userManager.CreateAsync(newManager);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(newManager, Constants.DefaultPassword);
                    await userManager.AddToRoleAsync(newManager, Constants.RoleManager);
                    await userManager.AddClaimsAsync(newManager, new List<Claim>()
                    {
                        new Claim("Role", Constants.RoleManager),
                        new Claim("IsPasswordChanged", newManager.IsPasswordChanged.ToString())
                    });
                }
            }
        }

        private static async Task SeedOperators(SystemDataContext context, UserManager<UserDomain> userManager)
        {
            if (!context.Users.Any(user => user.UserName == Constants.DefaultOperator1Username))
            {
                var newOperator = new UserDomain
                {
                    UserName = Constants.DefaultOperator1Username,
                    Email = Constants.DefaultOperator1Username,
                    CreatedOn = DateTime.Now,
                    IsPasswordChanged = false
                };

                var result = await userManager.CreateAsync(newOperator);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(newOperator, Constants.DefaultPassword);
                    await userManager.AddToRoleAsync(newOperator, Constants.RoleOperator);

                    await userManager.AddClaimsAsync(newOperator, new List<Claim>()
                    {
                        new Claim("Role", Constants.RoleOperator),
                        new Claim("IsPasswordChanged", newOperator.IsPasswordChanged.ToString())
                    });
                }
            }

            if (!context.Users.Any(user => user.UserName == Constants.DefaultOperator2Username))
            {
                var newOperator = new UserDomain
                {
                    UserName = Constants.DefaultOperator2Username,
                    Email = Constants.DefaultOperator2Username,
                    CreatedOn = DateTime.Now,
                    IsPasswordChanged = false
                };

                var result = await userManager.CreateAsync(newOperator);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(newOperator, Constants.DefaultPassword);
                    await userManager.AddToRoleAsync(newOperator, Constants.RoleOperator);
                    await userManager.AddClaimsAsync(newOperator, new List<Claim>()
                    {
                        new Claim("Role", Constants.RoleOperator),
                        new Claim("IsPasswordChanged", newOperator.IsPasswordChanged.ToString())
                    });
                }
            }
        }
    }
}
