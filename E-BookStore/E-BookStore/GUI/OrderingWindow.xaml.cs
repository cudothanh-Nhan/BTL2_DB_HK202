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
        private int customerId;
        private Grid getLayout(int orderId, string status, Order order)
        {
            Thickness defaultPadding = new Thickness(10, 0, 0, 0);
            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 10, 0, 10);
            ColumnDefinition[] colDef = new ColumnDefinition[2];
            for (int i = 0; i < 2; i++) colDef[i] = new ColumnDefinition();
         
            colDef[0].Width = new GridLength(150);
            colDef[1].Width = new GridLength(400);
            grid.ColumnDefinitions.Add(colDef[0]);
            grid.ColumnDefinitions.Add(colDef[1]);

            RowDefinition[] rowDef = new RowDefinition[4];
            for (int i = 0; i < 4; i++) rowDef[i] = new RowDefinition();
            rowDef[0].Height = new GridLength(20); ;
            rowDef[2].Height = rowDef[3].Height = new GridLength(30);
            rowDef[1].Height = new GridLength(150);
            foreach (var r in rowDef) grid.RowDefinitions.Add(r);

            TextBlock orderInfo = new TextBlock();
            orderInfo.Text = "Order ID: " + orderId;
            orderInfo.FontSize = 13;

            TextBlock nProduct = new TextBlock();
            nProduct.Text = "There are " + order.ItemsOfOrder.Count + " products";
            nProduct.FontSize = 13;
            nProduct.Padding = defaultPadding;

            Image img = getImage(order.ItemsOfOrder[0].ImgUrl);

            StackPanel productInfo = new StackPanel();
            TextBlock productName = new TextBlock();
            productName.Text = order.ItemsOfOrder[0].Name;
            productName.FontSize = 20;
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
            quantity.FontSize = uPrice.FontSize = productTotal.FontSize = 15;
            productTotal.Padding = defaultPadding;
            productInfo.Children.Add(productName);
            productInfo.Children.Add(quantity);
            productInfo.Children.Add(uPrice);
            productInfo.Children.Add(productTotal);

            TextBlock orderTotal = new TextBlock();
            orderTotal.Text = "Order Total: " + String.Format("{0:n0}", order.Total);
            orderTotal.FontSize = 18;
            orderTotal.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D0011B"));
            orderTotal.Padding = defaultPadding;

            TextBlock detailUrl = new TextBlock();
            detailUrl.Text = "Click for more details";
            detailUrl.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0046AB"));
            detailUrl.TextDecorations = TextDecorations.Underline;
            detailUrl.Padding = defaultPadding;
            detailUrl.FontSize = 13;

            Button cancelButton = new Button();
            if (status == Order.S_CANCELED)
            {
                cancelButton.Content = "Order again";
                cancelButton.Click += OrderAgain_OnClick;
            }
            else if (status == Order.S_COMPLETED)
            {
                cancelButton.Content = "Review";
            }
            else
            {
                cancelButton.Content = "Cancel";
                cancelButton.Click += Cancel_OnClick;
            }
            cancelButton.Tag = order.Id;
            cancelButton.Height = 40;
            cancelButton.FontSize = 15;
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
        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            bll.updateStatus(int.Parse(btn.Tag.ToString()), Order.S_CANCELED);
            reload(Order.S_CANCELED);
            reload(Order.S_ON_CART);
        }
        private void OrderAgain_OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            bll.updateStatus(int.Parse(btn.Tag.ToString()), Order.S_ON_CART);
            reload(Order.S_CANCELED);
            reload(Order.S_ON_CART);
        }
        private void reload(string status)
        {
            List<Order> orderList = bll.getOrder(customerId, status);
            if(status == Order.S_ON_CART)
            {
                onCartStack.Children.Clear();
                foreach (var order in orderList)
                {
                    onCartStack.Children.Add(getLayout(order.Id, status, order));
                    onCartStack.Children.Add(new Separator());
                }
            }
            else if(status == Order.S_CANCELED)
            {
                canceledStack.Children.Clear();
                foreach (var order in orderList)
                {
                    canceledStack.Children.Add(getLayout(order.Id, status, order));
                    canceledStack.Children.Add(new Separator());
                }
            }
        }
        public OrderingWindow()
        {
            InitializeComponent();
            bll = new OrderingBLL(this);
            this.customerId = 0;
            //bll.getOrder(0, Order.S_ON_CART);
            reload(Order.S_ON_CART);
            reload(Order.S_CANCELED);
        }
    }
}
