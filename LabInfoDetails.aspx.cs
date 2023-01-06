using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using CrossPlatformAESEncryption.Helper;
using Validation;

public partial class LabInfoDetails : System.Web.UI.Page
{
    ClsLabInfo objLabInfo = new ClsLabInfo();
    InputValidation Ival = new InputValidation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Cookies["loggedIn"] != null)
                {
                    if (Request.Form["request"] != null)
                    {
                        HttpPostedFile file = null;
                        if (Request.Files.Count > 0)
                        {
                            try
                            {
                                for (int i = 0; i < Request.Files.Count; i++)
                                {
                                    file = Request.Files[i];
                                    file.SaveAs(Server.MapPath("~/temp/") + file.FileName);
                                }
                            }
                            catch { }
                        }
                    }
                    else
                    {

                        loadLabDetails();
                        loadLabContactDetails();

                    }
                }
                else
                {
                    Response.Redirect("LabLogin.aspx", false);
                }
            }
            catch
            {
                // Response.Redirect("Error.htm");
            }
        }
    }
    public void loadLabDetails()
    {
        try
        {
            DataSet ds = objLabInfo.getLabDetails(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    hiddenLogo.Value = row["sLabLogo"].ToString();
                    copyFilesTo(hiddenLogo, "~/images/", "~/temp/");
                    hiddenImages.Value = row["sLabImages"].ToString();
                    copyFilesTo(hiddenImages, "~/images/", "~/temp/");

                    //HDMLTSignature.Value = row["sColG"].ToString();
                    //copyFilesTo(HDMLTSignature, "~/images/", "~/temp/");
                    //HMDPathologistSignature.Value = row["sColH"].ToString();
                    //copyFilesTo(HMDPathologistSignature, "~/images/", "~/temp/");

                    //Load Details
                    spanLabId.InnerText = row["sLabId"].ToString();
                    spanLabName.InnerText = row["sLabName"].ToString();
                    //spanLabRegId.InnerText = row["sLabRegID"].ToString();
                    spanLogo.InnerHtml = "<img title='" + row["sLabLogo"].ToString() + "' src='temp/" + row["sLabLogo"].ToString() + "' id='" + row["sLabLogo"].ToString() + "' height='50px' width='50px'/>";
                    spanLabDetails.InnerText = row["sLabDetails"].ToString();
                    // spanhomeCollection.InnerText = row["sColF"].ToString();

                    //Bind details to editable fields
                    txtLabName.Text = row["sLabName"].ToString();
                    txtLabRegID.Text = row["sLabRegID"].ToString();
                    txtLabDetails.Text = row["sLabDetails"].ToString();
                    // txtHomeCollection.Text = row["sColF"].ToString();
                    //logoDiv.InnerHtml = "<img title='" + row["sLabLogo"].ToString() + "' src='temp/" + row["sLabLogo"].ToString() + "' id='" + row["sLabLogo"].ToString() + "' height='50px' width='50px'/>";                
                }
            }
        }
        catch (Exception Ex)
        {
            //Response.Redirect("Error.htm");
        }
    }
    protected void loadLabContactDetails()
    {
        try
        {
            DataSet ds = objLabInfo.getLabDetails(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load Details

                    spanLabEmail.InnerText = row["sLabEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabEmailId"].ToString()) : "";
                    spanLabContact.InnerText = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                    spanLabAddress.InnerText = row["sLabAddress"].ToString();

                    //Bind details to editable fields
                    txtLabEmail.Text = row["sLabEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabEmailId"].ToString()) : "";
                    txtLabContact.Text = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                    txtLabAddress.Text = row["sLabAddress"].ToString();
                }
            }
        }
        catch
        {
           // Response.Redirect("Error.htm");
        }
    }
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string labId = spanLabId.InnerText;
    //        string labName = txtLabName.Text;
    //        string LabRegID = txtLabRegID.Text;
    //        string labDetails = txtLabDetails.Text;
    //        string labHomeCollection = "";
    //        string labImages = hiddenImages.Value.ToString().TrimStart('@').TrimEnd('@');
    //        string labLogo = hiddenLogo.Value.ToString().TrimStart('@').TrimEnd('@');
    //        string DMLTsign = HDMLTSignature.Value.ToString().TrimStart('@').TrimEnd('@');
    //        string MDsign = HMDPathologistSignature.Value.ToString().TrimStart('@').TrimEnd('@');
    //          string Msg = "";
    //        String DMLTsignextention = System.IO.Path.GetExtension(DMLTsign);
    //        String labLogoextention = System.IO.Path.GetExtension(labLogo);
    //        String MDsignextention = System.IO.Path.GetExtension(MDsign);
    //        String labImagesextention = System.IO.Path.GetExtension(labImages);
    //        if (labLogoextention.ToLower() != ".jpg" && labLogoextention.ToLower() != ".png" && labLogoextention.ToLower() != ".jpeg" && labLogoextention.ToLower() != ".gif" && labLogoextention.ToLower() != ".bmp")
    //        {
    //            Msg += "Please Upload only jpg,png,jpeg,gif,bmp images only";
    //        }
    //        if (DMLTsignextention.ToLower() != ".jpg" && DMLTsignextention.ToLower() != ".png" && DMLTsignextention.ToLower() != ".jpeg" && DMLTsignextention.ToLower() != ".gif" && DMLTsignextention.ToLower() != ".bmp")
    //        {
    //            Msg += "Please Upload only jpg,png,jpeg,gif,bmp images only";
    //        }
    //        if (MDsignextention.ToLower() != ".jpg" && MDsignextention.ToLower() != ".png" && MDsignextention.ToLower() != ".jpeg" && MDsignextention.ToLower() != ".gif" && MDsignextention.ToLower() != ".bmp")
    //        {
    //            Msg += "Please Upload only jpg,png,jpeg,gif,bmp images only";
    //        }
    //        if (labImagesextention.ToLower() != ".jpg" && labImagesextention.ToLower() != ".png" && labImagesextention.ToLower() != ".jpeg" && labImagesextention.ToLower() != ".gif" && labImagesextention.ToLower() != ".bmp")
    //        {
    //            Msg += "Please Upload only jpg,png,jpeg,gif,bmp images only";
    //        }
    //        if (Msg.Length < 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
    //        }
    //        else
    //        {

    //            if (objLabInfo.updateLabDetails(labId, labName, LabRegID, labImages, labDetails, labLogo, labHomeCollection, DMLTsign, MDsign) == 1)
    //            {
    //                copyFilesTo(hiddenLogo, "~/temp/", "~/images/");
    //                copyFilesTo(hiddenImages, "~/temp/", "~/images/");
    //                copyFilesTo(HDMLTSignature, "~/temp/", "~/images/");
    //                copyFilesTo(HMDPathologistSignature, "~/temp/", "~/images/");
    //                clearTempFolder("~/temp/");
    //                lblMessage.Text = "LabInfo Details Updated successfully";
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    //                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.reload();", true);
    //            }
    //            else if (objLabInfo.updateLabDetails(labId, labName, LabRegID, labImages, labDetails, labLogo, labHomeCollection, DMLTsign, MDsign) == 0)
    //            {
    //                lblMessage.Text = "Error occurred While Updating Labinfo Details";
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
    //               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
    //            }
    //        }
    //    }
    //    catch
    //    {
    //       // Response.Redirect("Error.htm");
    //    }
    //}
    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (!Ival.IsTextBoxEmpty(txtLabEmail.Text))
            {
                if (!Ival.IsValidEmailAddress(txtLabEmail.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (!Ival.IsTextBoxEmpty(txtLabContact.Text))
            {
                if (Ival.IsInteger(txtLabContact.Text))
                {
                    if (!Ival.MobileValidation(txtLabContact.Text))
                    {
                        Msg += "● Please Enter Valid Mobile Number";
                    }
                }
            }
            if (Msg.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
            }
            else
            {
                string labId = Request.Cookies["labId"].Value.ToString();
                string labEmail = txtLabEmail.Text;
                string labContact = txtLabContact.Text;
                string labAddress = txtLabAddress.Text;
                string labLocation = "";

                if (objLabInfo.updateLabContactDetails(labId, labEmail, labContact, labAddress, labLocation) == 1)
                {
                    lblMessage.Text = "Lab Contact Details Updated successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.reload();", true);
                }
                if (objLabInfo.updateLabContactDetails(labId, labEmail, labContact, labAddress, labLocation) == 0)
                {
                    lblMessage.Text = "Error occurred while updating Lab Contact Detail";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
                }
            }
        }
        catch
        {
            //Response.Redirect("Error.htm");
        }
    }
    protected void copyFilesTo(HiddenField hiddenFile, string sourceFolder, string destinationFolder)
    {
        try
        {
            if (hiddenFile.Value != "")
            {
                if (hiddenFile.Value.Contains('@'))
                {
                    hiddenFile.Value = hiddenFile.Value.ToString().TrimEnd('@').TrimStart('@');
                    string[] files = hiddenFile.Value.Split('@');
                    foreach (string file in files)
                    {
                        string sourceFile = Server.MapPath(sourceFolder) + file;
                        string destinationFile = Server.MapPath(destinationFolder) + file;

                        if (File.Exists(Server.MapPath(sourceFolder) + file))
                        {
                            if (!File.Exists(Server.MapPath(destinationFolder) + file))
                            {
                                System.IO.File.Copy(sourceFile, destinationFile);
                            }
                        }
                    }
                }
                else
                {
                    string sourceFile = Server.MapPath(sourceFolder) + hiddenFile.Value.ToString();
                    string destinationFile = Server.MapPath(destinationFolder) + hiddenFile.Value.ToString();

                    if (File.Exists(Server.MapPath(sourceFolder) + hiddenFile.Value.ToString()))
                    {
                        if (!File.Exists(Server.MapPath(destinationFolder) + hiddenFile.Value.ToString()))
                        {
                            System.IO.File.Copy(sourceFile, destinationFile);
                        }
                    }
                }
            }
        }
        catch
        {
            //Response.Redirect("Error.htm");
        }
    }
    protected void clearTempFolder(string sourceFolder)
    {
        if (sourceFolder == "~/temp/")
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(sourceFolder));

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }


    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            string labId = Request.Cookies["labId"].Value.ToString();
            string labName = txtLabName.Text;
            string LabRegID = txtLabRegID.Text;
            string labDetails = txtLabDetails.Text;
            string labHomeCollection = "";
            string labImages = hiddenImages.Value.ToString().TrimStart('@').TrimEnd('@');
            string labLogo = hiddenLogo.Value.ToString().TrimStart('@').TrimEnd('@');
           // string DMLTsign = HDMLTSignature.Value.ToString().TrimStart('@').TrimEnd('@');
          //  string MDsign = HMDPathologistSignature.Value.ToString().TrimStart('@').TrimEnd('@');
            string Msg = "";
          //  String DMLTsignextention = System.IO.Path.GetExtension(DMLTsign);
            String labLogoextention = System.IO.Path.GetExtension(labLogo);
          //  String MDsignextention = System.IO.Path.GetExtension(MDsign);
            String labImagesextention = System.IO.Path.GetExtension(labImages);
            if (labLogoextention.ToLower() != ".jpg" && labLogoextention.ToLower() != ".png" && labLogoextention.ToLower() != ".jpeg" && labLogoextention.ToLower() != ".gif" && labLogoextention.ToLower() != ".bmp")
            {
                Msg += "Please Upload only jpg,png,jpeg,gif,bmp images only";
            }
            //if (DMLTsignextention.ToLower() != ".jpg" && DMLTsignextention.ToLower() != ".png" && DMLTsignextention.ToLower() != ".jpeg" && DMLTsignextention.ToLower() != ".gif" && DMLTsignextention.ToLower() != ".bmp")
            //{
            //    Msg += "Please Upload only jpg,png,jpeg,gif,bmp images only";
            //}
            //if (MDsignextention.ToLower() != ".jpg" && MDsignextention.ToLower() != ".png" && MDsignextention.ToLower() != ".jpeg" && MDsignextention.ToLower() != ".gif" && MDsignextention.ToLower() != ".bmp")
            //{
            //    Msg += "Please Upload only jpg,png,jpeg,gif,bmp images only";
            //}
            if (labImagesextention.ToLower() != ".jpg" && labImagesextention.ToLower() != ".png" && labImagesextention.ToLower() != ".jpeg" && labImagesextention.ToLower() != ".gif" && labImagesextention.ToLower() != ".bmp")
            {
                Msg += "Please Upload only jpg,png,jpeg,gif,bmp images only";
            }
            if (Msg.Length < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
            }
            else
            {

                if (objLabInfo.updateLabDetails(labId, labName, LabRegID, labImages, labDetails, labLogo, labHomeCollection) == 1)
                {
                    copyFilesTo(hiddenLogo, "~/temp/", "~/images/");
                    copyFilesTo(hiddenImages, "~/temp/", "~/images/");
                   // copyFilesTo(HDMLTSignature, "~/temp/", "~/images/");
                   // copyFilesTo(HMDPathologistSignature, "~/temp/", "~/images/");
                    clearTempFolder("~/temp/");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Lab Details Update successfully');location.href='LabInfoDetails.aspx';", true);
                }
                else if (objLabInfo.updateLabDetails(labId, labName, LabRegID, labImages, labDetails, labLogo, labHomeCollection) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Error Occured');location.href='LabInfoDetails.aspx';", true);
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }
    }
}