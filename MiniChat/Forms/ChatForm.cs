using MiniChat.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MiniChat
{
    public partial class ChatForm : Form
    {
        private int userId;
        private string userName;
        private DatabaseHelper db;

        public ChatForm(int id, string name)
        {
            InitializeComponent();
            userId = id;
            userName = name;
            lblUser.Text = "Chat with " + userName;
            db = new DatabaseHelper();
            LoadMessages();
        }

        private void LoadMessages()
        {
            flpMessages.Controls.Clear();
            DataTable dt = db.GetMessages(userId);

            foreach (DataRow row in dt.Rows)
            {
                Label lbl = new Label();
                lbl.Text = row["Message"].ToString();
                lbl.AutoSize = true;
                lbl.MaximumSize = new Size(300, 0);
                lbl.Padding = new Padding(10);
                lbl.Margin = new Padding(5);
                lbl.BackColor = Color.DodgerBlue;
                lbl.ForeColor = Color.White;
                lbl.Font = new Font("Segoe UI", 10);

                Panel pnl = new Panel();
                pnl.Width = flpMessages.Width - 25;
                pnl.Height = lbl.Height + 10;
                pnl.Controls.Add(lbl);
                lbl.Location = new Point(pnl.Width - lbl.Width, 0);

                flpMessages.Controls.Add(pnl);
            }

            if (flpMessages.Controls.Count > 0)
                flpMessages.ScrollControlIntoView(flpMessages.Controls[flpMessages.Controls.Count - 1]);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                db.AddMessage(userId, message);
                txtMessage.Clear();
                LoadMessages();
            }
        }
    }
}
