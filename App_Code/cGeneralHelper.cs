using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for cGeneralHelper
/// </summary>
public class cGeneralHelper
{
	public cGeneralHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}
	public static string getApplicationPath()
	{
		string strApplicationPath = "";
		string[] strPath = HttpContext.Current.Request.Url.ToString().Split('/');
		if (strPath.Length >= 3)
		{
			strApplicationPath = strPath[0] + "/" + strPath[1] + "/" + strPath[2] + "/";
		}
		if (HttpContext.Current.Request.ApplicationPath != "/")
		{
			strApplicationPath += HttpContext.Current.Request.ApplicationPath.Substring(1, HttpContext.Current.Request.ApplicationPath.Length - 1) + "/";

			//strApplicationPath = "http://" + HttpContext.Current.Request.Url.Host + "" + HttpContext.Current.Request.ApplicationPath + "/";
		}

		return strApplicationPath;
	}
	public static string JSONEscape(string i_sString)
	{
		JavaScriptSerializer oJSON = new JavaScriptSerializer();
		StringBuilder sb = new StringBuilder();
		oJSON.Serialize(i_sString, sb);
		sb.Remove(0, 1);
		sb.Remove(sb.Length - 1, 1);
		return sb.ToString();
	}
    public static string FindTimeStamp(DataTable dt)
    {
        DateTime dtStartDate = Convert.ToDateTime(dt.Rows[0]["dtCreatedOn"]);
        DateTime dtEndDate = Convert.ToDateTime(dt.Rows[0]["dtStatusCloseTime"]);
        int noofDays = Convert.ToInt32(dt.Rows[0]["noofDays"]);
        string sTimeTaken = "";
        if (noofDays == 0)
        {
            //TimeSpan span = dtEndDate - dtStartDate;
            //double totalMinutes = span.TotalHours;
            int starthour = dtStartDate.Hour;
            int startMinu = dtStartDate.Minute;
            int endHour = dtEndDate.Hour;
            int endMinu = dtEndDate.Minute;
            int diffHour = 0;
            int diffMinu = 0;
            if (starthour < 7)
            {
                starthour = 7;
                startMinu = 0;
            }
            diffHour = endHour - starthour;
            diffMinu = endMinu - startMinu;
            if (diffMinu < 0)
            {
                diffHour -= 1;
                diffMinu = 60 + diffMinu;
            }
            sTimeTaken = diffHour + " H : " + diffMinu + " Min";
        }
        else if (noofDays == 1)
        {
            //TimeSpan span = dtEndDate - dtStartDate;
            int starthour = dtStartDate.Hour;
            int startMinu = dtStartDate.Minute;
            int endHour = dtEndDate.Hour;
            int endMinu = dtEndDate.Minute;
            int diffHour = 0;
            int diffMinu = 0;
            if (starthour < 7)
            {
                starthour = 7;
                startMinu = 0;
            }
            int betweenHours = 0;
            if (starthour == 7)
            {
                betweenHours = 15;
                diffHour = endHour - starthour;
                diffMinu = endMinu - startMinu;
                diffHour += betweenHours;
                sTimeTaken = diffHour + " H : " + diffMinu + " Min";

            }
            else
            {
                betweenHours = 22 - starthour;
                diffHour = endHour - 7;
                diffMinu = endMinu - startMinu;
                diffHour += betweenHours;
                if (diffMinu < 0)
                {
                    diffHour -= 1;
                    diffMinu = 60 + diffMinu;
                }
                sTimeTaken = diffHour + " H : " + diffMinu + " Min";
            }
        }
        else if (noofDays > 1)
        {
            int starthour = dtStartDate.Hour;
            int startMinu = dtStartDate.Minute;
            int endHour = dtEndDate.Hour;
            int endMinu = dtEndDate.Minute;
            int diffHour = 0;
            int diffMinu = 0;
            if (starthour < 7)
            {
                starthour = 7;
                startMinu = 0;
            }
            int betweenHours = 0;
            if (starthour == 7)
            {
                betweenHours = 15 * (noofDays);
                diffHour = endHour - starthour;
                diffMinu = endMinu - startMinu;
                diffHour += betweenHours;
                sTimeTaken = diffHour + " H : " + diffMinu + " Min";
            }
            else
            {
                betweenHours = (15 * noofDays) - (starthour - 7);
                diffHour = endHour - 7;
                diffMinu = endMinu - startMinu;
                diffHour += betweenHours;
                if (diffMinu < 0)
                {
                    diffHour -= 1;
                    diffMinu = 60 + diffMinu;
                }
                sTimeTaken = diffHour + " H : " + diffMinu + " Min";
            }

        }
        return sTimeTaken;
    }
}