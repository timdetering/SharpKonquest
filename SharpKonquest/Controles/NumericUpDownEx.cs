using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public partial class NumericUpDownEx : UserControl
    {
        private int valor = 0;
        private int minimo = 0;
        private int maximo = 100;

        public NumericUpDownEx()
        {
            InitializeComponent();

            this.Height = textBox1.Height;
            textBox1.Text = valor.ToString();
            textBox1.TextChanged += textBox1_TextChanged;
        }

        public event EventHandler ValueChanged;

        public int Maximum
        {
            get { return maximo; }
            set
            {
                maximo = value;
                vScrollBar1.Maximum = value;
                if (Value > value)
                    Value = value;
            }
        }

        public int Minimum
        {
            get { return minimo; }
            set
            {
                minimo = value;
                vScrollBar1.Minimum = value;
                if (Value < value)
                    Value = value;
            }
        }

        public int Value
        {
            get { return valor; }
            set
            {
                if (value < Minimum)
                {
                    valor = Minimum;
                    ValorCambiado();
                }
                else if (value > Maximum)
                {
                    valor = Maximum;
                    ValorCambiado();
                }
                else if (value != valor)
                {
                    valor = value;
                    vScrollBar1.Value = valor;
                    ValorCambiado();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int valor;
                if (int.TryParse(textBox1.Text, out valor))
                    Value = valor;
                else
                    Value = vScrollBar1.Minimum;
            }
            catch
            {
                Value = maximo;
            }
        }

        void TeclaPulsadaEnTextBoxNumerico(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                if (e.KeyChar != 8)
                    e.Handled = true;
            }
        }

        private void NumericUpDownEx_Resize(object sender, EventArgs e)
        {
            this.Height = textBox1.Height;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && vScrollBar1.Value < Maximum)
                Value++;
            else if (e.KeyCode == Keys.Down && vScrollBar1.Value > Minimum)
                Value--;
        }

        private void ValorCambiado(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.SmallDecrement)
                Value++;
            else if (e.Type == ScrollEventType.SmallIncrement)
                Value--;
        }


        void ValorCambiado()
        {
            textBox1.TextChanged -= textBox1_TextChanged;

            textBox1.Text = valor.ToString();
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);

            textBox1.TextChanged += textBox1_TextChanged;
        }
    }
}
