using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UITemplate.Controls
{
    public interface PathChildFrame
    {

        #region 属性

        PathItem PathItem {get;set;}

        object ParentBaseFrame {get;set;}

        double GetWidth();

        void ShowContent(object param);

        #endregion

        

    }
}
