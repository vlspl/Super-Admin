<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="TestDetails.aspx.cs" Inherits="TestDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Test Details</a>
      </div>
      <div class="mr-5">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item pt-1">
          <a id="back" runat="server" href="TestList.aspx" class="btn btn-color"><i class="fa fa-arrow-left mr-2" aria-hidden="true"></i>Back</a>
             <button onclick="window.print()" class="btn btn-color">Print </button>
          </li>
        </ul>
      </div>
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
            <div class="card mt-2 mb-3">
                <div class="card-body container-fluid">
                    <div class="px-3 text-13">
                        <h5 class="text-center pt-3 pb-1 text-17">
                            <span id="spanTestCodeName" runat="server"></span>
                        </h5>
                        <div class="row bg p-3">
                            <div class="col-md-3">
                                Test Id: <span id="spanTestId" runat="server"></span>
                            </div>
                            <div class="col-md-3">
                                Price : <span id="spanPrice" runat="server"></span>
                            </div>
                            <div class="col-md-3">
                                Section : <span id="spanSection" runat="server"></span>
                            </div>
                            <div class="col-md-3">
                                Profile : <span id="spanProfile" runat="server"></span>
                            </div>
                        </div>
                        <div class="row bg p-3">
                            <div class="col">
                                <h5 class="d-block text-17 fa-color">
                                    Test Info:</h5>
                                <p>
                                    <span id="spanTestInfo" runat="server">Pre test instruction</span></p>
                            </div>
                        </div>
                        <div class="row bg p-3">
                            <div class="col-md-6 ">
                                <h5 class="d-block text-17 fa-color">
                                    Test Interpretation:</h5>
                                <p>
                                    <span id="spanTestInterpretation" runat="server">Test Interpretation</span></p>
                            </div>
                            <div class="col-md-6">
                                <h5 class="d-block text-17 fa-color">
                                    Test Limitation:</h5>
                                <p>
                                    <span id="spanTestLimitation" style="font-size: 14px" runat="server">Test Limitation</span></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
            <table class="table" id="testDetail">
                <thead class="table-header">
                <tr>
                        <th>
                            Analyte
                        </th>
                        <th>
                            Sub Analyte
                        </th>
                        <th>
                            Specimen
                        </th>
                        <th>
                            Method
                        </th>
                        <th>
                            Result Type
                        </th>
                        <th>
                            Reference Type
                        </th>
                        <th>
                            Male Age
                        </th>
                        <th>
                            Male
                        </th>
                        <th>
                            Female Age
                        </th>
                        <th>
                            Female
                        </th>
                        <th>
                            Grade
                        </th>
                        <th>
                            Unit
                        </th>
                        <th>
                            Interpritation
                        </th>
                        <th>
                            Upper Limit
                        </th>
                        <th>
                            Lower Limit
                        </th>
                        <th colspan="2">
                            Action
                        </th></tr>
                </thead>
                <tbody id="tbodyTestAnalyteSubAnalyte" runat="server">
                </tbody>
            </table>
            </div>
            <%--  <div id="testDetails" class="table-responsive">
                <ul class="responsive-table">
                    <li class="table-header">
                        <div class="col col-1 text-center">
                            Analyte</div>
                        <div class="col col-2 text-center">
                            Sub Analyte</div>
                        <div class="col col-3 text-center">
                            Specimen</div>
                        <div class="col col-4 text-center">
                            Method</div>
                        <div class="col col-5 text-center">
                            Result Type</div>
                        <div class="col col-6 text-center">
                            Reference Type</div>
                        <div class="col col-7 text-center">
                            Male Age</div>
                        <div class="col col-8 text-center">
                            Male</div>
                        <div class="col col-9 text-center">
                            Female Age</div>
                        <div class="col col-10 text-center">
                            Female</div>
                        <div class="col col-11 text-center">
                            Grade</div>
                        <div class="col col-12 text-center">
                            Unit</div>
                        <div class="col col-13 text-center">
                            Interpritation</div>
                        <div class="col col-14 text-center">
                            Upper Limit
                        </div>
                        <div class="col col-15 text-center">
                            Lower Limit</div>
                        <div class="col col-16 text-center">
                            Action</div>
                    </li>
                    <div id="page">
                        <asp:Literal ID="tbodyTestAnalyteSubAnalyte" runat="server"></asp:Literal></div>
                </ul>
            </div>--%>
        </div>
    </div>
    <div class="wrapper">
        <!-- Sidebar Holder -->
        <!-- sub header -->
        <!-- Modal Add Reference Values Start-->
        <div id="modalAddReferenceValues" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            Add Reference Values</h4>
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="cus-form">
                            <div class="">
                                <div class="form-group">
                                    <select class="form-control" id="selReferenceType1" name="selReferenceType1" clientidmode="Static">
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
                                        <label id="Label4" class="form-label">
                                            Male Details
                                        </label>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <input type="text" class="form-control" id="txtMaleMinAge1" placeholder="Male Min Age"
                                                        clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                                </div>
                                                <div class="col-md-6">
                                                    <input type="text" class="form-control" id="txtMaleMaxAge1" placeholder="Male Max Age"
                                                        clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                                </div>
                                                <asp:TextBox class="form-control" placeholder="Age range" ID="TextBox1" runat="server"
                                                    ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                                <label id="Label5" class="form-error">
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <select id="selMaleAgeUnit1" class="form-control" clientidmode="Static">
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
                                                    <input type="text" id="txtMinMaleValue1" class="form-control" placeholder="Min value Male"
                                                        clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                                </div>
                                                <div class="col-md-6">
                                                    <input type="text" id="txtMaxMaleValue1" class="form-control" placeholder="Max value Male"
                                                        clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                                </div>
                                                <asp:TextBox class="form-control" placeholder="Range/Value for male" ID="TextBox2"
                                                    runat="server" ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                                <label id="Label6" class="form-error">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <label id="Label7" class="form-label">
                                            Female Details
                                        </label>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <input type="text" class="form-control" id="txtFemaleMinAge1" placeholder="Female Min Age"
                                                        clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                                </div>
                                                <div class="col-md-6">
                                                    <input type="text" class="form-control" id="txtFemaleMaxAge1" placeholder="Female Max Age"
                                                        clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                                </div>
                                                <asp:TextBox class="form-control" placeholder="Age range" ID="TextBox3" runat="server"
                                                    ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                                <label id="Label8" class="form-error">
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <select id="selFemaleAgeUnit1" class="form-control" clientidmode="Static">
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
                                                    <input type="text" id="txtMinFemaleValue1" class="form-control" placeholder="Min value for female"
                                                        clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                                </div>
                                                <div class="col-md-6">
                                                    <input type="text" id="txtMaxFemaleValue1" class="form-control" placeholder="Max value for female"
                                                        clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                                </div>
                                                <asp:TextBox class="form-control" placeholder="Range/Value for female" ID="TextBox4"
                                                    runat="server" ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                                <label id="Label9" class="form-error">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" placeholder="Grade" ID="txtGrade1" runat="server"
                                                ClientIDMode="Static"></asp:TextBox>
                                            <label id="Label10" class="form-error">
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:TextBox class="form-control" placeholder="Units" ID="txtUnits1" runat="server"
                                                ClientIDMode="Static"></asp:TextBox>
                                            <label id="Label11" class="form-error">
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Interpretation" ID="txtInterpretation1"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="Label12" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Sensitivity" ID="txtUpperLimit1" runat="server"
                                        ClientIDMode="Static"></asp:TextBox>
                                    <label id="Label13" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Linearity" ID="txtLowerLimit1" runat="server"
                                        ClientIDMode="Static"></asp:TextBox>
                                    <label id="Label14" class="form-error">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="upp" runat="server">
                        <ContentTemplate>
                            <div class="modal-footer">
                                <input type="button" class="btn btn-submit" onclick="btnAdd()" value="Submit" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upp">
                        <ProgressTemplate>
                            <div class="text form_loader">
                                <img src="../../images/Loader.gif" alt="Loading">
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="loader" style="display: none;">
                        <img src="../images/Loader.gif" alt="Loading" />
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"
            aria-hidden="true" id="editpopup">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            Edit Test Details</h4>
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-6">
                                <label id="Label2" class="form-label">
                                    Male Details
                                </label>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <input type="text" class="form-control" id="txtMaleMinAge" placeholder="Male Min Age"
                                                clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="form-control" id="txtMaleMaxAge" placeholder="Male Max Age"
                                                clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                        </div>
                                        <asp:TextBox class="form-control" placeholder="Age range" ID="txtMaleAge" runat="server"
                                            ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                        <label id="lblAge" class="form-error">
                                        </label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <select id="selMaleAgeUnit" class="form-control" clientidmode="Static">
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
                                            <input type="text" id="txtMinMaleValue" class="form-control" placeholder="Min value Male"
                                                clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" id="txtMaxMaleValue" class="form-control" placeholder="Max value Male"
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
                                            <input type="text" class="form-control" id="txtFemaleMinAge" placeholder="Female Min Age"
                                                clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="form-control" id="txtFemaleMaxAge" placeholder="Female Max Age"
                                                clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                                        </div>
                                        <asp:TextBox class="form-control" placeholder="Age range" ID="txtFemaleAge" runat="server"
                                            ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                        <label id="Label1" class="form-error">
                                        </label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
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
                                            <input type="text" id="txtMinFemaleValue" class="form-control" placeholder="Min value for female"
                                                clientidmode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" id="txtMaxFemaleValue" class="form-control" placeholder="Max value for female"
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
                        <input type="hidden" id="Hfalg" />
                        <input type="hidden" id="hdntestid" />
                        <input type="hidden" id="hTSAMID" />
                        <input type="hidden" id="hLABID" />
                        <input type="hidden" id="hRefVal" />
                    </div>
                    <div class="modal-footer">
                        <input type="button" class="btn btn-submit" onclick="btnUpdate()" value="Update" />
                    </div>
                </div>
            </div>
        </div>
    </div>
     <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript">
        function edit(testIddata) {
            $.ajax({
                url: "TestDetails.aspx/TestEdit",
                // data: JSON.stringify({ "hdnPageSequence": hdnPageSequence, "hdnPreviFrameId": hdnPreviFrameId, "hdnNextiFrameId": hdnNextiFrameId, "hdnModel": hdnModel, "hdnModelCnt": hdnModelCnt, "title": title }),
                data: JSON.stringify({ "testId": testIddata }),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    var result = data.d.split("&&&");

                    $('#txtMaleMinAge').val(result[1])
                    $('#txtMaleMaxAge').val(result[3])
                    $('#selMaleAgeUnit').val(result[5])
                    $('#txtMinMaleValue').val(result[7])
                    $('#txtMaxMaleValue').val(result[9])
                    $('#txtFemaleMinAge').val(result[11])
                    $('#txtFemaleMaxAge').val(result[13])
                    $('#selFemaleAgeUnit').val(result[15])
                    $('#txtMinFemaleValue').val(result[17])
                    $('#txtMaxFemaleValue').val(result[19])
                    $('#txtGrade').val(result[21])
                    $('#txtUnits').val(result[23])
                    $('#txtInterpretation').val(result[25])
                    $('#txtUpperLimit').val(result[27])
                    $('#txtLowerLimit').val(result[29])
                    $('#hLABID').val(result[31])
                    $('#hTSAMID').val(result[33])
                    $('#hRefVal').val(result[35])
                    $('#hdntestid').val(testIddata)
                    $('#editpopup').modal('show');
                }
            });
        }

        function btnUpdate() {

            var Maleminage = ($('#txtMaleMinAge').val() != "") ? $('#txtMaleMinAge').val() : "0";
            var MaleMaxAge = ($('#txtMaleMaxAge').val() != "") ? $('#txtMaleMaxAge').val() : "0";
            var MaleAgeunit = $('#selMaleAgeUnit').val();
            var MinMaleValue = $("#txtMinMaleValue").val();
            var MaxMaleValue = $("#txtMaxMaleValue").val();

            var Femaleminage = ($('#txtFemaleMinAge').val() != "") ? $('#txtFemaleMinAge').val() : "0";
            var FemaleMaxAge = ($('#txtFemaleMaxAge').val() != "") ? $('#txtFemaleMaxAge').val() : "0";
            var FemaleAgeunit = $('#selFemaleAgeUnit').val();
            var MinFemaleValue = $("#txtMinFemaleValue").val();
            var MaxFemaleValue = $("#txtMaxFemaleValue").val();

            $.ajax({
                url: "TestDetails.aspx/btnUpdate",
                data: JSON.stringify({ "testId": $('#hdntestid').val(), "MaleFromAge": Maleminage, "MaleToAge": MaleMaxAge, "MaleAgeUnit": MaleAgeunit,
                    "MinMaleValue": MinMaleValue, "MaxMaleValue": MaxMaleValue, "FemaleFromAge": Femaleminage, "FemaleToAge": FemaleMaxAge, "FemaleAgeUnit": FemaleAgeunit,
                    "MinFemaleValue": MinFemaleValue, "MaxFemaleValue": MaxFemaleValue, "txtGrade": $('#txtGrade').val(), "txtUnits": $('#txtUnits').val(), "txtInterpretation": $('#txtInterpretation').val(),
                    "txtUpperLimit": $('#txtUpperLimit').val(), "txtLowerLimit": $('#txtLowerLimit').val()
                   , "LABId": $('#hLABID').val()
                   , "TSAMID": $('#hTSAMID').val(), "hRefVal": $('#hRefVal').val()
                }),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    location.reload();
                }
            });
            $('#editpopup').modal('hide');
        }
   </script>
    <script type="text/javascript">

        function Add(testIddata) {
            $('#hTSAMID').val(testIddata)
            $('#Hfalg').val("1")
            $('#modalAddReferenceValues').modal('show');
        }
        function AddTSAMId(testIddata) {
            $('#hTSAMID').val(testIddata)
            $('#Hfalg').val("2")
            $('#modalAddReferenceValues').modal('show');
        }

        function btnAdd() {
            var Maleminage1 = ($('#txtMaleMinAge1').val() != "") ? $('#txtMaleMinAge1').val() : "0";
            var MaleMaxAge1 = ($('#txtMaleMaxAge1').val() != "") ? $('#txtMaleMaxAge1').val() : "0";
            var MaleAgeunit1 = $('#selMaleAgeUnit1').val();
            var MinMaleValue1 = $("#txtMinMaleValue1").val();
            var MaxMaleValue1 = $("#txtMaxMaleValue1").val();

            var Femaleminage1 = ($('#txtFemaleMinAge1').val() != "") ? $('#txtFemaleMinAge1').val() : "0";
            var FemaleMaxAge1 = ($('#txtFemaleMaxAge1').val() != "") ? $('#txtFemaleMaxAge1').val() : "0";
            var FemaleAgeunit1 = $('#selFemaleAgeUnit1').val();
            var MinFemaleValue1 = $("#txtMinFemaleValue1").val();
            var MaxFemaleValue1 = $("#txtMaxFemaleValue1").val();

            $.ajax({
                url: "TestDetails.aspx/btnAdd",
                data: JSON.stringify({ "MaleFromAge": Maleminage1, "MaleToAge": MaleMaxAge1, "MaleAgeUnit": MaleAgeunit1,
                    "MinMaleValue": MinMaleValue1, "MaxMaleValue": MaxMaleValue1, "FemaleFromAge": Femaleminage1, "FemaleToAge": FemaleMaxAge1, "FemaleAgeUnit": FemaleAgeunit1,
                    "MinFemaleValue": MinFemaleValue1, "MaxFemaleValue": MaxFemaleValue1, "txtGrade": $('#txtGrade1').val(), "txtUnits": $('#txtUnits1').val(),
                    "txtInterpretation": $('#txtInterpretation1').val(), "txtUpperLimit": $('#txtUpperLimit1').val(), "txtLowerLimit": $('#txtLowerLimit1').val(),
                    "LABId": $('#hLABID').val(), "TSAMID": $('#hTSAMID').val(), "hRefVal": $('#selReferenceType1').val(), "Flag": $('#Hfalg').val()
                }),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (msg) {
                    if (msg.d == 1) {
                        alert('Added Successfully.')
                        location.reload();

                    };
                    if (msg.d == -2) {
                        alert('Reference Range Already exists in Your List.')
                    };
                },
                error: function (data) {
                    alert('Something Went Wrong,Please Try again.')
                }
            });
            $('#modalAddReferenceValues').modal('hide');
        }
   </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#I1").hover(function () {
                alert("Hello");
                // $('.flyout').show();
            }, function () {
                alert("Hello out");
                // $('.flyout').hide();
            });
        });
    </script>
</asp:Content>
