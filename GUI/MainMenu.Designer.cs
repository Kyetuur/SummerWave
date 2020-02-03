using System.Drawing;

namespace GUI
{
    partial class MainMenu
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.showSurfaceButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // showSurfaceButton
            // 
            this.showSurfaceButton.BackColor = System.Drawing.Color.Yellow;
            this.showSurfaceButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.showSurfaceButton.Location = new System.Drawing.Point(103, 68);
            this.showSurfaceButton.Margin = new System.Windows.Forms.Padding(4);
            this.showSurfaceButton.Name = "showSurfaceButton";
            this.showSurfaceButton.Size = new System.Drawing.Size(69, 51);
            this.showSurfaceButton.TabIndex = 0;
            this.showSurfaceButton.Text = "Show Surface";
            this.showSurfaceButton.UseVisualStyleBackColor = false;
            this.showSurfaceButton.Click += new System.EventHandler(this.ShowSurfaceButtonEvent);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GameChanger);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(36, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 22);
            this.textBox1.TabIndex = 2;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(276, 192);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.showSurfaceButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showSurfaceButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

