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
        private Window parent;

        public Order MyOrder { get => myOrder; set => myOrder = value; }

        private Grid getLayout(ItemOfOrder item)
        {
            int itemIndex = MyOrder.ItemsOfOrder.FindIndex(element => element == item);
            string itemString = "MyOrder.ItemsOfOrder[" + itemIndex + "].";
            Thickness defaultPadding = new Thickness(10, 0, 0, 0);
            TextBlock orderTotal = new TextBlock();
            orderTotal.FontSize = 20;
            orderTotal.Padding = new Thickness(0, 0, 0, 10);

            Grid grid = new Grid();
            ColumnDefinition[] colDef = new ColumnDefinition[2];
            colDef[0] = new ColumnDefinition();
            colDef[1] = new ColumnDefinition();
            colDef[0].Width = new GridLength(110);
            colDef[1].Width = new GridLength(400);
            grid.ColumnDefinitions.Add(colDef[0]);
            grid.ColumnDefinitions.Add(colDef[1]);
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new GridLength(110);
            grid.RowDefinitions.Add(rowDef);

            Image img = UIHelper.getImage(item.ImgUrl);

            StackPanel itemInfo = new StackPanel(); ;

            TextBlock itemName = new TextBlock();
            itemName.Text = item.Name;
            itemName.FontSize = 20;
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

            TextBlock remaining = new TextBlock();
            remaining.Text = "Remaining 10 products";
            remaining.Foreground = Brushes.Red;
            remaining.FontStyle = FontStyles.Italic;
            remaining.FontSize = 13;

            quantityStack.Children.Add(quantity);
            quantityStack.Children.Add(remaining);

            TextBlock uPrice = new TextBlock();
            uPrice.Text = "Unit Price: " + String.Format("{0:n0}", item.UPrice);

            TextBlock productTotal = new TextBlock();
            productTotal.SetBinding(TextBlock.TextProperty, 
                UIHelper.getBinding(itemString + "Total", this, "Product Total: {0:n0}", BindingMode.OneWay));
            productTotal.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EDA500"));

            itemName.Padding = uPrice.Padding = productTotal.Padding = remaining.Padding = defaultPadding;
            uPrice.FontSize = productTotal.FontSize = 15;
            itemInfo.Children.Add(itemName);
            itemInfo.Children.Add(quantityStack);
            itemInfo.Children.Add(uPrice);
            itemInfo.Children.Add(productTotal);

            Grid.SetColumn(img, 0);
            Grid.SetRow(img, 0);

            Grid.SetColumn(itemInfo, 1);
            Grid.SetRow(itemInfo, 0);

            grid.Children.Add(img);
            grid.Children.Add(itemInfo);
            return grid;
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
                if(quantityVal <= 0)
                {
                    MessageBox.Show("Quantity must be greater than 0");
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
        public void reload()
        {
            foreach (var item in MyOrder.ItemsOfOrder)
                ItemStack.Children.Add(getLayout(item));
        }
        public OrderDetailWindow(Window parent, Order order)
        {
            InitializeComponent();
            this.MyOrder = new Order(order);
            this.preOrder = order;
            this.parent = parent;
            TotalField.SetBinding(TextBlock.TextProperty, UIHelper.getBinding("MyOrder.Total", this, "Total: {0:n0}", BindingMode.OneWay));
            reload();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.parent.Show();
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.preOrder.Copy(MyOrder);
            SaveButton.IsEnabled = false;
        }
    }
}
