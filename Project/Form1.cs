using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class loginform : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public loginform()
        {
            InitializeComponent();
            password_loginform.PasswordChar = '*';
        }

        private void loginform_Load(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            user user1 = verifyLogin();
        }

        private user verifyLogin()
        {
            string usernm = username_loginform.Text;
            string userpw = password_loginform.Text;

            var query = 
                from user in db.users
                where user.username == usernm
                select user;
            int result = query.Count<user>();
            if(result >= 1)
            {
                user user1 = query.First<user>();
                if (user1.password == userpw)
                    return user1;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
    }
}
