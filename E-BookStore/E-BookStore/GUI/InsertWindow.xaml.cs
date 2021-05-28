using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using E_BookStore.BLL;

namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class InsertWindow : Window
    {
        InsertAndEditBLL bll;
        public InsertWindow()
        {
            InitializeComponent();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            EditWindow edit = new EditWindow();
            edit.Show();
            Close();
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
            else if (bll.CheckIDExist(BooktextBoxID.Text))
            {
                MessageBox.Show("Product ID existed!");
            }
            else bll.insertBook(BooktextBoxID.Text, BooktextBoxImgUrl.Text, BooktextBoxName.Text,
                BooktextboxPrice.Text, BooktextboxQuantity.Text, BooktextBoxCity.Text, BooktextBoxStreet.Text,
                BooktextBoxLanguage.Text, BooktextBoxPublisher.Text, BooktextBoxPublishYear.Text, BooktextBoxPages.Text);
        }
        private void ResetBook(object sender, RoutedEventArgs e)
        {
            BooktextBoxID.Text = "";
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
            else if (bll.CheckIDExist(MagatextBoxID.Text))
            {
                MessageBox.Show("Product ID existed!");
            }
            else if (!bll.CheckMagaSeriIDExist(MagatextBoxSeriNameID.Text))
            {
                MessageBox.Show("Magazine seri ID does not exist!");
            }
            else bll.insertMaga(MagatextBoxID.Text, MagatextBoxImgUrl.Text, MagatextBoxSeriNameID.Text, MagatextBoxNo.Text,
                 MagatextboxPrice.Text, MagatextboxQuantity.Text, MagatextBoxCity.Text, MagatextBoxStreet.Text,
                 MagatextBoxLanguage.Text, MagatextBoxPublishDate.Text);
        }
        private void ResetMaga(object sender, RoutedEventArgs e)
        {
            MagatextBoxID.Text = "";
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