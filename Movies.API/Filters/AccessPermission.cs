using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Movies.API.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AccessPermission : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public AccessPermission(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context != null)
            {            
                var presentRoles = _role.Split(',').ToList();
                var user = context.HttpContext.User;
                if (user.Identity.IsAuthenticated)
                {
                    var roles = user.Identities.Select(identity => identity.Claims.Where(c => c.Type == ClaimTypes.Role)).FirstOrDefault();

                    if (roles != null && _role != "*")
                    {
                        foreach (var role in roles)
                        {
                            foreach (var item in presentRoles)
                            {
                                if (role.Value == item)
                                {
                                    return;
                                }
                            }
                        }
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new JsonResult("Unathorized")
                        {
                            Value = new { Code = "Unauthorized", Message = "UnauthorizedAccess" }
                        };
                    }
                }
            }
        }
    }

}