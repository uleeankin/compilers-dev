using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.analyzer
{
    public interface ILexicalAnalyzer
    {
        public abstract void Analyze(string element, int position);
    }
}
