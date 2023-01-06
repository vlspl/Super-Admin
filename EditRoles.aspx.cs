using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessHandler;
using System.Data.SqlClient;

public partial class EditRoles : System.Web.UI.Page
{
    Int64 Userid;
    int PageId;
    string column;
    CLSEditRoles objEditRoles = new CLSEditRoles();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadLabPagesList();
                }
            }
            else
            {
                Response.Redirect("LabLogin.aspx", false);
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void checkAddAll(object sender, EventArgs e)
    {
        CheckBox chckheader = (CheckBox)gridEditRole.HeaderRow.FindControl("checkAddAll");
        foreach (GridViewRow row in gridEditRole.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkadd");
            if (chckheader.Checked == true)
            {
                chckrw.Checked = true;
                chckheader.Checked = true;
            }
            else
            {
                chckrw.Checked = false;
                chckheader.Checked = false;
            }

        }
    }
    protected void checkEditAll(object sender, EventArgs e)
    {
        CheckBox chckheader = (CheckBox)gridEditRole.HeaderRow.FindControl("checkEditAll");
        foreach (GridViewRow row in gridEditRole.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkedit");
            if (chckheader.Checked == true)
            {
                chckrw.Checked = true;
                chckheader.Checked = true;
            }
            else
            {
                chckrw.Checked = false;
                chckheader.Checked = false;
            }

        }
    }
    protected void loadLabPagesList()
    {
        try
        {
            int userRoleid = Convert.ToInt32(Request.QueryString["roleuserid"]);
            int sLabId = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());
            DataSet ds = objEditRoles.getPageList(userRoleid.ToString(), sLabId.ToString());
            username.Text = ds.Tables[0].Rows[0]["sFullName"].ToString();
              Session["EditRole"] = ds;
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridEditRole.DataSource = ds;
                        gridEditRole.DataBind();
                       // lblusercount.Text = (gridAlltext.Rows.Count).ToString();
                        
                    }
                    else
                    {
                        //gridAlltext.EmptyDataText = "No records found";
                    }
                }
        }
        
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btneditrole_Click(object sender, EventArgs e)
    {
        DataSet getHwDetails = new DataSet();
        getHwDetails = (DataSet)Session["EditRole"];
        string sLabID = Request.Cookies["labId"].Value.ToString();
        CheckBox chkRowsAdd, chkRowsEdit, chkRowsView;
        for (int i = 0; i <= getHwDetails.Tables[0].Rows.Count - 1; i++)
        {
            chkRowsAdd = (gridEditRole.Rows[i].Cells[2].FindControl("chkadd") as CheckBox);
            if (!chkRowsAdd.Checked)
            {
                column = "sAdd";
            }
          
            if (chkRowsAdd.Checked)
            {
                Userid = getHwDetails.Tables[0].Rows[i].Field<Int64>("sUserid");
                PageId = getHwDetails.Tables[0].Rows[i].Field<Int32>("sPageId");
                column = "sAdd";
                if (column == "sAdd")
                {
                    DataAccessLayer DAL = new DataAccessLayer();
                    SqlParameter[] param = new SqlParameter[]
                     {
                         new SqlParameter("@UserId",Userid),
                         new SqlParameter("@PageId",PageId),
                          new SqlParameter("@sLabID",sLabID),
                         new SqlParameter("@Column",column),
                         new SqlParameter("@ColumnValue","1") 
                     };

                    DAL.ExecuteStoredProcedure("Sp_EditLabUserRolesNew", param);
                }
            }
           else
            {
                Userid = getHwDetails.Tables[0].Rows[i].Field<Int64>("sUserid");
                PageId = getHwDetails.Tables[0].Rows[i].Field<Int32>("sPageId"); 

                DataAccessLayer DAL = new DataAccessLayer();
                SqlParameter[] param = new SqlParameter[]
                     {
                         new SqlParameter("@UserId",Userid),
                         new SqlParameter("@PageId",PageId),
                           new SqlParameter("@sLabID",sLabID),
                         new SqlParameter("@Column",column),
                         new SqlParameter("@ColumnValue","0") 
                     };

                DAL.ExecuteStoredProcedure("Sp_EditLabUserRolesNew", param);
            }
        }
        for (int i = 0; i <= getHwDetails.Tables[0].Rows.Count - 1; i++)
        {
            chkRowsEdit = (gridEditRole.Rows[i].Cells[3].FindControl("chkedit") as CheckBox);
            if (!chkRowsEdit.Checked)
            {
                column = "sEdit";
            }

            if (chkRowsEdit.Checked)
            {
                Userid = getHwDetails.Tables[0].Rows[i].Field<Int64>("sUserid");
                PageId = getHwDetails.Tables[0].Rows[i].Field<Int32>("sPageId");
                column = "sEdit";
                if (column == "sEdit")
                {
                    DataAccessLayer DAL = new DataAccessLayer();
                    SqlParameter[] param = new SqlParameter[]
                     {
                         new SqlParameter("@UserId",Userid),
                         new SqlParameter("@PageId",PageId),
                          new SqlParameter("@sLabID",sLabID),
                         new SqlParameter("@Column",column),
                         new SqlParameter("@ColumnValue","1") 
                     };

                    DAL.ExecuteStoredProcedure("Sp_EditLabUserRolesNew", param);
                }
            }
            else
            {
                Userid = getHwDetails.Tables[0].Rows[i].Field<Int64>("sUserid");
                PageId = getHwDetails.Tables[0].Rows[i].Field<Int32>("sPageId");

                DataAccessLayer DAL = new DataAccessLayer();
                SqlParameter[] param = new SqlParameter[]
                     {
                         new SqlParameter("@UserId",Userid),
                         new SqlParameter("@PageId",PageId),
                           new SqlParameter("@sLabID",sLabID),
                         new SqlParameter("@Column",column),
                         new SqlParameter("@ColumnValue","0") 
                     };

                DAL.ExecuteStoredProcedure("Sp_EditLabUserRolesNew", param);
            }
        }
        for (int i = 0; i <= getHwDetails.Tables[0].Rows.Count - 1; i++)
        {
            chkRowsView = (gridEditRole.Rows[i].Cells[4].FindControl("chkview") as CheckBox);
            if (!chkRowsView.Checked)
            {
                column = "sView";
            }

            if (chkRowsView.Checked)
            {
                Userid = getHwDetails.Tables[0].Rows[i].Field<Int64>("sUserid");
                PageId = getHwDetails.Tables[0].Rows[i].Field<Int32>("sPageId");
                column = "sView";
                if (column == "sView")
                {
                    DataAccessLayer DAL = new DataAccessLayer();
                    SqlParameter[] param = new SqlParameter[]
                     {
                         new SqlParameter("@UserId",Userid),
                         new SqlParameter("@PageId",PageId),
                          new SqlParameter("@sLabID",sLabID),
                         new SqlParameter("@Column",column),
                         new SqlParameter("@ColumnValue","1") 
                     };

                    DAL.ExecuteStoredProcedure("Sp_EditLabUserRolesNew", param);
                }
            }
            else
            {
                Userid = getHwDetails.Tables[0].Rows[i].Field<Int64>("sUserid");
                PageId = getHwDetails.Tables[0].Rows[i].Field<Int32>("sPageId");

                DataAccessLayer DAL = new DataAccessLayer();
                SqlParameter[] param = new SqlParameter[]
                     {
                         new SqlParameter("@UserId",Userid),
                         new SqlParameter("@PageId",PageId),
                           new SqlParameter("@sLabID",sLabID),
                         new SqlParameter("@Column",column),
                         new SqlParameter("@ColumnValue","0") 
                     };

                DAL.ExecuteStoredProcedure("Sp_EditLabUserRolesNew", param);
            }
        }
        Response.Redirect(@"ManageUsers.aspx");
    }
}