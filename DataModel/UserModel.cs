using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class UserModel
    {
        public string Login { get; set; }

        public string Pwd { get; set; }

        public string Name { get; set; }

        public string Firstname { get; set; }

        public Byte[] Picture { get; set; }

        public string Role { get; set; }

        public bool Connected { get; set; }
    }
}