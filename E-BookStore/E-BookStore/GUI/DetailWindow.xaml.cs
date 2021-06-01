using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class DetailWindow : Window
    {
        int proID = 0;
        int CusID = 0;
        string role;


        public static Image getImage(string url)
        {
            var image = new Image();
            var fullFilePath = url;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();
            image.Source = bitmap;
            image.Stretch = Stretch.UniformToFill;
            return image;
        }

        public DetailWindow(string _role, int _proID, int _CusID)
        {
            InitializeComponent();
            this.proID = _proID;
            this.CusID = _CusID;
            this.role = _role;
            string proType = bllDetail.checkType(this.proID);
            if (proType == "Book")
            {
                Book book = new Book();
                book = bllDetail.getBookDetail(proID);
                //img = getImage(book.ImgUrl);
                var fullFilePath = book.ImgUrl;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();
                img.Source = bitmap;
                img.Stretch = Stretch.UniformToFill;
                Name.Text = book.Name;
                publisher.Text = book.Publisher;
                price.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", book.Price) + "đ";
                Language.Text = book.Language;
                PublishDate.Text = book.PublishYear.Year.ToString();
                Quantity.Text = book.Quantiy.ToString();
            }
            else
            {
                Magazine maga = new Magazine();
                maga = bllDetail.getMagaDetail(this.proID);
                var fullFilePath = maga.ImgUrl;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();
                img.Source = bitmap;
                img.Stretch = Stretch.UniformToFill;
                Name.Text = maga.SeriName.Name + " No." + maga.No.ToString();
                publisher.Text = maga.SeriName.Publisher;
                price.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", maga.Price) + "đ";
                Language.Text = maga.Language;
                PublishDate.Text = maga.PublishDate.Day.ToString() + "/" + maga.PublishDate.Month.ToString() + "/"
                    + maga.PublishDate.Year.ToString();
                Quantity.Text = maga.Quantiy.ToString();
            }
            textBoxQuantity.PreviewTextInput += TextBoxQuantity_PreviewTextInput;
            textBoxQuantity.TextChanged += TextBoxQuantity_TextChanged;
            textBlockTotalQuantity.Text = bllDetail.ShowTotalProCate(this.CusID).ToString();
        }

        private void TextBoxQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count = 0;
            int quantity = 0;
            Int32.TryParse(textBoxQuantity.Text, out count);
            Int32.TryParse(Quantity.Text, out quantity);
            if (count > quantity) textBoxQuantity.Text = quantity.ToString();
        }

        private static readonly Regex _regex = new Regex("[^0-9]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void TextBoxQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        public static Image GetImage(string url)
        {
            var image = new Image();
            var fullFilePath = url;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();
            image.Source = bitmap;
            image.Stretch = Stretch.UniformToFill;
            return image;
        }
        DetailBLL bllDetail = new DetailBLL();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // check if quantity is valid
            if (bllDetail.checkQuantity(textBoxQuantity.Text))
            {
                textBlockTotalQuantity.Text = bllDetail.ShowTotalProCate(this.CusID).ToString();
            }
            else
            {
                MessageBox.Show("Invalid quantity!");
                return;
            }
            // check if customer has a order oncart yet?
            if (bllDetail.CheckOrderExistBLL(this.CusID))
            {
                // if yes, work on this order
                int x = Int32.Parse(textBoxQuantity.Text);
                bllDetail.UpdatePPartOf(this.proID, x, this.CusID);
                textBlockTotalQuantity.Text = bllDetail.ShowTotalProCate(this.CusID).ToString();
                MessageBox.Show("Add product successfully!");
            }
            else
            {
                // otherwise, create a new order
                int x = Int32.Parse(textBoxQuantity.Text);
                bllDetail.InsertPPartOf(this.proID, x, this.CusID);
                textBlockTotalQuantity.Text = bllDetail.ShowTotalProCate(this.CusID).ToString();
                MessageBox.Show("Add product successfully!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainUIWindow main = new MainUIWindow(this.role, this.CusID);
            main.Show();
            Close();
        }
    }
}