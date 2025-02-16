using System;
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

namespace PaySlips.UI.View.Base
{
    /// <summary>
    /// Логика взаимодействия для TitleBarView.xaml
    /// </summary>
    public partial class TitleBarView : UserControl
    {
        public TitleBarView()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                Window.GetWindow(this)?.DragMove();
        }
    }
}
