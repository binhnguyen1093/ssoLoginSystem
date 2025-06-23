using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginSystem.Models
{
    [Table("applications")]
    public class Application
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string Code { get; set; }

        public List<Role> Roles { get; set; }
    }
}
