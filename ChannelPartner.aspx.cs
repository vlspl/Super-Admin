using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using BitsBizLogic;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using Validation;
using System.Net;
using System.IO;
using System.Web.UI.WebControls;


public partial class SuperAdmin_ChannelPartner : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                labdrp.Visible = false;
                db.bindDrp("select distinct sLabId, sLabName from labMaster where IsActive=1 and sLabStatus='Active' order by sLabName asc", drplab, "sLabName", "sLabId");
                drplab.Items.Insert(0, new ListItem("-Select-", "-Select-"));
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (drptype.Text != "-Select-")
        {
            string Msg = "";
            {
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
                        OrgLogo.PostedFile.SaveAs(Server.MapPath("../images/profileimage/" + filename));
                        filename = "../images/profileimage/" + Session.SessionID + OrgLogo.FileName;
                    }
                }
                if (Ival.IsTextBoxEmpty(txtChnPName.Text))
                {
                    Msg += "● Please Enter Valid Channel Partner Name";
                }
                if (Ival.IsTextBoxEmpty(txtChnnelCode.Text))
                {
                    Msg += "● Please Enter Valid Channel Partner Code";
                }
                if (!Ival.IsTextBoxEmpty(txtEmail.Text))
                {
                    if (!Ival.IsValidEmailAddress(txtEmail.Text))
                    {
                        Msg += "● Please Enter Valid Email Id";
                    }
                }
                if (Ival.IsInteger(txtmobilenumber.Text))
                {
                    if (!Ival.MobileValidation(txtmobilenumber.Text))
                    {
                        Msg += "● Please Enter Valid Mobile Number";
                    }
                }
                else
                {
                    Msg += "● Please Enter Valid Mobile Number";
                }
                if (Msg.Length > 0)
                {
                    litErrorMessage.Text = ApplicationLogic.ErrorWarning(Msg);
                }
                else
                {
                    string _EmailId = txtEmail.Text != "" ? CryptoHelper.Encrypt(txtEmail.Text.ToLower()) : "";
                    string _Mobile = txtmobilenumber.Text != "" ? CryptoHelper.Encrypt(txtmobilenumber.Text) : "";

                    SqlParameter[] param = new SqlParameter[]
                         {
                             new SqlParameter("@ChannelPartnerName",txtChnPName.Text),
                             new SqlParameter("@ChannelCode",txtChnnelCode.Text.ToUpper()),
                             new SqlParameter("@Contact",_Mobile),
                             new SqlParameter("@Email",_EmailId),
                             new SqlParameter("@Address",txtAdress.Text),
                             new SqlParameter("@ProfilePic",filename),
                              new SqlParameter("@labId",drplab.SelectedValue.ToString()),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
                    int Result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddChannelPartner", param);
                    string url = "https://users.visionarylifescience.com/?code=" + txtChnnelCode.Text.ToUpper() + "";
                    string tinyUrl;
                    string api = "http://tinyurl.com/api-create.php?url=";
                    var request = WebRequest.Create(api + url);
                    var res = request.GetResponse();
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        tinyUrl = reader.ReadToEnd();
                    }

                    newWhatsapp wa = new newWhatsapp();
                    wa.sendWhatsappMsg("+91" + txtmobilenumber.Text, "Chanel_Partner", txtChnPName.Text + ',' + txtChnnelCode.Text.ToUpper() + ',' + tinyUrl, drplab.SelectedValue.ToString());
                    // Display Message
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Channel Partner Added successfully');location.href='ViewChannelPartnerList.aspx';", true);
                    if (Result >= 1)
                    {
                        litErrorMessage.Text = ApplicationLogic.SuccessMessage("● Saved Suceessfully.");
                    }
                    else if (Result == -1)
                    {
                        litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Channel Partner Code Already Exist!");
                    }
                    else if (Result == -2)
                    {
                        litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Mobile Number Already Exist!");
                    }
                    else if (Result == -3)
                    {
                        litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Email Id Already Exist!");
                    }
                    else
                    {
                        litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Something Went Wrong, Please try Again!");
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Type');", true);
        }
        }
    protected void drptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drptype.Text == "Lab")
        {
            labdrp.Visible = true;
        }
        else
        {
            labdrp.Visible = false ;
            //int numval;
            //string Num = randomNum(numval);
            //txtChnnelCode.Text = genrateCode("HOW", Num);
        }

    }

  
    protected void drplab_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtChnPName.Text = db.getData("select sLabName from labMaster where sLabId='"+drplab.SelectedValue.ToString()+"'");
        txtAdress.Text = db.getData("select sLabAddress from labMaster where sLabId='" + drplab.SelectedValue.ToString() + "'");
        int SLabId =  int.Parse(drplab.SelectedValue.ToString()) ;
        txtChnnelCode.Text = genrateCode("LAB", SLabId);
    }
    public string genrateCode(string Name, int maxNum)
    {

        string dName, num;
        dName = Name.Substring(0, 3).ToUpper();
        num = maxNum.ToString().PadLeft(6, '0');
        return dName + num;
    }
}
