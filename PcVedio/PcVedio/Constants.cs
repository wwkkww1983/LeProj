using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcVedio
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
        public int ID;
        public string Encode;
        public string Plain;
    }
}
