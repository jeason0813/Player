using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass;

namespace Player
{
    class XSPF
    {
        static public List<PlaylistItem> Load(string path)
        {
            List<PlaylistItem> ret = new List<PlaylistItem>();

            try
            {
                using (XmlTextReader reader = new XmlTextReader(path))
                {
                    bool trackFound = false;

                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (reader.Name == "track")
                                    trackFound = true;
                                break;

                            case XmlNodeType.Text:
                                if (trackFound)
                                {
                                    string pa = reader.ReadString();
                                    if (!new System.Text.RegularExpressions.Regex("^(ftp|http)").IsMatch(pa))
                                    {
                                        pa = new Uri(pa).LocalPath; //convert from URI back to local path to access

                                        if (!System.IO.File.Exists(pa)) continue; //ignore stale entries
                                    }
                                    
                                    ret.Add(GetTags(pa));

                                    trackFound = false;
                                }
                                break;
                        }
                    }
                };
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message + e.StackTrace, "Error loading playlist", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }

        static public void Save(string path, List<PlaylistItem> items)
        {
            try
            {
                using (XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("playlist");
                    writer.WriteAttributeString("version", "1");
                    writer.WriteAttributeString("xmlns", "http://xspf.org/ns/0/");

                    writer.WriteStartElement("trackList");

                    foreach (PlaylistItem p in items)
                    {
                        writer.WriteStartElement("track");

                        writer.WriteStartElement("location");
                        writer.WriteString(new Uri(p.Path).AbsoluteUri); //sanitize for XML
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                };
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message + e.StackTrace, "Error saving playlist", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static public PlaylistItem GetTags(string path)
        {
            TAG_INFO tagInfo = new TAG_INFO(path);

            if (!new System.Text.RegularExpressions.Regex("^(ftp|http)").IsMatch(path))
            {
                string extension = new System.IO.FileInfo(path).Extension;
                
                if (!MainForm.supportedExts.Contains("*" + extension.ToLower()))
                {
                    MessageBox.Show("File \"" + path + "\" is an unsupported file type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                int type = PlaylistItem.TYPE_STREAM_FILE;

                if (Bass.SupportedMusicExtensions.Contains("*" + extension))
                    type = PlaylistItem.TYPE_MUSIC;
                
                tagInfo = BassTags.BASS_TAG_GetFromFile(path);

                if (tagInfo == null)
                    return new PlaylistItem("?", "?", "?", "?", path, type);
                else
                    return new PlaylistItem(tagInfo.track, tagInfo.title, tagInfo.artist, tagInfo.album, path, type);
            }
            else
            {
                if (MainForm.stream != 0)
                {
                    bool tagsAvailable = BassTags.BASS_TAG_GetFromURL(MainForm.stream, tagInfo);
                    if (tagsAvailable)
                        return new PlaylistItem(tagInfo.track, tagInfo.title, tagInfo.artist, tagInfo.album, path, PlaylistItem.TYPE_STREAM_URL);
                    else
                        return new PlaylistItem("?", "?", "?", "?", path, PlaylistItem.TYPE_STREAM_URL);
                }
                else
                    return new PlaylistItem("?", "?", "?", "?", path, PlaylistItem.TYPE_STREAM_URL);
            }
        }
    }
}
