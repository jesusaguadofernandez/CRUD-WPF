using LogIn_BBDD;
using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class paginaAgregar : Page
    {
        public paginaAgregar()
        {
            InitializeComponent();
            SQLClass.Instance.listaPrincipal(productsLB, categoriesLB);
            SQLClass.Instance.categoriesList(miCombo);
            productsLB.Loaded += (s, e) => SubscribeToScrollEvents(productsLB, categoriesLB);
            categoriesLB.Loaded += (s, e) => SubscribeToScrollEvents(categoriesLB, productsLB);
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
        private void agregarProducto(object sender, RoutedEventArgs e)
        {
            if (miCombo.SelectedItem != null && (ProductTB.Text != null || ProductTB.Text.Length > 0))
            {
                string categoryName = miCombo.SelectedItem.ToString();
                string productName = ProductTB.Text;
                SQLClass.Instance.agregarProducto(productName, categoryName);
                SQLClass.Instance.listaPrincipal(productsLB, categoriesLB);
            }
            else
            {
                MessageBox.Show("Rellene todos los campos");
            }
            
        }

        //Scroll de ListBox simultáneo
        private void SubscribeToScrollEvents(ListBox listBox, ListBox targetListBox)
        {
            var scrollViewer = GetScrollViewer(listBox);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += (s, e) => SyncScroll(targetListBox, e);
            }
        }

        private void SyncScroll(ListBox targetListBox, ScrollChangedEventArgs e)
        {
            var targetScrollViewer = GetScrollViewer(targetListBox);
            if (targetScrollViewer != null)
            {
                targetScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
                targetScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
            }
        }

        private ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer scrollViewer)
            {
                return scrollViewer;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                var result = GetScrollViewer(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }

}
