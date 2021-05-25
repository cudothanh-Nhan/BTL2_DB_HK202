using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        MainUIBLL bll;
        private Grid getProUI(List<Book> bookList, List<Magazine> magaList, string proType)
        {
            Thickness defaultPadding = new Thickness(5, 0, 0, 0);
            //List<Book> bookList = new List<Book>();
            //bookList = bll.getallBookUI();
            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 0, 0, 0);
            int proCount = 0;
            switch (proType)
            {
                case "Book":        proCount = bookList.Count;                      break;
                case "Magazine":    proCount = magaList.Count;                      break;
                case "All":         proCount = bookList.Count + magaList.Count;     break;
            }
            ColumnDefinition[] colDef = new ColumnDefinition[3 * proCount];
            for (int k = 0; k < 3 * proCount; k++) colDef[k] = new ColumnDefinition();
            for (int k = 0; k < 3 * proCount; k++)
            {
                if (k % 3 == 0)
                {
                    colDef[k].Width = new GridLength(75);
                }
                else if (k % 3 == 1)
                {
                    colDef[k].Width = new GridLength(120);
                }
                else
                {
                    colDef[k].Width = new GridLength(5);
                }
            }
            foreach (var c in colDef)
            {
                grid.ColumnDefinitions.Add(c);
            }

            RowDefinition[] rowDef = new RowDefinition[3];
            for (int k = 0; k < 3; k++) rowDef[k] = new RowDefinition();
            rowDef[0].Height = new GridLength(50);
            rowDef[1].Height = rowDef[2].Height = new GridLength(20);
            foreach (var r in rowDef) grid.RowDefinitions.Add(r);
            int i = 0;
            switch (proType)
            {
                case "Magazine":
                    for (; i < proCount; i++)
                    {
                        Image img = getImage(magaList[i].ImgUrl);

                        TextBlock magaName = new TextBlock();
                        magaName.Text = magaList[i].SeriName.Name + " No." + magaList[i].No;
                        magaName.FontSize = 20;
                        magaName.FontWeight = FontWeights.Bold;
                        magaName.Padding = defaultPadding;
                        magaName.TextWrapping = TextWrapping.Wrap;

                        TextBlock magaQuantity = new TextBlock();
                        magaQuantity.Text = (magaList[i].Quantiy > 0) ? "In stock" : "Out of stock";
                        magaQuantity.Padding = defaultPadding;
                        magaQuantity.TextAlignment = TextAlignment.Center;
                        magaQuantity.Background = (magaList[i].Quantiy > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
                        magaQuantity.FontSize = 14;

                        TextBlock magaPrice = new TextBlock();
                        magaPrice.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", magaList[i].Price) + "đ";
                        magaPrice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF8C00"));
                        magaPrice.Padding = defaultPadding;
                        magaPrice.FontSize = 16;

                        TextBlock separator = new TextBlock();
                        separator.Width = 0.4;
                        separator.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));

                        Grid.SetRowSpan(separator, 3);
                        Grid.SetColumn(separator, 3 * i + 2);

                        Grid.SetRowSpan(img, 3);
                        Grid.SetColumn(img, 3 * i);

                        Grid.SetRow(magaName, 0);
                        Grid.SetColumn(magaName, 3 * i + 1);

                        Grid.SetRow(magaPrice, 1);
                        Grid.SetColumn(magaPrice, 3 * i + 1);
                        Grid.SetRow(magaQuantity, 2);
                        Grid.SetColumn(magaQuantity, 3 * i + 1);

                        grid.Children.Add(img);
                        grid.Children.Add(magaName);
                        grid.Children.Add(magaPrice);
                        grid.Children.Add(magaQuantity);
                        grid.Children.Add(separator);
                    }
                    break;

                case "Book":
                    for (; i < proCount; i++)
                    {
                        Image img = getImage(bookList[i].ImgUrl);

                        TextBlock bookName = new TextBlock();
                        bookName.Text = bookList[i].Name;
                        bookName.FontSize = 20;
                        bookName.FontWeight = FontWeights.Bold;
                        bookName.Padding = defaultPadding;
                        bookName.TextWrapping = TextWrapping.Wrap;

                        TextBlock bookQuantity = new TextBlock();
                        bookQuantity.Text = (bookList[i].Quantiy > 0) ? "In stock" : "Out of stock";
                        bookQuantity.Padding = defaultPadding;
                        bookQuantity.TextAlignment = TextAlignment.Center;
                        bookQuantity.Background = (bookList[i].Quantiy > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
                        bookQuantity.FontSize = 14;

                        TextBlock bookPrice = new TextBlock();
                        bookPrice.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", bookList[i].Price) + "đ";
                        bookPrice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF8C00"));
                        bookPrice.Padding = defaultPadding;
                        bookPrice.FontSize = 16;

                        TextBlock separator = new TextBlock();
                        separator.Width = 0.4;
                        separator.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));

                        Grid.SetRowSpan(separator, 3);
                        Grid.SetColumn(separator, 3 * i + 2);

                        Grid.SetRowSpan(img, 3);
                        Grid.SetColumn(img, 3 * i);

                        Grid.SetRow(bookName, 0);
                        Grid.SetColumn(bookName, 3 * i + 1);

                        Grid.SetRow(bookPrice, 1);
                        Grid.SetColumn(bookPrice, 3 * i + 1);
                        Grid.SetRow(bookQuantity, 2);
                        Grid.SetColumn(bookQuantity, 3 * i + 1);

                        grid.Children.Add(img);
                        grid.Children.Add(bookName);
                        grid.Children.Add(bookPrice);
                        grid.Children.Add(bookQuantity);
                        grid.Children.Add(separator);
                    }
                    break;

                case "All":
                    for (; i < bookList.Count; i++)
                    {
                        Image img = getImage(bookList[i].ImgUrl);

                        TextBlock bookName = new TextBlock();
                        bookName.Text = bookList[i].Name;
                        bookName.FontSize = 20;
                        bookName.FontWeight = FontWeights.Bold;
                        bookName.Padding = defaultPadding;
                        bookName.TextWrapping = TextWrapping.Wrap;
                        
                        TextBlock bookQuantity = new TextBlock();
                        bookQuantity.Text = (bookList[i].Quantiy > 0) ? "In stock" : "Out of stock";
                        bookQuantity.Padding = defaultPadding;
                        bookQuantity.TextAlignment = TextAlignment.Center;
                        bookQuantity.Background = (bookList[i].Quantiy > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
                        bookQuantity.FontSize = 14;

                        TextBlock bookPrice = new TextBlock();
                        bookPrice.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", bookList[i].Price) + "đ";
                        bookPrice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF8C00"));
                        bookPrice.Padding = defaultPadding;
                        bookPrice.FontSize = 16;

                        TextBlock separator = new TextBlock();
                        separator.Width = 0.4;
                        separator.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));

                        Grid.SetRowSpan(separator, 3);
                        Grid.SetColumn(separator, 3 * i + 2);

                        Grid.SetRowSpan(img, 3);
                        Grid.SetColumn(img, 3 * i);

                        Grid.SetRow(bookName, 0);
                        Grid.SetColumn(bookName, 3 * i + 1);

                        Grid.SetRow(bookPrice, 1);
                        Grid.SetColumn(bookPrice, 3 * i + 1);
                        Grid.SetRow(bookQuantity, 2);
                        Grid.SetColumn(bookQuantity, 3 * i + 1);

                        grid.Children.Add(img);
                        grid.Children.Add(bookName);
                        grid.Children.Add(bookPrice);
                        grid.Children.Add(bookQuantity);
                        grid.Children.Add(separator);
                    }
                    for (; i < proCount; i++)
                    {
                        int index = i - bookList.Count;
                        Image img = getImage(magaList[index].ImgUrl);

                        TextBlock magaName = new TextBlock();
                        magaName.Text = magaList[index].SeriName.Name + " No." + magaList[index].No;
                        magaName.FontSize = 20;
                        magaName.FontWeight = FontWeights.Bold;
                        magaName.Padding = defaultPadding;
                        magaName.TextWrapping = TextWrapping.Wrap;

                        TextBlock magaQuantity = new TextBlock();
                        magaQuantity.Text = (magaList[index].Quantiy > 0) ? "In stock" : "Out of stock";
                        magaQuantity.Padding = defaultPadding;
                        magaQuantity.TextAlignment = TextAlignment.Center;
                        magaQuantity.Background = (magaList[index].Quantiy > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
                        magaQuantity.FontSize = 14;

                        TextBlock magaPrice = new TextBlock();
                        magaPrice.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", magaList[index].Price) + "đ";
                        magaPrice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF8C00"));
                        magaPrice.Padding = defaultPadding;
                        magaPrice.FontSize = 16;

                        TextBlock separator = new TextBlock();
                        separator.Width = 0.4;
                        separator.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));

                        Grid.SetRowSpan(separator, 3);
                        Grid.SetColumn(separator, 3 * i + 2);

                        Grid.SetRowSpan(img, 3);
                        Grid.SetColumn(img, 3 * i);

                        Grid.SetRow(magaName, 0);
                        Grid.SetColumn(magaName, 3 * i + 1);

                        Grid.SetRow(magaPrice, 1);
                        Grid.SetColumn(magaPrice, 3 * i + 1);
                        Grid.SetRow(magaQuantity, 2);
                        Grid.SetColumn(magaQuantity, 3 * i + 1);

                        grid.Children.Add(img);
                        grid.Children.Add(magaName);
                        grid.Children.Add(magaPrice);
                        grid.Children.Add(magaQuantity);
                        grid.Children.Add(separator);
                    }
                    break;
            }
            return grid;
        }
        private void getallProUI(List<Book> bookList, List<Magazine> magaList, string proType)
        {
            int proCount = 0;
            switch (proType)
            {
                case "Book":        proCount = bookList.Count;                      break;
                case "Magazine":    proCount = magaList.Count;                      break;
                case "All":         proCount = bookList.Count + magaList.Count;     break;
            }
            double row = Math.Ceiling(proCount / 4.0);
            for (int i = 0; i < row; i++)
            {
                List<Magazine> magaListOnRow = new List<Magazine>();
                List<Book> bookListOnRow = new List<Book>();
                switch (proType)
                {
                    case "Magazine":
                        for (int j = 0; j < 4; j++)
                        {
                            if (4 * i + j < magaList.Count)
                            {
                                magaListOnRow.Add(magaList[4 * i + j]);
                            }
                        }
                        break;

                    case "Book":
                        for (int j = 0; j < 4; j++)
                        {
                            if (4 * i + j < bookList.Count)
                            {
                                bookListOnRow.Add(bookList[4 * i + j]);
                            }
                        }
                        break;

                    case "All":
                        for (int j = 0; j < 4; j++)
                        {
                            if (4 * i + j < bookList.Count)
                            {
                                bookListOnRow.Add(bookList[4 * i + j]);
                            }
                            else if (4 * i + j - bookList.Count < magaList.Count)
                            {
                                magaListOnRow.Add(magaList[4 * i + j - bookList.Count]);
                            }
                        }
                        break;
                }

                Separator separator = new Separator();
                separator.HorizontalAlignment = HorizontalAlignment.Left;
                separator.Width = (i != row - 1) ? 795 : -2.5 + 200 * ((proCount % 4 == 0) ? 4 : (proCount % 4));

                DisplayBookUI.Children.Add(getProUI(bookListOnRow, magaListOnRow, proType));
                DisplayBookUI.Children.Add(separator);
            }
        }
        private string search_string = "";
        private void Instock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bll = new MainUIBLL();
            List<Magazine> magaList = new List<Magazine>();
            List<Book> bookList = new List<Book>();
            magaList = bll.getallMagaUI();
            bookList = bll.getallBookUI();
            List<Magazine> magaToDisplay = new List<Magazine>();
            List<Book> bookToDisplay = new List<Book>();

            if (Instock.SelectedIndex == 0)
            {
                foreach (var i in magaList)
                {
                    if (i.Quantiy > 0)
                    {
                        magaToDisplay.Add(i);
                    }
                }
                foreach (var i in bookList)
                {
                    if (i.Quantiy > 0)
                    {
                        bookToDisplay.Add(i);
                    }
                }
            }
            else
            {
                foreach (var i in magaList)
                {
                    magaToDisplay.Add(i);
                }
                foreach (var i in bookList)
                {
                    bookToDisplay.Add(i);
                }
            }
            List<Magazine> magaToSearch = new List<Magazine>();
            List<Book> bookToSearch = new List<Book>();
            foreach (var i in magaToDisplay)
            {
                if ((i.SeriName.Name + " No." + i.No).Contains(search_string, StringComparison.OrdinalIgnoreCase))
                {
                    magaToSearch.Add(i);
                }
            }
            foreach (var i in bookToDisplay)
            {
                if (i.Name.Contains(search_string, StringComparison.OrdinalIgnoreCase))
                {
                    bookToSearch.Add(i);
                }
            }
            string proType;
            if (ProType.SelectedIndex == 0)
            {
                proType = "Book";
            }
            else if (ProType.SelectedIndex == 1)
            {
                proType = "Magazine";
            }
            else
            {
                proType = "All";
            }

            DisplayBookUI.Children.Clear();
            Separator separator = new Separator();
            separator.Width = 795;

            DisplayBookUI.Children.Add(separator);
            getallProUI(bookToSearch, magaToSearch, proType);
        }

        private void ProType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bll = new MainUIBLL();
            List<Magazine> magaList = new List<Magazine>();
            List<Book> bookList = new List<Book>();
            magaList = bll.getallMagaUI();
            bookList = bll.getallBookUI();
            List<Magazine> magaToDisplay = new List<Magazine>();
            List<Book> bookToDisplay = new List<Book>();

            if (Instock.SelectedIndex == 0)
            {
                foreach (var i in magaList)
                {
                    if (i.Quantiy > 0)
                    {
                        magaToDisplay.Add(i);
                    }
                }
                foreach (var i in bookList)
                {
                    if (i.Quantiy > 0)
                    {
                        bookToDisplay.Add(i);
                    }
                }
            }
            else
            {
                foreach (var i in magaList)
                {
                    magaToDisplay.Add(i);
                }
                foreach (var i in bookList)
                {
                    bookToDisplay.Add(i);
                }
            }
            List<Magazine> magaToSearch = new List<Magazine>();
            List<Book> bookToSearch = new List<Book>();
            foreach (var i in magaToDisplay)
            {
                if ((i.SeriName.Name + " No." + i.No).Contains(search_string, StringComparison.OrdinalIgnoreCase))
                {
                    magaToSearch.Add(i);
                }
            }
            foreach (var i in bookToDisplay)
            {
                if (i.Name.Contains(search_string, StringComparison.OrdinalIgnoreCase))
                {
                    bookToSearch.Add(i);
                }
            }
            string proType;
            if (ProType.SelectedIndex == 0)
            {
                proType = "Book";
            }
            else if (ProType.SelectedIndex == 1)
            {
                proType = "Magazine";
            }
            else
            {
                proType = "All";
            }

            DisplayBookUI.Children.Clear();
            Separator separator = new Separator();
            separator.Width = 795;

            DisplayBookUI.Children.Add(separator);
            getallProUI(bookToSearch, magaToSearch, proType);

            //MessageBox.Show(Instock.SelectedItem.ToString());
        }
        private void Search_click(object sender, RoutedEventArgs e)
        {
            bll = new MainUIBLL();
            search_string = SearchTaskBar.Text;
            List<Magazine> magaList = new List<Magazine>();
            List<Book> bookList = new List<Book>();
            magaList = bll.getallMagaUI();
            bookList = bll.getallBookUI();
            List<Magazine> magaToDisplay = new List<Magazine>();
            List<Book> bookToDisplay = new List<Book>();

            if (Instock.SelectedIndex == 0)
            {
                foreach (var i in magaList)
                {
                    if (i.Quantiy > 0)
                    {
                        magaToDisplay.Add(i);
                    }
                }
                foreach (var i in bookList)
                {
                    if (i.Quantiy > 0)
                    {
                        bookToDisplay.Add(i);
                    }
                }
            }
            else
            {
                foreach (var i in magaList)
                {
                    magaToDisplay.Add(i);
                }
                foreach (var i in bookList)
                {
                    bookToDisplay.Add(i);
                }
            }
            List<Magazine> magaToSearch = new List<Magazine>();
            List<Book> bookToSearch = new List<Book>();
            foreach (var i in magaToDisplay)
            {
                if ((i.SeriName.Name + " No." + i.No).Contains(search_string, StringComparison.OrdinalIgnoreCase))
                {
                    magaToSearch.Add(i);
                }
            }
            foreach (var i in bookToDisplay)
            {
                if (i.Name.Contains(search_string, StringComparison.OrdinalIgnoreCase))
                {
                    bookToSearch.Add(i);
                }
            }
            string proType;
            if (ProType.SelectedIndex == 0)
            {
                proType = "Book";
            }
            else if (ProType.SelectedIndex == 1)
            {
                proType = "Magazine";
            }
            else
            {
                proType = "All";
            }

            DisplayBookUI.Children.Clear();
            Separator separator = new Separator();
            separator.Width = 795;
            DisplayBookUI.Children.Add(separator);
            getallProUI(bookToSearch, magaToSearch, proType);
        }

        public MainUIWindow()
        {
            InitializeComponent();
            bll = new MainUIBLL();

            List<Book> bookList = new List<Book>();
            List<Magazine> magaList = new List<Magazine>();
            bookList = bll.getallBookUI();
            magaList = bll.getallMagaUI();
            //bookList.Sort(
            //    delegate (Book p1, Book p2)
            //    {
            //        return p1.Price.CompareTo(p2.Price);
            //    }
            //);
            //magaList.Sort(
            //    delegate (Magazine p1, Magazine p2)
            //    {
            //        return p1.Price.CompareTo(p2.Price);
            //    }
            //);
            Separator separator = new Separator();
            separator.Width = 795;
            DisplayBookUI.Children.Add(separator);
            getallProUI(bookList, magaList, "All");

            ProType.SelectionChanged += ProType_SelectionChanged;
            Instock.SelectionChanged += Instock_SelectionChanged;
            Search.Click += Search_click;
        }

    }
}
