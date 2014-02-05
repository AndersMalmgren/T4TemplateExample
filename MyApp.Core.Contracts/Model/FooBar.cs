using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Contracts.Model
{
    public class FooBar
    {
        public FooBar(string random)
        {
            Random = random;
        }

        public string Random { get; set; }
    }
}
