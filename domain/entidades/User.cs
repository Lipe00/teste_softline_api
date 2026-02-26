using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace domain.entidades
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int Id { get; set; }
        [Column("user_name")]
        public string Name { get; set; }
        [Column("user_password")]
        public string Password { get; set; }
    }
}
