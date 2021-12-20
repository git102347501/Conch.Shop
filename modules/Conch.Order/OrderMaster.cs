using System.ComponentModel.DataAnnotations;

namespace Conch.Order
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class OrderMaster
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [Key] 
        public Guid Id { get; set; }

        /// <summary>
        /// 账户ID
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 订单简称
        /// </summary>
        [StringLength(128)] 
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 订单商品
        /// </summary>
        public virtual ICollection<OrderGoods> OrderGoodsList { get; set; }
    }
}