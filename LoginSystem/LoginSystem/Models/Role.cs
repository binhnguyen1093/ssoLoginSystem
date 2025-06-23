using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginSystem.Models
{
    [Table("roles")]
    public class Role
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("application_id")]
        public int ApplicationId { get; set; }

        public Application Application { get; set; }

        public List<User> Users { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
