using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BitsBizLogic;
using DataAccessHandler;
using Validation;

public partial class SuperAdmin_AddLifeStyleDisorder : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                grdviewLifeStyle.DataSource = DAL.GetDataTable("Sp_GetLifeStyleDisorder");
                grdviewLifeStyle.DataBind();
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
        {
            string filename = Session.SessionID + FuIcon.FileName;
            if (FuIcon.HasFile)
            {
                String fileextension = System.IO.Path.GetExtension(FuIcon.FileName);
                if (fileextension.ToLower() != ".jpg" && fileextension.ToLower() != ".png" && fileextension.ToLower() != ".jpeg" && fileextension.ToLower() != ".gif" && fileextension.ToLower() != ".bmp")
                {
                    Msg = "Please Upload only jpg,png,jpeg,gif,bmp images only</br> ";
                }
                else
                {
                    FuIcon.PostedFile.SaveAs(Server.MapPath("../images/icons/" + filename));
                    filename = "/images/icons/" + Session.SessionID + FuIcon.FileName;
                }
                if (Ival.IsTextBoxEmpty(txtDisorderame.Text.ToString()))
                {
                    Msg += "● Please Enter Valid Disorder Name";
                }
                if (Msg.Length > 0)
                {
                    litErrorMessage.Text = ApplicationLogic.ErrorWarning(Msg);
                }
                else
                {
                    SqlParameter[] param = new SqlParameter[]
                         {
                             new SqlParameter("@Name",txtDisorderame.Text),
                             new SqlParameter("@Details",txtDetails.Text),
                             new SqlParameter("@Icon",filename),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
                    int Result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddLifeStyleDisorder", param);
                    // Display Message
                    if (Result >= 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('LifeStyleDisorder Added successfully');location.href='AddLifeStyleDisorder.aspx';", true);
                     
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
    }
    protected void grdviewLifeStyle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteID = Convert.ToInt32(grdviewLifeStyle.DataKeys[e.RowIndex].Value.ToString());
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",deleteID)
            };

            DAL.ExecuteStoredProcedure("Sp_DeleteLifeStyle ", param);
            grdviewLifeStyle.DataSource = DAL.GetDataTable("Sp_GetLifeStyleDisorder");
            grdviewLifeStyle.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }
    }
    protected void grdviewLifeStyle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewLifeStyle.PageIndex = e.NewPageIndex;
        grdviewLifeStyle.DataSource = DAL.GetDataTable("Sp_GetLifeStyleDisorder");
        grdviewLifeStyle.DataBind();
    }
    protected void grdviewLifeStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = (grdviewLifeStyle.DataKeys[grdviewLifeStyle.SelectedRow.DataItemIndex].Value).ToString();

        try
        {

            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",id)
            };

            DAL.ExecuteStoredProcedure("Sp_ActiveOrgnization ", param);
            grdviewLifeStyle.DataSource = DAL.GetDataTable("Sp_GetLifeStyleDisorder");
            grdviewLifeStyle.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }

        grdviewLifeStyle.DataSource = DAL.GetDataTable("Sp_GetLifeStyleDisorder");
        grdviewLifeStyle.DataBind();

    }
}