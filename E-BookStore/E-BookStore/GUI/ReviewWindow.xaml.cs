using E_BookStore.BLL;
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
    /// Interaction logic for ReviewWindow.xaml
    /// </summary>
    public partial class ReviewWindow : Window
    {
        private int productId;
        private int customerId;
        private ReviewBLL bll;
        public ReviewWindow()
        {
            InitializeComponent();
        }
        public ReviewWindow(int productId, int customerId)
        {
            InitializeComponent();
            bll = new ReviewBLL();
            this.productId = productId;
            this.customerId = customerId;
        }
        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            if (Comment.Text == String.Empty)
                Comment.Text = "No caption";
            bll.insertReview(productId, ImgUrl.Text, Comment.Text, Convert.ToInt32(RatingSlider.Value), customerId);
            this.Close();
        }
        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
