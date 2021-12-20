using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conch.Shared.Dtos.Stock
{
    public class GetStockOutput
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public List<GetStockGoodsDto> GoodsList { get; set; }
    }

    public class GetStockGoodsDto
    {
        public Guid GoodsId { get; set; }

        public string Name { get; set; }
        public int Num { get; set; }

        public int FreezeNum { get; set; }

        public int SellNum { get; set; }
    }
}
