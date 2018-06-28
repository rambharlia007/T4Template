using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public interface IRule
    {
        void Compute(Dictionary<string, string> inputValues);
    }
}
