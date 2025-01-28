using LogIn_BBDD;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD_2T
{
    public partial class paginaModificar : Page
    {
        public paginaModificar()
        {
            InitializeComponent();
            SQLClass.Instance.listaPrincipal(productsLB, categoriesLB);
            SQLClass.Instance.categoriesList(miCombo);
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SQLClass.Instance.CloseConnection();
            Application.Current.Shutdown();
        }
        private void modificarProducto(object sender, RoutedEventArgs e)
        {
            if (nuevoTB.Text != null && antiguoTB.Text != null)
            {
                string nuevoNombre = nuevoTB.Text;
                string antiguoNombre = antiguoTB.Text;
                string categoryName = miCombo.SelectedItem.ToString();
                SQLClass.Instance.modificarProducto(nuevoNombre, antiguoNombre, categoryName);
                SQLClass.Instance.listaPrincipal(productsLB, categoriesLB);
            }
            else
            {
                MessageBox.Show("Rellene todos los campos");
            }

        }
    }
}
