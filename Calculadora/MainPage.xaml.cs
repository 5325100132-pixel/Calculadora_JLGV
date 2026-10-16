namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        private double numeroAnterior = 0;
        private double numeroActual = 0;
        private string operador = "";
        private bool esNuevoNumero = true;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnNumeroClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string digito = button.CommandParameter.ToString();

                if (lblPantalla.Text == "0" || esNuevoNumero || lblPantalla.Text == "Error")
                {
                    lblPantalla.Text = digito;
                    esNuevoNumero = false;
                }
                else
                {
                    lblPantalla.Text += digito;
                }
            }
        }

        private void OnPuntoClicked(object sender, EventArgs e)
        {
            
            if (lblPantalla.Text == "Error") return;

            if (esNuevoNumero)
            {
                lblPantalla.Text = "0.";
                esNuevoNumero = false;
            }
            else if (!lblPantalla.Text.Contains("."))
            {
                lblPantalla.Text += ".";
            }
        }

        private void OnOperadorClicked(object sender, EventArgs e)
        {
            if (sender is Button button && lblPantalla.Text != "Error")
            {
                
                if (!string.IsNullOrEmpty(operador) && !esNuevoNumero)
                {
                    OnIgualClicked(sender, e);
                }
                else
                {
                    double.TryParse(lblPantalla.Text, out numeroAnterior);
                }

                operador = button.CommandParameter.ToString();
                esNuevoNumero = true;
            }
        }

        private void OnIgualClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(operador) || lblPantalla.Text == "Error") return;

            double.TryParse(lblPantalla.Text, out numeroActual);
            double resultado = 0;

            switch (operador)
            {
                case "+":
                    resultado = numeroAnterior + numeroActual;
                    break;
                case "-":
                    resultado = numeroAnterior - numeroActual;
                    break;
                case "×":
                    resultado = numeroAnterior * numeroActual;
                    break;
                case "÷":
                    
                    if (numeroActual == 0)
                    {
                        lblPantalla.Text = "Error";
                        operador = "";
                        esNuevoNumero = true;
                        return;
                    }
                    resultado = numeroAnterior / numeroActual;
                    break;
                case "%":
                    resultado = (numeroAnterior * numeroActual) / 100;
                    break;
            }

            lblPantalla.Text = resultado.ToString();
            numeroAnterior = resultado;
            operador = "";
            esNuevoNumero = true;
        }

        private void OnBorrarUltimoClicked(object sender, EventArgs e)
        {
            
            if (lblPantalla.Text != "Error" && !esNuevoNumero)
            {
                string textoActual = lblPantalla.Text;
                if (textoActual.Length > 1)
                {
                    lblPantalla.Text = textoActual[..^1];
                }
                else
                {
                    lblPantalla.Text = "0";
                    esNuevoNumero = true;
                }
            }
        }

        private void OnAcClicked(object sender, EventArgs e)
        {
            
            numeroAnterior = 0;
            numeroActual = 0;
            operador = "";
            lblPantalla.Text = "0";
            esNuevoNumero = true;
        }
    }
}
