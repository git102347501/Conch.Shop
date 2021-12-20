using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conch.Shared.Dtos.Stock
{
    public class UpdateNumDto
    {
        public Guid Id { get; set; }
        public int Num { get; set; }
    }

    public class UpdateNumInput
    {
        public List<UpdateNumDto> List { get; set; }
    }
}
