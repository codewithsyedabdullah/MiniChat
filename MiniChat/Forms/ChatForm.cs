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
            this.DoubleBuffered = true;
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Resize += ChatForm_Resize;



            userId = id;
            userName = name;
            lblUser.Text = "Chat with " + userName;
            db = new DatabaseHelper();
            LoadMessages();
        }
        private void ChatForm_Resize(object sender, EventArgs e)
        {
            foreach (Control pnl in flpMessages.Controls)
            {
                if (pnl is Panel panel)
                {
                    panel.MaximumSize = new Size(flpMessages.Width - 50, 0);

                    Label lbl = null;
                    Label lblTime = null;

                    if (panel.Controls.Count > 0 && panel.Controls[0] is Label l)
                        lbl = l;

                    if (panel.Controls.Count > 1 && panel.Controls[1] is Label t)
                        lblTime = t;

                    if (lbl != null)
                        lbl.MaximumSize = new Size(panel.MaximumSize.Width - 20, 0);

                    if (lbl != null && lblTime != null)
                    {
                        lblTime.Top = lbl.Bottom + 2;
                        lblTime.Left = lbl.Left + (lbl.Width - lblTime.Width);
                    }

                    bool isUser = lbl != null && lbl.BackColor == Color.LightBlue;
                    int panelWidth = panel.PreferredSize.Width;
                    panel.Left = isUser ? flpMessages.Width - panelWidth - 20 : 10;
                }
            }
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
            string msg = txtMessage.Text.Trim();
            if (msg == "") return;

            // Save user message to DB
            db.AddMessage(userId, msg);
            AddMessageBubble(msg, true);
            txtMessage.Clear();

            // Bot reply after 500ms
            Task.Delay(500).ContinueWith(_ =>
            {
                Invoke(new Action(() =>
                {
                    string reply = GenerateReply(msg);
                    AddMessageBubble(reply, false);
                    db.AddMessage(userId, reply);       // optionally save bot reply in DB
                }));
            });


            //CODE FOR REPEATING SAME MESSAGE

            //string msg = txtMessage.Text.Trim();
            //if (msg == "") return;

            //db.AddMessage(userId, msg);
            //AddMessageBubble(msg, true);
            //txtMessage.Clear();

            //// trigger demo bot
            //Task.Delay(500).ContinueWith(_ =>
            //{
            //    Invoke(new Action(() =>
            //    {
            //        AddMessageBubble("Demo reply: " + msg, false);
            //    }));
            //});
        }


        private void AddAutoReply(string userMessage)
        {
            string reply = GenerateReply(userMessage);
            db.AddMessage(userId, reply);
            LoadMessages();
        }

        private string GenerateReply(string input)
        {
            input = input.ToLower();

            if (input.Contains("hi") || input.Contains("hello"))
                return "Hello! How are you?";

            if (input.Contains("how are you"))
                return "I'm good! Thanks for asking.";

            if (input.Contains("fine") || input.Contains("good"))
                return "Glad to hear that!";

            if (input.Contains("bye"))
                return "Goodbye!";

            return "I don't really understand. Try asking me how I am, or say hello, or say bye.";
        }

        private void AddMessageBubble(string text, bool isUser)
        {
            Panel container = new Panel();
            container.AutoSize = true;
            container.MaximumSize = new Size(flpMessages.Width - 50, 0);
            container.Padding = new Padding(5);

            // Message label
            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.MaximumSize = new Size(250, 0);
            lbl.Text = text;
            lbl.Padding = new Padding(8);
            lbl.BackColor = isUser ? Color.LightBlue : Color.LightGray;
            lbl.Margin = new Padding(3);
            container.Controls.Add(lbl);

            // Timestamp label
            Label lblTime = new Label();
            lblTime.AutoSize = true;
            lblTime.Text = DateTime.Now.ToString("hh:mm tt"); // e.g. 03:45 PM
            lblTime.Font = new Font("Segoe UI", 7);
            lblTime.ForeColor = Color.Gray;
            lblTime.Margin = new Padding(3, 0, 3, 3);
            lblTime.Top = lbl.Bottom + 2; // place timestamp below the message
            lblTime.Left = lbl.Left;       // align timestamp with the message
            container.Controls.Add(lblTime);

            // Align bubbles
            container.Dock = DockStyle.Top;
            container.Anchor = isUser ? AnchorStyles.Top | AnchorStyles.Right
                                      : AnchorStyles.Top | AnchorStyles.Left;

            flpMessages.Controls.Add(container);
            flpMessages.ScrollControlIntoView(container);
        }


    }
}
