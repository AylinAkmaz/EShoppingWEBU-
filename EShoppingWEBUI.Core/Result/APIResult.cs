using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShoppingWEBUI.Core.Result
{
    public class APIResult<T>
    {
        public T Data { get; set; }
        public string Mesaj { get; set; }
        public HataBilgisi HataBilgisi { get; set; }


    }
}
