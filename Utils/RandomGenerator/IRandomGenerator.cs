using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.RandomGenerator
{
    public interface IRandomGenerator
    {
        public string GenerateId();
        public string GenerateCode();
    }
}
