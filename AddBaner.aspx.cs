using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BitsBizLogic;
using DataAccessHandler;
public partial class SuperAdmin_plugins_AddBaner : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetBanerList");
                grdviewOrgnization.DataBind();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        string filename = Session.SessionID + OrgLogo.FileName;
        if (OrgLogo.HasFile)
        {
            String fileextension = System.IO.Path.GetExtension(OrgLogo.FileName);
            if (fileextension.ToLower() != ".jpg" && fileextension.ToLower() != ".png" && fileextension.ToLower() != ".jpeg" && fileextension.ToLower() != ".gif" && fileextension.ToLower() != ".bmp")
            {
                Msg = "Please Upload only jpg,png,jpeg,gif,bmp images only</br> ";
            }
            else
            {
                OrgLogo.PostedFile.SaveAs(Server.MapPath("../SuperAdmin/images/Baner/" + filename));
                filename = Session.SessionID + OrgLogo.FileName;
            }
            if (Msg.Length > 0)
            {
                litErrorMessage.Text = ApplicationLogic.ErrorWarning(Msg);
            }
            else
            {
                ///Submit Data To Database

                SqlParameter[] param = new SqlParameter[]
                         {
                             new SqlParameter("@OrgLogo",filename),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
                int Result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddBaner", param);
                // Display Message
                if (Result >= 1)
                {
                    if (Result == 1)
                    {
                        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetBanerList");
                        grdviewOrgnization.DataBind();
                    }
                    else if (Result == -2)
                    {
                        litErrorMessage.Text = ApplicationLogic.ErrorWarning("●User Already Exist!");
                    }
                }
                else if (Result == -2)
                {
                    litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Already Exist!");
                }
                else
                {
                    litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Something Went Wrong, Please try Again!");
                }
            }
        }
    }

    protected void grdviewOrgnization_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteID = Convert.ToInt32(grdviewOrgnization.DataKeys[e.RowIndex].Value.ToString());
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",deleteID)
            };

            DAL.ExecuteStoredProcedure("Sp_DeleteBaner ", param);
            grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetBanerList");
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }
    }
    protected void grdviewOrgnization_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewOrgnization.PageIndex = e.NewPageIndex;
        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetBanerList");
        grdviewOrgnization.DataBind();
    }



    protected void grdviewOrgnization_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = (grdviewOrgnization.DataKeys[grdviewOrgnization.SelectedRow.DataItemIndex].Value).ToString();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",id)
            };

            DAL.ExecuteStoredProcedure("Sp_Activebaner ", param);
            grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetBanerList");
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }

        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetBanerList");
        grdviewOrgnization.DataBind();

    }
}