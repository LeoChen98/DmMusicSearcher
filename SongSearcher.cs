using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;



// 大猫音乐盒音乐搜索引擎
//（包括歌曲搜索引擎和歌词搜索引擎）
// ver:6.0.0.0
// Last edit at 2017-1-22 00:58:28
// By:Leo
namespace MusicSearcher
{
    /// <summary>
    /// 歌曲搜索引擎
    /// ver：6.0.0.0
    /// Last edit at 2017-1-22 00:58:19
    /// By: Leo
    /// </summary>
    public class SongSearcher
    {
        /// <summary>
        /// 返回数据结构
        /// </summary>
        public struct ResultData
        {
            /// <summary>
            /// 歌曲列表
            /// </summary>
            List<SongListItem> List;

            /// <summary>
            /// 歌曲列表
            /// </summary>
            public List<SongListItem> List1
            {
                get
                {
                    return List;
                }

                set
                {
                    List = value;
                }
            }

            /// <summary>
            /// 结果总数
            /// </summary>
            public int Tot
            {
                get
                {
                    return tot;
                }

                set
                {
                    tot = value;
                }
            }

            /// <summary>
            /// 歌曲总数
            /// </summary>
            private int tot;

        }

        /// <summary>
        /// 歌曲信息结构
        /// </summary>
        public class SongListItem
        {
            string SongName;
            string Singer;
            string Album;
            string SongId;
            string SongInterval;
            string Lrc;
            SongPic Pic;
            QualityList Quality;
            /// <summary>
            /// 歌名
            /// </summary>
            public string SongName1
            {
                get
                {
                    return SongName;
                }

                set
                {
                    SongName = value;
                }
            }
            /// <summary>
            /// 歌手
            /// </summary>
            public string Singer1
            {
                get
                {
                    return Singer;
                }

                set
                {
                    Singer = value;
                }
            }
            /// <summary>
            /// 专辑
            /// </summary>
            public string Album1
            {
                get
                {
                    return Album;
                }

                set
                {
                    Album = value;
                }
            }
            /// <summary>
            /// 歌曲ID
            /// </summary>
            public string SongId1
            {
                get
                {
                    return SongId;
                }

                set
                {
                    SongId = value;
                }
            }
            /// <summary>
            /// 歌曲长度（单位：秒）
            /// </summary>
            public string SongInterval1
            {
                get
                {
                    return SongInterval;
                }

                set
                {
                    SongInterval = value;
                }
            }
            /// <summary>
            /// 歌词地址
            /// </summary>
            public string Lrc1
            {
                get
                {
                    return Lrc;
                }

                set
                {
                    Lrc = value;
                }
            }
            /// <summary>
            /// 图片结构
            /// </summary>
            public SongPic Pic1
            {
                get
                {
                    return Pic;
                }

                set
                {
                    Pic = value;
                }
            }
            /// <summary>
            /// 音质列表
            /// </summary>
            public QualityList Quality1
            {
                get
                {
                    return Quality;
                }

                set
                {
                    Quality = value;
                }
            }
        }
        /// <summary>
        /// 歌曲图片结构
        /// </summary>
        public class SongPic
        {
            string Large;
            string Middle;
            string Small;
            /// <summary>
            /// 大图
            /// </summary>
            public string Large1
            {
                get
                {
                    return Large;
                }

                set
                {
                    Large = value;
                }
            }
            /// <summary>
            /// 中图
            /// </summary>
            public string Middle1
            {
                get
                {
                    return Middle;
                }

                set
                {
                    Middle = value;
                }
            }
            /// <summary>
            /// 小图
            /// </summary>
            public string Small1
            {
                get
                {
                    return Small;
                }

                set
                {
                    Small = value;
                }
            }
        }
        /// <summary>
        /// 音质列表
        /// </summary>
        public class QualityList
        {
            /// <summary>
            /// 是否有对应音质
            /// </summary>
            bool q128, q192, q256, q320, ape, flac, qother;
            /// <summary>
            /// 音质对应的文件大小
            /// </summary>
            string s128, s192, s256, s320, sape, sflac, sother;
            /// <summary>
            /// 音质对应的文件地址
            /// </summary>
            string f128, f192, f256, f320, fape, fflac, fother;
            /// <summary>
            /// 是否有128kbps音质
            /// </summary>
            public bool Q128
            {
                get
                {
                    return q128;
                }

                set
                {
                    q128 = value;
                }
            }
            /// <summary>
            /// 是否有192kbps音质
            /// </summary>
            public bool Q192
            {
                get
                {
                    return q192;
                }

                set
                {
                    q192 = value;
                }
            }
            /// <summary>
            /// 是否有256kbps音质
            /// </summary>
            public bool Q256
            {
                get
                {
                    return q256;
                }

                set
                {
                    q256 = value;
                }
            }
            /// <summary>
            /// 是否有320kbps音质
            /// </summary>
            public bool Q320
            {
                get
                {
                    return q320;
                }

                set
                {
                    q320 = value;
                }
            }
            /// <summary>
            /// 是否有ape音质
            /// </summary>
            public bool Ape
            {
                get
                {
                    return ape;
                }

                set
                {
                    ape = value;
                }
            }
            /// <summary>
            /// 是否有flac音质
            /// </summary>
            public bool Flac
            {
                get
                {
                    return flac;
                }

                set
                {
                    flac = value;
                }
            }
            /// <summary>
            /// 是否有其他音质
            /// </summary>
            public bool Qother
            {
                get
                {
                    return qother;
                }

                set
                {
                    qother = value;
                }
            }
            /// <summary>
            /// 128kbps音质文件大小
            /// </summary>
            public string S128
            {
                get
                {
                    return s128;
                }

                set
                {
                    s128 = value;
                }
            }
            /// <summary>
            /// 192kbps音质文件大小
            /// </summary>
            public string S192
            {
                get
                {
                    return s192;
                }

                set
                {
                    s192 = value;
                }
            }
            /// <summary>
            /// 256kbps音质文件大小
            /// </summary>
            public string S256
            {
                get
                {
                    return s256;
                }

                set
                {
                    s256 = value;
                }
            }
            /// <summary>
            /// 320kbps音质文件大小
            /// </summary>
            public string S320
            {
                get
                {
                    return s320;
                }

                set
                {
                    s320 = value;
                }
            }
            /// <summary>
            /// ape音质文件大小
            /// </summary>
            public string Sape
            {
                get
                {
                    return sape;
                }

                set
                {
                    sape = value;
                }
            }
            /// <summary>
            /// flac音质文件大小
            /// </summary>
            public string Sflac
            {
                get
                {
                    return sflac;
                }

                set
                {
                    sflac = value;
                }
            }
            /// <summary>
            /// 其他音质文件大小
            /// </summary>
            public string Sother
            {
                get
                {
                    return sother;
                }

                set
                {
                    sother = value;
                }
            }
            /// <summary>
            /// 128kbps音质文件地址
            /// </summary>
            public string F128
            {
                get
                {
                    return f128;
                }

                set
                {
                    f128 = value;
                }
            }
            /// <summary>
            /// 192kbps音质文件地址
            /// </summary>
            public string F192
            {
                get
                {
                    return f192;
                }

                set
                {
                    f192 = value;
                }
            }
            /// <summary>
            /// 256kbps音质文件地址
            /// </summary>
            public string F256
            {
                get
                {
                    return f256;
                }

                set
                {
                    f256 = value;
                }
            }
            /// <summary>
            /// 320kbps音质文件地址
            /// </summary>
            public string F320
            {
                get
                {
                    return f320;
                }

                set
                {
                    f320 = value;
                }
            }
            /// <summary>
            /// ape音质文件地址
            /// </summary>
            public string Fape
            {
                get
                {
                    return fape;
                }

                set
                {
                    fape = value;
                }
            }
            /// <summary>
            /// flac音质文件地址
            /// </summary>
            public string Fflac
            {
                get
                {
                    return fflac;
                }

                set
                {
                    fflac = value;
                }
            }
            /// <summary>
            /// 其他音质文件地址
            /// </summary>
            public string Fother
            {
                get
                {
                    return fother;
                }

                set
                {
                    fother = value;
                }
            }
        }

