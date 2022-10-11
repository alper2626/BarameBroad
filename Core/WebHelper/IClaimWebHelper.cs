using System;
using System.Collections.Generic;
using BaseEntities.EnumTypes;

namespace Core.WebHelper
{
    public interface IClaimWebHelper
    {
        bool IsAuthenticate();

        bool IsInRole(List<UserRole> assignableRoles);

        string UserIp { get; }

        bool IsInSingleRole(UserRole role);

        T GetClaimValue<T>(string key);

        Guid Id { get; }

        string FullName { get; }

    }
}