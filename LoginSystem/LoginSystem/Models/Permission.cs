using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginSystem.Models
{
    [Table("permissions")]
    public class Permission
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("action")]
        public string Action { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}
