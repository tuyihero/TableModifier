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
    public class PathItem : Displayable
    {
        public UIElement PathControl { get; set; }

        public int PathLevel { get; set; }

        public double Width { get; set; }
    }
}
