<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="TestMaster.aspx.cs" Inherits="TestMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .loader
        {
            position: fixed;
            z-index: 50;
            height: 100vh;
            width: 100%;
            background: #fff;
            top: 0;
        }
        
        
        .form_loader
        {
            background: #ffffffc4;
            width: 100%;
            height: 100%;
        }
        
        
        .form_loader img
        {
            vertical-align: middle;
            margin-top: 25%;
        }
        .testmaster, .testbook
        {
            position: relative;
            padding-bottom: 60px;
        }
    </style>
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
       <h3 class="navbar-brand">Test Master</h3>
      </div>
    </div>
  </nav>
    <div class="wrapper">
        <div id="content">
            <div id="div1" class="testmaster first" clientidmode="Static">
                <!-- Section -->
                <asp:HiddenField ID="hiddenSectionIdName" runat="server" ClientIDMode="Static" />
                <div class="table_div">
                    <div class="container-fluid">
                        <h5 class="navbar-brand">
                            Select Section and Profile
                        </h5>
                        <br />
                        <div class="row testmasterwrap">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="selSection" runat="server" ClientIDMode="Static"
                                        onchange="javascript:sectionSelected(this)">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <a href="" class="btn btn-color HideAddbtn" data-toggle="modal" data-target="#modalAddSection">
                                    Add Section</a>
                            </div>
                            <!-- Profile -->
                            <asp:HiddenField ID="hiddenProfileId" runat="server" ClientIDMode="Static" />
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList class="form-control" ID="selProfile" runat="server" ClientIDMode="Static"
                                        onchange="javascript:profileSelected(this)">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <a href="" class="btn btn-color HideAddbtn" data-toggle="modal" class='HideEditbtn'
                                    data-target="#modalAddProfile">Add Profile</a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-11">
                            </div>
                            <div class="col-md-1">
                                <button class="btn btn-submit nextbtn" id="btnDiv1Next" clientidmode="Static">
                                    Next</button></div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="div2" class="testmaster" clientidmode="Static">
                <!-- TEST DETAILS -->
                <asp:HiddenField ID="hiddenTestId" runat="server" ClientIDMode="Static" />
                <div class="table_div">
                    <div class="container">
                        <h5 class="navbar-brand">
                            Test Details
                        </h5>
                        <br />
                        <div class="row testDetail ">
                            <div class="col col-md-6">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Test Code" ID="txtTestCode" runat="server"
                                        ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Test Interpretation" ID="txtTestInterpretation"
                                        TextMode="MultiLine" Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Test useful for" ID="txtTestUsefulFor"
                                        TextMode="MultiLine" Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col col-md-6">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Test Name" ID="txtTestName" runat="server"
                                        ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Test Limitation" ID="txtTestLimitation"
                                        TextMode="MultiLine" Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Test Clinical References" ID="txtTestClinicalReferences"
                                        TextMode="MultiLine" Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-submit prevbtn" id="btnDiv2Previous" clientidmode="Static">
                                    Previous</button>
                                <button class="btn btn-submit nextbtn" id="btnDiv2Next" clientidmode="Static">
                                    Next</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="div3" class="testmaster">
                <div class="table_div">
                    <div class="container-fluid">
                        <h5 class="navbar-brand">
                            Select Analytes
                        </h5>
                        <br />
                        <div class="row masterform">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Search analyte by name" ID="txtSearchAnalyte"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <a href="" class="btn btn-color white-txt" data-toggle="modal" data-target="#modalAddAnalyte">
                                    Add Analyte</a>
                            </div>
                        </div>
                        <asp:HiddenField ID="hiddenAnalyteIdName" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hiddenAnalyteId" runat="server" ClientIDMode="Static" />
                        <div class="row testmasterwrap">
                            <table class="table table-bordered table-hover fixed-table" id="tabAnalyteList">
                                <thead>
                                    <tr>
                                        <td style="width: 5%">
                                            Action
                                        </td>
                                        <td style="width: 5%">
                                            Sr No.
                                        </td>
                                        <td style="width: 5%">
                                            Analyte Name
                                        </td>
                                    </tr>
                                </thead>
                                <tbody id="tbodyAnalyteList" runat="server" clientidmode="Static">
                                </tbody>
                            </table>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-submit prevbtn" id="btnDiv3Previous" clientidmode="Static">
                                    Previous</button>
                                <button class="btn btn-submit nextbtn" type="button" id="btnLoadSubAnalytes" onclick="javascript:loadSubAnalytesOfAnalytes()"
                                    clientidmode="Static">
                                    Next</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="div4" class="testmaster">
                <div class="table_div">
                    <div class="container-fluid">
                        <h5 class="navbar-brand">
                            Select SubAnalytes
                        </h5>
                        <br />
                        <div class="row masterform">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Search subanalyte by name" ID="txtSearchSubAnalyte"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <a id="linkAddSubAnalyte" class="btn btn-color white-txt" href="" data-toggle="modal"
                                    data-target="#modalAddSubAnalyte" style="display: none" clientidmode="Static">Add
                                    SubAnalyte</a>
                            </div>
                        </div>
                        <asp:HiddenField ID="hiddenSubAnalyteIdName" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hiddenSubAnalyteId" runat="server" ClientIDMode="Static" />
                        <div class="row testmasterwrap">
                            <table class="table"  id="tabSubAnalyteList">
                                <thead class="table-header">
                                    <tr>
                                        <td>
                                            Action
                                        </td>
                                        <td>
                                            SubAnalyte Id
                                        </td>
                                        <td>
                                            SubAnalyte Name
                                        </td>
                                        <td>
                                            Analyte Name
                                        </td>
                                    </tr>
                                </thead>
                                <tbody id="tbodySubAnalyteList" runat="server" clientidmode="Static">
                                </tbody>
                            </table>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-submit prevbtn" id="btnDiv4Previous" clientidmode="Static">
                                    Previous</button>
                                <button class="btn btn-submit nextbtn" type="button" id="btnLoadSelectedAnalytesSubAnalytes"
                                    clientidmode="Static" onclick="javascript:loadSelectedAnalytesSubAnalytes()">
                                    Next</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="div5" class="testmaster">
                <div class="table_div">
                    <div class="container-fluid">
                        <h5 class="navbar-brand">
                            Specimen/Method Details
                        </h5>
                        <br />
                        <asp:HiddenField ID="hiddenAnalyteSMIdName" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hiddenSubAnalyteSMIdName" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hiddenAnalyteSMR" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hiddenSubAnalyteSMR" runat="server" ClientIDMode="Static" />
                        <div class="row testmasterwrap">
                            <table class="table  fixed-table" id="tabSpecimenMethod">
                                <thead class="table-header">
                                    <tr>
                                        <th>
                                            Analyte
                                        </th>
                                        <th>
                                            SubAnalyte
                                        </th>
                                        <th>
                                            Specimen and Method
                                        </th>
                                        <th>
                                        Action
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tbodySpecimenMethod" runat="server" clientidmode="Static">
                                </tbody>
                            </table>
                           
                        </div>
                         <div class="row">
                            <div class="col-md-10">
                            </div>
                            <div class="col-md-2">
                                <button class="btn btn-submit prevbtn" id="btnDiv5Previous" clientidmode="Static">
                                Previous</button>
                            <button class="btn btn-submit nextbtn" type="button" id="btnLoadAnalyteSubAnalyteSMR"
                                clientidmode="Static" onclick="javascript:loadAnalyteSubAnalyteSMR()">
                                Next</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="div6" class="testmaster">
                <div class="table_div">
                    <div class="container-fluid">
                        <h5 class="navbar-brand">
                         Reference Values
                        </h5>
                        <br />
               
                <asp:HiddenField ID="hiddenAnalyteSMRRefVal" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenSubAnalyteSMRRefVal" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenSelectedAnalyteSMR" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenSelectedSubAnalyteSMR" runat="server" ClientIDMode="Static" />
                <div class="row testmasterwrap">
                    <table class="table  fixed-table" id="tabReferenceValues">
                        <thead class="table-header">
                            <tr>
                                <td>
                                    Analyte
                                </td>
                                <td>
                                    SubAnalyte
                                </td>
                                <td>
                                    Specimen and Method
                                </td>
                                <td>
                                    Result Type
                                </td>
                                <td>
                                    Reference Values
                                </td>
                                <td>
                                Action
                                </td>
                            </tr>
                        </thead>
                        <tbody id="tbodyReferenceValues" runat="server" clientidmode="Static">
                        </tbody>
                    </table>
                </div>
                  <div class="row">
                            <div class="col-md-10">
                            </div>
                            <div class="col-md-2">
                                <asp:UpdatePanel ID="upp1" runat="server">
                        <ContentTemplate>
                            <button class="btn btn-submit prevbtn" id="btnDiv6Previous" clientidmode="Static">
                                Previous</button>
                            <asp:Button class="btn btn-submit" ID="btnCreateTest" Text="Create Test"
                                runat="server" ClientIDMode="Static" OnClick="btnCreateTest_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upp1">
                        <ProgressTemplate>
                            <div class="text form_loader">
                                <img src="../images/Loader.gif" alt="Loading">
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                            </div>
                        </div>
            </div>
        </div>
    </div>
    </div>
    </div>
    <!-- MODALS START -->
    <!-- Modal Add Section Start-->
    <div id="modalAddSection" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">  <h4 class="modal-title">
                        Add Section</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                  
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Section name" ID="txtSection" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblSection" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" id="btnAddSection" class="btn btn-submit" value="Submit" onclick="javascript:return addSection()"
                        clientidmode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Add Section End-->
    <!-- Modal Add Profile Start-->
    <div id="modalAddProfile" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header"> <h4 class="modal-title">
                        Add Profile</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                   
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Profile name" ID="txtProfileName"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblProfileName" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" id="btnAddProfile" class="btn btn-submit" value="Submit" onclick="javascript:return addProfile(this)"
                        clientidmode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Add Profile End-->
    <!-- Modal Add Analyte Start-->
    <div id="modalAddAnalyte" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header"> <h4 class="modal-title">
                        Add Analyte</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                   
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Analyte name" ID="txtAnalyteName"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblAnalyteName" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" id="btnAddAnalyte" class="btn btn-submit" value="Submit" onclick="javascript:return addAnalyte()"
                        clientidmode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Add Analyte End-->
    <!-- Modal Add SubAnalyte Start-->
    <div id="modalAddSubAnalyte" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header"> <h4 class="modal-title">
                        Add Sub Analyte</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                   
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <div class="form-group">
                                <select id="selAnalyteName" class="form-control" runat="server" clientidmode="Static">
                                </select>
                                <label id="lblAnalyteName" class="form-error">
                                </label>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Sub Analyte name" ID="txtSubAnalyteName"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblSubAnalyteName" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" id="btnAddSubAnalyte" class="btn btn-submit" value="Submit"
                        onclick="javascript:return addSubAnalyte()" clientidmode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Add SubAnalyte End-->
    <!-- Modal Add Specimen Method Start-->
    <div id="modalAddSpecimenMethod" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header"> <h4 class="modal-title">
                        Add Specimen Method</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                   
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <!-- Specimen Details-->
                        <div class="">
                            <h5>
                                Specimen Details</h5>
                            <div class="form-group">
                          
                                <input class="form-control" list="browsers" placeholder="Specimen Name" id="txtSpecimenName"
                                    runat="server" name="txtSpecimenName" clientidmode="Static">
                                <datalist id="browsers" runat="server" clientidmode="Static"> </datalist>
                                <label id="lblSpecimenName" class="form-error">
                                </label>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Quantity" ID="txtQuantity" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblQuantity" class="form-error">
                                </label>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Time Period" ID="txtTimePeriod" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblTimePeriod" class="form-error">
                                </label>
                            </div>
                        </div>
                        <!-- Method Details-->
                        <div class="">
                            <h5>
                                Method Details</h5>
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Method" ID="txtMethod" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblMethod" class="form-error">
                                </label>
                            </div>
                        </div>
                        <!-- Result type-->
                        <div class="">
                            <h5>
                                Result Type</h5>
                            <div class="form-group">
                                <select class="form-control" id="selResultType" clientidmode="Static">
                                    <option value="">Select Result type</option>
                                    <option value="Quantitative">Quantitative</option>
                                    <option value="Qualitative">Qualitative</option>
                                    <option value="Descriptive">Descriptive</option>
                                </select>
                                <label id="lblResultType" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" id="btnAddSpecimenMethod" class="btn btn-submit" value="Submit"
                        onclick="javascript:return addSpecimenMethod()" clientidmode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Add Specimen Method End-->
    <div id="modalAddReferenceValues" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="width:130%">
                <div class="modal-header"> <h4 class="modal-title">
                        Add Reference Values</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                   
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <div class="form-group">
                                <select class="form-control" id="selReferenceType" clientidmode="Static">
                                    <option value="">Select Reference Type</option>
                                    <option value="Reference as per gender and age">Reference as per gender and age</option>
                                    <option value="Reference value as per severity of disease">Reference value as per severity
                                        of disease</option>
                                    <option value="Reference as per limits of detection">Reference as per limits of detection</option>
                                    <option value="NA">Not Applicable</option>
                                </select>
                                <label id="lblReferenceType" class="form-error">
                                </label>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <label id="Label2" class="form-label">
                                        Male Details
                                    </label>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" id="txtMaleMinAge" placeholder="Min Age"
                                                    clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                            </div>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" id="txtMaleMaxAge" placeholder="Max Age"
                                                    clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                            </div>
                                            <asp:TextBox class="form-control" placeholder="Age range" ID="txtMaleAge" runat="server"
                                                ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                            <label id="lblAge" class="form-error">
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <select id="selMaleAgeUnit" class="form-control" clientidmode="Static">
                                                    <option value="">Age Unit</option>
                                                    <option value="year">year</option>
                                                    <option value="month">month</option>
                                                    <option value="day">day</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <input type="text" id="txtMinMaleValue" class="form-control" placeholder="Min value"
                                                    clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                            </div>
                                            <div class="col-md-6">
                                                <input type="text" id="txtMaxMaleValue" class="form-control" placeholder="Max value"
                                                    clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                            </div>
                                            <asp:TextBox class="form-control" placeholder="Range/Value for male" ID="txtMaleRange"
                                                runat="server" ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                            <label id="lblMaleRange" class="form-error">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <label id="Label3" class="form-label">
                                        Female Details
                                    </label>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" id="txtFemaleMinAge" placeholder="Min Age"
                                                    clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                            </div>
                                            <div class="col-md-6">
                                                <input type="text" class="form-control" id="txtFemaleMaxAge" placeholder="Max Age"
                                                    clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                            </div>
                                            <asp:TextBox class="form-control" placeholder="Age range" ID="txtFemaleAge" runat="server"
                                                ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                            <label id="Label1" class="form-error">
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <select id="selFemaleAgeUnit" class="form-control" clientidmode="Static">
                                                    <option value="">Select Age Unit</option>
                                                    <option value="year">year</option>
                                                    <option value="month">month</option>
                                                    <option value="day">day</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <input type="text" id="txtMinFemaleValue" class="form-control" placeholder="Min value"
                                                    clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                            </div>
                                            <div class="col-md-6">
                                                <input type="text" id="txtMaxFemaleValue" class="form-control" placeholder="Max value"
                                                    clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                            </div>
                                            <asp:TextBox class="form-control" placeholder="Range/Value for female" ID="txtFemaleRange"
                                                runat="server" ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                            <label id="lblFemaleRange" class="form-error">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Grade" ID="txtGrade" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblGrade" class="form-error">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Units" ID="txtUnits" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblUnits" class="form-error">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Interpretation" ID="txtInterpretation"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblInterpretation" class="form-error">
                                </label>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Sensitivity" ID="txtLowerLimit" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblLowerLimit" class="form-error">
                                </label>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Linearity" ID="txtUpperLimit" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblUpperLimit" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="upp" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer">
                            <input type="button" id="btnAddReferenceValues" class="btn btn-submit" value="Submit"
                                onclick="javascript:return addReferenceValues()" clientidmode="Static" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upp">
                    <ProgressTemplate>
                        <div class="text form_loader">
                            <img src="images/Loader.gif" alt="Loading">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="loader" style="display: none;">
                    <img src="images/Loader.gif" alt="Loading" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".testmaster").each(function (e) {
                if (e != 0)
                    $(this).hide();
            });

            $(".nextbtn").click(function () {
                if ($(".testmaster:visible").next().length != 0) {
                    if ($(this).attr('id') == "btnDiv1Next") {
                        if ($("#selSection").val() != "" && $("#selProfile").val() != "") {
                            $(".testmaster:visible").next().show().prev().hide();
                        }
                        else {
                            alert("Select section and profile both");
                        }
                    }
                    else if ($(this).attr('id') == "btnDiv2Next") {

                        if ($("#txtTestCode").val() != "" && $("#txtTestName").val() != "" && $("#txtTestUsefulFor").val() != "" && $("#txtTestInterpretation").val() != "" && $("#txtTestLimitation").val() != "" && $("#txtTestClinicalReferences").val() != "") {
                            $(".testmaster:visible").next().show().prev().hide();
                        }
                        else {
                            alert("Enter all test details");
                        }
                    }
                    else if ($(this).attr('id') == "btnLoadSubAnalytes") {

                        if ($("#hiddenAnalyteIdName").val() != "") {
                            $(".testmaster:visible").next().show().prev().hide();
                        }
                        else {
                            alert("Select analytes");
                        }
                    }
                    else if ($(this).attr('id') == "btnLoadSelectedAnalytesSubAnalytes") {

                        $(".testmaster:visible").next().show().prev().hide();
                    }
                    else if ($(this).attr('id') == "btnLoadAnalyteSubAnalyteSMR") {

                        if ($("#hiddenAnalyteSMR").val() != "" || $("#hiddenSubAnalyteSMR").val() != "") {
                            $(".testmaster:visible").next().show().prev().hide();
                        }
                        else {
                            alert("Select specimen and method for analytes or subanalytes selected");
                        }
                    }
                }
                else {
                    $(".testmaster:visible").hide();
                    $(".testmaster:first").show();
                }
                return false;
            });

            $(".prevbtn").click(function () {
                if ($(".testmaster:visible").prev().length != 0) {
                    $(".testmaster:visible").prev().show().next().hide();
                }
                else {
                    $(".testmaster:visible").hide();
                    $(".testmaster:last").show();
                }
                return false;
            });
        });
    
    </script>
    <!-- SECTION/PROFILE JS-->
    <script type="text/javascript">
        // on section selection load profiles belonging to that section in the profile dropdown list
        function sectionSelected(obj) {

            var sectionId = $(obj).val();

            $('#selProfile').find('option').show();
            $('#selProfile').find('option[value=""]').prop('selected', true);

            $("#selProfile option").each(function (i) {
                if ($(this).val().split('|')[0] != sectionId && $(this).val() != "") {
                    $(this).hide();
                }
            });

            $("#hiddenSectionIdName").val();
        }

        //add new section in the database and refresh section dropdown list to show added section
        function addSection() {

            if ($("#txtSection").val() == "") {
                $("#lblSection").html("Enter section name");
                return false;
            }
            else {

                var parameter = { "sectionName": $("#txtSection").val() };
                $.ajax({
                    type: "POST",
                    url: "TestMaster.aspx/addSection",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {
                        var json = JSON.parse(response.d);
                        //console.log(json);

                        //if error occurs
                        if (json["key"] == 0) {
                            alert("Error occured");

                            /*$("#selProfile option").each(function (i) {
                            if ($(this).val().split('|')[0] != $("#selSection").val() && $(this).val() != "") {
                            $(this).hide();
                            }
                            });*/
                        }
                        //if section already exists
                        else if (json["key"] == 2) {
                            alert("Section already exists");

                            /*$("#selProfile option").each(function (i) {
                            if ($(this).val().split('|')[0] != $("#selSection").val() && $(this).val() != "") {
                            $(this).hide();
                            }
                            });*/
                        }
                        //if section added successfully, reload section dropdown
                        else if (json["key"] == 1) {
                            alert("Section added, please select from list");

                            $('#selSection').empty();

                            $('#selSection').append($('<option>', {
                                value: "",
                                text: "Select Section"
                            }));

                            for (var i = 0; i < json["value"].length; i++) {
                                $('#selSection').append($('<option>', {
                                    value: json["value"][i]["sSectionId"],
                                    text: json["value"][i]["sSectionName"]
                                }));
                            }

                            $("#selProfile option").each(function (i) {
                                if ($(this).val().split('|')[0] != $("#selSection").val() && $(this).val() != "") {
                                    $(this).hide();
                                }
                            });

                            console.log(json["value"]);
                        }

                        //console.log("success" + response.d);
                    },
                    error: function (response) {
                        console.log("error" + response.d);
                    },
                    failure: function (response) {
                        console.log("fail" + response.d);
                    }
                });
            }

            //$(".modal-backdrop").remove();
            $("#modalAddSection").modal('hide');
        }

        // on profile selection ad profileID|Name to hiddenfield
        function profileSelected(obj) {

            var profileId = $(obj).val().split("|")[1];

            $("#hiddenProfileId").val(profileId);
        }

        //add new profile in the database and refresh profile dropdown list to show added profile
        function addProfile() {

            if ($("#txtProfileName").val() == "") {
                $("#lblProfileName").html("Enter profile name");
                return false;
            }
            else if ($("#selSection").val() == "") {
                alert("Section not selected");
                return false;
            }
            else {

                var parameter = { "profileName": $("#txtProfileName").val(), "sectionId": $("#selSection").val() };
                $.ajax({
                    type: "POST",
                    url: "TestMaster.aspx/addProfile",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {
                        var json = JSON.parse(response.d);
                        //console.log(json);

                        //if error occurs
                        if (json["key"] == 0) {
                           // alert("Error occured");

                            /* $("#selProfile option").each(function (i) {
                            if ($(this).val().split('|')[0] != $("#selSection").val() && $(this).val() != "") {
                            $(this).hide();
                            }
                            });*/
                        }
                        //if profile already exists
                        else if (json["key"] == 2) {
                            alert("Profile already exists");

                            /*$("#selProfile option").each(function (i) {                        
                            if ($(this).val().split('|')[0] != $("#selSection").val() && $(this).val() != "") {
                            $(this).hide();
                            }
                            });*/

                        }
                        //if profile added successfully, reload section dropdown
                        else if (json["key"] == 1) {
                            alert("Profile added, please select from list");

                            $('#selProfile').empty();

                            $('#selProfile').append($('<option>', {
                                value: "",
                                text: "Select Profile"
                            }));

                            for (var i = 0; i < json["value"].length; i++) {
                                $('#selProfile').append($('<option>', {
                                    value: json["value"][i]["sSectionId"] + "|" + json["value"][i]["sTestProfileId"],
                                    text: json["value"][i]["sProfileName"]
                                }));
                            }

                            $("#selProfile option").each(function (i) {
                                if ($(this).val().split('|')[0] != $("#selSection").val() && $(this).val() != "") {
                                    $(this).hide();
                                }
                            });

                            console.log(json["value"]);
                        }

                        //console.log("success" + response.d);
                    },
                    error: function (response) {
                        console.log("error" + response.d);
                    },
                    failure: function (response) {
                        console.log("fail" + response.d);
                    }
                });
            }

            //$(".modal-backdrop").remove();
            $("#modalAddProfile").modal('hide');
        }
    </script>
    <!-- ANALYTE JS-->
    <script type="text/javascript">

        var analyteIdsNames = new Array();
        var analyteIds = new Array();

        //when user checks analytes from the list, add their id's into hiddenfield
        function analyteSelected(obj) {

            if ($(obj).is(':checked')) {
                analyteIdsNames.push($(obj).val() + "|" + $(obj).attr('text'));
                analyteIds.push($(obj).val());
            }
            else {
                analyteIdsNames.splice(analyteIdsNames.indexOf($(obj).val() + "|" + $(obj).attr('text')), 1);
                analyteIds.splice(analyteIds.indexOf($(obj).val()), 1);
            }
            $("#hiddenAnalyteIdName").val(analyteIdsNames.join('$'));
            $("#hiddenAnalyteId").val(analyteIds.join(','));

            $("#linkAddSubAnalyte").hide();
            $("#tbodySubAnalyteList").html("");
        }


        $("#txtSearchAnalyte").keyup(function () {
            var input, filter, table, tr, td, i, txtValue;
            input = document.getElementById("txtSearchAnalyte");
            filter = input.value.toUpperCase();
            table = document.getElementById("tabAnalyteList");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[2];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        });


        //add new analyte and append it to the displayed analyte list
        function addAnalyte() {

            var tableContent = $("#tbodyAnalyteList").html();

            if ($("#txtAnalyteName").val() == "") {
                $("#lblAnalyteName").html("Enter analyte name");
                return false;
            }
            else {

                // if entered analyte already exists, then return false without entering ajax block
                filter = $("#txtAnalyteName").val().toUpperCase();
                table = document.getElementById("tbodyAnalyteList");
                tr = table.getElementsByTagName("tr");

                for (i = 0; i < tr.length; i++) {
                    td = tr[i].getElementsByTagName("td")[2];

                    if (td) {
                        var checkdupstring = td.innerHTML.toUpperCase().localeCompare(filter);
                        if (checkdupstring = 0) {
                            $("#txtAnalyteName").val("");
                            $("#lblAnalyteName").html("Analyte already exists");
                            return false;
                        }
                    }
                }


                var parameter = { "analyteName": $("#txtAnalyteName").val() };
                $.ajax({
                    type: "POST",
                    url: "TestMaster.aspx/addAnalyte",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {
                        var json = JSON.parse(response.d);
                        //console.log(json);

                        //if error occurs
                        if (json["key"] == 0) {
                            alert("Error occured");

                        }
                        //if analyte already exists
                        else if (json["key"] == 2) {
                            alert("Analyte already exists");
                        }
                        //if analyte added successfully, reload section dropdown
                        else if (json["key"] == 1) {
                            alert("Analyte added, please select from list");

                            //$("#tbodyAnalyteList").html(tableContent);

                            for (var i = 0; i < json["value"].length; i++) {
                                var record = "<tr id='rowAnalyte" + json["value"][i]["sAnalyteId"] + "'>" +
                                       "<td scope='col'><input type='checkbox' value='" + json["value"][i]["sAnalyteId"] + "' id='chkAnalyte" + json["value"][i]["sAnalyteId"] + "' text='" + json["value"][i]["sAnalyteName"] + "' name='chkAnalyte' clientidmode='Static' onchange='javascript:analyteSelected(this)' ></td>" +
                                       "<td scope='col'>" + json["value"][i]["sAnalyteId"] + "</td>" +
                                       "<td scope='col'>" + json["value"][i]["sAnalyteName"] + "</td>" +
                                    "</tr>"

                                $("#tbodyAnalyteList").append(record);
                            }

                            // console.log(json["value"]);
                        }

                        console.log("success" + response.d);
                    },
                    error: function (response) {
                        console.log("error" + response.d);
                    },
                    failure: function (response) {
                        console.log("fail" + response.d);
                    }
                });
            }

            $("#txtAnalyteName").val("");
            //$(".modal-backdrop").remove();
            $("#modalAddAnalyte").modal('hide');
        }

        //if analytes are selected, load sub analytes of those analytes
        function loadSubAnalytesOfAnalytes() {
            if ($("#hiddenAnalyteId").val() == "") {
                //alert("select analytes first");
            }
            else {

                if ($("#hiddenAnalyteId").val() != "") {
                    $("#linkAddSubAnalyte").show();
                }
                else {
                    $("#linkAddSubAnalyte").hide();
                }

                //refresh $("#hiddenSubAnalyteId").val() and $("#hiddenSubAnalyteIdName").val() on load subanalytes
                $("#hiddenSubAnalyteId").val("");
                $("#hiddenSubAnalyteIdName").val("");

                //load analyte dropdown in addSubAnalyteModal
                var analyteIdName = $("#hiddenAnalyteIdName").val().split('$');

                //select analyte dropdown in add subanalyte modal
                $('#selAnalyteName').empty();

                for (var i = 0; i < analyteIdName.length; i++) {

                    var analyteId = analyteIdName[i].split('|')[0];
                    var analyteName = analyteIdName[i].split('|')[1];

                    $('#selAnalyteName').append('<option value="' + analyteId + '">' + analyteName + '</option>');
                }

                var parameter = { "analyteIds": $("#hiddenAnalyteId").val() };
                $.ajax({
                    type: "POST",
                    url: "TestMaster.aspx/loadSubAnalyte",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {
                        var json = JSON.parse(response.d);
                        //console.log(json);

                        //if error occurs
                        if (json["key"] == 0) {
                            alert("Error occured");

                        }
                        //if subanalyte exists
                        else if (json["key"] == 1) {

                            $("#tbodySubAnalyteList").html("");
                            for (var i = 0; i < json["value"].length; i++) {

                                var record = "<tr id='rowSubAnalyte" + json["value"][i]["sSubAnalyteId"] + "'>" +
                                    "<td scope='col'><input type='checkbox' value='" + json["value"][i]["sSubAnalyteId"] + "' id='chkSubAnalyte" + json["value"][i]["sSubAnalyteId"] + "' text='" + json["value"][i]["sSubAnalyteName"] + "|" + json["value"][i]["sAnalyteName"] + "' name='chkSubAnalyte' clientidmode='Static' onchange='javascript:subAnalyteSelected(this)' ></td>" +
                                    "<td scope='col'>" + json["value"][i]["sSubAnalyteId"] + "</td>" +
                                    "<td scope='col'>" + json["value"][i]["sSubAnalyteName"] + "</td>" +
                                    "<td scope='col'>" + json["value"][i]["sAnalyteName"] + "</td>" +
                                "</tr>"

                                $("#tbodySubAnalyteList").append(record);
                            }

                            // console.log(json["value"]);
                        }

                        //console.log("success" + response.d);
                    },
                    error: function (response) {
                        console.log("error" + response.d);
                    },
                    failure: function (response) {
                        console.log("fail" + response.d);
                    }
                });
            }
        }

    </script>
    <!-- SUB ANALYTE JS-->
    <script type="text/javascript">

        //when user checks analytes from the list, add their id's into hiddenfield
        function subAnalyteSelected(obj) {

            // if subanalytes already exists, they should not be re-added
            var subAnalyteIdsNames = ($("#hiddenSubAnalyteIdName").val() != "") ? $("#hiddenSubAnalyteIdName").val().split('$') : new Array();
            var subAnalyteIds = ($("#hiddenSubAnalyteId").val() != "") ? $("#hiddenSubAnalyteId").val().split(',') : new Array();

            if ($(obj).is(':checked')) {
                if (subAnalyteIdsNames.indexOf($(obj).val() + "|" + $(obj).attr('text')) == -1) {
                    subAnalyteIdsNames.push($(obj).val() + "|" + $(obj).attr('text'));
                }
                if (subAnalyteIds.indexOf($(obj).val()) == -1) {
                    subAnalyteIds.push($(obj).val());
                }
            }
            else {
                subAnalyteIdsNames.splice(subAnalyteIdsNames.indexOf($(obj).val() + "|" + $(obj).attr('text')), 1);
                subAnalyteIds.splice(analyteIds.indexOf($(obj).val()), 1);
            }
            $("#hiddenSubAnalyteIdName").val(subAnalyteIdsNames.join('$'));
            $("#hiddenSubAnalyteId").val(subAnalyteIds.join(','));

            console.log(subAnalyteIdsNames);
            console.log(subAnalyteIds);
            //console.log($("#hiddenSubAnalyteIdName").val());
            //console.log($("#hiddenSubAnalyteId").val());

            $("#tbodySpecimenMethod").html("");
        }

        // search sub analyte by name,
        $("#txtSearchAnalyte").keyup(function () {
            var input, filter, table, tr, td, i, txtValue;
            input = document.getElementById("txtSearchAnalyte");
            filter = input.value.toUpperCase();
            table = document.getElementById("tabAnalyteList");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[2];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        });


        $("#txtSearchSubAnalyte").keyup(function () {

            var input, filter, table, tr, td, i, txtVal;
            input = document.getElementById("txtSearchSubAnalyte");
            filter = input.value.toUpperCase();
            table = document.getElementById("tabSubAnalyteList");
            tr = table.getElementsByTagName("tr");

            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[2];

                if (td) {
                    txtVal = td.textContent || td.innerText;
                    if (txtVal.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
//                    var checkdupstring = td.innerHTML.toUpperCase().localeCompare(filter);
//                    if (checkdupstring = 0) {
//                        tr[i].style.display = "";
//                    } else {
//                        tr[i].style.display = "";
//                    }
                }
            }
        });

        //add new sub analyte and append it to the displayed analyte list
        function addSubAnalyte() {

            var tableContent = $("#tbodySubAnalyteList").html();

            if ($("#txtSubAnalyteName").val() == "") {
                $("#lblSubAnalyteName").html("Enter sub analyte name");
                return false;
            }
            else {

                // if entered sub analyte already exists, then return false without entering ajax block
                filter = $("#txtSubAnalyteName").val().toUpperCase();
                table = document.getElementById("tbodySubAnalyteList");
                tr = table.getElementsByTagName("tr");

                for (i = 0; i < tr.length; i++) {
                    td = tr[i].getElementsByTagName("td")[2];

                    if (td) {
                        var checkdupstring = td.innerHTML.toUpperCase().localeCompare(filter);
                        if (checkdupstring = 0) {
                            $("#txtSubAnalyteName").val("");
                            $("#lblSubAnalyteName").html("Sub Analyte already exists");
                            return false;
                        }
                    }
                }

                var parameter = { "subAnalyteName": $("#txtSubAnalyteName").val(), "analyteId": $("#selAnalyteName").val() };
                $.ajax({
                    type: "POST",
                    url: "TestMaster.aspx/addSubAnalyte",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {
                        var json = JSON.parse(response.d);
                        //console.log(json);

                        //if error occurs
                        if (json["key"] == 0) {
                            alert("Error occured");

                        }
                        //if analyte already exists
                        else if (json["key"] == 2) {
                            alert("Sub Analyte already exists");
                        }
                        //if analyte added successfully, reload section dropdown
                        else if (json["key"] == 1) {
                            alert("Sub Analyte added, please select from list");

                            //$("#tbodySubAnalyteList").html(tableContent);

                            for (var i = 0; i < json["value"].length; i++) {
                                var record = "<tr id='rowSubAnalyte" + json["value"][i]["sSubAnalyteId"] + "'>" +
                                       "<td scope='col'><input type='checkbox' value='" + json["value"][i]["sSubAnalyteId"] + "' id='chkSubAnalyte" + json["value"][i]["sSubAnalyteId"] + "' text='" + json["value"][i]["sSubAnalyteName"] + "|" + json["value"][i]["sAnalyteName"] + "' name='chkSubAnalyte' clientidmode='Static' onchange='javascript:subAnalyteSelected(this)' ></td>" +
                                       "<td scope='col'>" + json["value"][i]["sSubAnalyteId"] + "</td>" +
                                       "<td scope='col'>" + json["value"][i]["sSubAnalyteName"] + "</td>" +
                                       "<td scope='col'>" + json["value"][i]["sAnalyteName"] + "</td>" +
                                    "</tr>"

                                $("#tbodySubAnalyteList").append(record);
                            }

                            // console.log(json["value"]);
                        }

                        console.log("success" + response.d);
                    },
                    error: function (response) {
                        console.log("error" + response.d);
                    },
                    failure: function (response) {
                        console.log("fail" + response.d);
                    }
                });
            }

            $("#txtSubAnalyteName").val("");
            //$(".modal-backdrop").remove();
            $("#modalAddSubAnalyte").modal('hide');
        }

    </script>
    <!-- SPECIMEN/METHOD JS-->
    <script type="text/javascript">

        function loadSelectedAnalytesSubAnalytes() {

            $("#tbodySpecimenMethod").html("");

            //if selected analyte/subanalyte list updated, clear hiddenAnalyteSMR and hiddenSubAnalyteSMR
            $("#hiddenAnalyteSMR").val("");
            $("#hiddenSubAnalyteSMR").val("");

            //hiddenAnalyteIdName contains the ',' separated analyteID|analyteName checked in analyte list       
            if ($("#hiddenAnalyteIdName").val() != "") {
                var analytes = $("#hiddenAnalyteIdName").val().split('$');
                var subAnalytes = $("#hiddenSubAnalyteIdName").val().split('$');

                for (var i = 0; i < analytes.length; i++) {
                    var record = "<tr>" +
                                "<td>" + analytes[i].split('|')[1] + "</td>" +
                                "<td></td>" +
                                "<td id='tdAnSpecimenMethod" + analytes[i].split('|')[0] + "'></td>" +
                                "<td><input type='button' id='btnAnalyte|" + analytes[i].split('|')[0] + "|" + analytes[i].split('|')[1] + "' value='Add Specimen Method' onclick='javascript:openAddSpecimenMethodModal(this)' class='btn btn-color white-txt'></td>" +
                         "</tr>";
                    $("#tbodySpecimenMethod").append(record);
                }

                //hiddenSubAnalyteIdName contains the ',' separated subAnalyteID|subAnalyteName checked in the sub analyte list
                if ($("#hiddenSubAnalyteIdName").val() != "") {
                    for (var i = 0; i < subAnalytes.length; i++) {
                        var record = "<tr>" +
                                "<td>" + subAnalytes[i].split('|')[2] + "</td>" +
                                "<td>" + subAnalytes[i].split('|')[1] + "</td>" +
                                "<td id='tdSubAnSpecimenMethod" + subAnalytes[i].split('|')[0] + "'></td>" +
                                "<td><input type='button' id='btnSubAnalyte|" + subAnalytes[i].split('|')[0] + "|" + subAnalytes[i].split('|')[1] + "|" + subAnalytes[i].split('|')[2] + "' value='Add Specimen Method' onclick='javascript:openAddSpecimenMethodModal(this)' class='btn btn-color white-txt'></td>" +
                         "</tr>";
                        $("#tbodySpecimenMethod").append(record);
                    }
                }
            }
            else {
                alert("select analytes first");
            }
        }

        function openAddSpecimenMethodModal(obj) {

            //hiddenAnalyteSMIdName contains analyteId|analyteName for which specimen method is to be added, on add specimen method button click, analyteId|analyteName is assigne to it
            $("#hiddenAnalyteSMIdName").val("");

            //hiddenSubAnalyteSMIdName contains subAnalyteId|subAnalyteName for which specimen method is to be added, on add specimen method button click, subAnalyteId|subAnalyteName is assigne to it
            $("#hiddenSubAnalyteSMIdName").val("");

            //if add specimen method button is clicked for analyte, assign id|name to hiddenanalyteSMIdName 
            if ($(obj).attr('id').split('|')[0] == "btnAnalyte") {
                var analyteIdName = ($(obj).attr('id').split('|')[0] == "btnAnalyte") ? $(obj).attr('id').split('|')[1] + "|" + $(obj).attr('id').split('|')[2] : "";
                $("#hiddenAnalyteSMIdName").val(analyteIdName);
            }
            //if add specimen method button is clicked for subAnalyte, assign id|name to hiddenSubAnalyteSMIdName 
            else if ($(obj).attr('id').split('|')[0] == "btnSubAnalyte") {
                var subAnalyteIdName = ($(obj).attr('id').split('|')[0] == "btnSubAnalyte") ? $(obj).attr('id').split('|')[1] + "|" + $(obj).attr('id').split('|')[2] + "|" + $(obj).attr('id').split('|')[3] : "";
                $("#hiddenSubAnalyteSMIdName").val(subAnalyteIdName);
            }

            //alert("[Analyte Id : " + $("#hiddenAnalyteSMIdName").val().split('|')[0] + ",Analyte Name : " + $("#hiddenAnalyteSMIdName").val().split('|')[1] + " ][ " + "SubAnalyte Id : " + $("#hiddenSubAnalyteSMIdName").val().split('|')[0] + ", " + "SubAnalyte Name : " + $("#hiddenSubAnalyteSMIdName").val().split('|')[1]+"]");

            $("#modalAddSpecimenMethod").modal("show");
        }

        function addSpecimenMethod() {

            if ($("#txtSpecimenName").val() == "") {
                $("#lblSpecimenName").html("Specimen name required");
                return false;
            }
            else if ($("#txtQuantity").val() == "") {
                $("#lblQuantity").html("Quantity required");
                return false;
            }
            else if ($("#txtTimePeriod").val() == "") {
                $("#lblTimePeriod").html("Time period required");
                return false;
            }
            else if ($("#selResultType").val() == "") {
                $("#lblResultType").html("Result type required");
                return false;
            }

            var sampleName = $("#txtSpecimenName").val();
            var quantity = $("#txtQuantity").val();
            var timePeriod = $("#txtTimePeriod").val();
            var method = $("#txtMethod").val();
            var resultType = $("#selResultType").val();

            var parameter = { "sampleType": sampleName, "quantity": quantity, "timePeriod": timePeriod, "method": method };
            $.ajax({
                type: "POST",
                url: "TestMaster.aspx/addSpecimenMethod",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                //dataType: "text/plain",
                success: function (response) {
                    var json = JSON.parse(response.d);
                    //console.log(json);

                    //if error occurs
                    if (json["key"] == 0) {
                        alert("Error occured");

                    }
                    //if specimen and method added successfully
                    else if (json["key"] == 1) {
                        //alert("Specimen and method added");

                        var specimenId = "";
                        var specimenName = "";
                        var quantity = "";
                        var timePeriod = "";
                        var methodId = "";
                        var methodName = "";
                        var resultType = $("#selResultType").val();


                        if (json["specimen"] != null) {
                            specimenId = json["specimen"][0]["sSpecimenId"];
                            specimenName = json["specimen"][0]["sSampleType"];
                            quantity = json["specimen"][0]["sQuantity"];
                            timePeriod = json["specimen"][0]["sTimePeriod"];
                        }

                        if (json["method"] != null) {
                            methodId = json["method"][0]["sMethodId"];
                            methodName = json["method"][0]["sMethodName"];
                        }

                        //if add specimen method button is clicked for analyte, hiddenAnalyteSMIdName will not be null
                        if ($("#hiddenAnalyteSMIdName").val() != "") {

                            var id = $("#hiddenAnalyteSMIdName").val().split("|")[0];
                            var name = $("#hiddenAnalyteSMIdName").val().split("|")[1];

                            var tdAnSpecimenMethodContent = "<p>" + specimenName + " " + quantity + " " + timePeriod + ", " + methodName + ", " + resultType + " <a href='javascript:void(0)' id='analyte*" + id + "|" + specimenId + "|" + methodId + "' onclick='javascript:removeSpecimenMethod(this)'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></p>";

                            var analyteSMR = [{ "analyteId": id, "analyteName": name, "specimenId": specimenId, "specimenName": specimenName, "quantity": quantity, "timePeriod": timePeriod, "methodId": methodId, "methodName": methodName, "resultType": resultType}];

                            //hiddenAnalyteSMR contains list of single/multiple specimen method combinations for each selected analyte
                            if ($("#hiddenAnalyteSMR").val() != "") {
                                var hiddenAnalyteSMR = JSON.parse($("#hiddenAnalyteSMR").val());
                                var analyteSMExists = false;

                                for (var i = 0; i < hiddenAnalyteSMR.length; i++) {
                                    if (hiddenAnalyteSMR[i]["analyteId"] == analyteSMR[0]["analyteId"] && hiddenAnalyteSMR[i]["specimenId"] == analyteSMR[0]["specimenId"] && hiddenAnalyteSMR[i]["methodId"] == analyteSMR[0]["methodId"]) {
                                        analyteSMExists = true;
                                    }
                                }

                                if (analyteSMExists == false) {
                                    hiddenAnalyteSMR[hiddenAnalyteSMR.length] = analyteSMR[0];

                                    $("#tdAnSpecimenMethod" + id).append(tdAnSpecimenMethodContent);
                                }

                                $("#hiddenAnalyteSMR").val(JSON.stringify(hiddenAnalyteSMR));

                            }
                            else {
                                $("#hiddenAnalyteSMR").val(JSON.stringify(analyteSMR));

                                $("#tdAnSpecimenMethod" + id).append(tdAnSpecimenMethodContent);
                            }

                            //console.log($("#hiddenAnalyteSMR").val());
                        }
                        //if add specimen method button is clicked for subAnalyte, hiddenSubAnalyteSMIdName will not be null
                        else if ($("#hiddenSubAnalyteSMIdName").val() != "") {

                            var id = $("#hiddenSubAnalyteSMIdName").val().split("|")[0];
                            var name = $("#hiddenSubAnalyteSMIdName").val().split("|")[1];
                            var analyteName = $("#hiddenSubAnalyteSMIdName").val().split("|")[2];

                            var tdSubAnSpecimenMethodContent = "<p>" + specimenName + " " + quantity + " " + timePeriod + ", " + methodName + ", " + resultType + " <a href='javascript:void(0)' id='subAnalyte*" + id + "|" + specimenId + "|" + methodId + "' onclick='javascript:removeSpecimenMethod(this)'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></p>";

                            var subAnalyteSMR = [{ "subAnalyteId": id, "subAnalyteName": name, "analyteName": analyteName, "specimenId": specimenId, "specimenName": specimenName, "quantity": quantity, "timePeriod": timePeriod, "methodId": methodId, "methodName": methodName, "resultType": resultType}];

                            //hiddenSubAnalyteSMR contains list of single/multiple specimen method combinations for each selected subAnalyte
                            if ($("#hiddenSubAnalyteSMR").val() != "") {
                                var hiddenSubAnalyteSMR = JSON.parse($("#hiddenSubAnalyteSMR").val());
                                var subAnalyteSMExists = false;

                                for (var i = 0; i < hiddenSubAnalyteSMR.length; i++) {
                                    if (hiddenSubAnalyteSMR[i]["subAnalyteId"] == subAnalyteSMR[0]["subAnalyteId"] && hiddenSubAnalyteSMR[i]["specimenId"] == subAnalyteSMR[0]["specimenId"] && hiddenSubAnalyteSMR[i]["methodId"] == subAnalyteSMR[0]["methodId"]) {
                                        subAnalyteSMExists = true;
                                    }
                                }

                                if (subAnalyteSMExists == false) {
                                    hiddenSubAnalyteSMR[hiddenSubAnalyteSMR.length] = subAnalyteSMR[0];

                                    $("#tdSubAnSpecimenMethod" + id).append(tdSubAnSpecimenMethodContent);
                                }

                                $("#hiddenSubAnalyteSMR").val(JSON.stringify(hiddenSubAnalyteSMR));

                            }
                            else {
                                $("#hiddenSubAnalyteSMR").val(JSON.stringify(subAnalyteSMR));

                                $("#tdSubAnSpecimenMethod" + id).append(tdSubAnSpecimenMethodContent);
                            }

                            //console.log($("#hiddenSubAnalyteSMR").val());
                        }

                        // console.log(json["value"]);
                    }

                    //console.log("success" + response.d);
                },
                error: function (response) {
                    console.log("error" + response.d);
                },
                failure: function (response) {
                    console.log("fail" + response.d);
                }
            });

            $("#modalAddSpecimenMethod").modal('hide');
        }

        function removeSpecimenMethod(obj) {

            var removeFrom = ($(obj).attr('id').split("*")[0] == "analyte") ? "analyte" : "subAnalyte";
            var ids = $(obj).attr('id').split("*")[1];
            var analyteId = ids.split("|")[0];
            var specimenId = ids.split("|")[1];
            var methodId = ids.split("|")[2];

            console.log(removeFrom + analyteId + specimenId + methodId);

            if (removeFrom == "analyte") {
                var hiddenAnalyteSMR = JSON.parse($("#hiddenAnalyteSMR").val());
                var analyteSMExists = false;

                //console.log(JSON.stringify(hiddenAnalyteSMR));

                var removeIndex = "";

                for (var i = 0; i < hiddenAnalyteSMR.length; i++) {
                    if (hiddenAnalyteSMR[i]["analyteId"] == analyteId && hiddenAnalyteSMR[i]["specimenId"] == specimenId && hiddenAnalyteSMR[i]["methodId"] == methodId) {
                        analyteSMExists = true;
                        removeIndex = i;
                    }
                }

                if (analyteSMExists == true) {
                    //console.log(removeIndex);

                    hiddenAnalyteSMR.splice(removeIndex, 1);
                    var stringHiddenAnalyteSMR = (hiddenAnalyteSMR.length > 0) ? JSON.stringify(hiddenAnalyteSMR) : "";

                    $("#hiddenAnalyteSMR").val(stringHiddenAnalyteSMR);
                    $(obj).closest('p').remove();
                    //console.log($("#hiddenAnalyteSMR").val());
                }
            }
            else if (removeFrom == "subAnalyte") {
                var hiddenSubAnalyteSMR = JSON.parse($("#hiddenSubAnalyteSMR").val());
                var subAnalyteSMExists = false;

                //console.log(JSON.stringify(hiddenSubAnalyteSMR));

                var removeIndex = "";

                //here analyteId is subAnalyteId
                for (var i = 0; i < hiddenSubAnalyteSMR.length; i++) {
                    if (hiddenSubAnalyteSMR[i]["subAnalyteId"] == analyteId && hiddenSubAnalyteSMR[i]["specimenId"] == specimenId && hiddenSubAnalyteSMR[i]["methodId"] == methodId) {
                        subAnalyteSMExists = true;
                        removeIndex = i;
                    }
                }

                if (subAnalyteSMExists == true) {
                    //console.log(removeIndex);

                    hiddenSubAnalyteSMR.splice(removeIndex, 1);
                    var stringHiddenSubAnalyteSMR = (hiddenSubAnalyteSMR.length > 0) ? JSON.stringify(hiddenSubAnalyteSMR) : "";

                    $("#hiddenSubAnalyteSMR").val(stringHiddenSubAnalyteSMR);
                    $(obj).closest('p').remove();
                    // console.log($("#hiddenSubAnalyteSMR").val());
                }
            }
        }

    
    </script>
    <!-- REFERENCE VALUES JS-->
    <script type="text/javascript">
        function loadAnalyteSubAnalyteSMR() {

            //hiddenAnalyteSMR contains list of single/multiple specimen method combinations for each selected analyte
            //hiddenSubAnalyteSMR contains list of single/multiple specimen method combinations for each selected subAnalyte

            //clear hiddenAnalyteSMRRefVal/hiddenSubAnalyteSMRRefVal on every next button clicked for analyte/subanalyte specimen method section
            $("#hiddenAnalyteSMRRefVal").val("");
            $("#hiddenSubAnalyteSMRRefVal").val("");

            if ($("#hiddenAnalyteSMR").val() == "" && $("#hiddenSubAnalyteSMR").val() == "") {
                // alert("Select specimen and method for analytes and sub analytes");
            }
            else {
                $("#tbodyReferenceValues").html("");

                if ($("#hiddenAnalyteSMR").val() != "") {
                    //console.log($("#hiddenAnalyteSMR").val());
                    //console.log($("#hiddenSubAnalyteSMR").val());

                    //add RefVal key in hiddenAnalyteSMRRefVal after loading it with analyte-specimen-method combination
                    $("#hiddenAnalyteSMRRefVal").val($("#hiddenAnalyteSMR").val());
                    var hiddenAnalyteSMRRefVal = JSON.parse($("#hiddenAnalyteSMRRefVal").val());

                    for (var i = 0; i < hiddenAnalyteSMRRefVal.length; i++) {

                        hiddenAnalyteSMRRefVal[i]["RefVal"] = [];
                    }
                    $("#hiddenAnalyteSMRRefVal").val(JSON.stringify(hiddenAnalyteSMRRefVal));
                    var analytesSMR = JSON.parse($("#hiddenAnalyteSMR").val());

                    for (var i = 0; i < analytesSMR.length; i++) {
                        var record = "<tr>" +
                                "<td>" + analytesSMR[i]["analyteName"] + "</td>" +
                                "<td></td>" +
                                "<td>" + analytesSMR[i]["specimenName"] + " " + analytesSMR[i]["quantity"] + " " + analytesSMR[i]["timePeriod"] + ", " + analytesSMR[i]["methodName"] + "</td>" +
                                "<td>" + analytesSMR[i]["resultType"] + "</td>" +
                                "<td id='tdAnRefVal" + i + "'></td>" +
                                "<td><input type='button' id='btnAnalyte|" + i + "' value='Add Reference Values' onclick='javascript:openAddReferenceValuesModal(this)' class='btn btn-color white-txt'></td>" +
                         "</tr>";
                        $("#tbodyReferenceValues").append(record);
                    }
                }
                if ($("#hiddenSubAnalyteSMR").val() != "") {

                    //add RefVal key in hiddenSubAnalyteSMRRefVal after loading it with analyte-specimen-method combination
                    $("#hiddenSubAnalyteSMRRefVal").val($("#hiddenSubAnalyteSMR").val());
                    var hiddenSubAnalyteSMRRefVal = JSON.parse($("#hiddenSubAnalyteSMRRefVal").val());
                    for (var i = 0; i < hiddenSubAnalyteSMRRefVal.length; i++) {

                        hiddenSubAnalyteSMRRefVal[i]["RefVal"] = [];
                    }
                    $("#hiddenSubAnalyteSMRRefVal").val(JSON.stringify(hiddenSubAnalyteSMRRefVal));
                    var subAnalytesSMR = JSON.parse($("#hiddenSubAnalyteSMR").val());

                    for (var i = 0; i < subAnalytesSMR.length; i++) {
                        var record = "<tr>" +
                                "<td>" + subAnalytesSMR[i]["analyteName"] + "</td>" +
                                "<td>" + subAnalytesSMR[i]["subAnalyteName"] + "</td>" +
                                "<td>" + subAnalytesSMR[i]["specimenName"] + " " + subAnalytesSMR[i]["quantity"] + " " + subAnalytesSMR[i]["timePeriod"] + ", " + subAnalytesSMR[i]["methodName"] + "</td>" +
                                "<td>" + subAnalytesSMR[i]["resultType"] + "</td>" +
                                "<td id='tdSubAnRefVal" + i + "'></td>" +
                                "<td><input type='button' id='btnSubAnalyte|" + i + "' value='Add Reference Values' onclick='javascript:openAddReferenceValuesModal(this)' class='btn btn-color white-txt'></td>" +
                         "</tr>";
                        $("#tbodyReferenceValues").append(record);
                    }
                }
            }
        }
        function openAddReferenceValuesModal(obj) {

            //hiddenSelectedAnalyteSMR contains index of analyte-specimen-method in hiddenAnalyteSMR for ref vals. to be added in respective ref val cell on add ref val button click
            $("#hiddenSelectedAnalyteSMR").val("");

            //hiddenSelectedSubAnalyteSMR contains index of selected subanalyte-specimen-method in hiddenSubAnalyteSMR for ref vals. to be added in respective ref val cell on add ref val button click
            $("#hiddenSelectedSubAnalyteSMR").val("");

            if ($(obj).attr('id').split('|')[0] == "btnAnalyte") {
                var analyteSMRIndex = ($(obj).attr('id').split('|')[0] == "btnAnalyte") ? $(obj).attr('id').split('|')[1] : "";
                $("#hiddenSelectedAnalyteSMR").val(analyteSMRIndex);
            }
            else if ($(obj).attr('id').split('|')[0] == "btnSubAnalyte") {
                var subAnalyteSMRIndex = ($(obj).attr('id').split('|')[0] == "btnSubAnalyte") ? $(obj).attr('id').split('|')[1] : "";
                $("#hiddenSelectedSubAnalyteSMR").val(subAnalyteSMRIndex);
            }
            //alert($("#hiddenSelectedAnalyteSMR").val() + " : " + $("#hiddenSelectedSubAnalyteSMR").val());
            $("#modalAddReferenceValues").modal('show');
        }

        function addReferenceValues() {


            if (validateReferenceValues()) {

                if ($("#txtMinMaleValue").val() == "" && $("#txtMaxMaleValue").val() == "") {
                    $("#txtMaleRange").val("");
                }
                else {
                    $("#txtMaleRange").val($("#txtMinMaleValue").val() + "-" + $("#txtMaxMaleValue").val());
                }

                if ($("#txtMinFemaleValue").val() == "" && $("#txtMaxFemaleValue").val() == "") {
                    $("#txtFemaleRange").val();
                }
                else {
                    $("#txtFemaleRange").val($("#txtMinFemaleValue").val() + "-" + $("#txtMaxFemaleValue").val());
                }

                var refType = $("#selReferenceType").val();

                var MaleFromAge = ($("#txtMaleMinAge").val() != "") ? $("#txtMaleMinAge").val() : "0";
                var MaleToAge = ($("#txtMaleMaxAge").val() != "") ? $("#txtMaleMaxAge").val() : "0";
                var MaleAgeUnit = $("#selMaleAgeUnit").val();

                var FemaleFromAge = ($("#txtFemaleMinAge").val() != "") ? $("#txtFemaleMinAge").val() : "0";
                var FemaleToAge = ($("#txtFemaleMaxAge").val() != "") ? $("#txtFemaleMaxAge").val() : "0";
                var FemaleAgeUnit = $("#selFemaleAgeUnit").val();

                var male = ($("#txtMaleRange").val() != "") ? $("#txtMaleRange").val() : "NA";
                var female = ($("#txtFemaleRange").val() != "") ? $("#txtFemaleRange").val() : "NA";
                var grade = ($("#txtGrade").val() != "") ? $("#txtGrade").val() : "NA";
                var units = ($("#txtUnits").val() != "") ? $("#txtUnits").val() : "NA";
                var interpretation = ($("#txtInterpretation").val() != "") ? $("#txtInterpretation").val() : "NA";
                var lowerLimit = ($("#txtLowerLimit").val() != "") ? $("#txtLowerLimit").val() : "NA";
                var upperLimit = ($("#txtUpperLimit").val() != "") ? $("#txtUpperLimit").val() : "NA";

                //if analyte-specimen-method is selected to add ref vals for
                if ($("#hiddenSelectedAnalyteSMR").val() != "") {

                    var analyteSMRIndex = $("#hiddenSelectedAnalyteSMR").val();

                    //alert("analyteSMRIndex : "+analyteSMRIndex);

                    var removeValues = refType + "|" + MaleFromAge + "|" + MaleToAge + "|" + MaleAgeUnit + "|" + FemaleFromAge + "|" + FemaleToAge + "|" + FemaleAgeUnit + "|" + male + "|" + female + "|" + grade + "|" + units + "|" + interpretation + "|" + lowerLimit + "|" + upperLimit;
                    var tdAnRefValContent = "<p>" + refType + "|" + MaleFromAge + "|" + MaleToAge + "|" + MaleAgeUnit + "|" + FemaleFromAge + "|" + FemaleToAge + "|" + FemaleAgeUnit + "|" + male + " | " + female + " | " + grade + " | " + units + " | " + interpretation + " | " + lowerLimit + " | " + upperLimit + " <a href='javascript:void(0)' id='analyte*" + analyteSMRIndex + "~" + removeValues + "' onclick='javascript:removeReferenceValue(this)'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></p>";

                    var analyteSMRRefVal = [{ "refType": refType, "MaleFromAge": MaleFromAge, "MaleToAge": MaleToAge, "MaleAgeUnit": MaleAgeUnit, "FemaleFromAge": FemaleFromAge, "FemaleToAge": FemaleToAge, "FemaleAgeUnit": FemaleAgeUnit, "male": male, "female": female, "grade": grade, "units": units, "interpretation": interpretation, "lowerLimit": lowerLimit, "upperLimit": upperLimit}];

                    var hiddenAnalyteSMRRefVal = JSON.parse($("#hiddenAnalyteSMRRefVal").val());
                    var analyteSMRefValCount = hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"].length;

                    //alert("Ref Val count for this a-s-m : " + analyteSMRefValCount);

                    if (analyteSMRefValCount > 0) {

                        var analyteSMRRefValExists = false;

                        for (var i = 0; i < analyteSMRefValCount; i++) {
                            if (hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["refType"] == refType &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["MaleFromAge"] == MaleFromAge &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["MaleToAge"] == MaleToAge &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["MaleAgeUnit"] == MaleAgeUnit &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["FemaleFromAge"] == FemaleFromAge &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["FemaleToAge"] == FemaleToAge &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["FemaleAgeUnit"] == FemaleAgeUnit &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["male"] == male &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["female"] == female &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["grade"] == grade &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["units"] == units &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["interpretation"] == interpretation &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["lowerLimit"] == lowerLimit &&
                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][i]["upperLimit"] == upperLimit
                    ) {
                                analyteSMRRefValExists = true;
                            }
                        }

                        if (analyteSMRRefValExists == false) {
                            hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][analyteSMRefValCount] = analyteSMRRefVal[0]

                            $("#tdAnRefVal" + analyteSMRIndex).append(tdAnRefValContent);
                        }

                        //console.log(JSON.stringify(hiddenAnalyteSMRRefVal));

                        $("#hiddenAnalyteSMRRefVal").val(JSON.stringify(hiddenAnalyteSMRRefVal));

                    }
                    else {

                        hiddenAnalyteSMRRefVal[analyteSMRIndex]["RefVal"][analyteSMRefValCount] = analyteSMRRefVal[0];

                        //console.log(JSON.stringify(hiddenAnalyteSMRRefVal));

                        $("#hiddenAnalyteSMRRefVal").val(JSON.stringify(hiddenAnalyteSMRRefVal));

                        $("#tdAnRefVal" + analyteSMRIndex).append(tdAnRefValContent);
                    }
                }
                //if subanalyte-specimen-method is selected to add ref vals for
                else if ($("#hiddenSelectedSubAnalyteSMR").val() != "") {

                    var subAnalyteSMRIndex = $("#hiddenSelectedSubAnalyteSMR").val();

                    var removeValues = refType + "|" + MaleFromAge + "|" + MaleToAge + "|" + MaleAgeUnit + "|" + FemaleFromAge + "|" + FemaleToAge + "|" + FemaleAgeUnit +
                    "|" + male + "|" + female + "|" + grade + "|" + units + "|" + interpretation + "|" + lowerLimit + "|" + upperLimit;

                    var tdSubAnRefValContent = "<p>" + refType + "|" + MaleFromAge + "|" + MaleToAge + "|" + MaleAgeUnit + "|" + FemaleFromAge +
                    "|" + FemaleToAge + "|" + FemaleAgeUnit + "|" + male + " | " + female + " | " + grade + " | " + units + " | " + interpretation +
                    " | " + lowerLimit + " | " + upperLimit + " <a href='javascript:void(0)' id='analyte*" + subAnalyteSMRIndex + "~"
                     + removeValues + "' onclick='javascript:removeReferenceValue(this)'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></p>";

                    var subAnalyteSMRRefVal = [{ "refType": refType, "MaleFromAge": MaleFromAge, "MaleToAge": MaleToAge, "MaleAgeUnit": MaleAgeUnit,
                        "FemaleFromAge": FemaleFromAge, "FemaleToAge": FemaleToAge, "FemaleAgeUnit": FemaleAgeUnit, "male": male, "female": female, "grade": grade,
                        "units": units, "interpretation": interpretation, "lowerLimit": lowerLimit, "upperLimit": upperLimit
                    }];

                    var hiddenSubAnalyteSMRRefVal = JSON.parse($("#hiddenSubAnalyteSMRRefVal").val());
                    var subAnalyteSMRefValCount = hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"].length;


                    if (subAnalyteSMRefValCount > 0) {

                        var subAnalyteSMRRefValExists = false;

                        for (var i = 0; i < subAnalyteSMRefValCount; i++) {
                            if (hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["refType"] == refType &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["MaleFromAge"] == MaleFromAge &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["MaleToAge"] == MaleToAge &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["MaleAgeUnit"] == MaleAgeUnit &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["FemaleFromAge"] == FemaleFromAge &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["FemaleToAge"] == FemaleToAge &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["FemaleAgeUnit"] == FemaleAgeUnit &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["male"] == male &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["female"] == female &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["grade"] == grade &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["units"] == units &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["interpretation"] == interpretation &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["lowerLimit"] == lowerLimit &&
                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][i]["upperLimit"] == upperLimit
                    ) {
                                subAnalyteSMRRefValExists = true;
                            }
                        }

                        if (subAnalyteSMRRefValExists == false) {
                            hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][subAnalyteSMRefValCount] = subAnalyteSMRRefVal[0]

                            $("#tdSubAnRefVal" + subAnalyteSMRIndex).append(tdSubAnRefValContent);
                        }

                        //console.log(JSON.stringify(hiddenSubAnalyteSMRRefVal));

                        $("#hiddenSubAnalyteSMRRefVal").val(JSON.stringify(hiddenSubAnalyteSMRRefVal));
                    }
                    else {

                        hiddenSubAnalyteSMRRefVal[subAnalyteSMRIndex]["RefVal"][subAnalyteSMRefValCount] = subAnalyteSMRRefVal[0];

                        $("#hiddenSubAnalyteSMRRefVal").val(JSON.stringify(hiddenSubAnalyteSMRRefVal));
                        $("#tdSubAnRefVal" + subAnalyteSMRIndex).append(tdSubAnRefValContent);
                    }
                }
                $("#modalAddReferenceValues").modal('hide');
            }
        }

        function removeReferenceValue(obj) {

            var removeFrom = ($(obj).attr('id').split("*")[0] == "analyte") ? "analyte" : "subAnalyte";
            var data = $(obj).attr('id').split("*")[1];
            var index = data.split("~")[0];
            var values = data.split("~")[1];

            var refType = values.split("|")[0];
            var MaleFromAge = values.split("|")[1];
            var MaleToAge = values.split("|")[2];
            var MaleAgeUnit = values.split("|")[3];
            var FemaleFromAge = values.split("|")[4];
            var FemaleToAge = values.split("|")[5];
            var FemaleAgeUnit = values.split("|")[6];
            var male = values.split("|")[7];
            var female = values.split("|")[8];
            var grade = values.split("|")[9];
            var units = values.split("|")[10];
            var interpretation = values.split("|")[11];
            var lowerLimit = values.split("|")[12];
            var upperLimit = values.split("|")[13];

            if (removeFrom == "analyte") {

                var hiddenAnalyteSMRRefVal = JSON.parse($("#hiddenAnalyteSMRRefVal").val());
                var analyteSMRRefValExists = false;

                var removeIndex = "";

                // console.log(JSON.parse($("#hiddenAnalyteSMRRefVal").val()));

                for (var i = 0; i < hiddenAnalyteSMRRefVal[index]["RefVal"].length; i++) {

                    if (hiddenAnalyteSMRRefVal[index]["RefVal"][i]["refType"] == refType &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["MaleFromAge"] == MaleFromAge &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["MaleToAge"] == MaleToAge &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["MaleAgeUnit"] == MaleAgeUnit &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["FemaleFromAge"] == FemaleFromAge &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["FemaleToAge"] == FemaleToAge &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["FemaleAgeUnit"] == FemaleAgeUnit &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["male"] == male &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["female"] == female &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["grade"] == grade &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["units"] == units &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["interpretation"] == interpretation &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["lowerLimit"] == lowerLimit &&
                    hiddenAnalyteSMRRefVal[index]["RefVal"][i]["upperLimit"] == upperLimit
                    ) {
                        analyteSMRRefValExists = true;
                        removeIndex = i;
                        break;
                    }
                }

                if (analyteSMRRefValExists == true) {
                    //console.log(removeIndex);

                    hiddenAnalyteSMRRefVal[index]["RefVal"].splice(removeIndex, 1);

                    $("#hiddenAnalyteSMRRefVal").val(JSON.stringify(hiddenAnalyteSMRRefVal));

                    $(obj).closest('p').remove();
                }

                //console.log(JSON.parse($("#hiddenAnalyteSMRRefVal").val()));
            }
            else if (removeFrom == "subAnalyte") {

                var hiddenSubAnalyteSMRRefVal = JSON.parse($("#hiddenSubAnalyteSMRRefVal").val());
                var subAnalyteSMRRefValExists = false;

                var removeIndex = "";

                // console.log(JSON.parse($("#hiddenSubAnalyteSMRRefVal").val()));

                for (var i = 0; i < hiddenSubAnalyteSMRRefVal[index]["RefVal"].length; i++) {

                    if (hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["refType"] == refType &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["MaleFromAge"] == MaleFromAge &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["MaleToAge"] == MaleToAge &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["MaleAgeUnit"] == MaleAgeUnit &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["FemaleFromAge"] == FemaleFromAge &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["FemaleToAge"] == FemaleToAge &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["FemaleAgeUnit"] == FemaleAgeUnit &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["male"] == male &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["female"] == female &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["grade"] == grade &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["units"] == units &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["interpretation"] == interpretation &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["lowerLimit"] == lowerLimit &&
                    hiddenSubAnalyteSMRRefVal[index]["RefVal"][i]["upperLimit"] == upperLimit
                    ) {
                        subAnalyteSMRRefValExists = true;
                        removeIndex = i;
                        break;
                    }
                }

                if (subAnalyteSMRRefValExists == true) {
                    //console.log(removeIndex);

                    hiddenSubAnalyteSMRRefVal[index]["RefVal"].splice(removeIndex, 1);

                    $("#hiddenSubAnalyteSMRRefVal").val(JSON.stringify(hiddenSubAnalyteSMRRefVal));

                    $(obj).closest('p').remove();
                }

                // console.log(JSON.parse($("#hiddenSubAnalyteSMRRefVal").val()));
            }
        }

        function checkAllReferenceValuesAdded() {
            var hiddenAnalyteSMRRefVal = JSON.parse($("#hiddenAnalyteSMRRefVal").val());

            for (var i = 0; i < hiddenAnalyteSMRRefVal.length; i++) {
                if (hiddenAnalyteSMRRefVal[i]["RefVal"].length == 0) {
                    alert("Please add reference values for all");
                    return false;
                }
            }

            var hiddenSubAnalyteSMRRefVal = JSON.parse($("#hiddenSubAnalyteSMRRefVal").val());

            for (var i = 0; i < hiddenSubAnalyteSMRRefVal.length; i++) {
                if (hiddenSubAnalyteSMRRefVal[i]["RefVal"].length == 0) {
                    alert("Please add reference values for all");
                    return false;
                }
            }
            return true;
        }
    </script>
    <!-- REFERENCE VALUES VALIDATION -->
    <script type="text/javascript">
        function validateReferenceValues() {

            if ($("#selReferenceType").val() == "" &&
           $("#txtMaleMinAge").val() == "" &&
           $("#txtMaleMaxAge").val() == "" &&
           $("#txtFemaleMinAge").val() == "" &&
           $("#txtFemaleMaxAge").val() == "" &&
           $("#selMaleAgeUnit").val() == "" &&
           $("#selFemaleAgeUnit").val() == "" &&
           $("#txtMinMaleValue").val() == "" &&
           $("#txtMaxMaleValue").val() == "" &&
           $("#txtMinFemaleValue").val() == "" &&
           $("#txtMaxFemaleValue").val() == "" &&
           $("#txtGrade").val() == "" &&
           $("#txtUnits").val() == "" &&
           $("#txtInterpretation").val() == "" &&
           $("#txtLowerLimit").val() == "" &&
           $("#txtUpperLimit").val() == "") {

                alert("All values cannot be empty");
                return false;
            }

            if ($("#selReferenceType").val() == "") {
                alert("Reference type required");
                return false;
            }

            if ($("#selReferenceType").val().toLowerCase() == "reference as per gender and age") {

                if ($("#txtMaleMinAge").val() == "" || $("#txtMaleMaxAge").val() == "") {
                    alert("Please enter age range. If not applicable enter 0");
                    return false;
                }
                else if (parseInt($("#txtMaleMinAge").val()) > parseInt($("#txtMaleMaxAge").val())) {
                    alert("Max age cannot be greater than min age");
                    return false;
                }

                if ($("#selMaleAgeUnit").val() == "") {

                    if (parseInt($("#txtMaleMinAge").val()) != 0 || parseInt($("#txtMaleMaxAge").val()) != 0) {
                        alert("Please select age unit");
                        return false;
                    }
                }
                else if ($("#selMaleAgeUnit").val().toLowerCase() == "day") {

                    if (parseInt($("#txtMaleMinAge").val()) > 31 || parseInt($("#txtMaleMaxAge").val()) > 31) {
                        alert("Please select month if min or max age is greater than 31 days");
                        return false;
                    }
                }
                else if ($("#selMaleAgeUnit").val().toLowerCase() == "month") {

                }


                if ($("#txtFemaleMinAge").val() == "" || $("#txtFemaleMaxAge").val() == "") {
                    alert("Please enter age range. If not applicable enter 0");
                    return false;
                }
                else if (parseInt($("#txtFemaleMinAge").val()) > parseInt($("#txtFemaleMaxAge").val())) {
                    alert("Max age cannot be greater than min age");
                    return false;
                }

                if ($("#selFemaleAgeUnit").val() == "") {

                    if (parseInt($("#txtFemaleMinAge").val()) != 0 || parseInt($("#txtFemaleMaxAge").val()) != 0) {
                        alert("Please select age unit");
                        return false;
                    }
                }
                else if ($("#selFemaleAgeUnit").val().toLowerCase() == "day") {

                    if (parseInt($("#txtFemaleMinAge").val()) > 31 || parseInt($("#txtFemaleMaxAge").val()) > 31) {
                        alert("Please select month if min or max age is greater than 31 days");
                        return false;
                    }
                }
                else if ($("#selFemaleAgeUnit").val().toLowerCase() == "month") {

                }

                if ($("#txtMinMaleValue").val() == "" || $("#txtMaxMaleValue").val() == "" || $("#txtMinFemaleValue").val() == "" || $("#txtMaxFemaleValue").val() == "") {
                    alert("Please enter value range for both male and female");
                    return false;
                }
                else {
                    if (parseFloat($("#txtMinMaleValue").val()) > parseFloat($("#txtMaxMaleValue").val())) {
                        alert("Max value cannot be greater than min value");
                        return false;
                    }

                    if (parseFloat($("#txtMinFemaleValue").val()) > parseFloat($("#txtMaxFemaleValue").val())) {
                        alert("Max value cannot be greater than min value");
                        return false;
                    }
                }

                if ($("#txtUnits").val() == "") {
                    alert("Please enter units");
                    return false;
                }
            }

            return true;

        }
    </script>
</asp:Content>
