using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace XczdServer
{
    public class DbHandler
    {
        private static SqlConnection conn = null;
        private static readonly object _ErrorInsertLock = new object();
        private readonly static DataTable errorTableSchema = new DataTable();
        //private Queue<string> ErrorNeedInsertQueue = new Queue<string>();
        /// <summary>
        /// 队列形式写入异常日志
        /// </summary>
        private static Queue<ErrorInfo> ErrorObjectsQueue = new Queue<ErrorInfo>();
        /// <summary>
        /// 由于使用频繁且数据量小，因此设备信息放入内存
        /// </summary>
        private readonly static Dictionary<int, Machines> AllMachinesIdEntity = new Dictionary<int, Machines>();

        #region 初始化
        public DbHandler()
        {
            if (errorTableSchema.Columns.Count < 1)
            {
                this.GetErrorTableSchema(errorTableSchema);
            }
            if (conn == null)
            {
                string strdbConn = Ini.GetItemValue("sqlServer", "connString");
                conn = new SqlConnection(strdbConn);
                try
                {
                    conn.Open();
                }
                catch { }
            }
            if (AllMachinesIdEntity.Count < 1)
            {
                this.InitialLoadAllMachines();
            }
        }

        private void GetErrorTableSchema(DataTable dt)
        {
            dt.Columns.AddRange(new DataColumn[] {   
                new DataColumn("ID",typeof(int)), 
                new DataColumn("ErrorType",typeof(int)),  
                new DataColumn("HappenTime",typeof(DateTime)),  
                new DataColumn("UserID",typeof(int)), 
                new DataColumn("Remark",typeof(string)),
                new DataColumn("RemarkBinary",typeof(byte[])),
                new DataColumn("ErrorMsg",typeof(string)),
                new DataColumn("ErrorSource",typeof(string)),
                new DataColumn("ErrorStack",typeof(string))
            });
        }

        private void InitialLoadAllMachines()
        {
            string strSql = "select * from Machines";
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AllMachinesIdEntity.Add(Convert.ToInt32(dr["ID"]), exchangeMachine(dr));
                }
            }
        }
        #endregion

        #region 生产数据
        public int InsertProductInfo(NetStructure.ProductInfo info)
        {
            int intRsp = 0;
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "sp_Insert_ProductInfo";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MachineId", info.MachineId);
                cmd.Parameters.AddWithValue("@ScheduleNumber", info.ScheduleNumber);
                cmd.Parameters.AddWithValue("@ChannelCount", info.ChannelCount);
                cmd.Parameters.AddWithValue("@StaffNumber", info.StaffNumber);
                cmd.Parameters.AddWithValue("@StaffName", info.StaffName);
                cmd.Parameters.AddWithValue("@MsgStatus", (int)info.MsgStatus);
                cmd.Parameters.AddWithValue("@ChannelFinish1", info.ChannelFinish1);
                cmd.Parameters.AddWithValue("@ChannelFinish2", info.ChannelFinish2);
                cmd.Parameters.AddWithValue("@ChannelFinish3", info.ChannelFinish3);
                cmd.Parameters.AddWithValue("@ChannelFinish4", info.ChannelFinish4);
                cmd.Parameters.AddWithValue("@ChannelFinish5", info.ChannelFinish5);
                cmd.Parameters.AddWithValue("@ChannelFinish6", info.ChannelFinish6);
                cmd.Parameters.AddWithValue("@UnusualCount", info.UnusualCount);

                object objRsp = cmd.ExecuteScalar();
                intRsp = Convert.ToInt32(objRsp);
            }
            return intRsp;
        }
        #endregion

        #region 设备设置
        public int InsertMachines(NetStructure.DeviceSetting info, out int machineId)
        {
            int rest = 0;
            machineId = 0;
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "sp_Device_Setting";
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@DeviceId", info.DeviceId);
                cmd.Parameters.Add(new SqlParameter("@DeviceId", SqlDbType.Int));
                cmd.Parameters["@DeviceId"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@DeviceId"].Value = info.DeviceId;
                cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.AddWithValue("@OperateType", info.OperateType);
                cmd.Parameters.AddWithValue("@RoomNumber", info.RoomNumber);
                cmd.Parameters.AddWithValue("@DeviceNumber", info.DeviceNumber);
                cmd.Parameters.AddWithValue("@IpAddress", info.IPAddress);

                cmd.ExecuteNonQuery();
                rest = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                if (rest == 0)
                {
                    machineId = Convert.ToInt32(cmd.Parameters["@DeviceId"].Value);
                }
            }
            return rest;
        }

        private Machines exchangeMachine(DataRow dr)
        {
            Machines info = new Machines();
            info.ID = Convert.ToInt32(dr["ID"]);
            info.Number = dr["Number"].ToString();
            info.Name = dr["Name"].ToString();
            info.RoomID = Convert.ToInt32(dr["RoomID"]);
            info.RoomNumber = dr["RoomNumber"].ToString();
            info.RoomName = dr["RoomName"].ToString();
            info.IpAddress = dr["IpAddress"].ToString();
            info.RemarkInfo = dr["RemarkInfo"].ToString();
            info.Status = (enumMachineStatus)Enum.Parse(typeof(enumMachineStatus), dr["Status"].ToString());

            return info;
        }

        public Machines SelectMachine(int machineId)
        {
            Machines tempItem = null;
            if (AllMachinesIdEntity.Keys.Contains(machineId))
            {
                tempItem = AllMachinesIdEntity[machineId];
            }
            return tempItem;
            //Machines info = null;
            //string strSql = string.Format("select * from Machines where ID={0}", machineId);
            //using (SqlCommand cmd = new SqlCommand(strSql, conn))
            //{
            //    //SqlDataReader dr = cmd.ExecuteReader();
            //    //if (dr.HasRows && dr.Read())
            //    //{
            //    //    info = exchangeMachine(dr);
            //    //}
            //    //dr.Close();
            //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    try
            //    {
            //        adapter.Fill(ds);
            //    }
            //    catch { }

            //    if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return info;
            //    DataRow dr = ds.Tables[0].Rows[0];
            //    info = exchangeMachine(dr);
            //}
            //return info;
        }

        public string GetRoomReportNumber(int roomId)
        {
            string info;
            string strSql = "select RepairNumber from FactoryRooms where RoomID=@roomId";
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.Add("@roomId", SqlDbType.Int);
                cmd.Parameters["@roomId"].Value = roomId;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                }
                catch { }

                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return string.Empty;
                DataRow dr = ds.Tables[0].Rows[0];

                info = dr["RepairNumber"].ToString();
            }
            return info;

        }
        #endregion

        #region 车间
        public FactoryRoom SelectRoom(string roomNumber)
        {
            FactoryRoom info = null;
            string strSql = "select * from FactoryRooms where RoomNumber=@RoomNumber";
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.Add("@RoomNumber", SqlDbType.Int);
                cmd.Parameters["@RoomNumber"].Value = roomNumber;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                }
                catch { }

                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return info;
                DataRow dr = ds.Tables[0].Rows[0];

                info = new FactoryRoom()
                {
                    FactoryName = dr["FactoryName"].ToString(),
                    RoomID = Convert.ToInt32(dr["RoomID"]),
                    RoomName = dr["RoomName"].ToString()
                };
            }
            return info;
        }

        public void UpdateRoomDeviceCount(int roomId)
        {
            string strSql = @"UPDATE FactoryRooms SET MachineCount=MachineCount+1 WHERE RoomID=@RoomID;";

            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@RoomID", roomId);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 心跳
        public void InsertHeartBreak(HeartBreak info)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "sp_Device_HeartBreak";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MachineId", info.MachineId);
                cmd.Parameters.AddWithValue("@MachineName", info.MachineName);
                cmd.Parameters.AddWithValue("@ChannelInfo", info.ChannelInfo);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 设备生产情况
        public void InsertMachineReport(MachineReport info)
        {
            string strSql = @"INSERT INTO MachineReports (DateCreate,MachineId,MachineName,MachineNumber,RoomId,RoomNumber,RoomName,[Status]) 
                              VALUES (@DateCreate,@MachineId,@MachineName,@MachineNumber,@RoomId,@RoomNumber,@RoomName,@Status)";

            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@DateCreate", info.DateCreate);
                cmd.Parameters.AddWithValue("@MachineId", info.MachineId);
                cmd.Parameters.AddWithValue("@MachineNumber", info.MachineNumber);
                cmd.Parameters.AddWithValue("@MachineName", info.MachineName);
                cmd.Parameters.AddWithValue("@RoomId", info.RoomId);
                cmd.Parameters.AddWithValue("@RoomNumber", info.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomName", info.RoomName);
                cmd.Parameters.AddWithValue("@Status", info.Status);

                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region 设备叫料
        public void InsertMachineCallMaterial(MachineCallMaterial info)
        {
            string strSql = @"INSERT INTO MachineCallMaterials (DateCreate,MachineId,MachineNumber,MachineName,RoomId,RoomNumber,RoomName,
                                    OrderId,OrderNumber,ScheduleId,SchueduleNumber,MaterialInfo,[Status]) 
                              VALUES (@DateCreate,@MachineId,@MachineNumber,@MachineName,@RoomId,@RoomNumber,@RoomName,
                                    @OrderId,@OrderNumber,@ScheduleId,@SchueduleNumber,@MaterialInfo,@Status)";

            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@DateCreate", info.DateCreate);
                cmd.Parameters.AddWithValue("@MachineId", info.MachineId);
                cmd.Parameters.AddWithValue("@MachineNumber", info.MachineNumber);
                cmd.Parameters.AddWithValue("@MachineName", info.MachineName);
                cmd.Parameters.AddWithValue("@RoomId", info.RoomId);
                cmd.Parameters.AddWithValue("@RoomNumber", info.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomName", info.RoomName);
                cmd.Parameters.AddWithValue("@OrderId", info.OrderId);
                cmd.Parameters.AddWithValue("@OrderNumber", info.OrderNumber);
                cmd.Parameters.AddWithValue("@ScheduleId", info.ScheduleId);
                cmd.Parameters.AddWithValue("@SchueduleNumber", info.SchueduleNumber);
                cmd.Parameters.AddWithValue("@MaterialInfo", info.MaterialInfo);
                cmd.Parameters.AddWithValue("@Status", info.Status);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 设备启动停止
        public void InsertMachineStartEnd(MachineStartEnd info)
        {
            string formatSql = @"INSERT INTO MachineStartEnds ({0},MachineId,MachineNumber,MachineName,RoomId,RoomNumber,
                                    OrderId,OrderNumber,ScheduleId,SchueduleNumber,UserId,UserNumber,[Status]) 
                              VALUES (@{0},@MachineId,@MachineNumber,@MachineName,@RoomId,@RoomNumber,
                                    @OrderId,@OrderNumber,@ScheduleId,@SchueduleNumber,@UserId,@UserNumber,@Status)";
            string strSql = string.Empty;

            if (info.Status == enumDeviceWorkStatus.Start)
                strSql = "DateStart";
            else
                strSql = "DateEnd";
            strSql = string.Format(formatSql, strSql);
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                if (info.Status == enumDeviceWorkStatus.Start)
                    cmd.Parameters.AddWithValue("@DateStart", info.DateStart);
                else
                    cmd.Parameters.AddWithValue("@DateEnd", info.DateEnd);

                cmd.Parameters.AddWithValue("@MachineId", info.MachineId);
                cmd.Parameters.AddWithValue("@MachineNumber", info.MachineNumber);
                cmd.Parameters.AddWithValue("@MachineName", info.MachineName);
                cmd.Parameters.AddWithValue("@RoomId", info.RoomId);
                cmd.Parameters.AddWithValue("@RoomNumber", info.RoomNumber);
                cmd.Parameters.AddWithValue("@OrderId", info.OrderId);
                cmd.Parameters.AddWithValue("@OrderNumber", info.OrderNumber);
                cmd.Parameters.AddWithValue("@ScheduleId", info.ScheduleId);
                cmd.Parameters.AddWithValue("@SchueduleNumber", info.SchueduleNumber);
                cmd.Parameters.AddWithValue("@UserId", info.UserId);
                cmd.Parameters.AddWithValue("@UserNumber", info.UserNumber);
                cmd.Parameters.AddWithValue("@Status", info.Status);

                cmd.ExecuteNonQuery();
            }
        }

        public void FinishStartStatus(int startId)
        {
            string strSql = @"update  MachineStartEnds set DateEnd=@DateEnd,[Status]=@Status where ID=@startId";

            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@startId", startId);
                cmd.Parameters.AddWithValue("@DateEnd", DateTime.Now);
                cmd.Parameters.AddWithValue("@Status", enumDeviceWorkStatus.Finish);

                cmd.ExecuteNonQuery();
            }
        }

        public int GetStartEndID(MachineStartEnd info)
        {
            int rest = 0;
            string strSql = @"select Top 1 ID from MachineStartEnds  with(nolock) where MachineId=@MachineId and SchueduleNumber=@SchueduleNumber
                                and [Status]=@Status ORDER BY ID desc";

            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@MachineId", info.MachineId);
                cmd.Parameters.AddWithValue("@SchueduleNumber", info.SchueduleNumber);
                //cmd.Parameters.AddWithValue("@UserNumber", info.UserNumber);
                cmd.Parameters.AddWithValue("@Status", info.Status);

                rest = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return rest;
        }
        #endregion

        #region 最近心跳
        public void InsertLastHeartBreak(LastHeartBreak info)
        {
            string strSql = @"INSERT INTO LastHeartBreaks (DateRefresh,MachineId,MachineName,RoomID,RoomName,FactoryName) 
                              VALUES (@DateRefresh,@MachineId,@MachineName,@RoomID,@RoomName,@FactoryName)
                              ;SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";

            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@DateRefresh", info.DateRefresh);
                cmd.Parameters.AddWithValue("@MachineId", info.MachineId);
                cmd.Parameters.AddWithValue("@MachineName", info.MachineName);
                cmd.Parameters.AddWithValue("@RoomID", info.RoomID);
                cmd.Parameters.AddWithValue("@RoomName", info.RoomName);
                cmd.Parameters.AddWithValue("@FactoryName", info.FactoryName);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLastHeartBreak(int machineId)
        {
            string strSql = @"UPDATE LastHeartBreaks SET DateRefresh=@DateRefresh where MachineId=@MachineId";

            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@DateRefresh", DateTime.Now);
                cmd.Parameters.AddWithValue("@MachineId", machineId);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 异常日志
        public void InsertErrorInfo()
        {
            while (true)
            {
                SqlBulkCopy buckCopy = new SqlBulkCopy(conn);
                buckCopy.DestinationTableName = "ErrorInfoes";
                DataTable dt = errorTableSchema.Clone();
                while (ErrorObjectsQueue.Count > 0)
                {
                    ErrorInfo errorItem = ErrorObjectsQueue.Dequeue();
                    if (errorItem == null) continue;
                    DataRow dr = dt.NewRow();
                    dr["ErrorType"] = (int)(errorItem.ErrorType);
                    dr["UserID"] = errorItem.userID;
                    dr["Remark"] = errorItem.Remark;
                    dr["RemarkBinary"] = errorItem.RemarkBinary;
                    dr["ErrorMsg"] = errorItem.ErrorMsg;
                    dr["ErrorSource"] = errorItem.ErrorSource;
                    dr["ErrorStack"] = errorItem.ErrorStack;
                    dr["HappenTime"] = errorItem.HappenTime;
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        buckCopy.WriteToServer(dt);
                    }
                    catch { }
                }
                //StringBuilder sbValue = new StringBuilder();                
                //bool hasContent = false;
                //while (ErrorNeedInsertQueue.Count > 0)
                //{
                //    string strItem = ErrorNeedInsertQueue.Dequeue();
                //    sbValue.Append(',');
                //    sbValue.Append(strItem);
                //    hasContent = true;
                //}
                //if (hasContent)
                //{
                //    sbValue.Remove(0, 1);
                //    sbValue.Insert(0,"INSERT INTO ErrorInfoes (HappenTime,ErrorType,Remark,userID,RemarkBinary,ErrorMsg,ErrorSource,ErrorStack) VALUES ");
                //    string strSql = sbValue.ToString();
                //    using (SqlCommand cmd = new SqlCommand(strSql, conn))
                //    {
                //        try
                //        {
                //            cmd.ExecuteNonQuery();
                //        }
                //        catch(Exception ex)
                //        {
                //            if (ex.Message.Contains("关闭"))
                //            {
                //                conn.Close();
                //                conn.Open();
                //            }
                //        }
                //    }
                //}
                System.Threading.Thread.Sleep(30000);//30秒写入一次
            }
        }

        public void InsertErrorInfo(enumSystemErrorCode type, Exception ex, string remark, byte[] remarkBinary)
        {
            //byte[] tempBinary = remarkBinary ?? new byte[] { };
            //string strValue = string.Format("('{0}',{1},'{2}',{3},'{4}',", DateTime.Now, (int)type, remark, 0,
            //    System.Text.Encoding.Unicode.GetString(tempBinary));

            //string strError = "'','','')";
            //if (ex != null)
            //{
            //    strError = string.Format("'{0}','{1}','{2}')", ex.Message, ex.Source, ex.StackTrace);
            //}
            //ErrorNeedInsertQueue.Enqueue(strValue + strError);
            ErrorObjectsQueue.Enqueue(new ErrorInfo()
            {
                ErrorMsg = ex == null ? string.Empty : ex.Message,
                ErrorSource = ex == null ? string.Empty : ex.Source,
                ErrorStack = ex == null ? string.Empty : ex.StackTrace,
                ErrorType = type,
                HappenTime = DateTime.Now,
                Remark = remark,
                RemarkBinary = remarkBinary,
                userID = 0
            });
        }
        #endregion

        #region 施工单
        private Schedules exchangeSchedule(DataRow dr)
        {
            Schedules info = new Schedules();
            info.CreatorID = Convert.ToInt32(dr["CreatorID"]);
            info.CreatorName = dr["CreatorName"].ToString();
            info.DateCreate = Convert.ToDateTime(dr["DateCreate"]);
            info.DateLastUpdate = Convert.ToDateTime(dr["DateLastUpdate"]);
            info.DetailInfo = dr["DetailInfo"].ToString();
            info.DownContinueCount = Convert.ToInt32(dr["DownContinueCount"]);
            info.FinishCount = Convert.ToInt32(dr["FinishCount"]);
            info.ID = Convert.ToInt32(dr["ID"]);
            info.LastUpdatePersonID = Convert.ToInt32(dr["LastUpdatePersonID"]);
            info.LastUpdatePersonName = dr["LastUpdatePersonName"].ToString();
            info.MachineId = Convert.ToInt32(dr["MachineId"]);
            info.MachineName = dr["MachineName"].ToString();
            info.MaterialDetail = dr["MaterialDetail"].ToString();
            info.MaterialInfo = dr["MaterialInfo"].ToString();
            info.NoticeInfo = dr["NoticeInfo"].ToString();
            info.Number = dr["Number"].ToString();
            info.OrderId = Convert.ToInt32(dr["OrderId"]);
            info.OrderNumber = dr["OrderNumber"].ToString();
            info.ProductCode = dr["ProductCode"].ToString();
            info.ProductCount = Convert.ToInt32(dr["ProductCount"]);
            info.ProductInfo = dr["ProductInfo"].ToString();
            info.ProductUnit = dr["ProductUnit"].ToString();
            info.RoomId = Convert.ToInt32(dr["RoomId"]);
            info.RoomNumber = dr["RoomNumber"].ToString();
            info.Status = (enumStatus)Enum.Parse(typeof(enumStatus), dr["Status"].ToString());
            info.OrderId = Convert.ToInt32(dr["OrderId"]);

            return info;
        }

        public Schedules SelectSchedule(int scheduleId)
        {
            Schedules info = null;
            string strSql = "select * from Schedules where ID=@ID";
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters["@ID"].Value = scheduleId;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                }
                catch { }

                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return info;
                DataRow dr = ds.Tables[0].Rows[0];

                info = this.exchangeSchedule(dr);
            }
            return info;
        }

        public Schedules SelectSchedule(string scheduleNumber)
        {
            Schedules info = null;
            string strSql = "select * from Schedules where Number=@Number";
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.Add("@Number", SqlDbType.NVarChar);
                cmd.Parameters["@Number"].Value = scheduleNumber;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                }
                catch { }

                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return info;
                DataRow dr = ds.Tables[0].Rows[0];

                info = this.exchangeSchedule(dr);
            }
            return info;
        }

        public void UpdateScheduleFinishCount(int scheduleId, int count, enumStatus status)
        {
            string strSql = @"UPDATE Schedules SET FinishCount=FinishCount+@FinishCount,Status=@Status WHERE ID=@ID;";

            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.AddWithValue("@ID", scheduleId);
                cmd.Parameters.AddWithValue("@FinishCount", count);
                cmd.Parameters.AddWithValue("@Status", (int)status);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion


        public UserProfile SelectUser(string userName)
        {
            UserProfile info = null;
            string strSql = "select * from UserProfiles where UserName=@UserName";
            using (SqlCommand cmd = new SqlCommand(strSql, conn))
            {
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
                cmd.Parameters["@UserName"].Value = userName;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                }
                catch { }

                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return info;
                DataRow dr = ds.Tables[0].Rows[0];

                info = new UserProfile()
                {
                    UserId = Convert.ToInt32(dr["UserId"]),
                    UserName = dr["UserName"].ToString()
                };
            }
            return info;
        }
    }
}