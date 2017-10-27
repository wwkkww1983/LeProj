using System;
using System.Text;
using ZdflCount.Models;

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
        /// <summary>
        /// 通道信息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="locIdx"></param>
        /// <param name="channel"></param>
        private void DecodeChannelInfo(byte[] buff, ref int locIdx, ref ChannelInfo channel)
        {
            channel.PlanCount = ConvertHelper.BytesToInt32(buff, locIdx, true);
            locIdx += 4;
            channel.Finish = ConvertHelper.BytesToInt32(buff, locIdx, true);
            locIdx += 4;
            channel.Exception = ConvertHelper.BytesToInt32(buff, locIdx, true);
            locIdx += 4;
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
            byte[] machineByte = new byte[2];
            Array.Copy(tempData, machineByte, 2);
            info.MachineId = ConvertHelper.BytesToInt16(machineByte, true);
            //施工单编码
            int locIdx = 2, tempLen = tempData[locIdx++];
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
            //姓名长度
            tempLen = tempData[locIdx++];
            //姓名
            byte[] nameByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, nameByte, 0, tempLen);
            info.StaffName = Encoding.GetEncoding("GBK").GetString(nameByte);
            locIdx += tempLen;
            //通道
            DecodeChannelInfo(tempData, ref locIdx, ref info.Channel1);
            DecodeChannelInfo(tempData, ref  locIdx, ref info.Channel2);
            DecodeChannelInfo(tempData, ref locIdx, ref info.Channel3);
            DecodeChannelInfo(tempData, ref  locIdx, ref info.Channel4);

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
                 ChannelCount = info.ChannelCount,
                 DateCreate = DateTime.Now,
                 staffId = info.StaffNumber,
                 StaffName = info.StaffName,
                 MachineIP = machine.IpAddress,
                 MachineId = machine.ID,
                 MachineName = machine.Name,

                 PlanCount1 = info.Channel1.PlanCount,
                 PlanCount2 = info.Channel2.PlanCount,
                 PlanCount3 = info.Channel3.PlanCount,
                 PlanCount4 = info.Channel4.PlanCount,

                 Finish1 = info.Channel1.Finish,
                 Finish2 = info.Channel2.Finish,
                 Finish3 = info.Channel3.Finish,
                 Finish4 = info.Channel4.Finish,

                 Exception1 = info.Channel1.Exception,
                 Exception2 = info.Channel2.Exception,
                 Exception3 = info.Channel3.Exception,
                 Exception4 = info.Channel4.Exception
             };
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            DbTableDbContext db = new DbTableDbContext();
            ProductInfo outInfo = this.DecodeData(buff);
            Machines machine = db.Machines.Find(outInfo.MachineId);
            Models.ProductInfo innerInfo = this.exchangeData(outInfo, machine);

            //记录原始数据
            db.ProductInfo.Add(innerInfo);
            db.SaveChanges();


            byte[] buffResp = { 1 };
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
        public HeartBreak DecodeData(byte[] buff)
        {
            HeartBreak info = new HeartBreak();
            byte[] tempData = buff;
            byte[] machineByte = new byte[2];
            Array.Copy(tempData, machineByte, 2);
            info.MachineId = ConvertHelper.BytesToInt16(machineByte, true);
            info.ChannelInfo = buff[2];

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

        public byte[] HandlerClientData(byte[] buff)
        {
            DbTableDbContext db = new DbTableDbContext();
            HeartBreak outInfo = this.DecodeData(buff);
            Machines machine = db.Machines.Find(outInfo.MachineId);
            Models.HeartBreak innerInfo = this.exchangeData(outInfo, machine);

            //记录原始数据
            db.HeartBreak.Add(innerInfo);
            db.SaveChanges();
            
            return null;
        }

        public bool ShouldResponse()
        {
            return false;
        }
    }

    public class ClientHandlerDeviceSetting : interfaceClientHanlder
    {
        /// <summary>
        /// 客户端返回消息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public DeviceSetting DecodeData(byte[] buff)
        {
            DeviceSetting info = new DeviceSetting();
            byte[] tempData = buff;

            info.OperateType = buff[0];
            int locIdx = 2, tempLen = buff[1];
            //设备编码
            byte[] numberByte = new byte[tempLen];
            Array.Copy(tempData,locIdx, numberByte,0, tempLen);
            info.DeviceNumber = System.Text.Encoding.GetEncoding("GBK").GetString(numberByte);
            locIdx += tempLen;
            //设备备注名
            tempLen = buff[locIdx++];
            numberByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, numberByte, 0, tempLen);
            info.DeviceName = System.Text.Encoding.GetEncoding("GBK").GetString(numberByte);
            locIdx += tempLen;
            //IP地址
            tempLen = 15;
            numberByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, numberByte, 0, tempLen);
            info.IPAddress = System.Text.Encoding.ASCII.GetString(numberByte);

            return info;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private Machines exchangeData(DeviceSetting info)
        {
            return new Machines()
            {
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
            Machines innerInfo = this.exchangeData(outInfo);

            //记录原始数据
            db.Machines.Add(innerInfo);
            db.SaveChanges();
            //生成返回结果
            byte[] byteID = ConvertHelper.Int16ToBytes(innerInfo.ID, true);
            byte[] byteResp = new byte[3];
            byteResp[0] = 1;
            Array.Copy(byteID, 0, byteResp, 1, 2);
            return byteResp;
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }
}


