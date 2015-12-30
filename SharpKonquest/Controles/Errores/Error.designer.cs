namespace System
{
    partial class Error
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
            this.imagen = new System.Windows.Forms.PictureBox();
            this.aviso = new System.Windows.Forms.Label();
            this.cabecera = new System.Windows.Forms.Label();
            this.info = new System.Windows.Forms.Label();
            this.infoError = new System.Windows.Forms.Label();
            this.bContinuar = new System.Windows.Forms.Button();
            this.bSalir = new System.Windows.Forms.Button();
            this.enviarInforme = new System.Windows.Forms.CheckBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imagen)).BeginInit();
            this.SuspendLayout();
            // 
            // imagen
            // 
            this.imagen.BackColor = System.Drawing.SystemColors.Control;
            this.imagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imagen.Location = new System.Drawing.Point(12, 8);
            this.imagen.Name = "imagen";
            this.imagen.Size = new System.Drawing.Size(128, 128);
            this.imagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagen.TabIndex = 3;
            this.imagen.TabStop = false;
            // 
            // aviso
            // 
            this.aviso.Location = new System.Drawing.Point(146, 74);
            this.aviso.Name = "aviso";
            this.aviso.Size = new System.Drawing.Size(250, 62);
            this.aviso.TabIndex = 10;
            // 
            // cabecera
            // 
            this.cabecera.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cabecera.Location = new System.Drawing.Point(146, 8);
            this.cabecera.Name = "cabecera";
            this.cabecera.Size = new System.Drawing.Size(250, 66);
            this.cabecera.TabIndex = 9;
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(12, 139);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(384, 16);
            this.info.TabIndex = 12;
            // 
            // infoError
            // 
            this.infoError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.infoError.Location = new System.Drawing.Point(12, 155);
            this.infoError.Name = "infoError";
            this.infoError.Size = new System.Drawing.Size(384, 64);
            this.infoError.TabIndex = 11;
            // 
            // bContinuar
            // 
            this.bContinuar.Location = new System.Drawing.Point(240, 225);
            this.bContinuar.Name = "bContinuar";
            this.bContinuar.Size = new System.Drawing.Size(75, 23);
            this.bContinuar.TabIndex = 13;
            this.bContinuar.UseVisualStyleBackColor = true;
            this.bContinuar.Click += new System.EventHandler(this.Continuar);
            // 
            // bSalir
            // 
            this.bSalir.Location = new System.Drawing.Point(321, 225);
            this.bSalir.Name = "bSalir";
            this.bSalir.Size = new System.Drawing.Size(75, 23);
            this.bSalir.TabIndex = 14;
            this.bSalir.UseVisualStyleBackColor = true;
            this.bSalir.Click += new System.EventHandler(this.Salir);
            // 
            // enviarInforme
            // 
            this.enviarInforme.Checked = true;
            this.enviarInforme.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enviarInforme.Location = new System.Drawing.Point(12, 225);
            this.enviarInforme.Name = "enviarInforme";
            this.enviarInforme.Size = new System.Drawing.Size(222, 24);
            this.enviarInforme.TabIndex = 15;
            this.enviarInforme.UseVisualStyleBackColor = true;
            // 
            // Error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 258);
            this.Controls.Add(this.enviarInforme);
            this.Controls.Add(this.bSalir);
            this.Controls.Add(this.bContinuar);
            this.Controls.Add(this.info);
            this.Controls.Add(this.infoError);
            this.Controls.Add(this.aviso);
            this.Controls.Add(this.cabecera);
            this.Controls.Add(this.imagen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Error";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.imagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imagen;
        private System.Windows.Forms.Label aviso;
        private System.Windows.Forms.Label cabecera;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.Label infoError;
        private System.Windows.Forms.Button bContinuar;
        private System.Windows.Forms.Button bSalir;
        private System.Windows.Forms.CheckBox enviarInforme;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}
