namespace HeDieuHanh
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trans_result = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cbb_language = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.search_context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // trans_result
            // 
            this.trans_result.Name = "trans_result";
            this.trans_result.Size = new System.Drawing.Size(61, 4);
            // 
            // cbb_language
            // 
            this.cbb_language.FormattingEnabled = true;
            this.cbb_language.Location = new System.Drawing.Point(80, 17);
            this.cbb_language.Name = "cbb_language";
            this.cbb_language.Size = new System.Drawing.Size(382, 21);
            this.cbb_language.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Language";
            // 
            // search_context
            // 
            this.search_context.Name = "contextMenuStrip1";
            this.search_context.Size = new System.Drawing.Size(181, 26);
            this.search_context.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.search_context_ItemClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 63);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbb_language);
            this.Name = "Main";
            this.Text = "Translator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip trans_result;
        private System.Windows.Forms.ComboBox cbb_language;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip search_context;
    }
}

