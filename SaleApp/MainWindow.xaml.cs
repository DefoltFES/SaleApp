using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Product> Products { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ListProducts.ItemsSource = Products;
            var list = new List<TypeProduct>();
            list.Add((new TypeProduct()
            {
                name = "Все типы",
                Products = App.dbContext.Products.ToList()
            }));
            list.AddRange(App.dbContext.TypeProducts.ToList());
            Type.ItemsSource = list;
            Sort.ItemsSource = new List<string>() {"По возрастанию", "По убыванию"};

        }

        private void Search_OnTextChanged(object sender, TextChangedEventArgs e)
        {
         
            if(Sort.SelectedItem == "По убыванию")
            {
               var type= Type.SelectedItem as TypeProduct;
               Products = new ObservableCollection<Product>(type.Products.Where(w=>w.name.Contains(Search.Text)).OrderByDescending(s1 => s1.min_price_agent));
               ListProducts.ItemsSource = Products;
            }
            if(Sort.SelectedItem == "По возрастанию")
            {
                var type = Type.SelectedItem as TypeProduct;
                Products = new ObservableCollection<Product>(type.Products.Where(w => w.name.Contains(Search.Text)).OrderBy(s1 => s1.min_price_agent));
                ListProducts.ItemsSource = Products;
            }
         

        }

        private void Sort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Sort.SelectedItem == "По убыванию")
            {
                var type = Type.SelectedItem as TypeProduct;
                Products = new ObservableCollection<Product>(type.Products.Where(w => w.name.Contains(Search.Text)).OrderByDescending(s1 => s1.min_price_agent));
                ListProducts.ItemsSource = Products;
            }
            if (Sort.SelectedItem == "По возрастанию")
            {
                var type = Type.SelectedItem as TypeProduct;
                Products = new ObservableCollection<Product>(type.Products.Where(w => w.name.Contains(Search.Text)).OrderBy(s1 => s1.min_price_agent));
                ListProducts.ItemsSource = Products;
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            EditProductWindow window = new EditProductWindow(new Product());
            if (window.ShowDialog() == true)
            {
                if (Sort.SelectedItem == "По убыванию")
                {
                    var type = Type.SelectedItem as TypeProduct;
                    Products = new ObservableCollection<Product>(type.Products.Where(w => w.name.Contains(Search.Text)).OrderByDescending(s1 => s1.min_price_agent));
                    ListProducts.ItemsSource = Products;
                }
                if (Sort.SelectedItem == "По возрастанию")
                {
                    var type = Type.SelectedItem as TypeProduct;
                    if(type.name=="Все типы")
                    {
                        type.Products = App.dbContext.Products.ToList();
                    }
                    Products = new ObservableCollection<Product>(type.Products.Where(w => w.name.Contains(Search.Text)).OrderBy(s1 => s1.min_price_agent));
                    ListProducts.ItemsSource = Products;
                }
            }

        }

        private void ListProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           var item = ListProducts.SelectedItem as Product;
           if (item != null)
           {
              EditProductWindow window = new EditProductWindow(item,true);
                if (window.ShowDialog() == true)
                {
                    if (Sort.SelectedItem == "По убыванию")
                    {
                        var type = Type.SelectedItem as TypeProduct;
                        Products = new ObservableCollection<Product>(type.Products.Where(w => w.name.Contains(Search.Text)).OrderByDescending(s1 => s1.min_price_agent));
                        ListProducts.ItemsSource = Products;
                    }
                    if (Sort.SelectedItem == "По возрастанию")
                    {
                        var type = Type.SelectedItem as TypeProduct;
                        if (type.name == "Все типы")
                        {
                            type.Products = App.dbContext.Products.ToList();
                        }
                        Products = new ObservableCollection<Product>(type.Products.Where(w => w.name.Contains(Search.Text)).OrderBy(s1 => s1.min_price_agent));
                        ListProducts.ItemsSource = Products;
                    }
                }
            }

        }
    }
}
