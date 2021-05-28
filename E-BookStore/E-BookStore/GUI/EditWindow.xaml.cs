using E_BookStore.BLL;
using E_BookStore.DTO;
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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        InsertAndEditBLL bll;
        public EditWindow()
        {
            InitializeComponent();
        }

        private void Insert(object sender, RoutedEventArgs e)
        {
            InsertWindow insert = new InsertWindow();
            insert.Show();
            Close();
        }
        private void BookIDGet(object sender, RoutedEventArgs e)
        {
            bll = new InsertAndEditBLL();
            Book book = new Book();
            
            if (!bll.getBookDetail(BooktextBoxID.Text, ref book))
            {
                MessageBox.Show("Please enter valid Product ID!");
                
            }
            else if (!bll.CheckBookIDExist(BooktextBoxID.Text))
            {
                MessageBox.Show("Product ID does not exist!");
            }
            else
            {
                BooktextBoxImgUrl.Text = (book.ImgUrl.Length == 0) ? "" : book.ImgUrl;
                BooktextBoxName.Text = (book.Name.Length == 0) ? "" : book.Name;
                BooktextboxPrice.Text = (book.Price.ToString().Length == 0) ? "" : book.Price.ToString();
                BooktextboxQuantity.Text = (book.Quantiy.ToString().Length == 0) ? "" : book.Quantiy.ToString();
                BooktextBoxCity.Text = (book.Store.City.Length == 0) ? "" : book.Store.City;
                BooktextBoxStreet.Text = (book.Store.Street.Length == 0) ? "" : book.Store.Street;
                BooktextBoxLanguage.Text = (book.Language.Length == 0) ? "" : book.Language;
                BooktextBoxPublisher.Text = (book.Publisher.Length == 0) ? "" : book.Publisher;
                BooktextBoxPublishYear.Text = (book.PublishYear.Year.ToString().Length == 0) ? "" : book.PublishYear.Year.ToString();
                BooktextBoxPages.Text = (book.NPage.ToString().Length == 0) ? "" : book.NPage.ToString();
            }
        }
        private void SubmitBook(object sender, RoutedEventArgs e)
        {
            bll = new InsertAndEditBLL();
            string error = "Please enter";
            bool empty = false;
            if (bll.CheckEmpty(BooktextBoxID.Text))
            {
                empty = true;
                error += " product ID";
            }
            if (bll.CheckEmpty(BooktextBoxImgUrl.Text))
            {
                error += ((empty) ? ", " : " ") + "image url";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextBoxName.Text))
            {
                error += ((empty) ? ", " : " ") + "name";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextboxPrice.Text))
            {
                error += ((empty) ? ", " : " ") + "price";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextboxQuantity.Text))
            {
                error += ((empty) ? ", " : " ") + "quantity";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextBoxCity.Text))
            {
                error += ((empty) ? ", " : " ") + "city";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextBoxStreet.Text))
            {
                error += ((empty) ? ", " : " ") + "street";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextBoxLanguage.Text))
            {
                error += ((empty) ? ", " : " ") + "language";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextBoxPublisher.Text))
            {
                error += ((empty) ? ", " : " ") + "publisher";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextBoxPublishYear.Text))
            {
                error += ((empty) ? ", " : " ") + "publish year";
                empty = true;
            }
            if (bll.CheckEmpty(BooktextBoxPages.Text))
            {
                error += ((empty) ? ", " : " ") + "pages";
                empty = true;
            }
            if (empty)
            {
                MessageBox.Show(error + "!");
            }
            else bll.editBook(BooktextBoxID.Text, BooktextBoxImgUrl.Text, BooktextBoxName.Text,
                BooktextboxPrice.Text, BooktextboxQuantity.Text, BooktextBoxCity.Text, BooktextBoxStreet.Text,
                BooktextBoxLanguage.Text, BooktextBoxPublisher.Text, BooktextBoxPublishYear.Text, BooktextBoxPages.Text);
        }
        private void ResetBook(object sender, RoutedEventArgs e)
        {
            BooktextBoxImgUrl.Text = "";
            BooktextBoxName.Text = "";
            BooktextboxPrice.Text = "";
            BooktextboxQuantity.Text = "";
            BooktextBoxCity.Text = "";
            BooktextBoxStreet.Text = "";
            BooktextBoxLanguage.Text = "";
            BooktextBoxPublisher.Text = "";
            BooktextBoxPublishYear.Text = "";
            BooktextBoxPages.Text = "";
        }
        private void CancelBook(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MagaIDGet(object sender, RoutedEventArgs e)
        {
            bll = new InsertAndEditBLL();
            Magazine maga = new Magazine();

            if (!bll.getMagaDetail(MagatextBoxID.Text, ref maga))
            {
                MessageBox.Show("Please enter valid Product ID!");

            }
            else if (!bll.CheckMagaIDExist(MagatextBoxID.Text))
            {
                MessageBox.Show("Product ID does not exist!");
            }
            else
            {
                MagatextBoxImgUrl.Text = (maga.ImgUrl.Length == 0) ? "" : maga.ImgUrl;
                MagatextBoxSeriNameID.Text = (maga.SeriName.Id.ToString().Length == 0) ? "" : maga.SeriName.Id.ToString();
                MagatextBoxNo.Text = (maga.No.ToString().Length == 0) ? "" : maga.No.ToString();
                MagatextboxPrice.Text = (maga.Price.ToString().Length == 0) ? "" : maga.Price.ToString();
                MagatextboxQuantity.Text = (maga.Quantiy.ToString().Length == 0) ? "" : maga.Quantiy.ToString();
                MagatextBoxCity.Text = (maga.Store.City.Length == 0) ? "" : maga.Store.City;
                MagatextBoxStreet.Text = (maga.Store.Street.Length == 0) ? "" : maga.Store.Street;
                MagatextBoxLanguage.Text = (maga.Language.Length == 0) ? "" : maga.Language;
                MagatextBoxPublishDate.Text = ((maga.PublishDate.Day.ToString() + "/" +
                    maga.PublishDate.Month.ToString() + "/" + maga.PublishDate.Year.ToString()).Length == 0) ? "" : maga.PublishDate.Day.ToString() + "/" +
                    maga.PublishDate.Month.ToString() + "/" + maga.PublishDate.Year.ToString();
            }
        }
        private void SubmitMaga(object sender, RoutedEventArgs e)
        {
            bll = new InsertAndEditBLL();
            string error = "Please enter";
            bool empty = false;
            if (bll.CheckEmpty(MagatextBoxID.Text))
            {
                empty = true;
                error += " product ID";
            }
            if (bll.CheckEmpty(MagatextBoxImgUrl.Text))
            {
                error += ((empty) ? ", " : " ") + "image url";
                empty = true;
            }
            if (bll.CheckEmpty(MagatextBoxSeriNameID.Text))
            {
                error += ((empty) ? ", " : " ") + "name";
                empty = true;
            }
            if (bll.CheckEmpty(MagatextBoxNo.Text))
            {
                error += ((empty) ? ", " : " ") + "no";
                empty = true;
            }
            if (bll.CheckEmpty(MagatextboxPrice.Text))
            {
                error += ((empty) ? ", " : " ") + "price";
                empty = true;
            }
            if (bll.CheckEmpty(MagatextboxQuantity.Text))
            {
                error += ((empty) ? ", " : " ") + "quantity";
                empty = true;
            }
            if (bll.CheckEmpty(MagatextBoxCity.Text))
            {
                error += ((empty) ? ", " : " ") + "city";
                empty = true;
            }
            if (bll.CheckEmpty(MagatextBoxStreet.Text))
            {
                error += ((empty) ? ", " : " ") + "street";
                empty = true;
            }
            if (bll.CheckEmpty(MagatextBoxLanguage.Text))
            {
                error += ((empty) ? ", " : " ") + "language";
                empty = true;
            }
            if (bll.CheckEmpty(MagatextBoxPublishDate.Text))
            {
                error += ((empty) ? ", " : " ") + "publish date";
                empty = true;
            }
            if (empty)
            {
                MessageBox.Show(error + "!");
            }
            else if (!bll.CheckMagaSeriIDExist(MagatextBoxSeriNameID.Text))
            {
                MessageBox.Show("Magazine seri ID does not exist!");
            }
            else bll.editMaga(MagatextBoxID.Text, MagatextBoxImgUrl.Text, MagatextBoxSeriNameID.Text, MagatextBoxNo.Text,
                 MagatextboxPrice.Text, MagatextboxQuantity.Text, MagatextBoxCity.Text, MagatextBoxStreet.Text,
                 MagatextBoxLanguage.Text, MagatextBoxPublishDate.Text);
        }
        private void ResetMaga(object sender, RoutedEventArgs e)
        {
            MagatextBoxImgUrl.Text = "";
            MagatextBoxSeriNameID.Text = "";
            MagatextBoxNo.Text = "";
            MagatextboxPrice.Text = "";
            MagatextboxQuantity.Text = "";
            MagatextBoxCity.Text = "";
            MagatextBoxStreet.Text = "";
            MagatextBoxLanguage.Text = "";
            MagatextBoxPublishDate.Text = "";
        }

        private void CancelMaga(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
