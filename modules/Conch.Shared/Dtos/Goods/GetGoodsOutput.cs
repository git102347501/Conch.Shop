using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conch.Shared.Dtos.Goods
{
    public class GetGoodsOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
