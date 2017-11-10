using System;
using System.Text;
using ZdflCount.Models;
using System.Linq;

namespace ZdflCount.App_Start
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
        private int decodeByte2Int(byte[] buff,ref int startIdx,int length)
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
        private ProductInfo DecodeData(byte[] buff)
        {
            ProductInfo info = new ProductInfo();
            byte[] tempData = buff;
            //机器码
            int locIdx = 0;
            info.MachineId = decodeByte2Int(buff,ref locIdx, 2);
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
            info.ChannelFinish1 = decodeByte2Int(buff,ref locIdx, 4);
            info.ChannelFinish2 = decodeByte2Int(buff, ref locIdx, 4);
            info.ChannelFinish3 = decodeByte2Int(buff, ref locIdx, 4);
            info.ChannelFinish4 = decodeByte2Int(buff, ref locIdx, 4);
            info.ChannelFinish5 = decodeByte2Int(buff, ref locIdx, 4);
            info.ChannelFinish6 = decodeByte2Int(buff, ref locIdx, 4);
            //异常数
            info.UnusualCount = decodeByte2Int(buff, ref locIdx, 4);

            return info;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        private Models.ProductInfo exchangeData(ProductInfo info, Machines machine)
        {
            return new Models.ProductInfo()
            {
                DateCreate = DateTime.Now,
                ChannelCount = info.ChannelCount,

                staffNumber = info.StaffNumber,
                StaffName = info.StaffName,

                MachineIP = machine.IpAddress,
                MachineId = machine.ID,
                MachineName = machine.Name,

                MsgType = (byte)info.MsgStatus,

                ChannelFinish1 = info.ChannelFinish1,
                ChannelFinish2 = info.ChannelFinish2,
                ChannelFinish3 = info.ChannelFinish3,
                ChannelFinish4 = info.ChannelFinish4,
                ChannelFinish5 = info.ChannelFinish5,
                ChannelFinish6 = info.ChannelFinish6,

                ExceptionCount = info.UnusualCount
            };
        }

        private void RecordStatisticsInfo(ProductInfo outInfo, Machines machine, int finishCount, DbTableDbContext db)
        {
            StatisticInfo statistics = new StatisticInfo()
            {
                Date = DateTime.Now,
                ExceptionCount = outInfo.UnusualCount,
                FinishCount = finishCount,
                MachineName = machine.Name,
                MachineNumber = machine.Number,
                StaffName = outInfo.StaffName,
                StaffNumber = outInfo.StaffNumber,
                RoomID = machine.RoomID,
                RoomName = machine.RoomName,
                Factory = "振德敷料"
            };
            //订单信息
            Schedules tempSchedule = db.Schedules.First(item => item.Number == outInfo.ScheduleNumber);
            statistics.OrderNumber = tempSchedule.OrderNumber;
            db.Statistics.Add(statistics);
            //施工单生产记录
            db.Schedules.Attach(tempSchedule);
            tempSchedule.FinishCount += finishCount;
            tempSchedule.Status =tempSchedule.FinishCount>=tempSchedule.ProductCount? enumStatus.Finished: enumStatus.Working;
            //订单生产记录
            Orders tempOrder = db.Orders.Find(tempSchedule.OrderId);
            db.Orders.Attach(tempOrder);
            tempOrder.ProductFinishedCount += finishCount;
            tempOrder.Status =tempOrder.ProductFinishedCount>= tempOrder.ProductCount? enumStatus.Finished: enumStatus.Working;
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            byte[] buffResp = { 1 };
            DbTableDbContext db = new DbTableDbContext();
            ProductInfo outInfo = this.DecodeData(buff);
            Machines machine = db.Machines.Find(outInfo.MachineId);
            Models.ProductInfo innerInfo = this.exchangeData(outInfo, machine);
            //记录统计表
            if (outInfo.MsgStatus == enumProductType.LoginOut)
            {
                Models.ProductInfo lastInfo = db.ProductInfo.OrderByDescending(tmpItem => tmpItem.ID).First(item => item.MachineId == outInfo.MachineId);
                if (lastInfo.MsgType != (byte)enumProductType.LoginIn)
                    return buffResp;
                if (lastInfo.staffNumber != outInfo.StaffNumber)
                {
                    db.RecordErrorInfo(enumSystemErrorCode.ProductOutInDiff, lastInfo.ToString() + "\r\n" + innerInfo.ToString(), null);
                }
                int currentFinishCount = (outInfo.ChannelFinish1 + outInfo.ChannelFinish2 + outInfo.ChannelFinish3 +
                    outInfo.ChannelFinish4 + outInfo.ChannelFinish5 + outInfo.ChannelFinish6 - lastInfo.ChannelFinish1 -
                    lastInfo.ChannelFinish2 - lastInfo.ChannelFinish3 - lastInfo.ChannelFinish4 - lastInfo.ChannelFinish5 -
                    lastInfo.ChannelFinish6);
                RecordStatisticsInfo(outInfo,machine,currentFinishCount,db);
            }
            //记录原始数据
            db.ProductInfo.Add(innerInfo);
            db.SaveChanges();
            buffResp[0] = 0;
            return buffResp;
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
        private HeartBreak DecodeData(byte[] buff)
        {
            HeartBreak info = new HeartBreak();
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
        private Models.HeartBreak exchangeData(HeartBreak info, Machines machine)
        {
            return new Models.HeartBreak()
            {
                DateCreate = DateTime.Now,
                ChannelInfo = info.ChannelInfo,
                MachineId = machine.ID,
                MachineName = machine.Number
            };
        }

        private void RefreshOnlineInfo(Machines machine, DbTableDbContext db)
        {
            DateTime now = DateTime.Now;
            DeviceStatus roomStatus = GlobalVariable.deviceStatusList.Find(item => item.RoomID == machine.RoomID);
            if (roomStatus == null)
            {
                FactoryRoom tempRoom = db.FactoryRoom.Find(machine.RoomID);
                roomStatus = new DeviceStatus()
                {
                    FactoryName = tempRoom.FactoryName,
                    MachineCount = tempRoom.MachineCount,
                    RoomID = machine.RoomID,
                    RoomName = machine.RoomName,
                    MachineList = new System.Collections.Generic.Dictionary<string, DateTime>()
                };
                roomStatus.MachineList.Add(machine.Number, now);
                GlobalVariable.deviceStatusList.Add(roomStatus);
            }
            else
            {
                if (roomStatus.MachineList.ContainsKey(machine.Number))
                {
                    roomStatus.MachineList[machine.Number] = now;
                }
                else
                {
                    roomStatus.MachineList.Add(machine.Number, now);
                }
                LastHeartBreak tempHeart = db.LastHeartBreak.Find(machine.ID);
                db.LastHeartBreak.Attach(tempHeart);
                tempHeart.DateRefresh = now;
                db.SaveChanges();
            }
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            DbTableDbContext db = new DbTableDbContext();
            HeartBreak outInfo = this.DecodeData(buff);
            Machines machine = db.Machines.Find(outInfo.MachineId);
            Models.HeartBreak innerInfo = this.exchangeData(outInfo, machine);
            //记录原始数据
            db.HeartBreak.Add(innerInfo);
            //记录设备状态
            RefreshOnlineInfo(machine, db);

            db.SaveChanges();            
            return null;
        }

        public bool ShouldResponse()
        {
            return false;
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
        private DeviceSetting DecodeData(byte[] buff)
        {
            DeviceSetting info = new DeviceSetting();
            byte[] tempData = buff;

            info.OperateType = tempData[0];
            int locIdx = 2, tempLen = tempData[1];
            //设备编码
            byte[] numberByte = new byte[tempLen];
            Array.Copy(tempData,locIdx, numberByte,0, tempLen);
            info.DeviceNumber = System.Text.Encoding.GetEncoding("GBK").GetString(numberByte);
            locIdx += tempLen;
            //车间号
            tempLen = 4;
            byte[] roomByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, roomByte, 0, tempLen);
            info.RoomID = ConvertHelper.BytesToInt32(roomByte);
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

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private Machines exchangeData(FactoryRoom room, DeviceSetting info)
        {
            return new Machines()
            {
                RoomID = room.RoomID,
                RoomName = room.RoomName,
                IpAddress = info.IPAddress,
                Number = info.DeviceNumber,
                Name = info.DeviceName,
                Status = enumMachineStatus.Normal
            };
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            DbTableDbContext db = new DbTableDbContext();
            DeviceSetting outInfo = this.DecodeData(buff);
            FactoryRoom tempRoom = db.FactoryRoom.Find(outInfo.RoomID);
            Machines innerInfo = this.exchangeData(tempRoom,outInfo);

            db.FactoryRoom.Attach(tempRoom);
            tempRoom.MachineCount += 1;

            //记录原始数据
            db.Machines.Add(innerInfo);
            db.SaveChanges();
            //生成返回结果
            byte[] byteID = ConvertHelper.Int16ToBytes(innerInfo.ID, true);
            byte[] byteResp = { 0, 0, 0 };
            Array.Copy(byteID, 0, byteResp, 1, 2);
            return byteResp;
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
        private ClientResp DecodeData(byte[] buff)
        {
            ClientResp info = new ClientResp();
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
            ClientResp outInfo = this.DecodeData(buff);

            GlobalVariable.DownScheduleWaitStatus[outInfo.MachineId] = false;
            if (!GlobalVariable.DownScheduleRespResult.Keys.Contains(outInfo.MachineId))
                GlobalVariable.DownScheduleRespResult.Add(outInfo.MachineId, outInfo.RespResult);
            else
                GlobalVariable.DownScheduleRespResult[outInfo.MachineId] = outInfo.RespResult;
            
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
        private ClientResp DecodeData(byte[] buff)
        {
            ClientResp info = new ClientResp();
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
            ClientResp outInfo = this.DecodeData(buff);

            GlobalVariable.DownScheCloseWaitStatus[outInfo.MachineId] = false;
            if (!GlobalVariable.DownScheCloseRespResult.Keys.Contains(outInfo.MachineId))
                GlobalVariable.DownScheCloseRespResult.Add(outInfo.MachineId, outInfo.RespResult);
            else
                GlobalVariable.DownScheCloseRespResult[outInfo.MachineId] = outInfo.RespResult;

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
        private ClientResp DecodeData(byte[] buff)
        {
            ClientResp info = new ClientResp();
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
            ClientResp outInfo = this.DecodeData(buff);

            GlobalVariable.DownScheDiscardWaitStatus[outInfo.MachineId] = false;
            if (!GlobalVariable.DownScheDiscardRespResult.Keys.Contains(outInfo.MachineId))
                GlobalVariable.DownScheDiscardRespResult.Add(outInfo.MachineId, outInfo.RespResult);
            else
                GlobalVariable.DownScheDiscardRespResult[outInfo.MachineId] = outInfo.RespResult;

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
            DbTableDbContext db = new DbTableDbContext();

            db.RecordErrorInfo(enumSystemErrorCode.TcpDefaultHandlerErr, "", buff);

            return null;
        }

        public bool ShouldResponse()
        {
            return false;
        }
    }
}


