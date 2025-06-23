using System.ComponentModel.DataAnnotations.Schema;

namespace LoginSystem.Models
{
    [Table("role_permissions")]
    public class RolePermission
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
