using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using FirstFloor.ModernUI.Presentation;

namespace UITemplate.Controls
{
    public class TabItem : Displayable
    {
        public TabPopType PopType { get; set; }

        public UIElement TabControl { get; set; }

        public TabItem()
        {
            PopType = TabPopType.POP_CLICK;
            TabControl = null;
        }
    }
}
