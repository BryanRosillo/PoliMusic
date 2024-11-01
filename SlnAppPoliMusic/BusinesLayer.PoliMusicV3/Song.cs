using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinesLayer.PoliMusicV3
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
            return new DataAccess.PoliMusicV3.Song(strConnectionString).Read();
        }

        public void updatePlay(string songPath)
        {
            new DataAccess.PoliMusicV3.Song(strConnectionString).Update(songPath);
        }
    }
}
