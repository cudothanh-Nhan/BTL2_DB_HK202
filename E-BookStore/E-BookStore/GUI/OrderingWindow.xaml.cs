﻿using System;
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
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OrderingWindow : Window, INotifyPropertyChanged
    {
        
        
        private OrderingBLL bll;
        private Account account;
        private List<Order> orderList;
        List<int> allCustomerId;

        public List<Order> OrderList { get => orderList; set => orderList = value; }
        internal OrderingBLL Bll { get => bll; set => bll = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        private Grid getLayout(int orderId, string status, Order order)
        {
            int orderIndex = this.OrderList.FindIndex(0, element => element == order);
            string orderString = "OrderList[" + orderIndex + "]";
            string itemString = orderString +  ".ItemsOfOrder[0]";
            Thickness defaultLeftPadding = new Thickness(10, 0, 0, 0);
            Thickness defaultRightPadding = new Thickness(0, 0, 10, 0);
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
            nProduct.SetBinding(TextBlock.TextProperty, UIHelper.getBinding(orderString + ".ItemsOfOrder.Count", this, "There are: {0} products", BindingMode.OneWay));
            nProduct.FontSize = 13;
            nProduct.Padding = defaultLeftPadding;

            Image img = UIHelper.getImage(order.ItemsOfOrder[0].ImgUrl);

            StackPanel productInfo = new StackPanel();

            TextBlock productName = new TextBlock();
            productName.Text = order.ItemsOfOrder[0].Name;
            productName.FontSize = 20;
            productName.FontWeight = FontWeights.Bold;

            TextBlock quantity = new TextBlock();
            quantity.SetBinding(TextBlock.TextProperty, UIHelper.getBinding(itemString + ".Quantity", this, "x{0}", BindingMode.OneWay));
            TextBlock uPrice = new TextBlock();
            uPrice.Text = "Unit Price: " + String.Format("{0:n0}", order.ItemsOfOrder[0].UPrice);

            TextBlock productTotal = new TextBlock();
            productTotal.SetBinding(TextBlock.TextProperty, UIHelper.getBinding(itemString + ".Total", this, "Product Total: {0:n0}", BindingMode.OneWay));
            productTotal.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EDA500"));

            quantity.FontSize = uPrice.FontSize = productTotal.FontSize = 15;
            productName.Padding = defaultLeftPadding;
            quantity.Padding = uPrice.Padding = productTotal.Padding  = defaultRightPadding;
            quantity.HorizontalAlignment = uPrice.HorizontalAlignment = productTotal.HorizontalAlignment = HorizontalAlignment.Right;

            productInfo.Children.Add(productName);
            productInfo.Children.Add(quantity);
            productInfo.Children.Add(uPrice);
            productInfo.Children.Add(productTotal);
            if(status == Order.S_ON_CART)
            {
                ComboBox shipSelection = new ComboBox();
                shipSelection.Tag = order.Id;
                shipSelection.Margin = new Thickness(10);
                shipSelection.Width = 200;
                shipSelection.Name = "ShipSelection" + order.Id;
                shipSelection.HorizontalAlignment = HorizontalAlignment.Right;
                shipSelection.SelectionChanged += Shipment_OnSelect;
                List<Shipment> shipList = bll.getAllShipment();
                foreach (var ship in shipList)
                {
                    string op = ship.ShipName + " - " + ship.ShipVal;
                    shipSelection.Items.Add(op);
                }
                productInfo.Children.Add(shipSelection);
            }
            else
            {
                TextBlock shipment = new TextBlock();
                shipment.Text = "Shipment: " + order.ShipName + " - " + String.Format("{0:n0}", order.ShipCash);
                shipment.FontSize = 15;
                shipment.Padding = defaultRightPadding;
                shipment.HorizontalAlignment = HorizontalAlignment.Right;
                productInfo.Children.Add(shipment);
            }

            TextBlock orderTotal = new TextBlock();
            orderTotal.SetBinding(TextBlock.TextProperty, UIHelper.getBinding(orderString + ".Total", this, "Order Total: {0:n0}", BindingMode.OneWay));
            orderTotal.Name = "OrderTotal" + order.Id;
            orderTotal.Tag = order.Total.ToString();
            orderTotal.FontSize = 18;
            orderTotal.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D0011B"));
            orderTotal.Padding = defaultRightPadding;
            orderTotal.HorizontalAlignment = HorizontalAlignment.Right;

            TextBlock detailUrl = new TextBlock();
            detailUrl.Text = "Click for more details";
            detailUrl.Tag = order.Id;
            detailUrl.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0046AB"));
            detailUrl.TextDecorations = TextDecorations.Underline;
            detailUrl.Padding = defaultRightPadding;
            detailUrl.HorizontalAlignment = HorizontalAlignment.Right;
            detailUrl.FontSize = 13;
            detailUrl.PreviewMouseDown += Detail_OnClick;

            Button actionButton = new Button();
            if (status == Order.S_CANCELED)
            {
                actionButton.Content = "Order again";
                actionButton.Click += OrderAgain_OnClick;
            }
            else if (status == Order.S_COMPLETED)
            {
                actionButton.Content = "Review";
            }
            else if(status == Order.S_ON_CART)
            {
               actionButton.Content = "Submit";
               actionButton.Click += Submit_OnClick; 
            }
            else
            {
                actionButton.Content = "Cancel";
                actionButton.Click += Cancel_OnClick;
            }
            actionButton.Tag = order.Id;
            actionButton.Height = 40;
            actionButton.FontSize = 15;
            actionButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff6600"));
            actionButton.Foreground = Brushes.White;

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

            Grid.SetRow(actionButton, 2);
            Grid.SetColumn(actionButton, 0);
            Grid.SetRowSpan(actionButton, 2);

            grid.Children.Add(orderInfo);
            grid.Children.Add(nProduct);
            grid.Children.Add(img);
            grid.Children.Add(productInfo);
            grid.Children.Add(orderTotal);
            grid.Children.Add(detailUrl);
            grid.Children.Add(actionButton);
            return grid;
        }
        private void Detail_OnClick(object sender, RoutedEventArgs e)
        {
            int orderId = int.Parse(((TextBlock)sender).Tag.ToString());
            OrderDetailWindow subwindow = new OrderDetailWindow(this, orderList[OrderList.FindIndex(element => element.Id == orderId)]);
            subwindow.Show();
            this.Hide();
        }
        private void Shipment_OnSelect(object sender, RoutedEventArgs e)
        {
            ComboBox cmbox = (ComboBox)sender;
            string[] parseItem = cmbox.SelectedItem.ToString().Split(" - ", StringSplitOptions.RemoveEmptyEntries);
            int orderId = int.Parse(cmbox.Tag.ToString());
            int orderIndex = OrderList.FindIndex(element => element.Id == orderId);
            OrderList[orderIndex].ShipCash = int.Parse(parseItem[1]);
            Debug.WriteLine(OrderList[orderIndex].Total);
        }
        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var cmBox = UIHelper.FindChild<ComboBox>(Application.Current.MainWindow, "ShipSelection" + btn.Tag.ToString());
            if (cmBox.SelectedItem != null)
            {
                string op = cmBox.SelectedItem.ToString();
                string[] parseItem = op.Split(" - ", StringSplitOptions.RemoveEmptyEntries);
                Shipment ship = new Shipment();
                ship.ShipName = parseItem[0];
                ship.ShipVal = int.Parse(parseItem[1]);
                bll.updateShip(int.Parse(btn.Tag.ToString()), ship);
                bll.updateStatus(int.Parse(btn.Tag.ToString()), Order.S_SUBMITTED);
                reload(Order.S_ON_CART);
                reload(Order.S_SUBMITTED);
            }
            else
                MessageBox.Show("Please choose shipment method");
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
            StackPanel stack = (StackPanel)this.FindName(status + "Stack");

            stack.Children.Clear();
            foreach (var order in orderList)
            {
                if (order.Status.val != status) continue;
                stack.Children.Add(getLayout(order.Id, status, order));
                stack.Children.Add(new Separator());
            }
            
        }
        public void reload()
        {
            reload(Order.S_ON_CART);
            reload(Order.S_CANCELED);
            reload(Order.S_SUBMITTED);
            reload(Order.S_PROCESSING);
            reload(Order.S_DELIVERING);
            reload(Order.S_COMPLETED);
        }
        public OrderingWindow()
        {
            InitializeComponent();
            bll = new OrderingBLL(this);
            this.account = new Account(Account.R_CUSTOMER, "johnwick", 0);
            this.allCustomerId = new List<int>();
            if (this.account.Role == Account.R_CUSTOMER) allCustomerId.Add(this.account.CustomerId);
            this.orderList = new List<Order>();
            foreach(var customerId in allCustomerId)
            {
                orderList.AddRange(bll.getOrder(customerId, Order.S_ON_CART));
                orderList.AddRange(bll.getOrder(customerId, Order.S_CANCELED));
                orderList.AddRange(bll.getOrder(customerId, Order.S_SUBMITTED));
            }
            reload();
        }
    }
}
