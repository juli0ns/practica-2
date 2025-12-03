using System;
using System.IO.Ports;
using System.Windows.Forms;


namespace arduinio_2
{
    public partial class Form1 : Form
    {
        SerialPort serialPort;
        bool puertoCerrado = false;
        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            serialPort.PortName = "COM5";
            serialPort.BaudRate = 9600;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (puertoCerrado == false)
            {
                conectar();
            }
            else
            {
                noConectado();
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            this.Invoke(new MethodInvoker(delegate
            {
                string[] values = data.Split('\t');
                if (values.Length == 2)
                {
                    label1.Text = values[1];
                    label2.Text = values[0];
                    listBox1.Items.Add(values[1] + "   " + values[0]);

                }
            }));
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void conectar()
        {

            try
            {
                puertoCerrado = true;
                serialPort.Open();
                button1.Text = "DESCONECTAR";
                button1.BackColor = Color.Black;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);

            }
        }
        private void noConectado()
        {
            try
            {
                puertoCerrado = false;
                serialPort.Close();
                button1.Text = "CONECTAR";
                button1.BackColor = Color.Blue;
                listBox1.Items.Clear();
                label1.Text = "Temperatura °C";
                label2.Text = "Humedad %";

            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

