using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TestApi
{
    public partial class Form1 : Form
    {
        static string nombre = "";
        static string precio = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetApiDataAsync(textBox1.Text);
        }

        private static readonly HttpClient client = new HttpClient();

        private static async Task GetApiDataAsync(String codigo)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost/apis/productos.php?codigo=" + codigo);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                Producto producto = JsonConvert.DeserializeObject<Producto>(responseBody);

                label2.Text = "Resultado: " + producto.nombre_producto + " " + producto.precio;

                richTextBox1.Clear();
                RichTextBox.AppendText("Nombre: " + producto.nombre_producto + "\n" + "Precio: " + producto.precio);

                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(producto.nombre_producto, producto.precio);

            }
            catch (HttpRequestException e)
            {

                Console.WriteLine($"Request error: {e.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    public class Producto
    {
        public string status { get; set; }
        public string nombre_producto { get; set; }
        public decimal precio { get; set; }
    }
}
