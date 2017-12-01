using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace XczdServer
{
    /// <summary>
    /// 处理来自设备端的数据
    /// </summary>
    public interface interfaceClientHanlder
    {
        byte[] HandlerClientData(byte[] buff);
        bool ShouldResponse();
    }

    /// <summary>
    /// 生产数据
    /// </summary>
    public class ClientHanlderProductInfo : interfaceClientHanlder
    {
        private int decodeByte2Int(byte[] buff, ref int startIdx, int length)
        {
            int result = 0;
            byte[] tempByte = new byte[length];
            Array.Copy(buff, startIdx, tempByte, 0, length);
            if (length == 2)
            {
                result = ConvertHelper.BytesToInt16(tempByte, true);
            }
            else if (length == 4)
            {
                result = ConvertHelper.BytesToInt32(tempByte, true);
            }
            startIdx += length;
            return result;
        }
        /// <summary>
        /// 上传生产情况解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private NetStructure.ProductInfo DecodeData(byte[] buff)
        {
            NetStructure.ProductInfo info = new NetStructure.ProductInfo();
            byte[] tempData = buff;
            //机器码
            int locIdx = 0;
            info.MachineId = decodeByte2Int(tempData, ref locIdx, 2);
            //施工单编码
            int tempLen = tempData[locIdx++];
            byte[] scheduleByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, scheduleByte, 0, tempLen);
            info.ScheduleNumber = Encoding.ASCII.GetString(scheduleByte);
            locIdx += tempLen;
            //通道数
            info.ChannelCount = tempData[locIdx++];
            //工号
            tempLen = tempData[locIdx++];
            byte[] numberByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, numberByte, 0, tempLen);
            info.StaffNumber = Encoding.ASCII.GetString(numberByte);
            locIdx += tempLen;
            //姓名
            tempLen = tempData[locIdx++];
            byte[] nameByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, nameByte, 0, tempLen);
            info.StaffName = Encoding.GetEncoding("GBK").GetString(nameByte);
            locIdx += tempLen;
            //状态标志位
            info.MsgStatus = (enumProductType)tempData[locIdx++];
            //各通道已完成数
            info.ChannelFinish1 = decodeByte2Int(tempData, ref locIdx, 4);
            info.ChannelFinish2 = decodeByte2Int(tempData, ref locIdx, 4);
            info.ChannelFinish3 = decodeByte2Int(tempData, ref locIdx, 4);
            info.ChannelFinish4 = decodeByte2Int(tempData, ref locIdx, 4);
            info.ChannelFinish5 = decodeByte2Int(tempData, ref locIdx, 4);
            info.ChannelFinish6 = decodeByte2Int(tempData, ref locIdx, 4);
            //异常数
            info.UnusualCount = decodeByte2Int(tempData, ref locIdx, 4);

            return info;
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            byte[] buffResp = { 0 };
            DbHandler db = new DbHandler();
            NetStructure.ProductInfo outInfo = this.DecodeData(buff);
            db.InsertProductInfo(outInfo);

            buffResp[0] = 0;
            return buffResp;
        }

        public void HandlerTest()
        {
            DbHandler db = new DbHandler();
            NetStructure.ProductInfo outInfo = new NetStructure.ProductInfo()
            {
                ChannelCount = 6,
                ChannelFinish1 = 50944,
                ChannelFinish2 = 52079,
                ChannelFinish3 = 51180,
                ChannelFinish4 = 51909,
                ChannelFinish5 = 5,
                ChannelFinish6 = 3,
                UnusualCount = 10,
                StaffNumber = "111",
                StaffName = "咦咦咦",
                MsgStatus = enumProductType.LoginIn,
                ScheduleNumber = "11111-0012",
                MachineId = 15
            };
            db.InsertProductInfo(outInfo);
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }

    /// <summary>
    /// 心跳数据
    /// </summary>
    public class ClientHandlerHeartBreak : interfaceClientHanlder
    {
        /// <summary>
        /// 客户端返回消息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private NetStructure.HeartBreak DecodeData(byte[] buff)
        {
            NetStructure.HeartBreak info = new NetStructure.HeartBreak();
            byte[] tempData = buff;
            info.MachineId = ConvertHelper.BytesToInt16(tempData, true);
            info.ChannelInfo = tempData[2];

            return info;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        private HeartBreak exchangeData(NetStructure.HeartBreak info, Machines machine)
        {
            return new HeartBreak()
            {
                DateCreate = DateTime.Now,
                ChannelInfo = info.ChannelInfo,
                MachineId = machine.ID,
                MachineName = machine.Number
            };
        }

        private void RefreshOnlineInfo(Machines machine)
        {
            const string ONLINE_FACTRORY_ROOM = "ONLINEFACTRORYROOM", PRE_ONLINE_MACHINE = "PREONLINEMACHINE", 
                         PRE_MACHINE_NAME_NUMBER = "PREMACHINENAMENUMBER", PRE_ONLINE_TIME = "PREONLINETIME";

            ServiceStack.Redis.IRedisClient client = GlobalVariable.RedisClient;
            HashSet<string> roomList = client.GetAllItemsFromSet(ONLINE_FACTRORY_ROOM);
            if (!roomList.Contains(machine.RoomName))
            {
                client.AddItemToSet(ONLINE_FACTRORY_ROOM, machine.RoomName);
            }
            string strMachineValue = PRE_ONLINE_MACHINE + machine.RoomName;
            HashSet<string> machineList = client.GetAllItemsFromSet(strMachineValue);
            if (!machineList.Contains(machine.Number))
            {
                client.AddItemToSet(strMachineValue, machine.Number);
                client.Set<string>(PRE_MACHINE_NAME_NUMBER + machine.Number, machine.Name);
            }
            client.Set<long>(PRE_ONLINE_TIME + machine.Number, DateTime.Now.Ticks);
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            DbHandler db = new DbHandler();
            NetStructure.HeartBreak outInfo = this.DecodeData(buff);
            Machines machine = db.SelectMachine(outInfo.MachineId);
            HeartBreak innerInfo = this.exchangeData(outInfo, machine);
            //记录原始数据
            db.InsertHeartBreak(innerInfo);
            //记录设备状态
            RefreshOnlineInfo(machine);

            byte[] resp = { };
            return resp;
        }

        public void HandlerTest()
        {
            DbHandler db = new DbHandler();
            NetStructure.HeartBreak outInfo = new NetStructure.HeartBreak()
            {
                MachineId = 19,
                ChannelInfo = 6
            };
            Machines machine = db.SelectMachine(outInfo.MachineId);
            HeartBreak innerInfo = this.exchangeData(outInfo, machine);
            //记录原始数据
            db.InsertHeartBreak(innerInfo);
            ////记录设备状态
            //RefreshOnlineInfo(machine);
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }

    /// <summary>
    /// 设备设置
    /// </summary>
    public class ClientHandlerDeviceSetting : interfaceClientHanlder
    {
        /// <summary>
        /// 客户端返回消息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private NetStructure.DeviceSetting DecodeData(byte[] buff)
        {
            NetStructure.DeviceSetting info = new NetStructure.DeviceSetting();
            byte[] tempData = buff;

            //机器码
            int locIdx = 0;
            info.DeviceId = ConvertHelper.BytesToInt16(tempData, locIdx, true);
            locIdx += 2;
            //操作类型
            info.OperateType = tempData[locIdx++];
            int tempLen = tempData[locIdx++];
            //设备编码
            byte[] numberByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, numberByte, 0, tempLen);
            info.DeviceNumber = System.Text.Encoding.GetEncoding("GBK").GetString(numberByte);
            locIdx += tempLen;
            //车间号
            tempLen = 4;
            byte[] roomByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, roomByte, 0, tempLen);
            info.RoomNumber = ConvertHelper.BytesToInt32(roomByte).ToString();
            locIdx += tempLen;
            //IP地址
            StringBuilder builderIP = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                builderIP.Append(tempData[locIdx++]);
                builderIP.Append('.');
            }
            info.IPAddress = builderIP.ToString().Substring(0, builderIP.Length - 1);

            return info;
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            int resInt = 0, machineId = 0;
            byte[] byteResp = { 0, 0, 0 };
            DbHandler db = new DbHandler();
            NetStructure.DeviceSetting outInfo = this.DecodeData(buff);
            resInt = db.InsertMachines(outInfo, out machineId);

            //生成返回结果
            byteResp[0] = (byte)resInt;
            byte[] byteID = ConvertHelper.Int16ToBytes(machineId, true);
            Array.Copy(byteID, 0, byteResp, 1, 2);
            return byteResp;
        }

        public void HandlerTest()
        {
            int resInt = 0, machineId = 0;
            byte[] byteResp = { 0, 0, 0 };
            DbHandler db = new DbHandler();
            NetStructure.DeviceSetting outInfo = new NetStructure.DeviceSetting()
            {
                DeviceId = 0,
                IPAddress = "1.1.1.12",
                DeviceName = "测试1",
                DeviceNumber = "TEST112",
                RoomNumber = "22",
                OperateType = 1
            };
            resInt = db.InsertMachines(outInfo, out machineId);

            //生成返回结果
            byteResp[0] = (byte)resInt;
            byte[] byteID = ConvertHelper.Int16ToBytes(machineId, true);
            Array.Copy(byteID, 0, byteResp, 1, 2);

        }

        public bool ShouldResponse()
        {
            return true;
        }
    }

    /// <summary>
    /// 设备报修记录
    /// </summary>
    public class ClientHandlerDeviceReport : interfaceClientHanlder
    {
        private const string EXTERNAL_URL = "http://192.168.0.231:8096/Onlinewebxz.asmx/InsertMaintainInfo";
        /// <summary>
        /// 客户端返回消息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private int DecodeData(byte[] buff)
        {
            byte[] tempData = buff;
            int machineId = ConvertHelper.BytesToInt16(tempData, true);

            return machineId;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        private MachineReport exchangeData(Machines machine)
        {
            return new MachineReport()
            {
                DateCreate = DateTime.Now,
                MachineNumber = machine.Number,
                MachineName = machine.Name,
                MachineId = machine.ID,
                RoomId = machine.RoomID,
                RoomNumber = machine.RoomName,
                RoomName = machine.RoomName
            };
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            byte[] buffResp = { 1 };
            DbHandler db = new DbHandler();
            try
            {
                int outInfo = this.DecodeData(buff);
                Machines machine = db.SelectMachine(outInfo);
                MachineReport innerInfo = this.exchangeData(machine);
                //记录原始数据
                db.InsertMachineReport(innerInfo);
                //调用外部接口
                string reportNumber = db.GetRoomReportNumber(machine.RoomID);
                Dictionary<string, string> tempParam = new Dictionary<string, string>() { { "code", machine.Number }, { "adminname", reportNumber } };
                string strInfo = WebInfo.PostPageInfo(EXTERNAL_URL, tempParam);
                if (!strInfo.Contains("成功"))
                {
                    db.InsertErrorInfo(enumSystemErrorCode.DeviceReportOutInterface, null, string.Format("{0}:{1}", reportNumber, machine.Number), null);
                }

                buffResp[0] = 0;
            }
            catch
            {

            }
            return buffResp;
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }

    /// <summary>
    /// 设备叫料记录
    /// </summary>
    public class ClientHandlerDeviceCallMaterial : interfaceClientHanlder
    {
        /// <summary>
        /// 客户端返回消息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private NetStructure.DeviceMaterial DecodeData(byte[] buff)
        {

            NetStructure.DeviceMaterial info = new NetStructure.DeviceMaterial();
            byte[] tempData = buff;
            //机器码
            int locIdx = 2;
            info.MachineId = ConvertHelper.BytesToInt16(tempData, 0, true);
            //施工单编码
            int tempLen = tempData[locIdx++];
            byte[] scheduleByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, scheduleByte, 0, tempLen);
            info.ScheduleNumber = Encoding.ASCII.GetString(scheduleByte);

            return info;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        private MachineCallMaterial exchangeData(Machines machine, Schedules schedule)
        {
            return new MachineCallMaterial()
            {
                DateCreate = DateTime.Now,
                MachineNumber = machine.Number,
                MachineId = machine.ID,
                MachineName = machine.Name,
                RoomId = machine.RoomID,
                RoomNumber = machine.RoomName,
                RoomName = machine.RoomName,
                OrderId = schedule.OrderId,
                OrderNumber = schedule.OrderNumber,
                ScheduleId = schedule.ID,
                SchueduleNumber = schedule.Number,
                MaterialInfo = schedule.MaterialDetail,
                Status = enumDeviceWarnningStatus.Unhandler
            };
        }


        public byte[] HandlerClientData(byte[] buff)
        {
            byte[] buffResp = { 1 };
            DbHandler db = new DbHandler();
            try
            {
                NetStructure.DeviceMaterial outInfo = this.DecodeData(buff);
                Machines machine = db.SelectMachine(outInfo.MachineId);
                Schedules schedule = db.SelectSchedule(outInfo.ScheduleNumber);
                MachineCallMaterial innerInfo = this.exchangeData(machine, schedule);
                //记录原始数据
                db.InsertMachineCallMaterial(innerInfo);

                buffResp[0] = 0;
            }
            catch
            {

            }
            return buffResp;
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }

    /// <summary>
    /// 设备启动停止记录
    /// </summary>
    public class ClientHandlerDeviceStartEnd : interfaceClientHanlder
    {
        /// <summary>
        /// 客户端返回消息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private NetStructure.DeviceStartEnd DecodeData(byte[] buff)
        {

            NetStructure.DeviceStartEnd info = new NetStructure.DeviceStartEnd();
            byte[] tempData = buff;
            //机器码
            int locIdx = 2;
            info.MachineId = ConvertHelper.BytesToInt16(tempData, 0, true);
            //启停标志
            info.Status = (enumDeviceWorkStatus)tempData[locIdx++];
            //施工单编码
            int tempLen = tempData[locIdx++];
            byte[] scheduleByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, scheduleByte, 0, tempLen);
            info.ScheduleNumber = Encoding.ASCII.GetString(scheduleByte);
            locIdx += tempLen;
            //员工工号
            tempLen = tempData[locIdx++];
            byte[] userByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, userByte, 0, tempLen);
            info.UserNumber = Encoding.ASCII.GetString(userByte);
            locIdx += tempLen;

            return info;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        private MachineStartEnd exchangeData(NetStructure.DeviceStartEnd outInfo, Machines machine, Schedules schedule, UserProfile userInfo)
        {
            return new MachineStartEnd()
            {
                MachineNumber = machine.Number,
                MachineId = machine.ID,
                MachineName = machine.Name,
                RoomId = machine.RoomID,
                RoomNumber = machine.RoomName,
                OrderId = schedule.OrderId,
                OrderNumber = schedule.OrderNumber,
                ScheduleId = schedule.ID,
                SchueduleNumber = schedule.Number,
                UserId = userInfo == null ? 0 : userInfo.UserId,
                UserNumber = userInfo == null ? "" : userInfo.UserName,
                Status = outInfo.Status
            };
        }


        public byte[] HandlerClientData(byte[] buff)
        {
            byte[] buffResp = { 1 };
            DbHandler db = new DbHandler();
            try
            {
                DateTime currentTime = DateTime.Now;
                NetStructure.DeviceStartEnd outInfo = this.DecodeData(buff);
                Machines machine = db.SelectMachine(outInfo.MachineId);
                Schedules schedule = db.SelectSchedule(outInfo.ScheduleNumber);
                UserProfile userInfo = db.SelectUser(outInfo.UserNumber);
                MachineStartEnd innerInfo = this.exchangeData(outInfo, machine, schedule, userInfo);
                //记录原始数据

                if (outInfo.Status == enumDeviceWorkStatus.Start)
                {
                    innerInfo.DateStart = currentTime;
                    db.InsertMachineStartEnd(innerInfo);
                }
                else
                {
                    int startId = db.GetStartEndID(new MachineStartEnd()
                    {
                        MachineId = outInfo.MachineId,
                        SchueduleNumber = outInfo.ScheduleNumber,
                        UserNumber = outInfo.UserNumber,
                        Status = enumDeviceWorkStatus.Start
                    });
                    if (startId == 0)
                    {
                        innerInfo.DateEnd = currentTime;
                        db.InsertMachineStartEnd(innerInfo);
                    }
                    else
                    {
                        db.FinishStartStatus(startId);
                    }
                }

                buffResp[0] = 0;
            }
            catch
            {

            }
            return buffResp;
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }

    /// <summary>
    /// 下派施工单返回消息
    /// </summary>
    public class ClientHandlerDownScheduleResp : interfaceClientHanlder
    {

        /// <summary>
        /// 客户端返回结果解码
        /// </summary>
        /// <param name="buff"></param>
        private NetStructure.ClientResp DecodeData(byte[] buff)
        {
            NetStructure.ClientResp info = new NetStructure.ClientResp();
            byte[] tempData = buff;
            info.MachineId = ConvertHelper.BytesToInt16(tempData, true);

            switch (tempData[2])
            {
                case 1: info.RespResult = enumErrorCode.DeviceRespFailInfo; break;
                case 2: info.RespResult = enumErrorCode.DeviceScheduleFull; break;
                default: info.RespResult = enumErrorCode.HandlerSuccess; break;
            }
            return info;
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            NetStructure.ClientResp outInfo = this.DecodeData(buff);

            ServiceStack.Redis.IRedisClient client = GlobalVariable.RedisClient;
            string strUserKey = client.Get<string>(GlobalVariable.PRE_INFO_TYPE_CREATE + outInfo.MachineId.ToString());

            client.Set<int>(GlobalVariable.PRE_RESP_DOWN_INFO + strUserKey, (int)outInfo.RespResult);
            client.Remove(GlobalVariable.PRE_DOWN_INFO_MACHINE + strUserKey);
            client.Remove(GlobalVariable.PRE_DOWN_INFO + strUserKey);

            return null;
        }

        public bool ShouldResponse()
        {
            return false;
        }
    }

    /// <summary>
    /// 关闭施工单返回信息
    /// </summary>
    public class ClientHandlerDownScheCloseResp : interfaceClientHanlder
    {

        /// <summary>
        /// 客户端返回结果解码
        /// </summary>
        /// <param name="buff"></param>
        private NetStructure.ClientResp DecodeData(byte[] buff)
        {
            NetStructure.ClientResp info = new NetStructure.ClientResp();
            byte[] tempData = buff;
            info.MachineId = ConvertHelper.BytesToInt16(tempData, true);

            switch (tempData[2])
            {
                case 1: info.RespResult = enumErrorCode.DeviceRespFailInfo; break;
                case 2: info.RespResult = enumErrorCode.DeviceScheduleWorking; break;
                default: info.RespResult = enumErrorCode.HandlerSuccess; break;
            }
            return info;
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            NetStructure.ClientResp outInfo = this.DecodeData(buff);

            ServiceStack.Redis.IRedisClient client = GlobalVariable.RedisClient;
            string strUserKey = client.Get<string>(GlobalVariable.PRE_INFO_TYPE_CLOSE + outInfo.MachineId.ToString());

            client.Set<int>(GlobalVariable.PRE_RESP_DOWN_INFO + strUserKey, (int)outInfo.RespResult);
            client.Remove(GlobalVariable.PRE_DOWN_INFO_MACHINE + strUserKey);
            client.Remove(GlobalVariable.PRE_DOWN_INFO + strUserKey);

            return null;
        }

        public bool ShouldResponse()
        {
            return false;
        }
    }

    /// <summary>
    /// 报废施工单返回信息
    /// </summary>
    public class ClientHandlerDownScheDiscardResp : interfaceClientHanlder
    {

        /// <summary>
        /// 客户端返回结果解码
        /// </summary>
        /// <param name="buff"></param>
        private NetStructure.ClientResp DecodeData(byte[] buff)
        {
            NetStructure.ClientResp info = new NetStructure.ClientResp();
            byte[] tempData = buff;
            info.MachineId = ConvertHelper.BytesToInt16(tempData, true);

            switch (tempData[2])
            {
                case 1: info.RespResult = enumErrorCode.DeviceRespFailInfo; break;
                case 2: info.RespResult = enumErrorCode.DeviceScheduleWorking; break;
                default: info.RespResult = enumErrorCode.HandlerSuccess; break;
            }
            return info;
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            NetStructure.ClientResp outInfo = this.DecodeData(buff);

            ServiceStack.Redis.IRedisClient client = GlobalVariable.RedisClient;
            string strUserKey = client.Get<string>(GlobalVariable.PRE_INFO_TYPE_DISCARD + outInfo.MachineId.ToString());

            client.Set<int>(GlobalVariable.PRE_RESP_DOWN_INFO + strUserKey, (int)outInfo.RespResult);
            client.Remove(GlobalVariable.PRE_DOWN_INFO_MACHINE + strUserKey);
            client.Remove(GlobalVariable.PRE_DOWN_INFO + strUserKey);

            return null;
        }

        public bool ShouldResponse()
        {
            return false;
        }
    }

    /// <summary>
    /// 命令错误，默认处理方式
    /// </summary>
    public class ClientHandlerNoneDefault : interfaceClientHanlder
    {
        public byte[] HandlerClientData(byte[] buff)
        {
            DbHandler db = new DbHandler();

            db.InsertErrorInfo(enumSystemErrorCode.TcpDefaultHandlerErr, null, "", buff);

            return null;
        }

        public bool ShouldResponse()
        {
            return false;
        }
    }
}
