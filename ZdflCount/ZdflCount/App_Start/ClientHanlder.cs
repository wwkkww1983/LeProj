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
                MachineId = machine.ID,
                MachineName = machine.Number,
                MsgType = (byte)info.MsgStatus,
                ScheduleNumber = info.ScheduleNumber,
                ChannelFinish1 = info.ChannelFinish1,
                ChannelFinish2 = info.ChannelFinish2,
                ChannelFinish3 = info.ChannelFinish3,
                ChannelFinish4 = info.ChannelFinish4,
                ChannelFinish5 = info.ChannelFinish5,
                ChannelFinish6 = info.ChannelFinish6,
                ExceptionCount = info.UnusualCount
            };
        }

        private void RecordStatisticsInfo(Models.ProductInfo lastInfo, int currInfoId, ProductInfo outInfo, Machines machine, int finishCount, DbTableDbContext db)
        {
            Schedules tempSchedule = db.Schedules.First(item => item.Number == outInfo.ScheduleNumber);
            StatisticInfo statistics = new StatisticInfo()
            {
                ProductIdStart = lastInfo.ID,
                DateStart = lastInfo.DateCreate,
                ProductIdOut = currInfoId,
                DateOut = DateTime.Now,
                ExceptionCount = outInfo.UnusualCount,
                FinishCount = finishCount,
                MachineId = machine.ID,
                MachineName = machine.Name,
                OrderNumber = tempSchedule.OrderNumber,
                ScheduleID = tempSchedule.ID,
                ScheduleNumber = tempSchedule.Number,
                StaffName = outInfo.StaffName,
                StaffNumber = outInfo.StaffNumber,
                RoomID = machine.RoomID,
                RoomName = machine.RoomName,
                Factory = "振德敷料"
            };
            db.Statistics.Add(statistics);
            //施工单生产记录
            db.Schedules.Attach(tempSchedule);
            tempSchedule.FinishCount += finishCount;
            tempSchedule.Status = tempSchedule.FinishCount >= tempSchedule.ProductCount ? enumStatus.Finished : enumStatus.Working;
            //订单生产记录
            Orders tempOrder = db.Orders.Find(tempSchedule.OrderId);
            db.Orders.Attach(tempOrder);
            tempOrder.ProductFinishedCount += finishCount;
            tempOrder.Status = tempOrder.ProductFinishedCount >= tempOrder.ProductCount ? enumStatus.Finished : enumStatus.Working;

            db.SaveChanges();
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            byte[] buffResp = { 0 };
            DbTableDbContext db = new DbTableDbContext();
            ProductInfo outInfo = this.DecodeData(buff);
            Models.ProductInfo lastInfo = db.ProductInfo.OrderByDescending(tmpItem => tmpItem.ID).FirstOrDefault(item => item.MachineId == outInfo.MachineId);
            Machines machine = db.Machines.Find(outInfo.MachineId);
            Models.ProductInfo innerInfo = this.exchangeData(outInfo, machine); 
            //记录原始数据
            db.ProductInfo.Add(innerInfo);
            db.SaveChanges();
            //记录统计表
            if (outInfo.MsgStatus == enumProductType.LoginOut)
            {                
                if (lastInfo == null || lastInfo.MsgType != (byte)enumProductType.LoginIn)
                {
                    db.RecordErrorInfo(enumSystemErrorCode.ProductOutWithoutIn, innerInfo.ToString(), null);
                    return buffResp;
                }
                if (lastInfo.staffNumber != outInfo.StaffNumber)
                {
                    db.RecordErrorInfo(enumSystemErrorCode.ProductOutInDiff, lastInfo.ToString() + "\r\n" + innerInfo.ToString(), null);
                }
                int currentFinishCount = (outInfo.ChannelFinish1 + outInfo.ChannelFinish2 + outInfo.ChannelFinish3 +
                    outInfo.ChannelFinish4 + outInfo.ChannelFinish5 + outInfo.ChannelFinish6 - lastInfo.ChannelFinish1 -
                    lastInfo.ChannelFinish2 - lastInfo.ChannelFinish3 - lastInfo.ChannelFinish4 - lastInfo.ChannelFinish5 -
                    lastInfo.ChannelFinish6);
                RecordStatisticsInfo(lastInfo, innerInfo.ID,outInfo, machine, currentFinishCount, db);
            }
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
                LastHeartBreak tempHeart = db.LastHeartBreak.FirstOrDefault(item => item.MachineId == machine.ID);
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
            byte[] resp = {};
            return resp;
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
        private DeviceSetting DecodeData(byte[] buff)
        {
            DeviceSetting info = new DeviceSetting();
            byte[] tempData = buff;

            //机器码
            int locIdx = 0;
            info.DeviceId = ConvertHelper.BytesToInt16(tempData, locIdx,true);
            locIdx += 2;
            //操作类型
            info.OperateType = tempData[locIdx++];
            int tempLen = tempData[locIdx++];
            //设备编码
            byte[] numberByte = new byte[tempLen];
            Array.Copy(tempData,locIdx, numberByte,0, tempLen);
            info.DeviceNumber = System.Text.Encoding.GetEncoding("GBK").GetString(numberByte);
            locIdx += tempLen;
            //车间号
            tempLen = 4;
            byte[] roomByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, roomByte, 0, tempLen);
            info.RoomNumber = ConvertHelper.BytesToInt32(roomByte).ToString ();
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
        /// <param name="room"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private Machines exchangeData(FactoryRoom room, DeviceSetting info)
        {
            return new Machines()
            {
                RoomID = room.RoomID,
                RoomName = room.RoomName,
                RoomNumber = room.RoomNumber,
                IpAddress = info.IPAddress,
                Number = info.DeviceNumber,
                Name = info.DeviceName,
                Status = enumMachineStatus.Normal
            };
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="innerInfo"></param>
        /// <param name="room"></param>
        /// <param name="info"></param>
        private void exchangeData(Machines innerInfo,FactoryRoom room, DeviceSetting info)
        {
            innerInfo.RoomID = room.RoomID;
            innerInfo.    RoomName = room.RoomName;
                innerInfo.RoomNumber = room.RoomNumber;
                innerInfo.IpAddress = info.IPAddress;
                innerInfo.Number = info.DeviceNumber;
                innerInfo.Name = info.DeviceName;
                innerInfo.Status = enumMachineStatus.Normal;
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            int resInt = 0, machineId = 0;
            byte[] byteResp = { 0, 0, 0 };
            DbTableDbContext db = new DbTableDbContext();
            DeviceSetting outInfo = this.DecodeData(buff);
            FactoryRoom tempRoom = db.FactoryRoom.FirstOrDefault(item => item.RoomNumber == outInfo.RoomNumber);
            Machines tempMachine = db.Machines.FirstOrDefault(item => item.Number == outInfo.DeviceNumber);
            Machines innerInfo;

            if (outInfo.DeviceId < 1)
            {
                resInt = tempRoom == null ? 3 : (tempMachine != null ? 4 : 0);
                if (resInt != 0)
                {
                    byteResp[0] = (byte)resInt;
                    return byteResp;
                }
                innerInfo = this.exchangeData(tempRoom, outInfo);
                db.FactoryRoom.Attach(tempRoom);
                tempRoom.MachineCount += 1;
                //记录原始数据
                db.Machines.Add(innerInfo);
                //最近心跳设备记录
                db.LastHeartBreak.Add(new LastHeartBreak()
                {
                    DateRefresh = DateTime.Now,
                    FactoryName = tempRoom.FactoryName,
                    MachineId = innerInfo.ID,
                    RoomID = tempRoom.RoomID,
                    RoomName = tempRoom.RoomName,
                    MachineName = outInfo.DeviceNumber
                });
            }
            else
            {
                innerInfo = db.Machines.FirstOrDefault(item => (item.IpAddress == outInfo.IPAddress || item.Number == outInfo.DeviceNumber) && item.ID != outInfo.DeviceId);
                if (innerInfo != null)
                {
                    byteResp[0] = (byte)4;
                    return byteResp;
                }
                innerInfo = db.Machines.Find(outInfo.DeviceId);
                db.Machines.Attach(innerInfo);
                this.exchangeData(innerInfo,tempRoom, outInfo);
                machineId = innerInfo.ID;
            }
            db.SaveChanges();
            machineId = innerInfo.ID;

            //生成返回结果
            byte[] byteID = ConvertHelper.Int16ToBytes(machineId, true);
            Array.Copy(byteID, 0, byteResp, 1, 2);
            return byteResp;
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
        private Models.MachineReport exchangeData(Machines machine)
        {
            return new Models.MachineReport()
            {
                DateCreate = DateTime.Now,
                MachineNumber = machine.Number,
                MachineId = machine.ID,
                RoomId = machine.RoomID,
                RoomNumber = machine.RoomName,
                RoomName = machine.RoomName
            };
        }


        public byte[] HandlerClientData(byte[] buff)
        {
            byte[] buffResp = { 1 };
            DbTableDbContext db = new DbTableDbContext();
            try
            {
                int outInfo = this.DecodeData(buff);
                Machines machine = db.Machines.Find(outInfo);
                MachineReport innerInfo = this.exchangeData(machine);
                //记录原始数据
                db.MachineReport.Add(innerInfo);

                db.SaveChanges();
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
        private DeviceMaterial DecodeData(byte[] buff)
        {

            DeviceMaterial info = new DeviceMaterial();
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
        private Models.MachineCallMaterial exchangeData(Machines machine, Schedules schedule)
        {
            return new Models.MachineCallMaterial()
            {
                DateCreate = DateTime.Now,
                MachineNumber = machine.Number,
                MachineId = machine.ID,
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
            DbTableDbContext db = new DbTableDbContext();
            try
            {
                DeviceMaterial outInfo = this.DecodeData(buff);
                Machines machine = db.Machines.Find(outInfo.MachineId);
                Schedules schedule = db.Schedules.FirstOrDefault(item => item.Number == outInfo.ScheduleNumber);
                MachineCallMaterial innerInfo = this.exchangeData(machine, schedule);
                //记录原始数据
                db.MachineCallMaterial.Add(innerInfo);

                db.SaveChanges();
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
        private DeviceStartEnd DecodeData(byte[] buff)
        {

            DeviceStartEnd info = new DeviceStartEnd();
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
        private Models.MachineStartEnd exchangeData(DeviceStartEnd outInfo, Machines machine, Schedules schedule, UserProfile userInfo)
        {
            return new Models.MachineStartEnd()
            {
                MachineNumber = machine.Number,
                MachineId = machine.ID,
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
            DbTableDbContext db = new DbTableDbContext();
            try
            {
                DateTime currentTime = DateTime.Now;
                DeviceStartEnd outInfo = this.DecodeData(buff);
                Machines machine = db.Machines.Find(outInfo.MachineId);
                Schedules schedule = db.Schedules.FirstOrDefault(item => item.Number == outInfo.ScheduleNumber);
                UserProfile userInfo = new UsersContext().UserProfiles.FirstOrDefault(item => item.UserName == outInfo.UserNumber);
                MachineStartEnd innerInfo = this.exchangeData(outInfo, machine, schedule, userInfo);
                //记录原始数据

                if (outInfo.Status == enumDeviceWorkStatus.Start)
                {
                    innerInfo.DateStart =currentTime;
                    db.MachineStartEnd.Add(innerInfo);
                }
                else
                {
                    MachineStartEnd lastRecord = db.MachineStartEnd.FirstOrDefault(item => item.MachineId == outInfo.MachineId &&
                    item.SchueduleNumber == outInfo.ScheduleNumber &&
                    item.UserNumber == outInfo.UserNumber &&
                    item.Status == enumDeviceWorkStatus.Start);
                    if (lastRecord == null)
                    {
                        innerInfo.DateEnd = currentTime;
                        db.MachineStartEnd.Add(innerInfo);
                    }
                    else
                    {
                        db.MachineStartEnd.Attach(lastRecord);
                        lastRecord.DateEnd = currentTime;
                        lastRecord.Status = enumDeviceWorkStatus.End;
                    }
                }

                db.SaveChanges();
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


