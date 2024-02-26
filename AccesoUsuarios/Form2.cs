using Newtonsoft.Json.Linq;

namespace AccesoUsuarios

{
    public partial class Form2 : Form
    {
        private string adminUsername;
        private string adminPasswordHash;

        public Form2()
        {
            InitializeComponent();
            LeerConfiguracionAdmin();
        }

        private void LeerConfiguracionAdmin()
        {
            string settingsJson = File.ReadAllText("settings.json");
            JObject settings = JObject.Parse(settingsJson);
            adminUsername = settings["AdminUsername"].ToString();
            adminPasswordHash = settings["AdminPassword"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox3.Text;
            string contraseña = textBox1.Text;
            string contraseñaHashed = Utility.HashPassword(contraseña);

            if (usuario == adminUsername && contraseñaHashed == adminPasswordHash)
            {
                Form3 form3 = new Form3();
                this.Hide();
                form3.Show();
            }
            else
            {
                MessageBox.Show("Credenciales de administrador incorrectas.");
            }
        }
    }
}
