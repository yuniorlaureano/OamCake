using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OamCake.Data.Dto
{
    public class CatalogCreationDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public long[] ProductsId { get; set; }
    }
}
