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
using E_BookStore.BLL;
using E_BookStore.DTO;

namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for MainUIWindow.xaml
    /// </summary>
    public partial class MainUIWindow : Window
    {

        public static Image getImage(string url, int height, int width)
        {
            var image = new Image();
            var fullFilePath = url;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            image.Source = bitmap;
            image.Height = height;
            image.Width = width;
            return image;
        }
        MainUIBLL bll;
        public MainUIWindow()
        {
            InitializeComponent();
            bll = new MainUIBLL();
            bll.getallBookUI();
            bll.getBookDetail(6);
            bll.getallMagaUI();
            bll.getMagaDetail(12);
            bll.getReview(12);
        }
    }
}
