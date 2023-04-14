namespace AudioSplit
{
    partial class FormTemplateHelp
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
            this.richText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richText
            // 
            this.richText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richText.Location = new System.Drawing.Point(2, 2);
            this.richText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richText.Name = "richText";
            this.richText.ReadOnly = true;
            this.richText.Size = new System.Drawing.Size(799, 865);
            this.richText.TabIndex = 0;
            this.richText.Text = "";
            // 
            // FormTemplateHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 869);
            this.Controls.Add(this.richText);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "FormTemplateHelp";
            this.Text = "AudioSplit Template Help";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTemplateHelp_FormClosing);
            this.Load += new System.EventHandler(this.FormTemplateHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richText;
    }
}