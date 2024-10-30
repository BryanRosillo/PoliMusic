using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPoliMusicV2.Entities;
using WebAppPoliMusicV2.Utils;

namespace WebAppPoliMusicV2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>if (window.parent != window.top) window.parent.location.href = window.location.origin+'/login.aspx'; </script>");
#if DEBUG
                txtLogin.Text = "userPlayer";
                //txtLogin.Text = "userAdmin";
                txtPassword.Text = "12345678";
                txtPassword.Attributes.Add("value", txtPassword.Text);
                txtPassword.TextMode = TextBoxMode.Password;
#endif
                lblMessage.Text = string.Empty;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please, write a login/password";
                return;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["BDD_PoliMusicConnectionString"].ConnectionString;
            DataTable dtUser = AuthenticateUser(login, password, connectionString);
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                User user = new User();
                int userId = Convert.ToInt32(dtUser.Rows[0]["ID_USER"]);
                user.UserName = login;
                user.Photo = dtUser.Rows[0]["USER_PHOTO"].ToString();
                user.Email = dtUser.Rows[0]["EMAIL"].ToString();
                user.Birthday = dtUser.Rows[0]["BIRTHDAY"].ToString();
                user.UserType = Convert.ToInt32(dtUser.Rows[0]["USER_TYPE"]);
                if (user.UserType == Constants.ID_USER_NORMAL)
                {
                    Session[Constants.USER] = user.UserName;
                    FormsAuthentication.RedirectFromLoginPage(login, true);
                    Response.Redirect("~/Songs.aspx?uid=" + user.ID, true);
                }
                else if (user.UserType == Constants.ID_USER_ADMIN)
                {
                    Session[Constants.USER] = user.UserName;
                    FormsAuthentication.RedirectFromLoginPage(login, true);
                    Response.Redirect("~/DesktopAdmin.aspx?uid=" + user.ID, true);
                }
                else
                {
                    lblMessage.Text = "Login/password incorrect, try again";
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Login/password incorrect, try again";
                return;
            }
            //#endif
            lblMessage.Text = string.Empty;
        }
        public DataTable AuthenticateUser(string login, string password, string connectionString)
        {
            DataTable dtUser = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("", con))
                {
                    string sentence = "SELECT [ID_USER] ,[USERNAME] ,[PASSWORD] ,[EMAIL], [BIRTHDAY], [USER_PHOTO],[USER_TYPE] " +
                        "FROM [TBL_USER] " +
                        "WHERE [USERNAME] = @USERNAME AND [PASSWORD] = @PASSWORD";
                    cmd.CommandText = sentence;
                    cmd.Parameters.AddWithValue("@USERNAME", login);
                    cmd.Parameters.AddWithValue("@PASSWORD", password);
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtUser);
                        return dtUser;
                    }
                }
            }
        }
    }

}