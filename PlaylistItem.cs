using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Player
{
    class PlaylistItem
    {
        public string Track, Title, Artist, Album, Path = "";
        ListViewItem listViewItem;
        public int Type = 0;

        public const int TYPE_STREAM_FILE = 0;
        public const int TYPE_STREAM_URL = 1;
        public const int TYPE_MUSIC = 2;
        
        public PlaylistItem(string track, string title, string artist, string album, string path)
        {
            Track = track;
            Title = title;
            Artist = artist;
            Album = album;
            Path = path;
        }

        public PlaylistItem(string track, string title, string artist, string album, string path, int type)
        {
            Track = track;
            Title = title;
            Artist = artist;
            Album = album;
            Path = path;
            Type = type;
        }

        public ListViewItem ListViewItem //the ListViewItem belonging to this entry
        {
            get { return this.listViewItem; }
            set { this.listViewItem = value; }
        }
    }
}
