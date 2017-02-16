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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.officeStatusStrip1 = new System.Windows.Office2007.OfficeStatusStrip();
            this.rondaActual = new System.Windows.Forms.ToolStripStatusLabel();
            this.futurePanel1 = new System.Windows.Office2007.BackPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listaMensajes = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.mapa = new SharpKonquest.Clases.Mapa();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Info = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imagenPlaneta = new System.Windows.FutureStyle.ReflectionPicture();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listaChat = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.BottomToolStripPanel, "toolStripContainer1.BottomToolStripPanel");
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.officeStatusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            this.toolStripContainer1.ContentPanel.Controls.Add(this.futurePanel1);
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.LeftToolStripPanel, "toolStripContainer1.LeftToolStripPanel");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.RightToolStripPanel, "toolStripContainer1.RightToolStripPanel");
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.TopToolStripPanel, "toolStripContainer1.TopToolStripPanel");
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // officeStatusStrip1
            // 
            resources.ApplyResources(this.officeStatusStrip1, "officeStatusStrip1");
            this.officeStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rondaActual});
            this.officeStatusStrip1.Name = "officeStatusStrip1";
            // 
            // rondaActual
            // 
            resources.ApplyResources(this.rondaActual, "rondaActual");
            this.rondaActual.Name = "rondaActual";
            // 
            // futurePanel1
            // 
            resources.ApplyResources(this.futurePanel1, "futurePanel1");
            this.futurePanel1.BackColor = System.Drawing.Color.Transparent;
            this.futurePanel1.Controls.Add(this.splitContainer1);
            this.futurePanel1.Controls.Add(this.panel1);
            this.futurePanel1.DrawShadows = false;
            this.futurePanel1.Name = "futurePanel1";
            this.futurePanel1.ShadowOpacity = ((byte)(0));
            this.futurePanel1.ShadowSize = 0;
            this.futurePanel1.Style = System.Windows.Office2007.BackPanelStyles.Luna;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.splitContainer2_Panel1_Resize);
            this.splitContainer1.Resize += new System.EventHandler(this.TamañoCambiado);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.listaMensajes);
            this.panel3.Name = "panel3";
            // 
            // listaMensajes
            // 
            resources.ApplyResources(this.listaMensajes, "listaMensajes");
            this.listaMensajes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.listaMensajes.Name = "listaMensajes";
            this.listaMensajes.UseCompatibleStateImageBehavior = false;
            this.listaMensajes.View = System.Windows.Forms.View.Details;
            this.listaMensajes.SizeChanged += new System.EventHandler(this.listaMensajes_SizeChanged);
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.mapa);
            this.panel4.Name = "panel4";
            // 
            // mapa
            // 
            resources.ApplyResources(this.mapa, "mapa");
            this.mapa.BackColor = System.Drawing.Color.Transparent;
            this.mapa.ColorFondoCeldas = System.Drawing.Color.Black;
            this.mapa.ImagenFondo = null;
            this.mapa.Name = "mapa";
            this.mapa.MouseSobrePlaneta += new SharpKonquest.Clases.Mapa.DelegadoPlaneta(this.SobrePlaneta);
            this.mapa.MouseFueraPlaneta += new SharpKonquest.Clases.Mapa.DelegadoPlaneta(this.FueraPlaneta);
            this.mapa.MouseClicPlaneta += new SharpKonquest.Clases.Mapa.DelegadoPlaneta(this.ClicPlaneta);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            this.splitContainer2.Panel1.Controls.Add(this.Info);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            // 
            // Info
            // 
            resources.ApplyResources(this.Info, "Info");
            this.Info.Name = "Info";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.imagenPlaneta);
            this.panel2.Name = "panel2";
            // 
            // imagenPlaneta
            // 
            resources.ApplyResources(this.imagenPlaneta, "imagenPlaneta");
            this.imagenPlaneta.AjustSize = true;
            this.imagenPlaneta.Image = null;
            this.imagenPlaneta.Name = "imagenPlaneta";
            this.imagenPlaneta.OpacityIndex = ((byte)(165));
            this.imagenPlaneta.ReflectDistance = 5;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.listaChat);
            this.groupBox1.Controls.Add(this.textoChat);
            this.groupBox1.Controls.Add(this.listaJugadores);
            this.groupBox1.Controls.Add(this.bChat);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // listaChat
            // 
            resources.ApplyResources(this.listaChat, "listaChat");
            this.listaChat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listaChat.Name = "listaChat";
            this.listaChat.UseCompatibleStateImageBehavior = false;
            this.listaChat.View = System.Windows.Forms.View.Details;
            this.listaChat.SizeChanged += new System.EventHandler(this.listaChat_SizeChanged);
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // textoChat
            // 
            resources.ApplyResources(this.textoChat, "textoChat");
            this.textoChat.Name = "textoChat";
            this.textoChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bChat_KeyDown);
            // 
            // listaJugadores
            // 
            resources.ApplyResources(this.listaJugadores, "listaJugadores");
            this.listaJugadores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listaJugadores.FormattingEnabled = true;
            this.listaJugadores.Name = "listaJugadores";
            // 
            // bChat
            // 
            resources.ApplyResources(this.bChat, "bChat");
            this.bChat.Name = "bChat";
            this.bChat.UseVisualStyleBackColor = true;
            this.bChat.Click += new System.EventHandler(this.bChat_Click);
            this.bChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bChat_KeyDown);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.bFinTurno);
            this.panel1.Controls.Add(this.bCancelarHerramienta);
            this.panel1.Controls.Add(this.peticion);
            this.panel1.Controls.Add(this.lblTurno);
            this.panel1.Name = "panel1";
            // 
            // bFinTurno
            // 
            resources.ApplyResources(this.bFinTurno, "bFinTurno");
            this.bFinTurno.Name = "bFinTurno";
            this.bFinTurno.UseVisualStyleBackColor = true;
            this.bFinTurno.Click += new System.EventHandler(this.FinalizarTurno);
            // 
            // bCancelarHerramienta
            // 
            resources.ApplyResources(this.bCancelarHerramienta, "bCancelarHerramienta");
            this.bCancelarHerramienta.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancelarHerramienta.Name = "bCancelarHerramienta";
            this.bCancelarHerramienta.UseVisualStyleBackColor = true;
            this.bCancelarHerramienta.Click += new System.EventHandler(this.CancelarHerramienta);
            // 
            // peticion
            // 
            resources.ApplyResources(this.peticion, "peticion");
            this.peticion.Name = "peticion";
            // 
            // lblTurno
            // 
            resources.ApplyResources(this.lblTurno, "lblTurno");
            this.lblTurno.Name = "lblTurno";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.partidaToolStripMenuItem,
            this.opcioToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // partidaToolStripMenuItem
            // 
            resources.ApplyResources(this.partidaToolStripMenuItem, "partidaToolStripMenuItem");
            this.partidaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarNuevaPartidaToolStripMenuItem,
            this.toolStripMenuItem5,
            this.guardarpartida,
            this.cargarPartidaToolStripMenuItem});
            this.partidaToolStripMenuItem.Name = "partidaToolStripMenuItem";
            // 
            // iniciarNuevaPartidaToolStripMenuItem
            // 
            resources.ApplyResources(this.iniciarNuevaPartidaToolStripMenuItem, "iniciarNuevaPartidaToolStripMenuItem");
            this.iniciarNuevaPartidaToolStripMenuItem.Name = "iniciarNuevaPartidaToolStripMenuItem";
            this.iniciarNuevaPartidaToolStripMenuItem.Click += new System.EventHandler(this.NuevaPartida);
            // 
            // toolStripMenuItem5
            // 
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            // 
            // guardarpartida
            // 
            resources.ApplyResources(this.guardarpartida, "guardarpartida");
            this.guardarpartida.Name = "guardarpartida";
            this.guardarpartida.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // cargarPartidaToolStripMenuItem
            // 
            resources.ApplyResources(this.cargarPartidaToolStripMenuItem, "cargarPartidaToolStripMenuItem");
            this.cargarPartidaToolStripMenuItem.Name = "cargarPartidaToolStripMenuItem";
            this.cargarPartidaToolStripMenuItem.Click += new System.EventHandler(this.cargarPartidaToolStripMenuItem_Click);
            // 
            // opcioToolStripMenuItem
            // 
            resources.ApplyResources(this.opcioToolStripMenuItem, "opcioToolStripMenuItem");
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
            // 
            // mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem
            // 
            resources.ApplyResources(this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem, "mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem");
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.Checked = true;
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.CheckOnClick = true;
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem.Name = "mostrarMensajesAlRecibirEventosImportanteToolStripMenuItem";
            // 
            // limitarTiempoDeTurnoToolStripMenuItem
            // 
            resources.ApplyResources(this.limitarTiempoDeTurnoToolStripMenuItem, "limitarTiempoDeTurnoToolStripMenuItem");
            this.limitarTiempoDeTurnoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sinLímiteToolStripMenuItem,
            this.segundosToolStripMenuItem,
            this.sgeundosToolStripMenuItem,
            this.minutosToolStripMenuItem,
            this.minutosToolStripMenuItem1,
            this.minutosToolStripMenuItem2});
            this.limitarTiempoDeTurnoToolStripMenuItem.Name = "limitarTiempoDeTurnoToolStripMenuItem";
            this.limitarTiempoDeTurnoToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.limitarTiempoDeTurnoToolStripMenuItem_Click);
            // 
            // sinLímiteToolStripMenuItem
            // 
            resources.ApplyResources(this.sinLímiteToolStripMenuItem, "sinLímiteToolStripMenuItem");
            this.sinLímiteToolStripMenuItem.Name = "sinLímiteToolStripMenuItem";
            this.sinLímiteToolStripMenuItem.Tag = "-1";
            // 
            // segundosToolStripMenuItem
            // 
            resources.ApplyResources(this.segundosToolStripMenuItem, "segundosToolStripMenuItem");
            this.segundosToolStripMenuItem.Name = "segundosToolStripMenuItem";
            this.segundosToolStripMenuItem.Tag = "30";
            // 
            // sgeundosToolStripMenuItem
            // 
            resources.ApplyResources(this.sgeundosToolStripMenuItem, "sgeundosToolStripMenuItem");
            this.sgeundosToolStripMenuItem.Name = "sgeundosToolStripMenuItem";
            this.sgeundosToolStripMenuItem.Tag = "60";
            // 
            // minutosToolStripMenuItem
            // 
            resources.ApplyResources(this.minutosToolStripMenuItem, "minutosToolStripMenuItem");
            this.minutosToolStripMenuItem.Name = "minutosToolStripMenuItem";
            this.minutosToolStripMenuItem.Tag = "120";
            // 
            // minutosToolStripMenuItem1
            // 
            resources.ApplyResources(this.minutosToolStripMenuItem1, "minutosToolStripMenuItem1");
            this.minutosToolStripMenuItem1.Name = "minutosToolStripMenuItem1";
            this.minutosToolStripMenuItem1.Tag = "300";
            // 
            // minutosToolStripMenuItem2
            // 
            resources.ApplyResources(this.minutosToolStripMenuItem2, "minutosToolStripMenuItem2");
            this.minutosToolStripMenuItem2.Name = "minutosToolStripMenuItem2";
            this.minutosToolStripMenuItem2.Tag = "600";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // dibujarFlotasPropiasToolStripMenuItem
            // 
            resources.ApplyResources(this.dibujarFlotasPropiasToolStripMenuItem, "dibujarFlotasPropiasToolStripMenuItem");
            this.dibujarFlotasPropiasToolStripMenuItem.Checked = true;
            this.dibujarFlotasPropiasToolStripMenuItem.CheckOnClick = true;
            this.dibujarFlotasPropiasToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dibujarFlotasPropiasToolStripMenuItem.Name = "dibujarFlotasPropiasToolStripMenuItem";
            this.dibujarFlotasPropiasToolStripMenuItem.CheckedChanged += new System.EventHandler(this.dibujarFlotasPropiasToolStripMenuItem_Click);
            // 
            // dibujarFlotasEnemigasToolStripMenuItem
            // 
            resources.ApplyResources(this.dibujarFlotasEnemigasToolStripMenuItem, "dibujarFlotasEnemigasToolStripMenuItem");
            this.dibujarFlotasEnemigasToolStripMenuItem.Checked = true;
            this.dibujarFlotasEnemigasToolStripMenuItem.CheckOnClick = true;
            this.dibujarFlotasEnemigasToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dibujarFlotasEnemigasToolStripMenuItem.Name = "dibujarFlotasEnemigasToolStripMenuItem";
            this.dibujarFlotasEnemigasToolStripMenuItem.CheckedChanged += new System.EventHandler(this.dibujarFlotasEnemigasToolStripMenuItem_Click);
            // 
            // mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem
            // 
            resources.ApplyResources(this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem, "mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem");
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.CheckOnClick = true;
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.Name = "mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem";
            this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem.CheckedChanged += new System.EventHandler(this.mostrarDestinoEnLaLíneaDeFlotaToolStripMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // interfazToolStripMenuItem
            // 
            resources.ApplyResources(this.interfazToolStripMenuItem, "interfazToolStripMenuItem");
            this.interfazToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.porDefectoToolStripMenuItem,
            this.office2007ToolStripMenuItem,
            this.oscuraToolStripMenuItem});
            this.interfazToolStripMenuItem.Name = "interfazToolStripMenuItem";
            // 
            // porDefectoToolStripMenuItem
            // 
            resources.ApplyResources(this.porDefectoToolStripMenuItem, "porDefectoToolStripMenuItem");
            this.porDefectoToolStripMenuItem.Name = "porDefectoToolStripMenuItem";
            this.porDefectoToolStripMenuItem.Click += new System.EventHandler(this.porDefectoToolStripMenuItem_Click);
            // 
            // office2007ToolStripMenuItem
            // 
            resources.ApplyResources(this.office2007ToolStripMenuItem, "office2007ToolStripMenuItem");
            this.office2007ToolStripMenuItem.Name = "office2007ToolStripMenuItem";
            this.office2007ToolStripMenuItem.Click += new System.EventHandler(this.office2007ToolStripMenuItem_Click);
            // 
            // oscuraToolStripMenuItem
            // 
            resources.ApplyResources(this.oscuraToolStripMenuItem, "oscuraToolStripMenuItem");
            this.oscuraToolStripMenuItem.Name = "oscuraToolStripMenuItem";
            this.oscuraToolStripMenuItem.Click += new System.EventHandler(this.oscuraToolStripMenuItem_Click);
            // 
            // imagenDeFondoToolStripMenuItem
            // 
            resources.ApplyResources(this.imagenDeFondoToolStripMenuItem, "imagenDeFondoToolStripMenuItem");
            this.imagenDeFondoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ningunaToolStripMenuItem,
            this.espacioToolStripMenuItem,
            this.degradadoToolStripMenuItem,
            this.elegirToolStripMenuItem});
            this.imagenDeFondoToolStripMenuItem.Name = "imagenDeFondoToolStripMenuItem";
            // 
            // ningunaToolStripMenuItem
            // 
            resources.ApplyResources(this.ningunaToolStripMenuItem, "ningunaToolStripMenuItem");
            this.ningunaToolStripMenuItem.Name = "ningunaToolStripMenuItem";
            this.ningunaToolStripMenuItem.Click += new System.EventHandler(this.ningunaToolStripMenuItem_Click);
            // 
            // espacioToolStripMenuItem
            // 
            resources.ApplyResources(this.espacioToolStripMenuItem, "espacioToolStripMenuItem");
            this.espacioToolStripMenuItem.Name = "espacioToolStripMenuItem";
            this.espacioToolStripMenuItem.Click += new System.EventHandler(this.espacioToolStripMenuItem_Click);
            // 
            // degradadoToolStripMenuItem
            // 
            resources.ApplyResources(this.degradadoToolStripMenuItem, "degradadoToolStripMenuItem");
            this.degradadoToolStripMenuItem.Name = "degradadoToolStripMenuItem";
            this.degradadoToolStripMenuItem.Click += new System.EventHandler(this.degradadoToolStripMenuItem_Click);
            // 
            // elegirToolStripMenuItem
            // 
            resources.ApplyResources(this.elegirToolStripMenuItem, "elegirToolStripMenuItem");
            this.elegirToolStripMenuItem.Name = "elegirToolStripMenuItem";
            this.elegirToolStripMenuItem.Click += new System.EventHandler(this.elegirToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            // 
            // alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem
            // 
            resources.ApplyResources(this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem, "alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem");
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.Checked = true;
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.CheckOnClick = true;
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem.Name = "alarmaAlInicioDelTurnoDelJugadorToolStripMenuItem";
            // 
            // alarmaAlRecibirMensajeDeChatToolStripMenuItem
            // 
            resources.ApplyResources(this.alarmaAlRecibirMensajeDeChatToolStripMenuItem, "alarmaAlRecibirMensajeDeChatToolStripMenuItem");
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.Checked = true;
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.CheckOnClick = true;
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alarmaAlRecibirMensajeDeChatToolStripMenuItem.Name = "alarmaAlRecibirMensajeDeChatToolStripMenuItem";
            // 
            // ayudaToolStripMenuItem
            // 
            resources.ApplyResources(this.ayudaToolStripMenuItem, "ayudaToolStripMenuItem");
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem,
            this.buscarActualizacionesToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            // 
            // acercaDeToolStripMenuItem
            // 
            resources.ApplyResources(this.acercaDeToolStripMenuItem, "acercaDeToolStripMenuItem");
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // buscarActualizacionesToolStripMenuItem
            // 
            resources.ApplyResources(this.buscarActualizacionesToolStripMenuItem, "buscarActualizacionesToolStripMenuItem");
            this.buscarActualizacionesToolStripMenuItem.Name = "buscarActualizacionesToolStripMenuItem";
            this.buscarActualizacionesToolStripMenuItem.Click += new System.EventHandler(this.buscarActualizacionesToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton2
            // 
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.NuevaPartida);
            // 
            // toolStrip2
            // 
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton6,
            this.toolStripButton5,
            this.toolStripButton4,
            this.toolStripButton7,
            this.toolStripButton1,
            this.toolStripButton3});
            this.toolStrip2.Name = "toolStrip2";
            // 
            // toolStripButton6
            // 
            resources.ApplyResources(this.toolStripButton6, "toolStripButton6");
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Click += new System.EventHandler(this.EnviarFlota);
            // 
            // toolStripButton5
            // 
            resources.ApplyResources(this.toolStripButton5, "toolStripButton5");
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Click += new System.EventHandler(this.MedirDistancia);
            // 
            // toolStripButton4
            // 
            resources.ApplyResources(this.toolStripButton4, "toolStripButton4");
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Click += new System.EventHandler(this.MostrarFlotasEnMovimiento);
            // 
            // toolStripButton7
            // 
            resources.ApplyResources(this.toolStripButton7, "toolStripButton7");
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Click += new System.EventHandler(this.SimularBatalla);
            // 
            // toolStripButton1
            // 
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            resources.ApplyResources(this.toolStripButton3, "toolStripButton3");
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.IniciarCalculadora);
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.limpiarToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            // 
            // limpiarToolStripMenuItem1
            // 
            resources.ApplyResources(this.limpiarToolStripMenuItem1, "limpiarToolStripMenuItem1");
            this.limpiarToolStripMenuItem1.Name = "limpiarToolStripMenuItem1";
            this.limpiarToolStripMenuItem1.Click += new System.EventHandler(this.limpiarToolStripMenuItem1_Click);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.limpiarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // limpiarToolStripMenuItem
            // 
            resources.ApplyResources(this.limpiarToolStripMenuItem, "limpiarToolStripMenuItem");
            this.limpiarToolStripMenuItem.Name = "limpiarToolStripMenuItem";
            this.limpiarToolStripMenuItem.Click += new System.EventHandler(this.limpiarToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.SegundoTranscurrido);
            // 
            // peticionTexto
            // 
            resources.ApplyResources(this.peticionTexto, "peticionTexto");
            this.peticionTexto.Name = "peticionTexto";
            // 
            // toolStripMenuItem4
            // 
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "skx";
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "skx";
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            resources.ApplyResources(this.openFileDialog2, "openFileDialog2");
            // 
            // Principal
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.bCancelarHerramienta;
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Principal";
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
