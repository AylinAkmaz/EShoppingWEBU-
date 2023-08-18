using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShoppingWEBUI.Core.DTO;

namespace EShoppingWEBUI.Core.ViewModel
{
    public class ProductViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }


    }
}
