using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

namespace AppKeyVault
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Necesitamos crear opciones de como recuperar la clave
            //Debemos indicar el numero de reintentos antes de un error...
            SecretClientOptions options = new SecretClientOptions
            {
                Retry =
                {
                    Delay = TimeSpan.FromSeconds(3), //Tiempo para cada reintento
                    MaxDelay = TimeSpan.FromSeconds(15),//Maximo tiempo de retraso que va a ver
                    MaxRetries= 5, //Maximo de intentos si no da la clave
                    Mode = RetryMode.Exponential //exponencial significa que no es fijo, que depende de Azure
                }
            };

            string urlKeyVault = "https://mykeyvaultaie.vault.azure.net/"; //url de nuestro keyvault en azure
            Uri uri = new Uri(urlKeyVault);

            //Como estoy validado con este equipo puedo decirle que use la de este equipo (DefaultAzureCredential), sino habria que usar un token
            SecretClient client = new SecretClient(uri, new DefaultAzureCredential(), options);
            KeyVaultSecret secret = client.GetSecret("passwordchampions");
            string value = secret.Value;
            this.label1.Text = value;

        }
    }
}