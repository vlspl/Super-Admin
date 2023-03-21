using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for CLSUserProfileFDMListInsert
/// </summary>
public class CLSUserProfileFDMListInsert
{
    DataAccessLayer DAL = new DataAccessLayer();
    public string name { get; set; }
    public string type { get; set; }
    public string remark { get; set; }
    public string status { get; set; }
    public string createdBy { get; set; }
    public string createdDate { get; set; }
    public string listId { get; set; }
	public CLSUserProfileFDMListInsert()
	{
		 
   
	}
     public int insertFDMList()
    {
        int returnVal = 0;
        int labId;
        //int orgId;
        try
        {
           
           
            SqlParameter[] param = new SqlParameter[]
            {
                //new SqlParameter("@sUserName", sUserName),
                new SqlParameter("@name", name),
                new SqlParameter("@type", type),
                new SqlParameter("@remark", remark),
                new SqlParameter("@status", status),
                new SqlParameter("@createdBy", createdBy),
                new SqlParameter("@createdDate", System.DateTime.Now),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
            labId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertUPFDMList", param);
           
           
            if (labId == -3)
            {
                returnVal = 3;
            }
            else
            {   

                returnVal = 0;
                
            }
            
           
        }
        catch (Exception)
        {
            returnVal = 1;
        }
        return returnVal;
    }
     public int updateList(string listId, string name, string type, string remark, string status, string createdBy, string createdDate)
     {
         try
         {
             SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@listId", listId),
                        new SqlParameter("@name", name),
                        new SqlParameter("@type",type),
                        new SqlParameter("@remark", remark),
                        new SqlParameter("@status", status),
                        new SqlParameter("@createdBy", createdBy),
                        new SqlParameter("@createdDate", createdDate),
                     //   new SqlParameter("@returnval", SqlDbType.Int),
                       
                    };
             DAL.ExecuteStoredProcedure("Sp_UpdUPFDMList", param);

            
             return 1;
         }
         catch (Exception)
         {
             return 0;
         }
     }
}