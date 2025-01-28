using LogIn_BBDD;
using System.Windows;

namespace CRUD_2T
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            miFrame.NavigationService.Navigate(new paginaInicio());
        }
        private void toInicio(object sender, RoutedEventArgs e) 
        {
            miFrame.NavigationService.Navigate(new paginaInicio());
        }
        private void toAgregar(object sender, RoutedEventArgs e) 
        {
            miFrame.NavigationService.Navigate(new paginaAgregar());
        }
        private void toModificar(object sender, RoutedEventArgs e)
        {
            miFrame.NavigationService.Navigate(new paginaModificar());
        }
        private void toBorrar(object sender, RoutedEventArgs e)
        {
            miFrame.NavigationService.Navigate(new paginaBorrar());
        }

        private void toSalir(object sender, RoutedEventArgs e)
        {
            SQLClass.Instance.CloseConnection();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