        /// <summary>
        /// MD5(32位加密)
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>MD5加密后的字符串</returns>
        public static string GetMd5HashStr(string str)
        {
            string pwd = string.Empty;

            //实例化一个md5对像
            MD5 md5 = MD5.Create();

            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }

            return pwd;
        }

        /// <summary>
        /// 百度音乐
        /// </summary>
        public class Baidu
        {
            /// <summary>
            /// 搜索函数
            /// </summary>
            /// <param name="Query">必选，关键字</param>
            /// <param name="PageNumber">可选，页码，默认为1</param>
            /// <param name="PageSize">可选，每页数据条数，默认为20</param>
            /// <returns>结果</returns>
            public static ResultData Search(string Query,int PageNumber = 1,int PageSize = 20)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    Req = (HttpWebRequest)WebRequest.Create("http://tingapi.ting.baidu.com/v1/restserver/ting?from=webqpp_music&format=json&callback=&method=baidu.ting.search.common&query=" + HttpUtility.UrlEncode(Query, Encoding.UTF8) + @"&page_size=" + PageSize + @"&page_no=" + PageNumber);
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    str = str.Replace("<em>", "").Replace(@"<\/em>","");
                    JObject p = JObject.Parse(str);
                    string[] Songlist = Regex.Split( p["song_list"].ToString().Replace("[", "").Replace("]", "").Replace("\r\n", "").Replace(" ",""),"},{");
                    JObject SearchInfo = JObject.Parse( p["pages"].ToString());
                    ResultData Result = new ResultData { List1=new List<SongListItem>()};
                    Result.Tot = int.Parse(SearchInfo["total"].ToString());
                    for(int i = 0; i < Songlist.Length ; i++)
                    {
                        if (i != 0 && i!=Songlist.Length -1)
                        {
                            Songlist[i] = "{" + Songlist[i] + "}";
                        }
                        else
                        {
                            if(i == 0 && i != Songlist.Length -1)
                            {
                                Songlist[i] = Songlist[i] + "}";
                            }
                            else
                            {
                                if (i == Songlist.Length - 1 && i!=0)
                                {
                                    Songlist[i] = "{" + Songlist[i];
                                }
                            }
                        }
                        JObject songinfo = JObject.Parse(Songlist[i]);
                        SongListItem SLI = new SongListItem { Album1 = songinfo["album_title"].ToString(), Singer1 = songinfo["author"].ToString(), SongId1 = songinfo["song_id"].ToString(), SongName1 = songinfo["title"].ToString(), Lrc1=songinfo["lrclink"].ToString(), Quality1=new QualityList(),Pic1=new SongPic(), SongInterval1 = "" };
                        string[] rate = songinfo["all_rate"].ToString().Split(',');
                        for(int j= 0; j < rate.Length; j++)
                        {
                            switch (rate[j])
                            {
                                case "128":
                                    SLI.Quality1.Q128 = true;
                                    break;
                                case "192":
                                    SLI.Quality1.Q192 = true;
                                    break;
                                case "256":
                                    SLI.Quality1.Q256 = true;
                                    break;
                                case "320":
                                    SLI.Quality1.Q320 = true;
                                    break;
                                case "flac":
                                    SLI.Quality1.Flac = true;
                                    break;
                                case "ape":
                                    SLI.Quality1.Ape = true;
                                    break;
                            }
                        }
                        Result.List1.Add(SLI);
                    }
                    return Result;
                }
                catch(Exception ex)
                {

                    return new ResultData();
                }
            }

            /// <summary>
            /// 获取歌曲文件
            /// </summary>
            /// <param name="Result">必选，搜索结果</param>
            /// <returns>结果</returns>
            public static SongListItem GetSong(SongListItem Result)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    byte[] param = Encoding.UTF8.GetBytes("param=" + Convert.ToBase64String(Encoding.UTF8.GetBytes("{\"key\":\"" + Result.SongId1 + "\",\"rate\":\"128,192,256,320,flac\",\"linkType\":0,\"isCloud\":0,\"version\":\"10.1.8.3\"}")));
                    Req = (HttpWebRequest)WebRequest.Create(@"http://musicmini2014.baidu.com/2016/app/link/getLinks.php");
                    Req.Method = "POST";
                    Req.ContentType = "application/x-www-form-urlencoded";
                    Req.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.2; WOW64; Trident/7.0)";
                    Stream reqstream = Req.GetRequestStream();
                    reqstream.Write(param, 0, param.Length);
                    reqstream.Close();
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    JObject p = JObject.Parse(str);
                    Result.Lrc1 = p["lyric_url"].ToString();
                    Result.Pic1.Large1 = p["album_image_url"].ToString();
                    Result.Pic1.Middle1= Regex.Split(p["album_image_url"].ToString(),",")[0] + ",w_250";
                    Result.Pic1.Small1= p["small_album_image_url"].ToString();
                    string[] Songfilelist = Regex.Split(p["file_list"].ToString().Replace("[", "").Replace("]", "").Replace("\r\n", "").Replace(" ", ""), "},{");
                    for (int i = 0; i < Songfilelist.Length; i++)
                    {
                        if (i != 0 && i != Songfilelist.Length - 1)
                        {
                            Songfilelist[i] = "{" + Songfilelist[i] + "}";
                        }
                        else
                        {
                            if (i == 0 && i != Songfilelist.Length - 1)
                            {
                                Songfilelist[i] = Songfilelist[i] + "}";
                            }
                            else
                            {
                                if (i == Songfilelist.Length - 1 && i != 0)
                                {
                                    Songfilelist[i] = "{" + Songfilelist[i];
                                }
                            }
                        }
                        JObject songdinfo = JObject.Parse(Songfilelist[i]);
                        if (int.Parse(songdinfo["kbps"].ToString()) <= 320)
                        {
                            switch (int.Parse(songdinfo["kbps"].ToString()))
                            {
                                case 128:
                                    Result.Quality1.F128 = @"http://zhangmenshiting.baidu.com/data2/music/" + songdinfo["file_id"] + @"/" + songdinfo["file_id"] + @".mp3";
                                    Result.Quality1.S128 = songdinfo["size"].ToString();
                                    break;
                                case 192:
                                    Result.Quality1.F192 = @"http://zhangmenshiting.baidu.com/data2/music/" + songdinfo["file_id"] + @"/" + songdinfo["file_id"] + @".mp3";
                                    Result.Quality1.S192 = songdinfo["size"].ToString();
                                    break;
                                case 256:
                                    Result.Quality1.F256 = @"http://zhangmenshiting.baidu.com/data2/music/" + songdinfo["file_id"] + @"/" + songdinfo["file_id"] + @".mp3";
                                    Result.Quality1.S256 = songdinfo["size"].ToString();
                                    break;
                                case 320:
                                    Result.Quality1.F320 = @"http://zhangmenshiting.baidu.com/data2/music/" + songdinfo["file_id"] + @"/" + songdinfo["file_id"] + @".mp3";
                                    Result.Quality1.S320 = songdinfo["size"].ToString();
                                    break;

                            }
                        }else
                        {
                            Result.Quality1.Fflac = @"http://zhangmenshiting.baidu.com/data2/music/" + songdinfo["file_id"] + @"/" + songdinfo["file_id"] + @".flac";
                            Result.Quality1.Sflac = songdinfo["size"].ToString();
                        }
                    }
                    return Result;
                }
                catch(Exception ex)
                {

                    return Result;
                }

            }
            /// <summary>
            /// 获取歌曲文件（播放专用）
            /// </summary>
            /// <param name="SongId">歌曲ID</param>
            /// <param name="Quanlity">音质</param>
            /// <returns>歌曲地址</returns>
            public static string GetSong(string SongId,string Quanlity)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                     
                    byte[] param = Encoding.UTF8.GetBytes("param=" + Convert.ToBase64String(Encoding.UTF8.GetBytes("{\"key\":\"" + SongId + "\",\"rate\":\"128,192,256,320,flac\",\"linkType\":0,\"isCloud\":0,\"version\":\"10.1.8.3\"}")));
                    Req = (HttpWebRequest)WebRequest.Create(@"http://musicmini2014.baidu.com/2016/app/link/getLinks.php");
                    Req.Method = "POST";
                    Req.ContentType = "application/x-www-form-urlencoded";
                    Req.UserAgent= "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.2; WOW64; Trident/7.0)";
                    Stream reqstream = Req.GetRequestStream();
                    reqstream.Write(param, 0, param.Length);
                    reqstream.Close();
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    JObject p = JObject.Parse(str);
                    string[] Songfilelist = Regex.Split(p["file_list"].ToString().Replace("[", "").Replace("]", "").Replace("\r\n", "").Replace(" ", ""), "},{");
