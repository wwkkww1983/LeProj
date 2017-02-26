using System;
using System.Data;

namespace CamInter
{
    /// <summary>
    /// 接口
    /// </summary>
    class tbConnector : tbGeneral
    {
        public ValueType DecodeOneItemByDb(DataRow dr)
        {
            Connectors info = new Connectors();

            info.Idx = Convert.ToInt32(dr["itemId"]);
            info.Name = dr["name"].ToString();
            info.Description = dr["desc"].ToString();

            return info;
        }

        public string CreateTableStructure()
        {
            return @"create table connectors (
                        itemId INTEGER PRIMARY KEY AUTOINCREMENT,
                        name nvarchar(20),
                        desc varchar(100));";
        }
    }

    /// <summary>
    /// 接口
    /// </summary>
    public struct Connectors
    {
        public int Idx;
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 详细备注
        /// </summary>
        public string Description;
    }
}
