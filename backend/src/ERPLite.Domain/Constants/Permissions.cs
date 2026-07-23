using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Domain.Constants;

public static class Permissions
{
    public static class Organization
    {
        public const string View = "Organization.View";
        public const string Create = "Organization.Create";
        public const string Update = "Organization.Update";
        public const string Delete = "Organization.Delete";
    }

    public static class User
    {
        public const string View = "User.View";
        public const string Create = "User.Create";
        public const string Update = "User.Update";
        public const string Delete = "User.Delete";
    }

    public static class Role
    {
        public const string View = "Role.View";
        public const string Create = "Role.Create";
        public const string Update = "Role.Update";
        public const string Delete = "Role.Delete";
        public const string Assign = "Role.Assign";
        public const string Remove = "Role.Remove";
    }
}
