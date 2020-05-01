using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreamingApplication.Models
{
    public class Songs
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string SongFileCover { get; set; }
        public string SongUrl { get; set; }
        public string SongDuration { get; set; }
        public string SingerName { get; set; }
    }
}
