using MiniChat.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace MiniChat.Forms
{
    public partial class Form1 : Form
    {
        DatabaseHelper db;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new DatabaseHelper();
            LoadUsers();
        }

        private void LoadUsers()
        {
            lstUsers.Items.Clear();
            DataTable dt = db.GetUsers();

            foreach (DataRow row in dt.Rows)
            {
                lstUsers.Items.Add(new UserItem
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString()
                });
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter user name:", "Add User", "");

            if (!string.IsNullOrWhiteSpace(name))
            {
                db.AddUser(name);
                LoadUsers();
            }
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // optional, khali chod do
        }

        private void lstUsers_DoubleClick(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem is UserItem user)
            {
                ChatForm chatForm = new ChatForm(user.Id, user.Name);
                chatForm.Show();
            }
        }

        class UserItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }
    }
}
