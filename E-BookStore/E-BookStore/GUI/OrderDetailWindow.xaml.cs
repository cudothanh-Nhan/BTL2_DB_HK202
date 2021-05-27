using E_BookStore.BLL;
using E_BookStore.DTO;
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

namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for OrderDetailWindow.xaml
    /// </summary>
    public partial class OrderDetailWindow : Window
    {
        private Order myOrder;
        private Order preOrder;
        private OrderingWindow parent;
        private OrderDetailBLL bll;
        public Order MyOrder { get => myOrder; set => myOrder = value; }

        private Grid getLayout(ItemOfOrder item)
        {
            int itemIndex = MyOrder.ItemsOfOrder.FindIndex(element => element == item);
            string itemString = "MyOrder.ItemsOfOrder[" + itemIndex + "].";
            Thickness defaultPadding = new Thickness(10, 5, 0, 0);
            TextBlock orderTotal = new TextBlock();
            orderTotal.FontSize = 20;
            orderTotal.Padding = new Thickness(0, 0, 0, 10);

            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 0, 0, 10);
            ColumnDefinition[] colDef = new ColumnDefinition[2];
            colDef[0] = new ColumnDefinition();
            colDef[1] = new ColumnDefinition();
            colDef[0].Width = new GridLength(110);
            colDef[1].Width = new GridLength(400);
            grid.ColumnDefinitions.Add(colDef[0]);
            grid.ColumnDefinitions.Add(colDef[1]);
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new GridLength(130);
            grid.RowDefinitions.Add(rowDef);

            Image img = UIHelper.getImage(item.ImgUrl);

            StackPanel itemInfo = new StackPanel(); ;

            TextBlock itemName = new TextBlock();
            itemName.Text = item.Name;
            itemName.FontSize = 17;
            itemName.FontWeight = FontWeights.Bold;
            itemName.Padding = defaultPadding;

            StackPanel quantityStack = new StackPanel();
            quantityStack.Orientation = Orientation.Horizontal;

            TextBox quantity = new TextBox();
            quantity.Name = "Quantity" + item.Id;
            quantity.Tag = item.Id;
            quantity.Text = item.Quantity.ToString();
            quantity.Height = 20;
            quantity.Width = 40;
            quantity.TextAlignment = TextAlignment.Center;
            quantity.Margin = defaultPadding;
            quantity.TextChanged += Quantity_TextChange;
            quantity.LostFocus += Quantity_LostFocus;
            if (MyOrder.Status.val != Order.S_ON_CART)
                quantity.IsEnabled = false;

            TextBlock remaining = new TextBlock();
            remaining.SetBinding(TextBlock.TextProperty, UIHelper.getBinding(itemString + "MaxQuantity", 
                        this, "Remaining {0} products", BindingMode.OneWay));
            remaining.Foreground = Brushes.Red;
            remaining.FontStyle = FontStyles.Italic;
            remaining.FontSize = 13;

            quantityStack.Children.Add(quantity);
            if(MyOrder.Status.val == Order.S_ON_CART)
                quantityStack.Children.Add(remaining);

            TextBlock uPrice = new TextBlock();
            uPrice.Text = "Unit Price: " + String.Format("{0:n0}", item.UPrice);

            TextBlock productTotal = new TextBlock();
            productTotal.SetBinding(TextBlock.TextProperty, 
                UIHelper.getBinding(itemString + "Total", this, "Product Total: {0:n0}", BindingMode.OneWay));
            productTotal.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EDA500"));

            Button deleteButton = new Button();
            deleteButton.Tag = item.Id;
            deleteButton.Content = "Delete";
            deleteButton.Width = 50;
            deleteButton.HorizontalAlignment = HorizontalAlignment.Left;
            deleteButton.Margin = new Thickness(10, 5, 5, 5);
            deleteButton.Click += Delete_Onclick;
            
            itemName.Padding = uPrice.Padding = productTotal.Padding = remaining.Padding = defaultPadding;
            uPrice.FontSize = productTotal.FontSize = 15;
            itemInfo.Children.Add(itemName);
            itemInfo.Children.Add(quantityStack);
            itemInfo.Children.Add(uPrice);
            itemInfo.Children.Add(productTotal);
            itemInfo.Children.Add(deleteButton);

            Grid.SetColumn(img, 0);
            Grid.SetRow(img, 0);

            Grid.SetColumn(itemInfo, 1);
            Grid.SetRow(itemInfo, 0);

            grid.Children.Add(img);
            grid.Children.Add(itemInfo);
            return grid;
        }
        private void Quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox quantity = (TextBox)sender;
            if(quantity.Text == string.Empty)
            {
                ItemOfOrder item = preOrder.ItemsOfOrder[MyOrder.ItemsOfOrder.
                    FindIndex(element => element.Id == int.Parse(quantity.Tag.ToString()))];
                quantity.Text = item.Quantity.ToString();
            }
        }
        private void Quantity_TextChange(object sender, RoutedEventArgs e)
        {
            TextBox quantity = (TextBox)sender;
            ItemOfOrder item = MyOrder.ItemsOfOrder[MyOrder.ItemsOfOrder.
                FindIndex(element => element.Id == int.Parse(quantity.Tag.ToString()))];
            try
            {
                if (quantity.Text == string.Empty) return;
                int quantityVal = int.Parse(quantity.Text);
                item.MaxQuantity = this.parent.Bll.getProductQuantity(item.Id);
                if(quantityVal < 0)
                {
                    MessageBox.Show("Quantity must be greater than 0");
                    quantity.Text = "1";
                    return;
                }
                else if(quantityVal > item.MaxQuantity)
                {
                    MessageBox.Show("Not enough product");
                    if (item.MaxQuantity == 0)
                        quantity.Text = "0";
                    else
                        quantity.Text = "1";
                    return;
                }
                MyOrder.Total -= item.Total;
                item.Quantity = int.Parse(quantity.Text);
                
                MyOrder.Total += item.Total;
                if (!this.MyOrder.Equals(this.preOrder))
                    SaveButton.IsEnabled = true;
                else
                    SaveButton.IsEnabled = false;
                Debug.WriteLine(item.Total);
            }
            catch(Exception ex) {
                Debug.WriteLine(ex.ToString());
                quantity.Text = "1";
            }

        }

        private void Delete_Onclick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int itemId = int.Parse(btn.Tag.ToString());
            bll.updateItemQuantity(MyOrder.Id, itemId, 0);
            this.MyOrder.ItemsOfOrder.RemoveAll(element => element.Id == itemId);
            this.preOrder.ItemsOfOrder.RemoveAll(element => element.Id == itemId);
            reload();
        }
        public void reload()
        {
            ItemStack.Children.Clear();
            foreach (var item in MyOrder.ItemsOfOrder)
            {
                ItemStack.Children.Add(getLayout(item));
            }
        }
        public OrderDetailWindow(OrderingWindow parent, Order order)
        {
            InitializeComponent();
            this.bll = new OrderDetailBLL();
            this.MyOrder = new Order(order);
            this.preOrder = order;
            this.parent = parent;
            OrderId.Text = MyOrder.Id.ToString();
            SubmissionTime.Text = MyOrder.Status.submissionTime;
            DeleveringTime.Text = MyOrder.Status.deliveringTime;
            CompletedTime.Text = MyOrder.Status.completedTime;
            CanceledTime.Text = MyOrder.Status.canceledTime;
            if (MyOrder.Status.val != Order.S_ON_CART)
                DeleteButton.IsEnabled = false;
            reload();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.parent.Show();
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            try
            {
                foreach(var item in MyOrder.ItemsOfOrder)
                {
                    bll.updateItemQuantity(MyOrder.Id, item.Id, item.Quantity);
                    item.MaxQuantity = this.parent.Bll.getProductQuantity(item.Id);
                    Debug.WriteLine(item.MaxQuantity);
                }
                this.preOrder.Copy(MyOrder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
