using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverJewelry_Repositories.Models.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public int Role {  get; set; }
    }
}
