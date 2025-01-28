using LogIn_BBDD;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace CRUD_2T
{
    public partial class MainWindow : Window
    {        
        private Window1 window1 = new Window1();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SQLClass.Instance.CloseConnection();
            Application.Current.Shutdown();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            MySqlDataReader mySqlDataReader = null;
            string consulta = $"select * from usuarios where Usuario = '{UserTB.Text}';";
            try
            {
                MySqlConnection connection = SQLClass.Instance.GetConnection();
                MySqlCommand mySqlCommand = new MySqlCommand(consulta, connection);
                mySqlDataReader = mySqlCommand.ExecuteReader();

                if (mySqlDataReader.Read())
                {
                    result = mySqlDataReader.GetString("Contrasena");
                    mySqlDataReader.Close();

                    if (PasswordTB.Password == result)
                    {
                        Window1 window1 = new Window1();
                        window1.Show();
                        Close();
                    }
                    else
                    {
                        errorTB.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    errorTB.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}