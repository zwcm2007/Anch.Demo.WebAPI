using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anch.Demo.Core
{
    [Table("SM_User")]
    public class SM_User : Entity<string>
    {
        [Column("vUserCode")]
        public override string Id { get; set; }

        [Column("vUserName")]
        public string UserName { get; set; }

        [Column("vPassword")]
        public string Password { get; set; }

        [Column("bAdmin")]
        public bool IsAdmin { get; set; }

        [Column("bStop")]
        public bool IsStop { get; set; }
    }
}