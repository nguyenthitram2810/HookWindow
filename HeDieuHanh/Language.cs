using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeDieuHanh
{
    class Language
    {
        public string code;
        public string name;
        public Language(string code, string name)
        {
            this.code = code;
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
