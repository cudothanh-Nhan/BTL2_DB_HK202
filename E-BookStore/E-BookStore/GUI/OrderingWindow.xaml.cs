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
using System.Diagnostics;
using MySql.Data.MySqlClient;
using E_BookStore.BLL;
using E_BookStore.DTO;

namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OrderingWindow : Window
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
        private OrderingBLL bll;
        private Grid getLayout(int orderId, string status)
        {
            Order order = bll.getOrder(0, Order.S_ON_CART);

            Thickness defaultPadding = new Thickness(5, 0, 0, 0);
            Grid grid = new Grid();
            ColumnDefinition[] colDef = new ColumnDefinition[2];
            for (int i = 0; i < 2; i++) colDef[i] = new ColumnDefinition();
         
            colDef[0].Width = new GridLength(100);
            colDef[1].Width = new GridLength(300);
            grid.ColumnDefinitions.Add(colDef[0]);
            grid.ColumnDefinitions.Add(colDef[1]);

            RowDefinition[] rowDef = new RowDefinition[4];
            for (int i = 0; i < 4; i++) rowDef[i] = new RowDefinition();
            rowDef[0].Height = rowDef[2].Height = rowDef[3].Height = new GridLength(20);
            rowDef[1].Height = new GridLength(70);
            foreach (var r in rowDef) grid.RowDefinitions.Add(r);

            TextBlock orderInfo = new TextBlock();
            orderInfo.Text = "Order ID: " + orderId;  

            TextBlock nProduct = new TextBlock();
            nProduct.Text = "There are " + order.ItemsOfOrder.Count + " products";

            Image img = getImage("https://salt.tikicdn.com/cache/w444/media/catalog/product/d/a/day-con-lam-giau-tap1a.jpg");

            StackPanel productInfo = new StackPanel();
            TextBlock productName = new TextBlock();
            productName.Text = order.ItemsOfOrder[0].Name;
            productName.FontSize = 13;
            productName.FontWeight = FontWeights.Bold;
            productName.Padding = defaultPadding;
            TextBlock quantity = new TextBlock();
            quantity.Text = "x" + order.ItemsOfOrder[0].Quantity;
            quantity.Padding = defaultPadding;
            TextBlock uPrice = new TextBlock();
            uPrice.Text = "Unit Price: " + order.ItemsOfOrder[0].UPrice;
            uPrice.Padding = defaultPadding;
            TextBlock productTotal = new TextBlock();
            productTotal.Text = "Product Total: " + String.Format("{0:n0}", order.ItemsOfOrder[0].Total);
            productTotal.Padding = defaultPadding;
            productInfo.Children.Add(productName);
            productInfo.Children.Add(quantity);
            productInfo.Children.Add(uPrice);
            productInfo.Children.Add(productTotal);

            TextBlock orderTotal = new TextBlock();
            orderTotal.Text = "Order Total: " + String.Format("{0:n0}", order.Total);
            orderTotal.FontSize = 13;
            orderTotal.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D0011B"));
            orderTotal.Padding = defaultPadding;

            TextBlock detailUrl = new TextBlock();
            detailUrl.Text = "Click for more details";
            detailUrl.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0046AB"));
            detailUrl.TextDecorations = TextDecorations.Underline;
            detailUrl.Padding = defaultPadding;

            Button cancelButton = new Button();
            cancelButton.Content = "Cancel";
            cancelButton.Height = 30;
            cancelButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff6600"));
            cancelButton.Foreground = Brushes.White;

            Grid.SetRow(orderInfo, 0);
            Grid.SetColumn(orderInfo, 0);

            Grid.SetRow(nProduct, 0);
            Grid.SetColumn(nProduct, 1);

            Grid.SetRow(img, 1);
            Grid.SetColumn(img, 0);

            Grid.SetRow(productInfo, 1);
            Grid.SetColumn(productInfo, 1);

            Grid.SetRow(orderTotal, 2);
            Grid.SetColumn(orderTotal, 1);

            Grid.SetRow(detailUrl, 3);
            Grid.SetColumn(detailUrl, 1);

            Grid.SetRow(cancelButton, 2);
            Grid.SetColumn(cancelButton, 0);
            Grid.SetRowSpan(cancelButton, 2);

            grid.Children.Add(orderInfo);
            grid.Children.Add(nProduct);
            grid.Children.Add(img);
            grid.Children.Add(productInfo);
            grid.Children.Add(orderTotal);
            grid.Children.Add(detailUrl);
            grid.Children.Add(cancelButton);
            return grid;
        }
        public OrderingWindow()
        {
            InitializeComponent();
            bll = new OrderingBLL(this);
            //bll.getOrder(0, Order.S_ON_CART);
            onCartStack.Children.Add(getLayout(0, Order.S_ON_CART));
        }
    }
}
