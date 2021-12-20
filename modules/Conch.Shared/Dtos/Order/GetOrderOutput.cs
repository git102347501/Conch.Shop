using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conch.Shared.Dtos.Order
{
    public class GetOrderOutput
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 账户ID
        /// </summary>
        public Guid AccountId { get; set; }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 订单商品
        /// </summary>
        public virtual ICollection<OrderGoodsDto> OrderGoodsList { get; set; }
    }

    /// <summary>
    /// 订单商品明细
    /// </summary>
    public class OrderGoodsDto
    {
        /// <summary>
        /// 商品
        /// </summary>
        public Guid GoodsId { get; set; }

        /// <summary>
        /// 商品名称-冗余数据
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }
}
