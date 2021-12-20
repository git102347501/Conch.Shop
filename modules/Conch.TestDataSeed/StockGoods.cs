using System.ComponentModel.DataAnnotations;

namespace Conch.Stock
{
    /// <summary>
    /// 仓库商品库存
    /// </summary>
    public class StockGoods
    {
        public Guid StockId { get; set; }

        public Guid GoodsId { get; set; }

        /// <summary>
        /// 商品名称-冗余数据
        /// </summary>
        [StringLength(64)]
        public string GoodsName { get; set; }

        /// <summary>
        /// 可销售数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 冻结数量
        /// </summary>
        [ConcurrencyCheck]
        public int FreezeNum { get; set; }

    }
}
