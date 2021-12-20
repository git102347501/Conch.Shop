using System.ComponentModel.DataAnnotations;

namespace Conch.Order
{
    /// <summary>
    /// 订单商品明细
    /// </summary>
    public class OrderGoods
    {
        /// <summary>
        /// 订单
        /// </summary>
        public Guid OrderMasterId { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public Guid GoodsId { get; set; }

        /// <summary>
        /// 商品名称-冗余数据
        /// </summary>
        [StringLength(64)] 
        public string GoodsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }
}