#pragma warning disable CS0162 // 检测到无法访问的代码
                    for (int i = 0; i < Songfilelist.Length; i++)
#pragma warning restore CS0162 // 检测到无法访问的代码
                    {
                        if (i != 0 && i != Songfilelist.Length - 1)
                        {
                            Songfilelist[i] = "{" + Songfilelist[i] + "}";
                        }
                        else
                        {
                            if (i == 0 && i != Songfilelist.Length - 1)
                            {
                                Songfilelist[i] = Songfilelist[i] + "}";
                            }
                            else
                            {
                                if (i == Songfilelist.Length - 1 && i != 0)
                                {
                                    Songfilelist[i] = "{" + Songfilelist[i];
                                }
                            }
                        }
                        JObject songdinfo = JObject.Parse(Songfilelist[i]);
                        if(songdinfo["format"].ToString() == "flac")
                        {
                            return @"http://zhangmenshiting.baidu.com/data2/music/" + songdinfo["file_id"] + @"/" + songdinfo["file_id"] + @".flac";
                        }else
                        {
                            return @"http://zhangmenshiting.baidu.com/data2/music/" + songdinfo["file_id"] + @"/" + songdinfo["file_id"] + @".mp3";
                        }
                    }
                    return "";
                }
                catch(Exception ex)
                {

                    return "";
                }

            }

            /// <summary>
            /// 获取xcode值
            /// </summary>
            /// <param name="SongId">必选，歌曲ID</param>
            /// <returns>xcode值</returns>
            public static  string GetXcode(string SongId)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"http://music.baidu.com/data/music/links?songIds=" + SongId);
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    JObject p = JObject.Parse(str);
                    return Regex.Split(Regex.Split( p["data"].First.ToString(),":")[1],"\"")[1];
                }
                catch(Exception ex)
                {

                    return "";
                }
            }
        }

        /// <summary>
        /// QQ音乐
        /// </summary>
        public class QQMusic
        {
            /// <summary>
            /// 搜索函数
            /// </summary>
            /// <param name="Query">必选，关键字</param>
            /// <param name="PageNumber">可选，页码，默认为1</param>
            /// <param name="PageSize">可选，每页数据条数，默认为20</param>
            /// <returns>结果</returns>
            public static ResultData Search(string Query, int PageNumber=1,int PageSize = 20)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"http://c.y.qq.com/soso/fcgi-bin/search_cp?remoteplace=txt.yqq.center&aggr=1&cr=1&catZhida=1&lossless=0&flag_qc=0&p=" + PageNumber + "&n=" + PageSize + "&w=" + HttpUtility.UrlEncode(Query, Encoding.UTF8) + @"&format=json&inCharset=utf8&outCharset=utf-8&platform=yqq");
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    JObject p = JObject.Parse(str);
                    ResultData Result = new ResultData { List1 = new List<SongListItem>() };
                    Result.Tot = int.Parse(p["data"]["song"]["totalnum"].ToString());
                    foreach(var songinfo in p["data"]["song"]["list"])
                    {
                        SongListItem SLI = new SongListItem { Album1 = songinfo["albumname"].ToString(), Singer1 = "", SongId1 = songinfo["songmid"].ToString(), SongName1 = songinfo["songname"].ToString(), Lrc1 = "", Quality1 = new QualityList(), Pic1 = new SongPic(), SongInterval1 = songinfo["interval"].ToString() };
                        SLI.Pic1.Large1 = "https://y.gtimg.cn/music/photo_new/T002R800x800M000" + songinfo["albummid"].ToString() + ".jpg?max_age=2592000";
                        SLI.Pic1.Middle1 = "https://y.gtimg.cn/music/photo_new/T002R500x500M000" + songinfo["albummid"].ToString() + ".jpg?max_age=2592000";
                        SLI.Pic1.Small1 = "https://y.gtimg.cn/music/photo_new/T002R200x200M000" + songinfo["albummid"].ToString() + ".jpg?max_age=2592000";
                        SLI.Lrc1 = "https://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric_new.fcg?format=json&platform=yqq&g_tk=938407465&songmid=" + songinfo["songmid"].ToString();
                        if (int.Parse(songinfo["type"].ToString()) == 0)
                        {
                            if (int.Parse(songinfo["size128"].ToString()) != 0)
                            {
                                SLI.Quality1.Q128 = true;
                                SLI.Quality1.S128 = songinfo["size128"].ToString();
                                SLI.Quality1.F128 = "http://dl.stream.qqmusic.qq.com/M500" + songinfo["songmid"].ToString() + ".mp3";
                            }
                            if (int.Parse(songinfo["size320"].ToString()) != 0)
                            {
                                SLI.Quality1.Q320 = true;
                                SLI.Quality1.S320 = songinfo["size320"].ToString();
                                SLI.Quality1.F320 = "http://dl.stream.qqmusic.qq.com/M800" + songinfo["songmid"].ToString() + ".mp3";
                            }

                            //接口不支持获取无损歌曲
                            //if (int.Parse(songinfo["sizeape"].ToString()) != 0)
                            //{
                            //    SLI.Quality1.Ape = true;
                            //    SLI.Quality1.Sape = songinfo["sizeape"].ToString();
                            //    SLI.Quality1.Fape = "http://dl.stream.qqmusic.qq.com/A000" + songinfo["songmid"].ToString() + ".ape";
                            //}
                            //if (int.Parse(songinfo["sizeflac"].ToString()) != 0)
                            //{
                            //    SLI.Quality1.Flac = true;
                            //    SLI.Quality1.Sflac = songinfo["sizeflac"].ToString();
                            //    SLI.Quality1.Fflac = "http://dl.stream.qqmusic.qq.com/F000" + songinfo["songmid"].ToString() + ".flac";
                            //}

                        }
                        else
                        {
                            if (int.Parse(songinfo["type"].ToString()) == 2)
                            {
                                SLI.Quality1.Qother = true;
                                SLI.Quality1.Fother = songinfo["songurl"].ToString();
                            }
                        }
                        SLI.Singer1 = songinfo["singer"][0]["name"].ToString();
                        Result.List1.Add(SLI);
                    }
                    return Result;
                }
                catch (Exception ex)
                {

                    return new ResultData();
                }
            }

            /// <summary>
            /// 获取文件参数
            /// </summary>
            /// <returns>vkey等，直接接在链接后即可</returns>
            public static string GetVkey()
            {

                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"https://c.y.qq.com/base/fcgi-bin/fcg_musicexpress.fcg?json=3&guid=4450729731&g_tk=938407465&loginUin=0&hostUin=0&format=json&inCharset=utf8&outCharset=GB2312&notice=0&platform=yqq&needNewCode=0");
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    JObject p = JObject.Parse(str);
                    return "?vkey=" + p["key"] + "&guid=4450729731&fromtag=30";
                }
                catch (Exception ex)
                {

                    return "";
                }
            }

            /// <summary>
            /// 获取lrc
            /// </summary>
            /// <param name="url">lrcurl</param>
            /// <returns>lrc内容</returns>
            public static string GetLrc(string url)
            {
                try
                {
                    HttpWebRequest req;
                    HttpWebResponse rep;
                    req = (HttpWebRequest)WebRequest.Create(url);
                    req.Referer = "http://y.qq.com";
                    rep = (HttpWebResponse)req.GetResponse();
                    StreamReader Reader = new StreamReader(rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    str = str.Substring(18, str.Length - 19);
                    JObject p = JObject.Parse(str);
                    byte[] tmp= Convert.FromBase64String(p["lyric"].ToString());
                    return Encoding.UTF8.GetString(tmp);
                }
                catch(Exception ex)
                {

                    return "";
                }
            }
        }

        /// <summary>
        /// 酷狗音乐
        /// </summary>
        public class Kugou
        {
            /// <summary>
            /// 搜索函数
            /// </summary>
            /// <param name="Query">必选，关键字</param>
            /// <param name="PageNumber">可选，页码，默认为1</param>
            /// <param name="PageSize">可选，每页数据条数，默认为20</param>
            /// <returns>结果</returns>
            public static ResultData Search(string Query, int PageNumber = 1, int PageSize = 20)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"http://songsearch.kugou.com/song_search_v2?clientver=8018&keyword=" + HttpUtility.UrlEncode(Query, Encoding.UTF8) + @"&pagesize=" + PageSize + @"&page=" + PageNumber);
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    JObject p = JObject.Parse(str);
                    ResultData Result = new ResultData { List1 = new List<SongListItem>() };
                    Result.Tot = int.Parse(p["data"]["total"].ToString());
                    foreach (var songinfo in p["data"]["lists"])
                    {
                        SongListItem SLI = new SongListItem { Album1 = songinfo["AlbumName"].ToString(), Singer1 = songinfo["SingerName"].ToString(), SongId1 = songinfo["ID"].ToString(), SongName1 = songinfo["SongName"].ToString(), Lrc1 = "", Quality1 = new QualityList(), Pic1 = new SongPic(), SongInterval1 = songinfo["Duration"].ToString()};
                        SLI.Lrc1 = "http://lyrics.kugou.com/search?ver=1&man=yes&client=pc&keyword=" + HttpUtility.UrlEncode(songinfo["SingerName"].ToString() + "-" + songinfo["SongName"].ToString(),Encoding.UTF8) + "&duration=" + songinfo["Duration"].ToString() + "000&hash=" + songinfo["FileHash"].ToString();
                        if (songinfo["FileHash"].ToString() != "00000000000000000000000000000000")
                        {
                            SLI.Quality1.Q128 = true;
                            SLI.Quality1.S128 = songinfo["FileSize"].ToString();
                            SLI.Quality1.F128 = songinfo["FileHash"].ToString();
                        }
                        if (songinfo["HQFileHash"].ToString() != "00000000000000000000000000000000")
                        {
                            SLI.Quality1.Q320 = true;
                            SLI.Quality1.S320 = songinfo["HQFileSize"].ToString();
                            SLI.Quality1.F320 = songinfo["HQFileHash"].ToString();
                        }
                        if (songinfo["SQFileHash"].ToString() != "00000000000000000000000000000000")
                        {
                            SLI.Quality1.Ape = true;
                            SLI.Quality1.Sape = songinfo["SQFileSize"].ToString();
                            SLI.Quality1.Fape = songinfo["SQFileHash"].ToString();
                        }
                        Result.List1.Add(SLI);
                    }
                    return Result;
                }
                catch (Exception ex)
                {

                    return new ResultData();
                }
            }

            /// <summary>
            /// 获取歌曲下载信息
            /// </summary>
            /// <param name="Hash">必选，歌曲Hash</param>
            /// <returns>歌曲地址</returns>
            public static string GetSongUrl(string Hash)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"http://trackercdn.kugou.com/i/?cmd=4&hash=" + Hash + @"&key=" + GetMd5HashStr(Hash + "kgcloud") + "&pid=1");
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    System.Diagnostics.Debug.WriteLine(str);
                    JObject p = JObject.Parse(str);
                    return p["url"].ToString();
                }
                catch(Exception ex)
                {

                    return "";
                }
            }

            /// <summary>
            /// 获取lrc
            /// </summary>
            /// <param name="url">lrcurl</param>
            /// <returns>lrc内容</returns>
            public static string GetLrc(string url)
            {
                try
                {
                    HttpWebRequest req1,req2;
                    HttpWebResponse rep1,rep2;
                    req1 = (HttpWebRequest)WebRequest.Create(url);
                    rep1 = (HttpWebResponse)req1.GetResponse();
                    StreamReader Reader1 = new StreamReader(rep1.GetResponseStream());
                    string str1 = Reader1.ReadToEnd();
                    JObject p = JObject.Parse(str1);
                    req2 = (HttpWebRequest)WebRequest.Create("http://lyrics.kugou.com/download?ver=1&client=pc&id=" + p["candidates"][0]["id"].ToString() + "&accesskey=" + p["candidates"][0]["accesskey"].ToString() + "&fmt=lrc&charset=utf8");
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
        }

        /// <summary>
        /// 5sing
        /// </summary>
        public class FiveSing
        {
            /// <summary>
            /// 搜索函数
            /// </summary>
            /// <param name="Query">必选，关键字</param>
            /// <param name="PageNumber">可选，页码，默认为1</param>
            /// <param name="PageSize">可选，每页数据条数，默认为20</param>
            /// <returns>结果</returns>
            public static ResultData Search(string Query, int PageNumber = 1, int PageSize = 10)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"http://search.5sing.kugou.com/home/json?keyword=" + HttpUtility.UrlEncode(Query,Encoding.UTF8) + "&sort=1&page=" + PageNumber + "&filter=0&type=0");
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    str = str.Replace("<em class=\\\\\\\"keyword\\\\\\\">", "").Replace("<\\/em>", "");
                    JObject p = JObject.Parse(str);
                    ResultData Result = new ResultData { List1 = new List<SongListItem>() };
                    Result.Tot = int.Parse(p["pageInfo"]["totalCount"].ToString());
                    foreach (var songinfo in p["list"])
                    {
                        SongListItem SLI = new SongListItem { Album1 = "", Singer1 = songinfo["singer"].ToString(), SongId1 = songinfo["songId"].ToString(), SongName1 = songinfo["songName"].ToString(), Lrc1 = "", Quality1 = new QualityList(), Pic1 = new SongPic(), SongInterval1 = "" };
                        SLI.Quality1.Qother = true;
                        SLI.Quality1.Sother = songinfo["songSize"].ToString();
                        SLI.Quality1.Fother = songinfo["typeEname"].ToString() + "-" + songinfo["songId"].ToString();
                        Result.List1.Add(SLI);
                    }
                    return Result;
                }
                catch (Exception ex)
                {

                    return new ResultData();
                }
            }


            ///<summary>
            /// 获取地址
            /// </summary>
            /// <param name="LinkStr">连接字符串，储存在fother</param>
            /// <returns>歌曲地址</returns>
            public static string GetSong(string LinkStr)
            {
                HttpWebRequest Req;
                HttpWebResponse Rep;
                try
                {
                    Req = (HttpWebRequest)WebRequest.Create(@"http://5sing.kugou.com/m/detail/" + LinkStr + "-1.html");
                    Rep = (HttpWebResponse)Req.GetResponse();
                    StreamReader Reader = new StreamReader(Rep.GetResponseStream());
                    string str = Reader.ReadToEnd();
                    str = Regex.Split(str, "<audio src=\"")[1];
                    str = Regex.Split(str, "\" id=\"player\" volume=\"0.5\"></audio>")[0];
                    return str;
                }
                catch(Exception ex)
                {

                    return "";
                }
            }

        }
    }
}
