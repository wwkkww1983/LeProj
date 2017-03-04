using System;
using System.Data;

namespace CamInter
{
    public interface itable
    {
        /// <summary>
        /// 数据表信息解码
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        ValueType DecodeOneItemByDb(DataRow dr);
        /// <summary>
        /// 表结构
        /// </summary>
        /// <returns></returns>
        string CreateTableStructure();
        //void EncodeOneItemToDB(
        /// <summary>
        /// 插入一条新记录
        /// </summary>
        /// <param name="item"></param>
        bool InsertOneItem(ValueType item);
    }
}
