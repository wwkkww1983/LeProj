using System;
using ZdflCount.Models;

namespace ZdflCount.App_Start
{
    public interface interfaceDbRecord<T>
    {
        string RecordInfo(T info,string ip);
    }

    public class RecordProductInfo : interfaceDbRecord<ProductInfo>
    {
        public string RecordInfo(ProductInfo info, string ip)
        {
            //记录原始数据
            DbTableDbContext db = new DbTableDbContext();
            Machines machine = db.GetMachineByIp(ip);
            Models.ProductInfo infoDb = new Models.ProductInfo()
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
            db.ProductionInfo.Add(infoDb);
            db.SaveChanges();

            return "";
        }
    }

    public class RecordHeart : interfaceDbRecord<Int32>
    {
        public string RecordInfo(Int32 info, string ip)
        {
            //记录原始数据
            DbTableDbContext db = new DbTableDbContext();
            HeartBreak infoDb = new HeartBreak()
            {
                DateCreate = DateTime.Now,
                ChannelInfo = info
            };
            db.HeartBreak.Add(infoDb);
            db.SaveChanges();

            return "";
        }
    }
}
