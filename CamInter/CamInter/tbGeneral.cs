using System;
using System.Data;

namespace CamInter
{
    public interface tbGeneral
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
    }
}
