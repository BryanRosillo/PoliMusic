using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace DataAccess.PoliMusicV3
{
    public class Song
    {
        private string strConnectionString;

        public Song(string strConnString)
        {
            strConnectionString = strConnString;
        }

        public DataTable Read()
        {
            DataTable dtSong = new DataTable();
            using (SqlConnection con = new SqlConnection(strConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [ID_SONG] ,[SONG_NAME] ,[SONG_PATH] ,[PLAYS] FROM [TBL_SONG]", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dtSong);
                        return dtSong;
                    }
                }
            }
        }

        public void Update(string songPath)
        {
            using (SqlConnection con = new SqlConnection(strConnectionString))
            {
                string query = "UPDATE TBL_SONG SET PLAYS = PLAYS + 1 WHERE SONG_PATH = @SongPath";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SongPath", songPath);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
