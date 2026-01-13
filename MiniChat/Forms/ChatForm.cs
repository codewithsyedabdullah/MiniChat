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
                    string reply = GenerateReply(msg);   // <-- use your predefined replies
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

            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.MaximumSize = new Size(250, 0);
            lbl.Text = text;
            lbl.Padding = new Padding(8);
            lbl.BackColor = isUser ? Color.LightBlue : Color.LightGray;
            lbl.Margin = new Padding(3);

            container.Controls.Add(lbl);

            // Align bubbles
            container.Dock = DockStyle.Top;
            container.Anchor = isUser ? AnchorStyles.Top | AnchorStyles.Right
                                      : AnchorStyles.Top | AnchorStyles.Left;

            flpMessages.Controls.Add(container);
            flpMessages.ScrollControlIntoView(container);
        }

    }
}
