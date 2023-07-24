using System;
using System.Collections.Generic;
using System.Text;

namespace YouYou.Business.Models.Enums
{
    public struct RoleWithIdEnum
    {
        public static Guid Admin = new Guid("b4029371-be9c-45a1-94a4-ccac2dff88db");
        public static Guid Operator = new Guid("efcb9598-9ac4-47de-9a9a-583c5192facb");
        public static Guid Coordinator = new Guid("091e4c3d-0aa4-48a6-b201-6e69c9577e51");
        public static Guid Designer = new Guid("b73a8dfc-4c15-474f-a30d-7df6f48179fa");
        public static Guid Editor = new Guid("a296fc56-ef20-4060-a65a-3e1385ff49de");
        public static Guid Client = new Guid("ec27cb0e-579c-45f3-94e8-25b92dc4a2d9");

        public static IEnumerable<Guid> GetRolesBackOfficeUsers()
        {
            List<Guid> rolesBackOfficeUsers = new List<Guid>()
            {
                Admin,
                Operator
            };
            return rolesBackOfficeUsers;
        }
    }
}
