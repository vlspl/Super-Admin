using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for CLSRoleMaster
/// </summary>
public class CLSRoleMaster
{
    DataAccessLayer DAL = new DataAccessLayer();
    // Role Master
    public string roleName { get; set; }
    public string status { get; set; }
    public string remark { get; set; }
    // Screen Master
    public string screenname { get; set; }
    public string displayname { get; set; }
    public string screenurl { get; set; }
    public string paraintid { get; set; }
    public string menuicon { get; set; }
    // Role Screen Master
   
   
	public CLSRoleMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insertRoleEntry()
    {
        int returnVal = 0;
        int roleId;
        try
        {
          SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@roleName", roleName),
                new SqlParameter("@status", status),
                new SqlParameter("@remark", remark),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
          roleId = DAL.ExecuteStoredProcedureRetnInt("sp_insertRole", param);
          if (roleId == -3)
          {
              returnVal = 3;
          }
        }
        catch (Exception)
        {
            returnVal = 1;
        }
        return returnVal;
    }
  
}