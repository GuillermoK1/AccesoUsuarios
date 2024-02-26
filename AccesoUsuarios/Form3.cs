using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json.Linq;

namespace AccesoUsuarios
{
    public partial class Form3 : Form
    {
        private string connectionString;

        public Form3()
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
            string contraseñaHashed = Utility.HashPassword(contraseña);

            if (RegistrarUsuario(usuario, contraseñaHashed))
            {
                MessageBox.Show("Usuario registrado con éxito.");
                this.Close();
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                MessageBox.Show("Error al registrar el usuario.");
            }
        }

        private bool RegistrarUsuario(string usuario, string contraseñaHashed)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO USUARIOS (NOMBRE_USUARIO, CONTRASEÑA) VALUES (:usuario, :contraseñaHashed)";
                OracleCommand command = new OracleCommand(query, connection);
                command.Parameters.Add(new OracleParameter("usuario", usuario));
                command.Parameters.Add(new OracleParameter("contraseñaHashed", contraseñaHashed));

                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
