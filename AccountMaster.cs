using System.ComponentModel.DataAnnotations;

namespace Conch.Shop
{
    public class AccountMaster
    {
        [Key]
        public Guid Id { get; set; }
        
        [StringLength(64)]
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
