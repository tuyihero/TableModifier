﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

using UITemplate.Controls;
using KeyChanger;
using TableConstruct;

using Collada4Res;

namespace TableModifierV2.Pages
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WriteCollada.Instance.WriteFile("./Collada", "ColladaTest", WriteCollada.Instance._TestPos, 
                WriteCollada.Instance._TestNormal, WriteCollada.Instance._TestPoly, WriteCollada.Instance._TestPolyCount
                , WriteCollada.Instance._TestMatrix);
        }

    }
}
