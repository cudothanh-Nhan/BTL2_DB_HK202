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
        private Account account;
        private List<ProductDisplay> getAllProduct(List<Book> bookList, List<Magazine> magaList)
        {
            List<ProductDisplay> productList = new List<ProductDisplay>();
            foreach (var i in bookList)
            {
                ProductDisplay product = new ProductDisplay(i.Name, i.ImgUrl, i.Price, i.Quantiy, i.Id, "Book", i.PublishYear);
                productList.Add(product);
            }
            foreach (var i in magaList)
            {
                ProductDisplay product = new ProductDisplay(i.SeriName.Name + " No." + i.No, i.ImgUrl, i.Price, i.Quantiy, i.Id, "Magazine", i.PublishDate);
                productList.Add(product);
            }
            return productList;
        }
        private Grid getProUI(List<ProductDisplay> productList)
        {
            Thickness defaultPadding = new Thickness(5, 0, 0, 0);
            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 0, 0, 0);
            int proCount = productList.Count;
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
            rowDef[1].Height = new GridLength(23);
            rowDef[2].Height = new GridLength(17);
            foreach (var r in rowDef) grid.RowDefinitions.Add(r);

            for (int i = 0; i < productList.Count; i++)
            {
                Image img = getImage(productList[i].ImgUrl);
                img.Tag = productList[i].Id;

                TextBlock proName = new TextBlock();
                proName.Tag = productList[i].Id;
                proName.Text = productList[i].Name;
                proName.FontSize = 20;
                proName.VerticalAlignment = VerticalAlignment.Top;
                //proName.FontWeight = FontWeights.Bold;
                proName.Padding = defaultPadding;
                proName.TextWrapping = TextWrapping.Wrap;
                proName.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#333333"));

                TextBlock proQuantity = new TextBlock();
                proQuantity.Text = (productList[i].Quantity > 0) ? "In stock" : "Out of stock";
                proQuantity.Padding = defaultPadding;
                proQuantity.TextAlignment = TextAlignment.Center;
                proQuantity.Background = (productList[i].Quantity > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
                proQuantity.FontSize = 14;

                TextBlock proPrice = new TextBlock();
                proPrice.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", productList[i].Price) + "đ";
                proPrice.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF8C00"));
                proPrice.Padding = defaultPadding;
                proPrice.FontSize = 18;
                proPrice.FontWeight = FontWeights.Medium;
;

                TextBlock separator = new TextBlock();
                separator.Width = 0.4;
                separator.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));

                Grid.SetRowSpan(separator, 3);
                Grid.SetColumn(separator, 3 * i + 2);

                Grid.SetRowSpan(img, 3);
                Grid.SetColumn(img, 3 * i);

                Grid.SetRow(proName, 0);
                Grid.SetColumn(proName, 3 * i + 1);

                Grid.SetRow(proPrice, 1);
                Grid.SetColumn(proPrice, 3 * i + 1);
                Grid.SetRow(proQuantity, 2);
                Grid.SetColumn(proQuantity, 3 * i + 1);

                img.PreviewMouseDown += getDetail;
                proName.PreviewMouseDown += getDetail;

                grid.Children.Add(img);
                grid.Children.Add(proName);
                grid.Children.Add(proPrice);
                grid.Children.Add(proQuantity);
                grid.Children.Add(separator);
            }
            return grid;
        }

        private void getallProUI(List<ProductDisplay> proList, string proType)
        {
            List<ProductDisplay> proDisplay = new List<ProductDisplay>();

            foreach (var i in proList)
            {
                if (proType != "All")
                {
                    if (i.Type == proType)
                    {
                        proDisplay.Add(i);
                    }
                }
                else proDisplay.Add(i);
            }

            double row = Math.Ceiling(proDisplay.Count / 4.0);
            for (int i = 0; i < row; i++)
            {
                List<ProductDisplay> proListOnRow = new List<ProductDisplay>();

                for (int j = 0; j < 4; j++)
                {
                    if (4 * i + j < proDisplay.Count)
                    {
                        proListOnRow.Add(proDisplay[4 * i + j]);
                    }
                }

                Separator separator = new Separator();
                separator.HorizontalAlignment = HorizontalAlignment.Left;
                separator.Width = (i != row - 1) ? 795 : -2.5 + 200 * ((proDisplay.Count % 4 == 0) ? 4 : (proDisplay.Count % 4));

                DisplayBookUI.Children.Add(getProUI(proListOnRow));
                DisplayBookUI.Children.Add(separator);
            }
        }
        private string search_string = "";
        private void Instock_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            List<ProductDisplay> proList = new List<ProductDisplay>();

            proList = getAllProduct(bookToSearch, magaToSearch);
            switch (ProSort.SelectedIndex)
            {
                case 0:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p2.Date.CompareTo(p1.Date);
                        }
                    );
                    break;
                case 1:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p1.Date.CompareTo(p2.Date);
                        }
                    );
                    break;
                case 2:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p2.Price.CompareTo(p1.Price);
                        }
                    );
                    break;
                case 3:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p1.Price.CompareTo(p2.Price);
                        }
                    );
                    break;
            }
            getallProUI(proList, proType);
        }

        private void ProType_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            List<ProductDisplay> proList = new List<ProductDisplay>();

            proList = getAllProduct(bookToSearch, magaToSearch);
            switch (ProSort.SelectedIndex)
            {
                case 0:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p2.Date.CompareTo(p1.Date);
                        }
                    );
                    break;
                case 1:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p1.Date.CompareTo(p2.Date);
                        }
                    );
                    break;
                case 2:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p2.Price.CompareTo(p1.Price);
                        }
                    );
                    break;
                case 3:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p1.Price.CompareTo(p2.Price);
                        }
                    );
                    break;
            }
            getallProUI(proList, proType);
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
            List<ProductDisplay> proList = new List<ProductDisplay>();

            proList = getAllProduct(bookToSearch, magaToSearch);
            switch (ProSort.SelectedIndex)
            {
                case 0:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p2.Date.CompareTo(p1.Date);
                        }
                    );
                    break;
                case 1:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p1.Date.CompareTo(p2.Date);
                        }
                    );
                    break;
                case 2:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p2.Price.CompareTo(p1.Price);
                        }
                    );
                    break;
                case 3:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p1.Price.CompareTo(p2.Price);
                        }
                    );
                    break;
            }
            getallProUI(proList, proType);
        }
        private void ProSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            List<ProductDisplay> proList = new List<ProductDisplay>();

            proList = getAllProduct(bookToSearch, magaToSearch);
            switch (ProSort.SelectedIndex)
            {
                case 0:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p2.Date.CompareTo(p1.Date);
                        }
                    );
                    break;
                case 1:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p1.Date.CompareTo(p2.Date);
                        }
                    );
                    break;
                case 2:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p2.Price.CompareTo(p1.Price);
                        }
                    );
                    break;
                case 3:
                    proList.Sort(
                        delegate (ProductDisplay p1, ProductDisplay p2)
                        {
                            return p1.Price.CompareTo(p2.Price);
                        }
                    );
                    break;
            }
            getallProUI(proList, proType);
        }

        private void getDetail(object sender, RoutedEventArgs e)
        {
            var proID = (sender.GetType() ==  typeof(TextBlock)) ? (sender as TextBlock)?.Tag : (sender as Image)?.Tag;
            MessageBox.Show(proID.ToString());
        }

        public MainUIWindow()
        {
            InitializeComponent();
            bll = new MainUIBLL();
            this.account = new Account("Manager", "nhanchodien", "nhanchodien");
            List<Book> bookList = new List<Book>();
            List<Magazine> magaList = new List<Magazine>();
            bookList = bll.getallBookUI();
            magaList = bll.getallMagaUI();
            Separator separator = new Separator();
            separator.Width = 795;
            DisplayBookUI.Children.Add(separator);
            List<ProductDisplay> proList = new List<ProductDisplay>();
            proList = getAllProduct(bookList, magaList);
            proList.Sort(
                delegate (ProductDisplay p1, ProductDisplay p2)
                {
                    return p2.Date.CompareTo(p1.Date);
                }
            );
            if (this.account.Role != "Manager")
            {
                manage.Visibility = Visibility.Hidden;
                manage.IsEnabled = false;
            }
            getallProUI(proList, "All");
            ProType.SelectionChanged += ProType_SelectionChanged;
            Instock.SelectionChanged += Instock_SelectionChanged;
            Search.Click += Search_click;
            ProSort.SelectionChanged += ProSort_SelectionChanged;
            SearchTaskBar.KeyDown += Search_KeyDown;
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search_click(this, new RoutedEventArgs());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertWindow insert = new InsertWindow();
            insert.Show();
            Close();
        }
    }
}
