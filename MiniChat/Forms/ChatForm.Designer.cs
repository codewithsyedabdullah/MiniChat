namespace MiniChat
{
    partial class ChatForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblUser = new Label();
            btnSend = new Button();
            txtMessage = new TextBox();
            flpMessages = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.Dock = DockStyle.Top;
            lblUser.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUser.Location = new Point(0, 0);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(800, 35);
            lblUser.TabIndex = 3;
            lblUser.Text = "Chat with ...";
            lblUser.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSend
            // 
            btnSend.BackColor = SystemColors.ActiveCaption;
            btnSend.Dock = DockStyle.Bottom;
            btnSend.Location = new Point(0, 410);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(800, 40);
            btnSend.TabIndex = 4;
            btnSend.TabStop = false;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // txtMessage
            // 
            txtMessage.Dock = DockStyle.Bottom;
            txtMessage.Location = new Point(0, 350);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(800, 60);
            txtMessage.TabIndex = 5;
            // 
            // flpMessages
            // 
            flpMessages.AutoScroll = true;
            flpMessages.Dock = DockStyle.Fill;
            flpMessages.FlowDirection = FlowDirection.TopDown;
            flpMessages.Location = new Point(0, 35);
            flpMessages.Name = "flpMessages";
            flpMessages.Size = new Size(800, 315);
            flpMessages.TabIndex = 6;
            flpMessages.WrapContents = false;
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flpMessages);
            Controls.Add(txtMessage);
            Controls.Add(btnSend);
            Controls.Add(lblUser);
            KeyPreview = true;
            Name = "ChatForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblUser;
        private Button btnSend;
        private TextBox txtMessage;
        private FlowLayoutPanel flpMessages;
    }
}
