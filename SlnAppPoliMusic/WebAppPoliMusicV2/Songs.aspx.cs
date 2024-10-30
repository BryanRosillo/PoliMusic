using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPoliMusicV2.Utils;

namespace WebAppPoliMusicV2
{
    public partial class Songs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserWelcomeMessage();
                LoadSongs();
            }
        }

        private void LoadUserWelcomeMessage()
        {
            if (Session[Constants.USER] != null)
            {
                lblWelcome.InnerText = "Bienvenido, " + Session[Constants.USER].ToString();
            }
          
        }

        private void LoadSongs()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BDD_PoliMusicConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID_SONG, SONG_NAME, SONG_PATH, PLAYS FROM TBL_SONG";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSongs.DataSource = dt;
                gvSongs.DataBind();
            }
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            Button btnPlay = (Button)sender;
            string songPath = btnPlay.CommandArgument;

            string script = $"playAudio('{songPath}');";
            ClientScript.RegisterStartupScript(this.GetType(), "PlayAudio", script, true);

            UpdatePlayCount(songPath);
        }

        private void UpdatePlayCount(string songPath)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BDD_PoliMusicConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE TBL_SONG SET PLAYS = PLAYS + 1 WHERE SONG_PATH = @SongPath";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SongPath", songPath);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            LoadSongs();  
        }

    }

    
}