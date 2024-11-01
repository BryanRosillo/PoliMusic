using EntityLayer.PoliMusicV3.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppPoliMusicV3
{
    public partial class Songs : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["BDD_PoliMusicConnectionString"].ConnectionString;
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
            if (Session[EntityLayer.PoliMusicV3.Util.Constants.USER] != null)
            {
                lblWelcome.InnerText = "Bienvenido, " + Session[EntityLayer.PoliMusicV3.Util.Constants.USER].ToString();
            }

        }

        private void LoadSongs()
        {
            DataTable Songs = new BusinesLayer.PoliMusicV3.Song(strConnString).Read();
            gvSongs.DataSource = Songs;
            gvSongs.DataBind();
            
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
            new BusinesLayer.PoliMusicV3.Song(strConnString).updatePlay(songPath);
            LoadSongs();
        }

    }
}
