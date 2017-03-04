using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace MusicSearcher
{
    /// <summary>
    /// 歌词搜索引擎
    /// ver：6.0.0.0
    /// 只用于根据歌名和歌手名获取歌词，songid获取歌词用结构内数据
    /// Last edit at 2017-1-22 00:58:39
    /// By: Leo
    /// </summary>
    public class LrcSearcher
    {
        /// <summary>
        /// 对Unicode编码的公共解码函数
        /// </summary>
        /// <param name="text">必选，Unicode编码</param>
        /// <returns>解码后字符串</returns>
        public static string UnicodeToGB(string text)
        {
            System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(text, "\\\\u([\\w]{4})");
            if (mc != null && mc.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match m2 in mc)
                {
                    string v = m2.Value;
                    string word = v.Substring(2);
                    byte[] codes = new byte[2];
                    int code = Convert.ToInt32(word.Substring(0, 2), 16);
                    int code2 = Convert.ToInt32(word.Substring(2), 16);
                    codes[0] = (byte)code2;
                    codes[1] = (byte)code;
                    text = text.Replace(v, Encoding.Unicode.GetString(codes));
                }
            }
            else
            {

            }
            return text;
        }

        /// <summary>
        /// lrc返回结构
        /// </summary>
        public struct LrcResult
        {
            /// <summary>
            /// 歌名
            /// </summary>
            public string SongName;
            /// <summary>
            /// 歌手
            /// </summary>
            public string Singer;
            /// <summary>
            /// 专辑
            /// </summary>
            public string Album;
            /// <summary>
            /// 歌词地址
            /// </summary>
            public string url;
            /// <summary>
            /// 歌词预览
            /// </summary>
            public string Relook;
        }

        /// <summary>
        /// 百度音乐歌词搜索
        /// </summary>
        /// <param name="Query">必选，关键词</param>
        /// <param name="pn">可选，页码，默认为1</param>
        /// <returns>歌词结果列表</returns>
        public static List<LrcResult> BaiduLrc(string Query, int pn = 1)
        {
            try
            {
                List<LrcResult> rs = new List<LrcResult>();
                HttpWebRequest Req;
                HttpWebResponse Rep;
                Req = (HttpWebRequest)WebRequest.Create(@"http://music.baidu.com/search/lrc?key=" + HttpUtility.UrlEncode(Query, Encoding.UTF8) + @"&start=" + (20 * (pn - 1)).ToString());
                Rep = (HttpWebResponse)Req.GetResponse();
                StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                string str = Reader.ReadToEnd();
                string[] tmp = Regex.Split(str, "<div class=\"song-content\">");
                for (int i = 1; i < tmp.Length; i++)
                {
                    string songname = Regex.Split(Regex.Split(Regex.Split(Regex.Split(tmp[i], "<span class=\"song-title\">歌曲:")[1], "</span>")[0], "</a>")[0], "\">")[1].Replace("<em>", "").Replace("</em>", "").Replace("\t", "").Replace("\n", "");
                    string singer = Regex.Split(Regex.Split(tmp[i], "<span class=\"artist-title\">歌手:")[1], ">")[2].Replace("<em>", "").Replace("</em>", "").Replace("</a", "");
                    string album = Regex.Split(Regex.Split(tmp[i], "<span class=\"album-title\">")[1].Replace("<em>", "").Replace("</em>", ""), ">")[1].Replace("</a", "");
                    string url = @"http://music.baidu.com" + Regex.Split(Regex.Split(tmp[i], "<a class=\"down-lrc-btn { 'href':'")[1], "' }\" href=\"#\">下载LRC歌词</a>")[0];
                    string relook = Regex.Split(Regex.Split(Regex.Split(tmp[i], "<p id=\"lyricCont-")[1], "</p>")[0].Replace("<em>", "").Replace("</em>", "").Replace("<br />", "\r\n"), ">")[1];
                    rs.Add(new LrcResult { SongName = songname, Singer = singer, Album = album, url = url, Relook = relook });
                }
                return rs;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// QQ音乐歌词搜索
        /// </summary>
        /// <param name="Query">必选，关键词</param>
        /// <param name="pn">可选，页码，默认为1</param>
        /// <param name="ps">可选，页码，默认为20</param>
        /// <returns></returns>
        public static List<LrcResult> QQMusicLrc(string Query, int pn = 1, int ps = 20)
        {
            try
            {
                List<LrcResult> rs = new List<LrcResult>();
                HttpWebRequest Req;
                HttpWebResponse Rep;
                Req = (HttpWebRequest)WebRequest.Create(@"http://c.y.qq.com/soso/fcgi-bin/search_cp?remoteplace=txt.yqq.center&format=json&t=7&p=" + pn + "&n=" + ps + "&w=" + HttpUtility.UrlEncode(Query, Encoding.UTF8));
                Rep = (HttpWebResponse)Req.GetResponse();
                StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                string str = Reader.ReadToEnd();
                JObject p = JObject.Parse(str);
                foreach (var songinfo in p["data"]["lyric"]["list"])
                {
                    string _singer = "";
                    foreach (var singer in songinfo["singer"])
                    {
                        _singer = _singer + "," + singer["name"].ToString();
                    }
                    _singer = _singer.Substring(0, _singer.Length - 1);
                    string url = "http://music.qq.com/miniportal/static/lyric/" + songinfo["songid"].ToString().Substring(songinfo["songid"].ToString().Length - 2, 2).Replace("0", "") + "/" + songinfo["songid"].ToString() + ".xml";
                    string relook = songinfo["content"].ToString().Replace("&lt;strong class=&quot;keyword&quot;&gt;", "").Replace("&lt;/strong&gt;", "").Replace("&lt;br/&gt;", "\r\n").Replace("&#39;", "'").Replace("&apos;", "'");
                    string songname = songinfo["songname"].ToString();
                    string album = songinfo["albumname"].ToString();
                    rs.Add(new LrcResult { SongName = songname, Singer = _singer, Album = album, url = url, Relook = relook });
                }
                return rs;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 酷狗歌词搜索
        /// </summary>
        /// <param name="Query">必选，关键词</param>
        /// <param name="duration">必选，歌曲时长（毫秒为单位）</param>
        /// <returns></returns>
        public static List<LrcResult> KugouLrc(string Query, int duration)
        {
            try
            {
                List<LrcResult> rs = new List<LrcResult>();
                HttpWebRequest req1;
                HttpWebResponse rep1;
                req1 = (HttpWebRequest)WebRequest.Create("http://lyrics.kugou.com/search?ver=1&man=yes&client=pc&keyword=" + HttpUtility.UrlEncode(Query, Encoding.UTF8) + "&duration=" + duration.ToString());
                rep1 = (HttpWebResponse)req1.GetResponse();
                StreamReader Reader1 = new StreamReader(rep1.GetResponseStream());
                string str1 = Reader1.ReadToEnd();
                JObject p = JObject.Parse(str1);
                foreach (var lrcinfo in p["candidates"])
                {
                    rs.Add(new LrcResult { SongName = lrcinfo["song"].ToString(), Singer = lrcinfo["singer"].ToString(), Album = "", Relook = "", url = "http://lyrics.kugou.com/download?ver=1&client=pc&id=" + lrcinfo["id"].ToString() + "&accesskey=" + lrcinfo["accesskey"].ToString() + "&fmt=lrc&charset=utf8" });
                }
                return rs;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 酷狗歌词内容获取
        /// </summary>
        /// <param name="url">歌词url</param>
        /// <returns>歌词内容</returns>
        public static string KugouGetLrcContent(string url)
        {
            try
            {
                HttpWebRequest req2;
                HttpWebResponse rep2;
                req2 = (HttpWebRequest)WebRequest.Create(url);
                rep2 = (HttpWebResponse)req2.GetResponse();
                StreamReader Reader2 = new StreamReader(rep2.GetResponseStream());
                string str2 = Reader2.ReadToEnd();
                JObject p1 = JObject.Parse(str2);
                byte[] tmp = Convert.FromBase64String(p1["content"].ToString());
                return Encoding.UTF8.GetString(tmp);
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 歌词迷歌词来源
        /// </summary>
        /// <param name="SongName">必选，歌名</param>
        /// <param name="Singer">可选，歌手名，默认为空</param>
        /// <returns>歌词结果列表</returns>
        public static List<LrcResult> GeCiMiLrc(string SongName, string Singer = "")
        {
            try
            {
                List<LrcResult> rs = new List<LrcResult>();
                HttpWebRequest Req;
                HttpWebResponse Rep;
                if (Singer != "")
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"http://geci.me/api/lyric/" + HttpUtility.UrlEncode(SongName, Encoding.UTF8) + "/" + HttpUtility.UrlEncode(Singer, Encoding.UTF8));
                }
                else
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"http://geci.me/api/lyric/" + HttpUtility.UrlEncode(SongName, Encoding.UTF8));
                }
                Req.Timeout = 300000;
                Rep = (HttpWebResponse)Req.GetResponse();
                StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                string str = Reader.ReadToEnd();
                JObject p = JObject.Parse(str);
                foreach (var songinfo in p["result"])
                {
                    string url = songinfo["lrc"].ToString();
                    string songname = UnicodeToGB(songinfo["song"].ToString());
                    rs.Add(new LrcResult { SongName = songname, Singer = Singer, Album = "", url = url, Relook = "" });
                }
                return rs;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
