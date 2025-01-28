using LogIn_BBDD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LogIn_BBDD
{
    public class SQLClass
    {
        private static SQLClass instance;
        private MySqlConnection conexion;
        private String server = "localhost";
        private String database = "mynorthwind";
        private String user = "root";
        private String password = "19022004";
        private String cadenaConexion;
        
        public SQLClass() {
            cadenaConexion = "Database=" + database + "; DataSource=" +
                server + "; User Id=" + user + "; Password=" + password;
        }
        public static SQLClass Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SQLClass();
                }
                return instance;
            }
        }
        public MySqlConnection GetConnection()
        {
            try
            {
                if (conexion == null || conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion = new MySqlConnection(cadenaConexion);
                    conexion.Open();
                }
                if (conexion.State == System.Data.ConnectionState.Broken)
                {
                    conexion.Close();
                    conexion.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al establecer la conexión: {ex.Message}");
                conexion = null;
            }
            return conexion;
        }
        public void CloseConnection() 
        {
            if (conexion != null) { 
                conexion.Close();
            }
        }
        public void listaPrincipal(ListBox productsLB, ListBox categoriesLB)
        {
            List<string> productos = new List<string>();
            List<string> categorias = new List<string>();
            string consulta = "SELECT ProductName, (SELECT CategoryName FROM categories WHERE CategoryID = products.CategoryID) as CategoryName FROM mynorthwind.products;";
            try
            {
                using (MySqlConnection conexion = SQLClass.Instance.GetConnection())
                {
                    MySqlCommand comando = new MySqlCommand(consulta, conexion);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            productos.Add(lector.GetString("ProductName"));
                            categorias.Add(lector.GetString("CategoryName"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener datos: {ex.Message}");
            }
            productsLB.ItemsSource = productos;
            categoriesLB.ItemsSource = categorias;
        }
        public void categoriesList(ComboBox miCombo)
        {
            List<string> resultados = new List<string>();
            string consulta = "SELECT categoryName FROM mynorthwind.categories;";
            try
            {
                using (MySqlConnection conexion = SQLClass.Instance.GetConnection())
                {
                    MySqlCommand comando = new MySqlCommand(consulta, conexion);
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            resultados.Add(lector.GetString("categoryName"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener datos: {ex.Message}");
            }
            miCombo.ItemsSource = resultados;
        }
        public void agregarProducto(string productName, string categoryName)
        {
            string consulta = "INSERT INTO mynorthwind.products (ProductName, CategoryID) VALUES ('" + productName + "', (SELECT CategoryID FROM categories WHERE CategoryName = '" + categoryName + "'));";
            using (MySqlConnection conexion = SQLClass.Instance.GetConnection())
            {
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
            }
        }
        public void modificarProducto(string nuevoNombre, string antiguoNombre, string categoryName)
        {
            string consulta = "UPDATE mynorthwind.products SET ProductName = '" + nuevoNombre + "' WHERE ProductName = '" + antiguoNombre + "'AND CategoryID = (SELECT CategoryID FROM mynorthwind.categories WHERE CategoryName = '" + categoryName + "');";
            using (MySqlConnection conexion = SQLClass.Instance.GetConnection())
            {
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
            }
        }
        public void borrarProducto(string productName)
        {
            string consulta = "DELETE FROM products WHERE ProductName = '" + productName + "';";
            using (MySqlConnection conexion = SQLClass.Instance.GetConnection())
            {
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
            }
        }
    }
}
