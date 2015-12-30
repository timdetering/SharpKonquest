namespace SharpKonquest
{
    partial class NuevaPartida
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.semillaAleatoria = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bBorrarJugador = new System.Windows.Forms.Button();
            this.bConectar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.color = new System.Windows.Forms.Control();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.IniciarPartida = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.nombreJugador = new System.Windows.Forms.XPTextBox();
            this.host = new System.Windows.Forms.XPTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.semillaMapa = new System.Windows.Forms.NumericUpDown();
            this.neutrales = new System.Windows.Forms.NumericUpDown();
            this.listaJugadores = new System.Windows.Forms.IconListBox();
            this.mapa = new SharpKonquest.Clases.Mapa();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.semillaMapa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neutrales)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Planetas neutrales:";
            // 
            // semillaAleatoria
            // 
            this.semillaAleatoria.Location = new System.Drawing.Point(135, 256);
            this.semillaAleatoria.Name = "semillaAleatoria";
            this.semillaAleatoria.Size = new System.Drawing.Size(71, 23);
            this.semillaAleatoria.TabIndex = 19;
            this.semillaAleatoria.Text = "Aleatorio";
            this.semillaAleatoria.UseVisualStyleBackColor = true;
            this.semillaAleatoria.Click += new System.EventHandler(this.MapaAleatorio);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(9, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Mapa:";
            // 
            // bBorrarJugador
            // 
            this.bBorrarJugador.Location = new System.Drawing.Point(131, 12);
            this.bBorrarJugador.Name = "bBorrarJugador";
            this.bBorrarJugador.Size = new System.Drawing.Size(75, 23);
            this.bBorrarJugador.TabIndex = 15;
            this.bBorrarJugador.Text = "Expulsar";
            this.bBorrarJugador.UseVisualStyleBackColor = true;
            this.bBorrarJugador.Click += new System.EventHandler(this.ExpulsarJugador);
            // 
            // bConectar
            // 
            this.bConectar.Location = new System.Drawing.Point(45, 102);
            this.bConectar.Name = "bConectar";
            this.bConectar.Size = new System.Drawing.Size(123, 37);
            this.bConectar.TabIndex = 14;
            this.bConectar.Text = "Unirse";
            this.bConectar.UseVisualStyleBackColor = true;
            this.bConectar.Click += new System.EventHandler(this.bConectar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Jugadores:";
            // 
            // color
            // 
            this.color.BackColor = System.Drawing.Color.DarkBlue;
            this.color.Location = new System.Drawing.Point(61, 76);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(58, 23);
            this.color.TabIndex = 30;
            this.color.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Color:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Nombre:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Servidor:";
            // 
            // colorDialog1
            // 
            this.colorDialog1.AllowFullOpen = false;
            this.colorDialog1.SolidColorOnly = true;
            // 
            // IniciarPartida
            // 
            this.IniciarPartida.Location = new System.Drawing.Point(57, 288);
            this.IniciarPartida.Name = "IniciarPartida";
            this.IniciarPartida.Size = new System.Drawing.Size(99, 23);
            this.IniciarPartida.TabIndex = 22;
            this.IniciarPartida.Text = "Iniciar partida";
            this.toolTip1.SetToolTip(this.IniciarPartida, "Inicia la partida una vez que se ha creado el servidor y se han unido jugadores");
            this.IniciarPartida.UseVisualStyleBackColor = true;
            this.IniciarPartida.Click += new System.EventHandler(this.ComenzarPartida);
            // 
            // nombreJugador
            // 
            this.nombreJugador.AcceptsReturn = false;
            this.nombreJugador.AcceptsTab = false;
            this.nombreJugador.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.nombreJugador.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.nombreJugador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreJugador.Location = new System.Drawing.Point(61, 47);
            this.nombreJugador.MaxLength = 32767;
            this.nombreJugador.Name = "nombreJugador";
            this.nombreJugador.ReadOnly = false;
            this.nombreJugador.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nombreJugador.Size = new System.Drawing.Size(128, 23);
            this.nombreJugador.TabIndex = 28;
            this.nombreJugador.Text = "Jugador 1";
            this.toolTip1.SetToolTip(this.nombreJugador, "Nombre del jugador que se va a añadir");
            this.nombreJugador.UseSystemPasswordChar = false;
            // 
            // host
            // 
            this.host.AcceptsReturn = false;
            this.host.AcceptsTab = false;
            this.host.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.host.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.host.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.host.Location = new System.Drawing.Point(61, 20);
            this.host.MaxLength = 32767;
            this.host.Name = "host";
            this.host.ReadOnly = false;
            this.host.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.host.Size = new System.Drawing.Size(128, 23);
            this.host.TabIndex = 26;
            this.host.Text = "localhost";
            this.toolTip1.SetToolTip(this.host, "Host de la partida. Si vas a jugar en el mismo ordenador, establece como host el " +
                    "nombre \"localhost\"");
            this.host.UseSystemPasswordChar = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.color);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nombreJugador);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.host);
            this.groupBox1.Controls.Add(this.bConectar);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 142);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unirse a una partida";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.semillaMapa);
            this.groupBox2.Controls.Add(this.neutrales);
            this.groupBox2.Controls.Add(this.IniciarPartida);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.listaJugadores);
            this.groupBox2.Controls.Add(this.bBorrarJugador);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.semillaAleatoria);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 317);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Crear nueva partida";
            // 
            // semillaMapa
            // 
            this.semillaMapa.Location = new System.Drawing.Point(12, 257);
            this.semillaMapa.Maximum = new decimal(new int[] {
            1569325055,
            23283064,
            0,
            0});
            this.semillaMapa.Name = "semillaMapa";
            this.semillaMapa.Size = new System.Drawing.Size(120, 20);
            this.semillaMapa.TabIndex = 24;
            this.semillaMapa.ValueChanged += new System.EventHandler(this.SemillaMapa);
            // 
            // neutrales
            // 
            this.neutrales.Location = new System.Drawing.Point(12, 213);
            this.neutrales.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.neutrales.Name = "neutrales";
            this.neutrales.Size = new System.Drawing.Size(120, 20);
            this.neutrales.TabIndex = 23;
            this.neutrales.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.neutrales.ValueChanged += new System.EventHandler(this.SemillaMapa);
            // 
            // listaJugadores
            // 
            this.listaJugadores.AutoScroll = true;
            this.listaJugadores.BorderBottom = 8;
            this.listaJugadores.BorderLeft = 13;
            this.listaJugadores.BorderRight = 13;
            this.listaJugadores.BorderTop = 8;
            this.listaJugadores.ImageSize = new System.Drawing.Size(32, 32);
            this.listaJugadores.ItemHeight = 25;
            this.listaJugadores.Location = new System.Drawing.Point(9, 39);
            this.listaJugadores.Name = "listaJugadores";
            this.listaJugadores.ProgressBarHeight = 13;
            this.listaJugadores.SelectedIndex = -1;
            this.listaJugadores.SelectedItem = null;
            this.listaJugadores.Size = new System.Drawing.Size(197, 146);
            this.listaJugadores.TabIndex = 12;
            // 
            // mapa
            // 
            this.mapa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mapa.BackColor = System.Drawing.Color.Black;
            this.mapa.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.mapa.Location = new System.Drawing.Point(231, 13);
            this.mapa.Margin = new System.Windows.Forms.Padding(4);
            this.mapa.Name = "mapa";
            this.mapa.Size = new System.Drawing.Size(458, 459);
            this.mapa.TabIndex = 0;
            // 
            // NuevaPartida
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(702, 481);
            this.Controls.Add(this.mapa);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "NuevaPartida";
            this.ShowInTaskbar = false;
            this.Text = "NuevaPartida";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NuevaPartida_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.semillaMapa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neutrales)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button semillaAleatoria;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bBorrarJugador;
        private System.Windows.Forms.Button bConectar;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.IconListBox listaJugadores;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.XPTextBox nombreJugador;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.XPTextBox host;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Control color;
        private System.Windows.Forms.Button IniciarPartida;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.NumericUpDown neutrales;
        public System.Windows.Forms.NumericUpDown semillaMapa;
        public SharpKonquest.Clases.Mapa mapa;
    }
}