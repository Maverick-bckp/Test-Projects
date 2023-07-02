using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MVCAppTest.Models;
using Oracle.ManagedDataAccess.Client;

namespace MVCAppTest.Repositories
{

    public class RakeAllocationLogRepository
    {
        static OracleConnection conTRISAPP = null;
        public List<RakeAllocationLog> GetAllData()
        {
            List<RakeAllocationLog> lrlog = new List<RakeAllocationLog>();

            using (conTRISAPP = new OracleConnection(ConfigurationManager.AppSettings["connTRISAPP"].ToString()))
            {
                DataTable dt = new DataTable();
                OracleCommand cmd = conTRISAPP.CreateCommand();
                cmd.CommandText = "select * from P_RAKE_ALLOCATION_LOG";
                cmd.CommandType = CommandType.Text;
                if (conTRISAPP.State != ConnectionState.Open)
                {
                    conTRISAPP.Open();
                }
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                oda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        RakeAllocationLog rlog = new RakeAllocationLog();
                        rlog.Unit = dr["Unit"].ToString();
                        rlog.TakeOverNum = dr["TakeOverNum"].ToString();
                        rlog.CCNum = dr["CCNum"].ToString();
                        rlog.LineNum = dr["LineNum"].ToString();
                        rlog.Destination = dr["Destination"].ToString();
                        lrlog.Add(rlog);
                    }
                }
            }
            return lrlog;
        }

        public void UpdateRakeAllocationLog(RakeAllocationLog rlog)
        {
            using (conTRISAPP = new OracleConnection(ConfigurationManager.AppSettings["connTRISAPP"].ToString()))
            {
                if (conTRISAPP.State != ConnectionState.Open)
                {
                    conTRISAPP.Open();
                }

                string updatesql = "Update P_RAKE_ALLOCATION_LOG set DESTINATION = '" + rlog.Destination + "' where LINENUM = '" + rlog.LineNum + "' and TakeOverNum='" + rlog.TakeOverNum + "'";

                OracleCommand cmd1 = new OracleCommand(updatesql, conTRISAPP);
                OracleTransaction transaction = conTRISAPP.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd1.Transaction = transaction;
                cmd1.CommandText = updatesql;
                cmd1.CommandType = CommandType.Text;
                int status = cmd1.ExecuteNonQuery();
                transaction.Commit();
            }
        }
    }
}