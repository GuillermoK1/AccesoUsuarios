using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json.Linq;

namespace AccesoUsuarios
{
    public partial class Form1 : Form
    {
        private string connectionString;

        public Form1()
        {
            InitializeComponent();
            LeerConfiguracion();
        }

        private void LeerConfiguracion()
        {
            string settingsJson = File.ReadAllText("settings.json");
            JObject settings = JObject.Parse(settingsJson);
            connectionString = settings["ConnectionString"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text;
            string contraseña = textBox2.Text;

            if (ValidarUsuario(usuario, contraseña))
            {
                MessageBox.Show("¡Bienvenido!");
                RegistrarAcceso(usuario);
                // Limpiar campos o realizar otras acciones si es necesario
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();
        }

        private bool ValidarUsuario(string usuario, string contraseña)
        {
            string hashedPassword = Utility.HashPassword(contraseña);

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM USUARIOS WHERE NOMBRE_USUARIO = :usuario AND CONTRASEÑA = :hashedPassword";
                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add(new OracleParameter("usuario", usuario));
                command.Parameters.Add(new OracleParameter("hashedPassword", hashedPassword));

                int userCount = Convert.ToInt32(command.ExecuteScalar());
                return userCount > 0;
            }
        }

        private void RegistrarAcceso(string usuario)
        {
            // Implementa la lógica para registrar el acceso en la tabla ACCESOS
        }
    }
}
