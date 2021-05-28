using System;
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
        //private Grid getBookUI(List<Book> bookList)
        //{
        //    Thickness defaultPadding = new Thickness(5, 0, 0, 0);
        //    //List<Book> bookList = new List<Book>();
        //    //bookList = bll.getallBookUI();
        //    Grid grid = new Grid();
        //    grid.Margin = new Thickness(0, 5, 0, 5);
        //    ColumnDefinition[] colDef = new ColumnDefinition[2 * bookList.Count];
        //    for (int i = 0; i < 2 * bookList.Count; i++) colDef[i] = new ColumnDefinition();

        //    for (int i = 0; i < 2 * bookList.Count; i++)
        //    {
        //        if (i % 2 == 0)
        //        {
        //            colDef[i].Width = new GridLength(75);
        //        }
        //        else
        //        {
        //            colDef[i].Width = new GridLength(125);
        //        }
        //    }
        //    foreach (var c in colDef)
        //    {
        //        grid.ColumnDefinitions.Add(c);
        //    }

        //    RowDefinition[] rowDef = new RowDefinition[3];
        //    for (int i = 0; i < 3; i++) rowDef[i] = new RowDefinition();
        //    rowDef[0].Height = new GridLength(50);
        //    rowDef[1].Height = rowDef[2].Height = new GridLength(20);
        //    foreach (var r in rowDef) grid.RowDefinitions.Add(r);
        //    for (int i = 0; i < bookList.Count; i++)
        //    {
        //        Image img = getImage(bookList[i].ImgUrl);

        //        TextBlock bookName = new TextBlock();
        //        bookName.Text = bookList[i].Name;
        //        bookName.FontSize = 20;
        //        bookName.FontWeight = FontWeights.Bold;
        //        bookName.Padding = defaultPadding;
        //        bookName.TextWrapping = TextWrapping.Wrap;
        //        //magaName.TextTrimming = TextTrimming.WordEllipsis;
        //        //bookName.Height = 95;
        //        TextBlock bookQuantity = new TextBlock();
        //        bookQuantity.Text = (bookList[i].Quantiy > 0) ? "In stock" : "Out of stock";
        //        bookQuantity.Padding = defaultPadding;
        //        bookQuantity.TextAlignment = TextAlignment.Center;
        //        bookQuantity.Background = (bookList[i].Quantiy > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
        //        TextBlock bookPrice = new TextBlock();
        //        bookPrice.Text = bookList[i].Price.ToString() + " đ";
        //        bookPrice.Padding = defaultPadding;
        //        bookQuantity.FontSize = bookPrice.FontSize = 15;

        //        Grid.SetRowSpan(img, 3);
        //        Grid.SetColumn(img, 2 * i);

        //        Grid.SetRow(bookName, 0);
        //        Grid.SetColumn(bookName, 2 * i + 1);

        //        Grid.SetRow(bookPrice, 1);
        //        Grid.SetColumn(bookPrice, 2 * i + 1);
        //        Grid.SetRow(bookQuantity, 2);
        //        Grid.SetColumn(bookQuantity, 2 * i + 1);

        //        grid.Children.Add(img);
        //        grid.Children.Add(bookName);
        //        grid.Children.Add(bookPrice);
        //        grid.Children.Add(bookQuantity);
        //    }
        //    return grid;
        //}
        //private Grid getMagaUI(List<Magazine> magaList)
        //{
        //    Thickness defaultPadding = new Thickness(5, 0, 0, 0);
        //    //List<Book> bookList = new List<Book>();
        //    //bookList = bll.getallBookUI();
        //    Grid grid = new Grid();
        //    grid.Margin = new Thickness(0, 5, 0, 5);
        //    ColumnDefinition[] colDef = new ColumnDefinition[2 * magaList.Count];
        //    for (int i = 0; i < 2 * magaList.Count; i++) colDef[i] = new ColumnDefinition();
        //    for (int i = 0; i < 2 * magaList.Count; i++)
        //    {
        //        if (i % 2 == 0)
        //        {
        //            colDef[i].Width = new GridLength(75);
        //        }
        //        else
        //        {
        //            colDef[i].Width = new GridLength(125);
        //        }
        //    }
        //    foreach (var c in colDef)
        //    {
        //        grid.ColumnDefinitions.Add(c);
        //    }

        //    RowDefinition[] rowDef = new RowDefinition[3];
        //    for (int i = 0; i < 3; i++) rowDef[i] = new RowDefinition();
        //    rowDef[0].Height = new GridLength(50);
        //    rowDef[1].Height = rowDef[2].Height = new GridLength(20);
        //    foreach (var r in rowDef) grid.RowDefinitions.Add(r);

        //    for (int i = 0; i < magaList.Count; i++)
        //    {
        //        Image img = getImage(magaList[i].ImgUrl);

        //        TextBlock magaName = new TextBlock();
        //        magaName.Text = magaList[i].SeriName.Name + " No." + magaList[i].No;
        //        magaName.FontSize = 20;
        //        magaName.FontWeight = FontWeights.Bold;
        //        magaName.Padding = defaultPadding;
        //        magaName.TextWrapping = TextWrapping.Wrap;
        //        //magaName.TextTrimming = TextTrimming.WordEllipsis;
        //        //bookName.Height = 95;
        //        TextBlock magaQuantity = new TextBlock();
        //        magaQuantity.Text = (magaList[i].Quantiy > 0) ? "In stock" : "Out of stock";
        //        magaQuantity.Padding = defaultPadding;
        //        magaQuantity.TextAlignment = TextAlignment.Center;
        //        magaQuantity.Background = (magaList[i].Quantiy > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
        //        TextBlock magaPrice = new TextBlock();
        //        magaPrice.Text = magaList[i].Price.ToString() + " đ";
        //        magaPrice.Padding = defaultPadding;
        //        magaQuantity.FontSize = magaPrice.FontSize = 15;

        //        Grid.SetRowSpan(img, 3);
        //        Grid.SetColumn(img, 2 * i);

        //        Grid.SetRow(magaName, 0);
        //        Grid.SetColumn(magaName, 2 * i + 1);

        //        Grid.SetRow(magaPrice, 1);
        //        Grid.SetColumn(magaPrice, 2 * i + 1);
        //        Grid.SetRow(magaQuantity, 2);
        //        Grid.SetColumn(magaQuantity, 2 * i + 1);

        //        grid.Children.Add(img);
        //        grid.Children.Add(magaName);
        //        grid.Children.Add(magaPrice);
        //        grid.Children.Add(magaQuantity);
        //    }
        //    return grid;
        //}
        private Grid getProUI(List<Book> bookList, List<Magazine> magaList, string proType)
        {
            Thickness defaultPadding = new Thickness(5, 0, 0, 0);
            //List<Book> bookList = new List<Book>();
            //bookList = bll.getallBookUI();
            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 5, 0, 5);
            int proCount = 0;
            switch (proType)
            {
                case "Book": proCount = bookList.Count; break;
                case "Magazine": proCount = magaList.Count; break;
                case "All": proCount = bookList.Count + magaList.Count; break;
            }
            ColumnDefinition[] colDef = new ColumnDefinition[2 * proCount];
            for (int k = 0; k < 2 * proCount; k++) colDef[k] = new ColumnDefinition();
            for (int k = 0; k < 2 * proCount; k++)
            {
                if (k % 2 == 0)
                {
                    colDef[k].Width = new GridLength(75);
                }
                else
                {
                    colDef[k].Width = new GridLength(125);
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

                        TextBlock magaPrice = new TextBlock();
                        magaPrice.Text = magaList[i].Price.ToString() + " đ";
                        magaPrice.Padding = defaultPadding;
                        magaQuantity.FontSize = magaPrice.FontSize = 15;

                        Grid.SetRowSpan(img, 3);
                        Grid.SetColumn(img, 2 * i);

                        Grid.SetRow(magaName, 0);
                        Grid.SetColumn(magaName, 2 * i + 1);

                        Grid.SetRow(magaPrice, 1);
                        Grid.SetColumn(magaPrice, 2 * i + 1);
                        Grid.SetRow(magaQuantity, 2);
                        Grid.SetColumn(magaQuantity, 2 * i + 1);

                        grid.Children.Add(img);
                        grid.Children.Add(magaName);
                        grid.Children.Add(magaPrice);
                        grid.Children.Add(magaQuantity);
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
                        //magaName.TextTrimming = TextTrimming.WordEllipsis;
                        //bookName.Height = 95;
                        TextBlock bookQuantity = new TextBlock();
                        bookQuantity.Text = (bookList[i].Quantiy > 0) ? "In stock" : "Out of stock";
                        bookQuantity.Padding = defaultPadding;
                        bookQuantity.TextAlignment = TextAlignment.Center;
                        bookQuantity.Background = (bookList[i].Quantiy > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
                        TextBlock bookPrice = new TextBlock();
                        bookPrice.Text = bookList[i].Price.ToString() + " đ";
                        bookPrice.Padding = defaultPadding;
                        bookQuantity.FontSize = bookPrice.FontSize = 15;

                        Grid.SetRowSpan(img, 3);
                        Grid.SetColumn(img, 2 * i);

                        Grid.SetRow(bookName, 0);
                        Grid.SetColumn(bookName, 2 * i + 1);

                        Grid.SetRow(bookPrice, 1);
                        Grid.SetColumn(bookPrice, 2 * i + 1);
                        Grid.SetRow(bookQuantity, 2);
                        Grid.SetColumn(bookQuantity, 2 * i + 1);

                        grid.Children.Add(img);
                        grid.Children.Add(bookName);
                        grid.Children.Add(bookPrice);
                        grid.Children.Add(bookQuantity);
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
                        //magaName.TextTrimming = TextTrimming.WordEllipsis;
                        //bookName.Height = 95;
                        TextBlock bookQuantity = new TextBlock();
                        bookQuantity.Text = (bookList[i].Quantiy > 0) ? "In stock" : "Out of stock";
                        bookQuantity.Padding = defaultPadding;
                        bookQuantity.TextAlignment = TextAlignment.Center;
                        bookQuantity.Background = (bookList[i].Quantiy > 0) ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FF00")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));
                        TextBlock bookPrice = new TextBlock();
                        bookPrice.Text = bookList[i].Price.ToString() + " đ";
                        bookPrice.Padding = defaultPadding;
                        bookQuantity.FontSize = bookPrice.FontSize = 15;

                        Grid.SetRowSpan(img, 3);
                        Grid.SetColumn(img, 2 * i);

                        Grid.SetRow(bookName, 0);
                        Grid.SetColumn(bookName, 2 * i + 1);

                        Grid.SetRow(bookPrice, 1);
                        Grid.SetColumn(bookPrice, 2 * i + 1);
                        Grid.SetRow(bookQuantity, 2);
                        Grid.SetColumn(bookQuantity, 2 * i + 1);

                        grid.Children.Add(img);
                        grid.Children.Add(bookName);
                        grid.Children.Add(bookPrice);
                        grid.Children.Add(bookQuantity);
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

                        TextBlock magaPrice = new TextBlock();
                        magaPrice.Text = magaList[index].Price.ToString() + " đ";
                        magaPrice.Padding = defaultPadding;
                        magaQuantity.FontSize = magaPrice.FontSize = 15;

                        Grid.SetRowSpan(img, 3);
                        Grid.SetColumn(img, 2 * i);

                        Grid.SetRow(magaName, 0);
                        Grid.SetColumn(magaName, 2 * i + 1);

                        Grid.SetRow(magaPrice, 1);
                        Grid.SetColumn(magaPrice, 2 * i + 1);
                        Grid.SetRow(magaQuantity, 2);
                        Grid.SetColumn(magaQuantity, 2 * i + 1);

                        grid.Children.Add(img);
                        grid.Children.Add(magaName);
                        grid.Children.Add(magaPrice);
                        grid.Children.Add(magaQuantity);
                    }
                    break;
            }
            return grid;
        }
        //private void getallBookUI(List<Book> bookList)
        //{
        //    double row = Math.Ceiling(bookList.Count / 4.0);
        //    for (int i = 0; i < row; i++)
        //    {
        //        List<Book> bookListOnRow = new List<Book>();
        //        for (int j = 0; j < 4; j++)
        //        {
        //            if (4 * i + j < bookList.Count)
        //            {
        //                bookListOnRow.Add(bookList[4 * i + j]);
        //            }
        //        }
        //        DisplayBookUI.Children.Add(getBookUI(bookListOnRow));
        //        DisplayBookUI.Children.Add(new Separator());
        //    }
        //}
        //private void getallMagaUI(List<Magazine> magaList)
        //{
        //    double row = Math.Ceiling(magaList.Count / 4.0);
        //    for (int i = 0; i < row; i++)
        //    {
        //        List<Magazine> magaListOnRow = new List<Magazine>();
        //        for (int j = 0; j < 4; j++)
        //        {
        //            if (4 * i + j < magaList.Count)
        //            {
        //                magaListOnRow.Add(magaList[4 * i + j]);
        //            }
        //        }
        //        DisplayBookUI.Children.Add(getMagaUI(magaListOnRow));
        //        DisplayBookUI.Children.Add(new Separator());

        //    }
        //}
        
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
                DisplayBookUI.Children.Add(getProUI(bookListOnRow, magaListOnRow, proType));
                DisplayBookUI.Children.Add(new Separator());
            }
        }
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
            getallProUI(bookToDisplay, magaToDisplay, proType);
            //MessageBox.Show(Instock.SelectedItem.ToString());
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
            getallProUI(bookToDisplay, magaToDisplay, proType);

            //MessageBox.Show(Instock.SelectedItem.ToString());
        }


        public MainUIWindow()
        {
            InitializeComponent();
            bll = new MainUIBLL();
            List<Book> bookList = new List<Book>();
            bookList = bll.getallBookUI();
            List<Magazine> magaList = new List<Magazine>();
            magaList = bll.getallMagaUI();
            getallProUI(bookList, magaList, "All");
            ProType.SelectionChanged += ProType_SelectionChanged;
            Instock.SelectionChanged += Instock_SelectionChanged;
            ngulon.PreviewMouseDown += Ngulon_PreviewMouseDown;
        }

        private void Ngulon_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DetailWindow detail = new DetailWindow(2, 1);
            detail.Show();
        }
    }
}
