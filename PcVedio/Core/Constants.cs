/// <summary>
/// copyright:  Zac (suoxd123@126.com)
/// 2017-03-14
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Constants
    {
    }

    /// <summary>
    /// 命令
    /// </summary>
    public enum enumCommandType
    {
        LOGIN1_REQ = 502,
        LOGIN1_RESP = 510,
        LOGIN2_REQ = 521,
        LOGIN2_RESP = 533,

        KEEP_ALIVE = 542,

        GET_PROPERTIES_REQ = 550,
        GET_PROPERTIES_RESP = 561,

        GET_PARAMS_REQ = 574,
        GET_PARAMS_RESP = 583,
        SET_PARAMS_REQ = 590,
        SET_PARAMS_RESP = 501,

        MONITOR_STATUS_REQ = 118,
        MONITOR_STATUS_RESP = 124,

        STATUS_CHANGED_NOTIFY = 135,

        PLAY_VIDEO_REQ = 147,
        PLAY_VIDEO_RESP = 151,
        STOP_VIDEO_REQ = 164,
        VIDEO_DATA = 176,

        PLAY_AUDIO_REQ = 188,
        PLAY_AUDIO_RESP = 195,
        STOP_AUDIO_REQ = 202,
        AUDIO_DATA = 214,

        START_TALK_REQ = 226,
        START_TALK_RESP = 234,
        STOP_TALK_REQ = 247,
        TALK_DATA = 253,

        PTZ_CONTROL_REQ = 264,
        PTZ_CONTROL_RESP = 277,

        SET_VIDEO_PERFORMANCE = 284,
        CAN_SET_VIDEO_PERFORMANCE_NOTIFY = 294,

        VIDEO_DATA2 = 374,

        PLAY_RECORD_REQ = 419,
        PLAY_RECORD_RESP = 424,
        PLAY_RECORD_NOTIFY = 432,
        STOP_RECORD_REQ = 443,
        PAUSE_RECORD_REQ = 454,

        CONTINUE_RECORD_REQ = 465
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public enum enumRespResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0x00,
        /// <summary>
        /// 用户权限不足
        /// </summary>
        NoRight,
        /// <summary>
        /// 当前音视频会话数目过多，拒绝
        /// </summary>
        TooMuch,
        /// <summary>
        /// 设备内部错误
        /// </summary>
        DeviceError,
        /// <summary>
        /// 参数错误
        /// </summary>
        ErrorParam,
        /// <summary>
        /// 当前禁止音视频访问
        /// </summary>
        Prohiden,
        /// <summary>
        /// 视频操作失败
        /// </summary>
        HandlerError,
        /// <summary>
        /// 当前连接正在播放视频
        /// </summary>
        IsWorking
    }

    /// <summary>
    /// 视频数据压缩格式
    /// </summary>
    public enum enumVideoZipType
    {
        MJPEG = 0x00,
        MPEG4,
        H264
    }

    /// <summary>
    /// 视频数据图像分辨率
    /// </summary>
    public enum enumVideoResolution
    {
        _128_96 = 0x00,
        _160_112,
        _160_120,
        _176_144,
        _320_240,
        _352_288,
        _640_480,
        _704_576,
        _720_576,
        _800_600,
        _1024_768,
        _1280_720,
        _256_144,
        _384_216,
        _512_288,
        _640_360,
        _768_432,
        _896_504,
        _1024_576
    }

    public struct VideoData
    {
        public enumVideoZipType Type;
        public enumVideoResolution Resolution;
        public int Video;
        public int NeedPhoto;
        public int IsKeyImg;
        public bool NewFlag;
        public int Time;
        public int TakeTime;
        public int Length;
        public byte[] Data;
    }

    public struct WifiRespInfo
    {
        public int Magic;
        public enumCommandType Code;
        public string ID;
        public string Version;
        public string Web;
        public string Name;
        public string IP;
        public string NetMask;
        public bool IsDhcp;
        public string IPStatic;
        public string NetMaskStatic;
        public string NetGateStatic;
        public string DNS1;
        public string DNS2;
        public int Port;
        public bool IsHttps;
        public string Number;
        public int Type;
    }

    public struct NormalDataStruct
    {
        public int MagicNumber;
        public enumCommandType Code;
        public int contentLen;
        public int FillField;
        public byte[] Content;
    }

    public struct Login1Struct
    {
        public int Length;
        public byte[] Encode;
        public byte[] Decode;
        public string Plain;
    }
}
