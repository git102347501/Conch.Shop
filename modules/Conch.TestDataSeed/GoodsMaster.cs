using System.ComponentModel.DataAnnotations;

namespace Conch.Goods
{
    /// <summary>
    /// 商品信息表
    /// </summary>
    public class GoodsMaster
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
