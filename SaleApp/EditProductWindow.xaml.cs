using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace SaleApp
{
    /// <summary>
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        public List<Material> Materials { get; set; }
        public Product Product { get; set; }
        public ObservableCollection<Production> Productions { get; set; }
        public bool IsEdit { get; set; }
        public EditProductWindow(Product product, bool isEdit=false)
        {
            InitializeComponent();
            Product = product;
            type.ItemsSource = App.dbContext.TypeProducts.ToList();
            guild.ItemsSource = App.dbContext.Guilds.ToList();
            Materials = App.dbContext.Materials.ToList();
            BoxColumn.ItemsSource = Materials;
            IsEdit = isEdit;
            if (!IsEdit)
            {
                title.Text = "Создание";
                Delete.Visibility = Visibility.Hidden;
                ImageProduct.Source = (BitmapImage)TryFindResource("DefoltImage");
                Productions=new ObservableCollection<Production>();
                ListMaterials.ItemsSource = Productions;
            }
            if (IsEdit)
            {
                title.Text = "Редактирование";
                Delete.Visibility = Visibility.Visible;
                name.Text = Product.name;
                price.Text = Convert.ToString(Product.min_price_agent);
                countPeople.Text = Convert.ToString(Product.min_amount_people);
                article.Text = Product.article;
                guild.SelectedItem= Product.Guild1;
                type.SelectedItem = Product.TypeProduct;
                Productions = new ObservableCollection<Production>(Product.Productions);
                ListMaterials.ItemsSource = Productions;
                if (Product.image==null || Product.image=="")
                {
                    ImageProduct.Source = (BitmapImage)TryFindResource("DefoltImage");
                }
                else
                {
                    ImageProduct.Source = new BitmapImage(new Uri(Product.image, UriKind.RelativeOrAbsolute));
                }
                
            }
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            var guildProduct= guild.SelectedItem as Guild;
            var typeProdutct= type.SelectedItem as TypeProduct;
            if (guildProduct == null || typeProdutct == null || name.Text=="" || article.Text=="" || price.Text=="")
            {
                if (!IsEdit)
                {
                    if (App.dbContext.Products.Where(x => x.article == article.Text).FirstOrDefault() != null)
                    {
                        MessageBox.Show("Продукт с таким артиклом существует");
                        return;
                    }
                }
                MessageBox.Show("Проверьте заполненность данных");
                return;
            }
            Product.name = name.Text;
            Product.min_price_agent = Convert.ToInt32(price.Text);
            Product.min_amount_people = Convert.ToInt32(countPeople.Text);
            Product.article = article.Text;
            Product.Guild1= guildProduct;
            Product.TypeProduct = typeProdutct;
            Product.Productions.Clear();
            Product.Productions = Productions.Where(x => x.Material != null).ToList();
            if(ImageProduct.Source == (BitmapImage)TryFindResource("DefoltImage"))
            {
                Product.image = null;
            }
            else
            {
                Product.image = ImageProduct.Source.ToString();
            }
            if (!IsEdit)
            {
                App.dbContext.Products.Add(Product);
            }
            
            App.dbContext.SaveChanges();
            DialogResult = true;
        }

        private void AddImage_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image File (*.jpeg;*.jpg;*.bmp;*.png)|*.jpeg; *.jpg;*.bmp;*.png",
                CheckPathExists = true,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ImageProduct.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            }

        }

        private void Article_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Price_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            Productions.Add(new Production());
            ImageProduct.UpdateLayout();
        }

        private void DeleteProduction_OnClick(object sender, RoutedEventArgs e)
        {
            var material = (sender as Button).DataContext as Production;
            Productions.Remove(material);
            ListMaterials.ItemsSource = Productions;
            ListMaterials.UpdateLayout();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delete = MessageBox.Show("Вы действительно хотите удалить продукт ?","Предупреждение",MessageBoxButton.YesNo);
            if (delete == MessageBoxResult.Yes)
            {
                App.dbContext.Products.Remove(Product);
                App.dbContext.SaveChanges();
                DialogResult = true;
            }
        }
    }
}
