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
        [ConcurrencyCheck] 
        public int Num { get; set; }

        /// <summary>
        /// 冻结数量
        /// </summary>
        [ConcurrencyCheck] 
        public int FreezeNum { get; set; }

        /// <summary>
        /// 获取可以销售的商品数
        /// </summary>
        /// <returns></returns>
        public int GetSellNum()
        {
            return this.Num - FreezeNum;
        }

        /// <summary>
        /// 销售扣库存
        /// </summary>
        /// <returns></returns>
        public void DeductInventory(int num)
        {
            if (this.FreezeNum < num)
            {
                throw new Exception("扣除库存大于冻结库存");
            }
            this.FreezeNum -= num;
            this.Num -= num;
        }

        /// <summary>
        /// 订单冻结库存
        /// </summary>
        /// <returns></returns>
        public void BlockInventory(int num)
        {
            if (this.GetSellNum() < num)
            {
                throw new Exception("可售数量小于订单库存");
            }
            this.FreezeNum += num;
        }
    }
}
