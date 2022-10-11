using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using BaseEntities.EnumTypes;
using Microsoft.AspNetCore.Http;

namespace Core.WebHelper
{
    public class ClaimWebHelper : IClaimWebHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimWebHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public T GetClaimValue<T>(string key)
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == key);
            if (claim == null)
                return default;
            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }

        public Guid Id => this.GetUserId();

        public string FullName => this.GetFullName();

        public string UserIp => this.GetIpAddress();


        public bool IsAuthenticate()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(List<UserRole> assignableRoles)
        {
            var roles = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Role);
            if (roles == null)
                return false;
            return roles.Value.Split(',').ToList().Intersect(assignableRoles.Select(q => q.ToString())).Any();
        }


        public bool IsInSingleRole(UserRole role)
        {
            var roles = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Role);
            if (roles == null)
                return false;

            return roles.Value.Split(',').ToList().Contains(role.ToString());
        }


        private Guid GetUserId()
        {
            var sId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.PrimarySid);
            if (sId != null)
                return Guid.Parse(sId.Value);

            return Guid.Empty;
        }

        private string GetFullName()
        {
            try
            {
                if (_httpContextAccessor.HttpContext == null)
                    return "System";
                var firstName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == "FirstName");
                var lastName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == "LastName");
                if (!string.IsNullOrEmpty(firstName?.Value))
                    return $"{firstName.Value} {lastName?.Value}";
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private string GetIpAddress()
        {
            return "176.88.83.178";
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            if (ip != null)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    ip = Dns.GetHostEntry(ip).AddressList
                        .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                }
                return ip.ToString();
            }

            return "";
        }
    }
}