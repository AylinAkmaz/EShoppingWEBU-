using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShoppingWEBUI.Core.DTO
{
    public class LoginDTO
    {
        public string KullanıcıAdı { get; set; }
        public string Şifre { get; set; }
        public string Token { get; set; }
        public string AdSoyad { get; set; }
        public int UserID { get; set; }
        public string EPosta { get; set; }


    }

}
