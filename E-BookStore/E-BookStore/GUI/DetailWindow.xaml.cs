﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class DetailWindow : Window
    {
        int proID = 0;
        int CusID = 0;


        public DetailWindow(int _proID, int _CusID)
        {
            InitializeComponent();
            this.proID = _proID;
            this.CusID = _CusID;
            textBlockTotalQuantity.Text = bllDetail.ShowTotalProCate(this.CusID).ToString();
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
    }
}