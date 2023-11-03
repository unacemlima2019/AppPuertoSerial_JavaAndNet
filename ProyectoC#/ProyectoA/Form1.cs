using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoA
{
    public partial class Form1 : Form
    {
        string dataOut;
        public Form1()
        {
            InitializeComponent();
        }
    
        private void btnAbrir_Click(object sender, EventArgs e)
        {
      
            try
            {

                serialPort2.PortName = cbxComPort.Text;
                serialPort2.BaudRate = Convert.ToInt32(cbxBaudRate.Text);
                serialPort2.DataBits = Convert.ToInt32(cbxDataBits.Text);
                serialPort2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbxStopBits.Text);
                serialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), cbxPartyBits.Text);
                progressBar1.Value = 100;

                serialPort2.DataReceived += new
                 SerialDataReceivedEventHandler(port_DataReceived);


                serialPort2.Open();
                MessageBox.Show("Conexion Exitosa...");

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(200);
            //MessageBox.Show(serialPort2.ReadExisting());
            txtChat.Text = "\r\n" + serialPort2.ReadExisting();
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cbxComPort.Items.AddRange(ports);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                serialPort2.Close();
                progressBar1.Value = 0;
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                dataOut=txtChat.Text;
                serialPort2.WriteLine(dataOut);
            }
        }

       
    }
}
