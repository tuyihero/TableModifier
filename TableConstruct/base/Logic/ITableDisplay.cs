using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableConstruct
{
    public interface ITableDisplay
    {
        string DisplayName { get; }

        string DisplayTips { get; }

        string WriteValue { get; }
    }
}
