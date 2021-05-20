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
using System.Windows.Shapes;

namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OrderingWindow : Window
    {
        public OrderingWindow()
        {
            InitializeComponent();
            StackPanel stack = demoStack;
            for(int i = 0; i < 20; i++)
            {
                TextBox txt = new TextBox();
                txt.Text = i.ToString();
                stack.Children.Add(txt);
            }
        }
    }
}
