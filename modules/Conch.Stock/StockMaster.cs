using System.ComponentModel.DataAnnotations;

namespace Conch.Stock
{
    /// <summary>
    /// 仓库信息
    /// </summary>
    public class StockMaster
    {
        [Key] 
        public Guid Id { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [StringLength(64)] 
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public virtual List<StockGoods> GoodsList { get; set; }
}
}