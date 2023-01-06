using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSLabApproval
/// </summary>
public class CLSLabApproval
{
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSLabApproval()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetLabApprovalList()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTempLabApprovalDetails");//Sp_GetTempLabApprovalDetails
        }
        catch (Exception)
        {
            return null;
        }
        return ds;
    }
	public DataSet GetLabApprovalList_dash()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTempLabApprovalDetails_dash");//Sp_GetTempLabApprovalDetails
        }
        catch (Exception)
        {
            return null;
        }
        return ds;
    }
}