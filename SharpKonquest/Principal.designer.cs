using System.Windows.Forms;
using System.Windows.FutureStyle.Office2007;
using System.Windows.FutureStyle;
using System.Drawing;
using System;
using System.ComponentModel;
using SharpKonquest.Clases;
namespace SharpKonquest
{
    partial class Principal
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.officeStatusStrip1 = new System.Windows.Office2007.OfficeStatusStrip();
            this.rondaActual = new System.Windows.Forms.ToolStripStatusLabel();
            this.futurePanel1 = new System.Windows.Office2007.BackPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listaMensajes = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mapa = new SharpKonquest.Clases.Mapa();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Info = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imagenPlaneta = new System.Windows.FutureStyle.ReflectionPicture();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listaChat = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.textoChat = new System.Windows.Forms.TextBox();
            this.listaJugadores = new System.Windows.Forms.ComboBox();
            this.bChat = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bFinTurno = new System.Windows.Forms.Button();
            this.bCancelarHerramienta = new System.Windows.Forms.Button();
            this.peticion = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.partidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarNuevaPartidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.guardarpartida = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarPartidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitarTiempoDeTurnoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sinLímiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.segundosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sgeundosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutosToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.dibujarFlotasPropiasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dibujarFlotasEnemigasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.interfazToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porDefectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.office2007ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oscuraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagenDeFondoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ningunaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espacioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.degradadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elegirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarActualizacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.limpiarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.limpiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.peticionTexto = new System.Windows.Forms.TextBox();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.officeStatusStrip1.SuspendLayout();
            this.futurePanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.officeStatusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.futurePanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(817, 532);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(817, 603);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // officeStatusStrip1
            // 
            this.officeStatusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.officeStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rondaActual});
            this.officeStatusStrip1.Location = new System.Drawing.Point(0, 0);
            this.officeStatusStrip1.Name = "officeStatusStrip1";
            this.officeStatusStrip1.Size = new System.Drawing.Size(817, 22);
            this.officeStatusStrip1.TabIndex = 0;
            // 
            // rondaActual
            // 
            this.rondaActual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.rondaActual.Name = "rondaActual";
            this.rondaActual.Size = new System.Drawing.Size(126, 17);
            this.rondaActual.Text = "SharpKonquest 2.0";
            // 
            // futurePanel1
            // 
            this.futurePanel1.BackColor = System.Drawing.Color.Transparent;
            this.futurePanel1.Controls.Add(this.splitContainer1);
            this.futurePanel1.Controls.Add(this.panel1);
            this.futurePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.futurePanel1.DrawShadows = false;
            this.futurePanel1.Location = new System.Drawing.Point(0, 0);
            this.futurePanel1.Name = "futurePanel1";
            this.futurePanel1.ShadowOpacity = ((byte)(0));
            this.futurePanel1.ShadowSize = 0;
            this.futurePanel1.Size = new System.Drawing.Size(817, 532);
            this.futurePanel1.Style = System.Windows.Office2007.BackPanelStyles.Luna;
            this.futurePanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 38);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.splitContainer2_Panel1_Resize);
            this.splitContainer1.Size = new System.Drawing.Size(817, 494);
            this.splitContainer1.SplitterDistance = 413;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.Resize += new System.EventHandler(this.TamañoCambiado);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listaMensajes);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 384);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(413, 110);
            this.panel3.TabIndex = 1;
            // 
            // listaMensajes
            // 
            this.listaMensajes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listaMensajes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.listaMensajes.Location = new System.Drawing.Point(4, 3);
            this.listaMensajes.Name = "listaMensajes";
            this.listaMensajes.Size = new System.Drawing.Size(405, 104);
            this.listaMensajes.TabIndex = 1;
            this.listaMensajes.UseCompatibleStateImageBehavior = false;
            this.listaMensajes.View = System.Windows.Forms.View.Details;
            this.listaMensajes.SizeChanged += new System.EventHandler(this.listaMensajes_SizeChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Turno";
            this.columnHeader1.Width = 43;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Texto";
            this.columnHeader2.Width = 354;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.mapa);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(413, 494);
            this.panel4.TabIndex = 2;
            // 
            // mapa
            // 
            this.mapa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mapa.BackColor = System.Drawing.Color.Transparent;
            this.mapa.ColorFondoCeldas = System.Drawing.Color.Black;
            this.mapa.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.mapa.ImagenFondo = null;
            this.mapa.Location = new System.Drawing.Point(0, 0);
            this.mapa.Margin = new System.Windows.Forms.Padding(4);
            this.mapa.Name = "mapa";
            this.mapa.Size = new System.Drawing.Size(360, 360);
            this.mapa.TabIndex = 0;
            this.mapa.MouseClicPlaneta += new SharpKonquest.Clases.Mapa.DelegadoPlaneta(this.ClicPlaneta);
            this.mapa.MouseSobrePlaneta += new SharpKonquest.Clases.Mapa.DelegadoPlaneta(this.SobrePlaneta);
            this.mapa.MouseFueraPlaneta += new SharpKonquest.Clases.Mapa.DelegadoPlaneta(this.FueraPlaneta);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.Info);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(403, 494);
            this.splitContainer2.SplitterDistance = 297;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 3;
            // 
            // Info
            // 
            this.Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Info.Location = new System.Drawing.Point(0, 281);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(403, 16);
            this.Info.TabIndex = 0;
            this.Info.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.imagenPlaneta);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(403, 281);
            this.panel2.TabIndex = 4;
            // 
            // imagenPlaneta
            // 
            this.imagenPlaneta.AjustSize = true;
            this.imagenPlaneta.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imagenPlaneta.Image = null;
            this.imagenPlaneta.Location = new System.Drawing.Point(101, 0);
            this.imagenPlaneta.Name = "imagenPlaneta";
            this.imagenPlaneta.OpacityIndex = ((byte)(165));
            this.imagenPlaneta.ReflectDistance = 5;
            this.imagenPlaneta.Size = new System.Drawing.Size(200, 334);
            this.imagenPlaneta.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listaChat);
            this.groupBox1.Controls.Add(this.textoChat);
            this.groupBox1.Controls.Add(this.listaJugadores);
            this.groupBox1.Controls.Add(this.bChat);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 187);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chat";
            // 
            // listaChat
            // 
            this.listaChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listaChat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listaChat.Location = new System.Drawing.Point(6, 46);
            this.listaChat.Name = "listaChat";
            this.listaChat.Size = new System.Drawing.Size(391, 138);
            this.listaChat.TabIndex = 6;
            this.listaChat.UseCompatibleStateImageBehavior = false;
            this.listaChat.View = System.Windows.Forms.View.Details;
            this.listaChat.SizeChanged += new System.EventHandler(this.listaChat_SizeChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Jugador";
            this.columnHeader3.Width = 86;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Texto";
            this.columnHeader4.Width = 321;
            // 
            // textoChat
            // 
            this.textoChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textoChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textoChat.Location = new System.Drawing.Point(133, 19);
            this.textoChat.Name = "textoChat";
            this.textoChat.Size = new System.Drawing.Size(194, 20);
            this.textoChat.TabIndex = 3;
            this.textoChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bChat_KeyDown);
            // 
            // listaJugadores
            // 
            this.listaJugadores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listaJugadores.FormattingEnabled = true;
            this.listaJugadores.Location = new System.Drawing.Point(6, 19);
            this.listaJugadores.Name = "listaJugadores";
            this.listaJugadores.Size = new System.Drawing.Size(121, 21);
            this.listaJugadores.TabIndex = 2;
            // 
            // bChat
            // 
            this.bChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bChat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bChat.Location = new System.Drawing.Point(333, 18);
            this.bChat.Name = "bChat";
            this.bChat.Size = new System.Drawing.Size(64, 23);
            this.bChat.TabIndex = 4;
            this.bChat.Text = "Enviar";
            this.bChat.UseVisualStyleBackColor = true;
            this.bChat.Click += new System.EventHandler(this.bChat_Click);
            this.bChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bChat_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bFinTurno);
            this.panel1.Controls.Add(this.bCancelarHerramienta);
            this.panel1.Controls.Add(this.peticion);
            this.panel1.Controls.Add(this.lblTurno);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(817, 38);
            this.panel1.TabIndex = 3;
            // 
            // bFinTurno
            // 
            this.bFinTurno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bFinTurno.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bFinTurno.Location = new System.Drawing.Point(724, 8);
            this.bFinTurno.Name = "bFinTurno";
            this.bFinTurno.Size = new System.Drawing.Size(81, 23);
            this.bFinTurno.TabIndex = 7;
            this.bFinTurno.Text = "Finalizar turno";
            this.bFinTurno.UseVisualStyleBackColor = true;
            this.bFinTurno.Visible = false;
            this.bFinTurno.Click += new System.EventHandler(this.FinalizarTurno);
            // 
            // bCancelarHerramienta
            // 
            this.bCancelarHerramienta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancelarHerramienta.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancelarHerramienta.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bCancelarHerramienta.Location = new System.Drawing.Point(643, 8);
            this.bCancelarHerramienta.Name = "bCancelarHerramienta";
            this.bCancelarHerramienta.Size = new System.Drawing.Size(75, 23);
            this.bCancelarHerramienta.TabIndex = 6;
            this.bCancelarHerramienta.Text = "Cancelar";
            this.bCancelarHerramienta.UseVisualStyleBackColor = true;
            this.bCancelarHerramienta.Visible = false;
            this.bCancelarHerramienta.Click += new System.EventHandler(this.CancelarHerramienta);
            // 
            // peticion
            // 
            this.peticion.AutoSize = true;
            this.peticion.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peticion.Location = new System.Drawing.Point(34, 10);
            this.peticion.Name = "peticion";
            this.peticion.Size = new System.Drawing.Size(0, 18);
            this.peticion.TabIndex = 1;
            // 
            // lblTurno
            // 
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurno.Location = new System.Drawing.Point(9, 7);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(0, 25);
            this.lblTurno.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.partidaToolStripMenuItem,
            this.opcioToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(817, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // partidaToolStripMenuItem
            // 
            this.partidaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarNuevaPartidaToolStripMenuItem,
            this.toolStripMenuItem5,
            this.guardarpartida,
            this.cargarPartidaToolStripMenuItem});
            this.partidaToolStripMenuItem.Name = "partidaToolStripMenuItem";
            this.partidaToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.partidaToolStripMenuItem.Text = "Partida";
            // 
            // iniciarNuevaPartidaToolStripMenuItem
            // 
            this.iniciarNuevaPartidaToolStripMenuItem.Name = "iniciarNuevaPartidaToolStripMenuItem";
            this.iniciarNuevaPartidaToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.iniciarNuevaPartidaToolStripMenuItem.Text = "Iniciar nueva partida";
            this.iniciarNuevaPartidaToolStripMenuItem.Click += new System.EventHandler(this.NuevaPartida);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(178, 6);
            this.toolStripMenuItem5.Visible = false;
            // 
            // guardarpartida
            // 
            this.guardarpartida.Name = "guardarpartida";
            this.guardarpartida.Size = new System.Drawing.Size(181, 22);
            this.guardarpartida.Text = "Guardar partida";
            this.guardarpartida.Visible = false;
            this.guardarpartida.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // cargarPartidaToolStripMenuItem
            // 
            this.cargarPartidaToolStripMenuItem.Name = "cargarPartidaToolStripMenuItem";
            this.cargarPartidaToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.cargarPartidaToolStripMenuItem.Text = "Cargar partida";
            this.cargarPartidaToolStripMenuItem.Visible = false;
            this.cargarPartidaToolStripMenuItem.Click += new System.EventHandler(this.cargarPartidaToolStripMenuItem_Click);
            // 
            // opcioToolStripMenuItem
            // 
            this.opcioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem,
            this.limitarTiempoDeTurnoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.dibujarFlotasPropiasToolStripMenuItem,
            this.dibujarFlotasEnemigasToolStripMenuItem,
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem,
            this.toolStripMenuItem2,
            this.interfazToolStripMenuItem,
            this.imagenDeFondoToolStripMenuItem,
            this.toolStripMenuItem3,
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem,
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem});
            this.opcioToolStripMenuItem.Name = "opcioToolStripMenuItem";
            this.opcioToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.opcioToolStripMenuItem.Text = "Opciones";
            // 
            // mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem
            // 
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.Checked = true;
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.CheckOnClick = true;
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.Name = "mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem";
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.Text = "Mostrar mensajes al recibir eventos importantes";
            // 
            // limitarTiempoDeTurnoToolStripMenuItem
            // 
            this.limitarTiempoDeTurnoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sinLímiteToolStripMenuItem,
            this.segundosToolStripMenuItem,
            this.sgeundosToolStripMenuItem,
            this.minutosToolStripMenuItem,
            this.minutosToolStripMenuItem1,
            this.minutosToolStripMenuItem2});
            this.limitarTiempoDeTurnoToolStripMenuItem.Name = "limitarTiempoDeTurnoToolStripMenuItem";
            this.limitarTiempoDeTurnoToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.limitarTiempoDeTurnoToolStripMenuItem.Text = "Limitar tiempo de turno";
            this.limitarTiempoDeTurnoToolStripMenuItem.Visible = false;
            this.limitarTiempoDeTurnoToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.limitarTiempoDeTurnoToolStripMenuItem_Click);
            // 
            // sinLímiteToolStripMenuItem
            // 
            this.sinLímiteToolStripMenuItem.Name = "sinLímiteToolStripMenuItem";
            this.sinLímiteToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.sinLímiteToolStripMenuItem.Tag = "-1";
            this.sinLímiteToolStripMenuItem.Text = "Sin límite";
            // 
            // segundosToolStripMenuItem
            // 
            this.segundosToolStripMenuItem.Name = "segundosToolStripMenuItem";
            this.segundosToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.segundosToolStripMenuItem.Tag = "30";
            this.segundosToolStripMenuItem.Text = "30 segundos";
            // 
            // sgeundosToolStripMenuItem
            // 
            this.sgeundosToolStripMenuItem.Name = "sgeundosToolStripMenuItem";
            this.sgeundosToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.sgeundosToolStripMenuItem.Tag = "60";
            this.sgeundosToolStripMenuItem.Text = "1 minuto";
            // 
            // minutosToolStripMenuItem
            // 
            this.minutosToolStripMenuItem.Name = "minutosToolStripMenuItem";
            this.minutosToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.minutosToolStripMenuItem.Tag = "120";
            this.minutosToolStripMenuItem.Text = "2 minutos";
            // 
            // minutosToolStripMenuItem1
            // 
            this.minutosToolStripMenuItem1.Name = "minutosToolStripMenuItem1";
            this.minutosToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.minutosToolStripMenuItem1.Tag = "300";
            this.minutosToolStripMenuItem1.Text = "5 minutos";
            // 
            // minutosToolStripMenuItem2
            // 
            this.minutosToolStripMenuItem2.Name = "minutosToolStripMenuItem2";
            this.minutosToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
            this.minutosToolStripMenuItem2.Tag = "600";
            this.minutosToolStripMenuItem2.Text = "10 minutos";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(323, 6);
            // 
            // dibujarFlotasPropiasToolStripMenuItem
            // 
            this.dibujarFlotasPropiasToolStripMenuItem.Checked = true;
            this.dibujarFlotasPropiasToolStripMenuItem.CheckOnClick = true;
            this.dibujarFlotasPropiasToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dibujarFlotasPropiasToolStripMenuItem.Name = "dibujarFlotasPropiasToolStripMenuItem";
            this.dibujarFlotasPropiasToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.dibujarFlotasPropiasToolStripMenuItem.Text = "Dibujar flotas propias";
            this.dibujarFlotasPropiasToolStripMenuItem.CheckedChanged += new System.EventHandler(this.dibujarFlotasPropiasToolStripMenuItem_Click);
            // 
            // dibujarFlotasEnemigasToolStripMenuItem
            // 
            this.dibujarFlotasEnemigasToolStripMenuItem.Checked = true;
            this.dibujarFlotasEnemigasToolStripMenuItem.CheckOnClick = true;
            this.dibujarFlotasEnemigasToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dibujarFlotasEnemigasToolStripMenuItem.Name = "dibujarFlotasEnemigasToolStripMenuItem";
            this.dibujarFlotasEnemigasToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.dibujarFlotasEnemigasToolStripMenuItem.Text = "Dibujar flotas enemigas";
            this.dibujarFlotasEnemigasToolStripMenuItem.CheckedChanged += new System.EventHandler(this.dibujarFlotasEnemigasToolStripMenuItem_Click);
            // 
            // mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem
            // 
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.CheckOnClick = true;
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.Name = "mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem";
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.Text = "Mostrar destino en la línea de flota";
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.CheckedChanged += new System.EventHandler(this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(323, 6);
            // 
            // interfazToolStripMenuItem
            // 
            this.interfazToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.porDefectoToolStripMenuItem,
            this.office2007ToolStripMenuItem,
            this.oscuraToolStripMenuItem});
            this.interfazToolStripMenuItem.Name = "interfazToolStripMenuItem";
            this.interfazToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.interfazToolStripMenuItem.Text = "Interfaz";
            // 
            // porDefectoToolStripMenuItem
            // 
            this.porDefectoToolStripMenuItem.Name = "porDefectoToolStripMenuItem";
            this.porDefectoToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.porDefectoToolStripMenuItem.Text = "Normal";
            this.porDefectoToolStripMenuItem.Click += new System.EventHandler(this.porDefectoToolStripMenuItem_Click);
            // 
            // office2007ToolStripMenuItem
            // 
            this.office2007ToolStripMenuItem.Name = "office2007ToolStripMenuItem";
            this.office2007ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.office2007ToolStripMenuItem.Text = "Office 2007";
            this.office2007ToolStripMenuItem.Click += new System.EventHandler(this.office2007ToolStripMenuItem_Click);
            // 
            // oscuraToolStripMenuItem
            // 
            this.oscuraToolStripMenuItem.Name = "oscuraToolStripMenuItem";
            this.oscuraToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.oscuraToolStripMenuItem.Text = "Oscura";
            this.oscuraToolStripMenuItem.Click += new System.EventHandler(this.oscuraToolStripMenuItem_Click);
            // 
            // imagenDeFondoToolStripMenuItem
            // 
            this.imagenDeFondoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ningunaToolStripMenuItem,
            this.espacioToolStripMenuItem,
            this.degradadoToolStripMenuItem,
            this.elegirToolStripMenuItem});
            this.imagenDeFondoToolStripMenuItem.Name = "imagenDeFondoToolStripMenuItem";
            this.imagenDeFondoToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.imagenDeFondoToolStripMenuItem.Text = "Imagen de fondo";
            // 
            // ningunaToolStripMenuItem
            // 
            this.ningunaToolStripMenuItem.Name = "ningunaToolStripMenuItem";
            this.ningunaToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.ningunaToolStripMenuItem.Text = "Ninguna";
            this.ningunaToolStripMenuItem.Click += new System.EventHandler(this.ningunaToolStripMenuItem_Click);
            // 
            // espacioToolStripMenuItem
            // 
            this.espacioToolStripMenuItem.Name = "espacioToolStripMenuItem";
            this.espacioToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.espacioToolStripMenuItem.Text = "Espacio";
            this.espacioToolStripMenuItem.Click += new System.EventHandler(this.espacioToolStripMenuItem_Click);
            // 
            // degradadoToolStripMenuItem
            // 
            this.degradadoToolStripMenuItem.Name = "degradadoToolStripMenuItem";
            this.degradadoToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.degradadoToolStripMenuItem.Text = "Degradado";
            this.degradadoToolStripMenuItem.Click += new System.EventHandler(this.degradadoToolStripMenuItem_Click);
            // 
            // elegirToolStripMenuItem
            // 
            this.elegirToolStripMenuItem.Name = "elegirToolStripMenuItem";
            this.elegirToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.elegirToolStripMenuItem.Text = "Elegir...";
            this.elegirToolStripMenuItem.Click += new System.EventHandler(this.elegirToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(323, 6);
            // 
            // alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem
            // 
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.Checked = true;
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.CheckOnClick = true;
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.Name = "alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem";
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.Text = "Alarma al inicio del turno del jugador";
            // 
            // alarmaAlRecibirMensajeDeChatToolStripMenuItem
            // 
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.Checked = true;
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.CheckOnClick = true;
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.Name = "alarmaAlRecibirMensajeDeChatToolStripMenuItem";
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.Text = "Alarma al recibir mensaje de chat";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem,
            this.buscarActualizacionesToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // buscarActualizacionesToolStripMenuItem
            // 
            this.buscarActualizacionesToolStripMenuItem.Name = "buscarActualizacionesToolStripMenuItem";
            this.buscarActualizacionesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.buscarActualizacionesToolStripMenuItem.Text = "Buscar actualizaciones";
            this.buscarActualizacionesToolStripMenuItem.Click += new System.EventHandler(this.buscarActualizacionesToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(97, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "Nue";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(85, 22);
            this.toolStripButton2.Text = "Nueva partida";
            this.toolStripButton2.Click += new System.EventHandler(this.NuevaPartida);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Enabled = false;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton6,
            this.toolStripButton5,
            this.toolStripButton4,
            this.toolStripButton7,
            this.toolStripButton1,
            this.toolStripButton3});
            this.toolStrip2.Location = new System.Drawing.Point(100, 24);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(565, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "Nue";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(78, 22);
            this.toolStripButton6.Text = "Enviar ataque";
            this.toolStripButton6.Click += new System.EventHandler(this.EnviarFlota);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(82, 22);
            this.toolStripButton5.Text = "Medir distancia";
            this.toolStripButton5.Click += new System.EventHandler(this.MedirDistancia);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(115, 22);
            this.toolStripButton4.Text = " Flotas en movimiento";
            this.toolStripButton4.Click += new System.EventHandler(this.MostrarFlotasEnMovimiento);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(80, 22);
            this.toolStripButton7.Text = "Simular Batalla";
            this.toolStripButton7.Click += new System.EventHandler(this.SimularBatalla);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(67, 22);
            this.toolStripButton1.Text = "Estadísticas";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(131, 22);
            this.toolStripButton3.Text = " Calculadora de Windows";
            this.toolStripButton3.Click += new System.EventHandler(this.IniciarCalculadora);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.limpiarToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(115, 26);
            // 
            // limpiarToolStripMenuItem1
            // 
            this.limpiarToolStripMenuItem1.Name = "limpiarToolStripMenuItem1";
            this.limpiarToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.limpiarToolStripMenuItem1.Text = "Limpiar";
            this.limpiarToolStripMenuItem1.Click += new System.EventHandler(this.limpiarToolStripMenuItem1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.limpiarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(115, 26);
            // 
            // limpiarToolStripMenuItem
            // 
            this.limpiarToolStripMenuItem.Name = "limpiarToolStripMenuItem";
            this.limpiarToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.limpiarToolStripMenuItem.Text = "Limpiar";
            this.limpiarToolStripMenuItem.Click += new System.EventHandler(this.limpiarToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.SegundoTranscurrido);
            // 
            // peticionTexto
            // 
            this.peticionTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peticionTexto.Location = new System.Drawing.Point(0, 0);
            this.peticionTexto.Name = "peticionTexto";
            this.peticionTexto.Size = new System.Drawing.Size(150, 20);
            this.peticionTexto.TabIndex = 0;
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(181, 6);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "skx";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Partidas de SharpKonquest|*.skx|Todos los archivos|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "skx";
            this.saveFileDialog1.Filter = "Partidas de SharpKonquest|*.skx|Todos los archivos|*.*";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "Imágenes|*.jpg;*.bmp;*.jpeg;*.png;*.bmp|Todos los archivos|*.*";
            // 
            // Principal
            // 
            this.CancelButton = this.bCancelarHerramienta;
            this.ClientSize = new System.Drawing.Size(817, 603);
            this.Controls.Add(this.toolStripContainer1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Principal";
            this.Text = "SharpKonquest 2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Cerrar);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.officeStatusStrip1.ResumeLayout(false);
            this.officeStatusStrip1.PerformLayout();
            this.futurePanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Office2007.BackPanel futurePanel1;
        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem partidaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarNuevaPartidaToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.Label peticion;
        private System.Windows.Forms.Button bFinTurno;
        private System.Windows.Forms.Button bCancelarHerramienta;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private SharpKonquest.Clases.Mapa mapa;
        private System.Windows.FutureStyle.ReflectionPicture imagenPlaneta;
        private System.Windows.Forms.ToolStripMenuItem opcioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limitarTiempoDeTurnoToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarActualizacionesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem limpiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dibujarFlotasPropiasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dibujarFlotasEnemigasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sinLímiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem segundosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sgeundosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem minutosToolStripMenuItem2;
        private System.Windows.Forms.TextBox textoChat;
        private System.Windows.Forms.Button bChat;
        private System.Windows.Forms.ComboBox listaJugadores;
        private System.Windows.Forms.ToolStripMenuItem mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem;
        private System.Windows.Office2007.OfficeStatusStrip officeStatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel rondaActual;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem interfazToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porDefectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem office2007ToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem alarmaAlRecibirMensajeDeChatToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem limpiarToolStripMenuItem1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem guardarpartida;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem cargarPartidaToolStripMenuItem;
        private Panel panel4;
        private ToolStripMenuItem imagenDeFondoToolStripMenuItem;
        private ToolStripMenuItem ningunaToolStripMenuItem;
        private ToolStripMenuItem espacioToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem elegirToolStripMenuItem;
        private OpenFileDialog openFileDialog2;
        private ToolStripMenuItem degradadoToolStripMenuItem;
        private ToolStripMenuItem mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem;
        private SplitContainer splitContainer2;
        private ToolStripMenuItem oscuraToolStripMenuItem;
        private ListView listaMensajes;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ListView listaChat;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
    }
}
