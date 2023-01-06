<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="EditTest.aspx.cs" Inherits="EditTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="wrapper">
      <!-- Sidebar Holder -->
      <script src="js/sidebar.js"></script>
      <!-- sub header -->
      <div id="content" class="">
         <div id="testlist" class="">
            <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4><a href="TestList.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></a>Edit Test</h4>
                     </div>
                  </div>
               </div>
            </nav>
            <!--  -->
                 
          <div id="div1" class="testmaster first" clientidmode="Static">              
              <!-- Section -->
              <div class="row testmasterwrap">
                <div class="col-sm-6">
                  <div class="form-group">
                    Section : 
                    <asp:DropDownList  ID="selSection"  runat="server" ClientIDMode="Static" onchange="javascript:sectionSelected(this)"> </asp:DropDownList>                   
                  </div>
                </div>
                    
              <!-- Profile -->
              <div class="col-sm-6">
                <div class="form-group">
                  Profile : 
                  <asp:DropDownList  ID="selProfile"  runat="server" ClientIDMode="Static"> </asp:DropDownList>                    
                </div>
              </div>
            </div>             
          </div>

          <div id="div2" class="testmaster" clientidmode="Static">             
             <asp:HiddenField ID="hiddenTestId" runat="server" ClientIDMode="Static"/>
             <div class="row testmasterwrap">
                <div class="col-sm-6">
                   <div class="form-group">
                      Test Code : 
                      <asp:TextBox  placeholder="Test Code" id="txtTestCode" runat="server" ClientIDMode="Static"></asp:TextBox>
                   </div> 
                </div>
                <div class="col-sm-6">
                   <div class="form-group">
                      Test Name :
                      <asp:TextBox  placeholder="Test Name" id="txtTestName" runat="server" ClientIDMode="Static"></asp:TextBox>
                   </div>
                </div>
                <div class="col-sm-6">
                   <div class="form-group">
                      Test Useful For : 
                      <asp:TextBox  placeholder="Test useful for" id="txtTestUsefulFor" TextMode="MultiLine" Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                   </div>
                </div>
                <div class="col-sm-6">
                   <div class="form-group">
                      Test Interpretation : 
                      <asp:TextBox  placeholder="Test Interpretation" id="txtTestInterpretation" TextMode="MultiLine" Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                   </div>
                </div>
                <div class="col-sm-6">
                   <div class="form-group">
                      Test Limitation : 
                      <asp:TextBox  placeholder="Test Limitation" id="txtTestLimitation" TextMode="MultiLine" Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                   </div>
                </div>
                <div class="col-sm-6">
                   <div class="form-group">
                      Test Clinical Reference : 
                      <asp:TextBox  placeholder="Test Clinical References" id="txtTestClinicalReferences" TextMode="MultiLine" Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                   </div>
                </div>
                <div class="col-sm-6">
                   <div class="form-group">
                      <asp:Button  class="lab-btn-secondary" ID="btnUpdateTestDetails" Text="Update Test Details" runat="server" ClientIDMode="Static" onclick="btnUpdateTestDetails_Click" />
                   </div>
                </div>
             </div>
          </div>


          <asp:HiddenField ID="hiddenAnalyteSMRRefVal" ClientIDMode="Static" runat="server" />
          <asp:HiddenField ID="hiddenSubAnalyteSMRRefVal" ClientIDMode="Static" runat="server" />

            <table class="table text-center booking table-bordered table-hover" id="tabTestAnalyteSubAnalyte" clientidmode="Static">
                <thead>
                    <tr>
                        <th scope='col'>Analyte</th>
                        <th scope='col'>Subanalyte</th>
                        <th scope='col'>Specimen | Quantity | Time period | Method | Result Type</th>
                        <th scope='col'>Reference Type | Age | Male | Female | Grade | Units | Interpretation | Lower Limit | Upper Limit</th>
                    </tr>
                    <tr>
                        <td scope='col'>
                            <a href='javascript:void(0)' id='btnOpenAddAnalyteModal' onclick='javascript:openAddAnalyteModal()'>Add Analyte</a>
                        </td>
                        <td scope='col'>
                            <a href='javascript:void(0)' id='btnOpenAddSubAnalyteModal' onclick='javascript:openAddSubAnalyteModal()'>Add SubAnalyte</a>
                        </td>
                        <td scope='col'>
                            <a href='javascript:void(0)' id='btnOpenAddSMRModal' onclick='javascript:openAddSMRModal()'>Add Specimen, Method</a>
                        </td>
                        <td scope='col'></td>
                    </tr>
                </thead>
               <tbody id="tbodyTestAnalyteSubAnalyte" runat="server" clientidmode="Static">                    
               </tbody>
            </table>
         </div>
      </div>
   </div>


<!-- MODAL START -->

<!-- Modal Specimen Method Start-->
<div id="modalSpecimenMethod" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Update Specimen Method</h4>
      </div>
      <div class="modal-body">
            <asp:HiddenField ID="hiddenSMRId" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hiddenAnalyteSubAnalyteSMR" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hiddenCurrentValues" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hiddenTestAnalyteSubAnalyteId" ClientIDMode="Static" runat="server" />
           <div class="cus-form">
              <!-- Specimen Details-->
              <div class="">            
                  <h5>Specimen Details</h5>            
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Specimen Name" id="txtSpecimenName" list="lstSpecimen" runat="server" ClientIDMode="Static"></asp:TextBox>
                     <datalist id="lstSpecimen" clientidmode="Static"></datalist>
                    <label id="lblSpecimenName" class="form-error"></label>
                  </div>
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Quantity" id="txtQuantity" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblQuantity" class="form-error"></label>
                  </div>
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Time Period" id="txtTimePeriod" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblTimePeriod" class="form-error"></label>
                  </div>
              </div>

              <!-- Method Details-->
              <div class="">      
                  <h5>Method Details</h5>                              
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Method" id="txtMethod" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblMethod" class="form-error"></label>
                  </div>
              </div>

              <!-- Result type-->
              <div class="">      
                  <h5>Result Type</h5>                              
                  <div class="form-group">
                    <select class="form-control" id="selResultType" clientidmode="Static">
                        <option value="">Select Result type</option>
                        <option value="Quantitative">Quantitative</option>
                        <option value="Qualitative">Qualitative</option>
                        <option value="Descriptive">Descriptive</option>
                    </select>
                    <label id="lblResultType" class="form-error"></label>
                  </div>
              </div>

            </div>
      </div>
      <div class="modal-footer">
         <input type="button" ID="btnUpdateSpecimenMethod" class="lab-btn-default"  value="Update" OnClick="javascript:return updateSMR()" ClientIDMode="Static" />  
      </div>
    </div>
  </div>
</div> 

<div id="modalAddSpecimenMethod" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Add Specimen Method</h4>
      </div>
      <div class="modal-body">
            <asp:HiddenField ID="hiddenTestAnalyteId" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hiddenTestSubAnalyteId" ClientIDMode="Static" runat="server" />
           <div class="cus-form">
			  <!-- Select Analyte -->
			  <div class="">      
                  <h5>Analyte</h5>                              
                  <div class="form-group">
                    <%--<select class="form-control" id="selAnalyte" clientidmode="Static" runat="server"></select>--%>
                    <asp:DropDownList  ID="selAnalyte"  runat="server" ClientIDMode="Static" onchange="javascript:analyteSelected(this)"> </asp:DropDownList>
                    <label id="lblAnalyte" class="form-error"></label>
                  </div>
              </div>
			  
			  <!-- Select SubAnalyte -->
			  <div class="">      
                  <h5>Sub Analyte</h5>                              
                  <div class="form-group">
                    <%--<select class="form-control" id="selSubAnalyte" clientidmode="Static" runat="server"></select>--%>
                    <asp:DropDownList  ID="selSubAnalyte"  runat="server" ClientIDMode="Static" onchange="javascript:subAnalyteSelected(this)"> </asp:DropDownList>
                    <label id="lblSubAnalyte" class="form-error"></label>
                  </div>
              </div>
			  
              <!-- Specimen Details-->
              <div class="">            
                  <h5>Specimen Details</h5>            
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Specimen Name" id="txtAddSpecimenName" list="lstAddSpecimen" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <datalist id="lstAddSpecimen" clientidmode="Static"></datalist>
                    <label id="lblAddSpecimenName" class="form-error"></label>
                  </div>
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Quantity" id="txtAddQuantity" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblAddQuantity" class="form-error"></label>
                  </div>
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Time Period" id="txtAddTimePeriod" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblAddTimePeriod" class="form-error"></label>
                  </div>
              </div>

              <!-- Method Details-->
              <div class="">      
                  <h5>Method Details</h5>                              
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Method" id="txtAddMethod" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblAddMethod" class="form-error"></label>
                  </div>
              </div>

              <!-- Result type-->
              <div class="">      
                  <h5>Result Type</h5>                              
                  <div class="form-group">
                    <select class="form-control" id="selAddResultType" clientidmode="Static" runat="server">
                        <option value="">Select Result type</option>
                        <option value="Quantitative">Quantitative</option>
                        <option value="Qualitative">Qualitative</option>
                        <option value="Descriptive">Descriptive</option>
                    </select>
                    <label id="lblAddResultType" class="form-error"></label>
                  </div>
              </div>

            </div>
      </div>
      <div class="modal-footer">
         <asp:Button  class="lab-btn-secondary" ID="btnAddSMR" Text="Add" runat="server" ClientIDMode="Static" OnClientClick="javascript:return validateAddSMR()" onclick="btnAddSMR_Click" />
      </div>
    </div>
  </div>
</div>
<!-- Modal Specimen Method End-->

<!-- Modal Reference Values Start-->
<div id="modalReferenceValues" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Reference Values</h4>
      </div>
      <div class="modal-body">
            <asp:HiddenField ID="hiddenTestReferenceId" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hiddenAnalyteSubAnalyteSMRRefVal" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hiddenCurrentRefValues" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hiddenTestSMId" ClientIDMode="Static" runat="server" />
           <div class="cus-form">
              <div class="">  
                  <div class="form-group">
                    <select class="form-control" id="selReferenceType" clientidmode="Static">
                        <option value="">Select Reference Type</option>
                        <option value="Reference as per gender and age">Reference as per gender and age</option>
                        <option value="Reference value as per severity of disease">Reference value as per severity of disease</option>
                        <option value="Reference as per limits of detection">Reference as per limits of detection</option>
                        <option value="NA">Not Applicable</option>
                    </select>
                    <label id="lblReferenceType" class="form-error"></label>
                  </div>
                                    
                  <div class="form-group">
                    <input type="text" id="txtMinAge" placeholder="Min Age" clientIdMode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');"/>
                     <select id="selMinAgeUnit" clientIdMode="Static" onchange="javascript:loadMaxAgeUnit(this)">
                        <option value="">Select age range unit</option>
                        <option value="day">days</option>
                        <option value="week">weeks</option>
                        <option value="month">months</option>
                        <option value="year">years</option>                                                
                    </select> -     

                    <input type="text" id="txtMaxAge" placeholder="Max Age" clientIdMode="Static" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');" />
                    <select id="selMaxAgeUnit" clientIdMode="Static">
                        <option value="">Select age range unit</option>
                        <option value="day">days</option>
                        <option value="week">weeks</option>
                        <option value="month">months</option>
                        <option value="year">years</option>                                                
                    </select>                    
                    <asp:TextBox class="form-control" placeholder="Age range" id="txtAge" runat="server" ClientIDMode="Static" style="display:none"></asp:TextBox>
                    <label id="lblAge" class="form-error"></label>
                  </div>

                  <div class="form-group">
                    <input type="text" id="txtMinMaleValue" placeholder="Min value for male" clientIdMode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" /> -
                    <input type="text" id="txtMaxMaleValue" placeholder="Max value for male" clientIdMode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')"/>
                    <asp:TextBox class="form-control" placeholder="Range/Value for male" id="txtMaleRange" runat="server" ClientIDMode="Static" style="display:none"></asp:TextBox>
                    <label id="lblMaleRange" class="form-error"></label>
                  </div>

                  <div class="form-group">
                    <input type="text" id="txtMinFemaleValue" placeholder="Min value for female" clientIdMode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" /> -
                    <input type="text" id="txtMaxFemaleValue" placeholder="Max value for female" clientIdMode="Static" onkeyup="javascript:this.value = this.value.replace(/[^-0-9\.]/g,'');if(this.value.split('.').length > 2)this.value=this.value.replace(/\.+$/,'')" />
                    <asp:TextBox class="form-control" placeholder="Range/Value for female" id="txtFemaleRange" runat="server" ClientIDMode="Static" style="display:none"></asp:TextBox>
                    <label id="lblFemaleRange" class="form-error"></label>
                  </div>

                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Grade" id="txtGrade" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblGrade" class="form-error"></label>
                  </div>

                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Units" id="txtUnits" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblUnits" class="form-error"></label>
                  </div>

                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Interpretation" id="txtInterpretation" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblInterpretation" class="form-error"></label>
                  </div>
                  
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Lower Limit" id="txtLowerLimit" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblLowerLimit" class="form-error"></label>
                  </div>

                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Upper Limit" id="txtUpperLimit" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblUpperLimit" class="form-error"></label>
                  </div>
              </div>

            </div>
      </div>
      <div class="modal-footer">
         <input type="button" ID="btnUpdateReferenceValues" class="lab-btn-default"  value="Update" OnClick="javascript:return updateReferenceValues()" ClientIDMode="Static" />  
         <input type="button" ID="btnAddReferenceValues" class="lab-btn-default"  value="Add" OnClick="javascript:return addReferenceValues()" ClientIDMode="Static" />  
      </div>
    </div>
  </div>
</div> 
<!-- Modal Reference Values End-->

<!-- Modal Add Analyte Start -->
<div id="modalAddAnalyte" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Add Analyte</h4>
      </div>
      <div class="modal-body">
        <div class="cus-form">
              <div class="">       
                <div class="form-group">
                    <asp:HiddenField ID="hiddenAnalyteId" runat="server" ClientIDMode="Static" />
                   <input type="text" id="txtAnalyteName" list="lstAnalyte" clientidmode="Static" oninput="javascript:onAnalyteSelect(this)"/>

                    <datalist id="lstAnalyte" clientidmode="Static">
                      <%--<option value="Internet Explorer">
                      <option value="Firefox">
                      <option value="Google Chrome">
                      <option value="Opera">
                      <option value="Safari">--%>
                    </datalist>
                    <label id="lblAnalyteName" class="form-error"></label>
                  </div>
              </div>
            </div>
      </div>
      <div class="modal-footer">
         <input type="button" ID="btnAddAnalyte" class="lab-btn-default" value="Submit" OnClick="javascript:return addAnalyte()" ClientIDMode="Static" />  
      </div>
    </div>
  </div>
</div> 
<!-- Modal Add Analyte End -->

<!-- Modal Add SubAnalyte Start -->
<div id="modalAddSubAnalyte" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Add SubAnalyte</h4>
      </div>
      <div class="modal-body">
			<div class="cus-form">
              <div class="">       
                 <div class="form-group">
                    <select id="selAnalyteName" runat="server" clientidmode="Static" onchange="javascript:analyteSelectedToAddSubAnalyte(this)"></select>
                    <label id="lblAddAnalyteName" class="form-error"></label>
                  </div>
                  <div class="form-group">
                    <select id="selSubAnalyteName" runat="server" clientidmode="Static"></select>
                    <label id="lblAddSubAnalyteName" class="form-error"></label>
                  </div>
              </div>
            </div>
      </div>
      <div class="modal-footer">
         <input type="button" ID="btnAddSubAnalyte" class="lab-btn-default" value="Submit" OnClick="javascript:return addSubAnalyte()" ClientIDMode="Static" />  
      </div>
    </div>
  </div>
</div> 
<!-- Modal Add SubAnalyte End -->

<!-- MODAL END -->


<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/SortTable.js"></script>

<!-- DOCUMENT READY -->
<script type="text/javascript">

    $(document).ready(function () {

        var tabContent = "";

        //Load Analyte details
        var hiddenAnalyteSMRRefVal = JSON.parse($("#hiddenAnalyteSMRRefVal").val());

        //console.log($("#hiddenAnalyteSMRRefVal").val());

        hiddenAnalyteSMRRefVal.forEach(function (hiddenAnalyteSMRRefVal, index) {

            var editSMR = hiddenAnalyteSMRRefVal["tasmId"] + "|" + hiddenAnalyteSMRRefVal["specimenName"] + "|" +
                                                    hiddenAnalyteSMRRefVal["quantity"] + "|" +
                                                    hiddenAnalyteSMRRefVal["timePeriod"] + "|" +
                                                    hiddenAnalyteSMRRefVal["methodName"] + "|" +
                                                    hiddenAnalyteSMRRefVal["resultType"] + "|" +
                                                    hiddenAnalyteSMRRefVal["testAnalyteId"];

            var specimenMethod = (hiddenAnalyteSMRRefVal["tasmId"] == "" || hiddenAnalyteSMRRefVal["tasmId"] == null) ? "" : hiddenAnalyteSMRRefVal["specimenName"] + " | " +
                                                    hiddenAnalyteSMRRefVal["quantity"] + " | " +
                                                    hiddenAnalyteSMRRefVal["timePeriod"] + " | " +
                                                    hiddenAnalyteSMRRefVal["methodName"] + " | " +
                                                    hiddenAnalyteSMRRefVal["resultType"] +
                                                    "<span style='float:right'>" +
                                                        "<a href='javascript:void(0)' id='btnDeleteAnalyteSMR' title='" + hiddenAnalyteSMRRefVal["tasmId"] + "' onclick='javascript:removeSMR(this)'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a>  " +
                                                        "<a href='javascript:void(0)' id='btnEditAnalyteSMR' title='" + editSMR + "' onclick='javascript:editSMR(this)'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>" +
                                                    "</span>";
            var addRefValButton = (hiddenAnalyteSMRRefVal["tasmId"] == "" || hiddenAnalyteSMRRefVal["tasmId"] == null) ? "" : "<span style='float:right'>" +
                                        "<a href='javascript:void(0)' id='btnAddAnalyteSMRRefVal' title='" + hiddenAnalyteSMRRefVal["tasmId"] + "'  onclick='javascript:openReferenceValueModal(this)'><i class='fa fa-plus-square-o' aria-hidden='true'></i></a>" +
                                    "</span><br>";

            tabContent += "<tr>" +
                                "<td scope='col'>" + hiddenAnalyteSMRRefVal["analyteName"] + "</td>" +
                                "<td scope='col'>---</td>" +

                                "<td scope='col'>" + specimenMethod + /*specimenMethod else part was here*/
                                "</td>" +
                                "<td scope='col'>" + addRefValButton /*addRefValButton else part was here*/;

            if (hiddenAnalyteSMRRefVal["tasmId"] != "" && hiddenAnalyteSMRRefVal["tasmId"] != null) {
                for (var i = 0; i < hiddenAnalyteSMRRefVal["refVal"].length; i++) {

                    var editSMRRefVal = hiddenAnalyteSMRRefVal["tasmId"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["tarId"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["refType"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["age"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["male"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["female"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["grade"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["units"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["interpretation"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["lowerLimit"] + "|" +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["upperLimit"];

                    var refVal = (hiddenAnalyteSMRRefVal["refVal"][i]["tarId"] == "" || hiddenAnalyteSMRRefVal["refVal"][i]["tarId"] == null) ? "" : hiddenAnalyteSMRRefVal["refVal"][i]["refType"] + " | " +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["age"] + " | " +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["male"] + " | " +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["female"] + " | " +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["grade"] + " | " +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["units"] + " | " +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["interpretation"] + " | " +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["lowerLimit"] + " | " +
                                        hiddenAnalyteSMRRefVal["refVal"][i]["upperLimit"] +
                                        "<span style='float:right'>" +
                                            "<a href='javascript:void(0)' id='btnDeleteAnalyteSMRRefVal' title='" + hiddenAnalyteSMRRefVal["refVal"][i]["tarId"] + "' onclick='javascript:removeReferenceValue(this)'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a>  " +
                                            "<a href='javascript:void(0)' id='btnEditAnalyteSMRRefVal' title='" + editSMRRefVal + "' onclick='javascript:editReferenceValue(this)'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>" +
                                        "</span>";

                    tabContent += "<p>" + refVal + /*refVal else part was here */
                                        "</p>";
                }
            }

            tabContent += "</td></tr>";
        });

        $("#tbodyTestAnalyteSubAnalyte").append(tabContent);

        tabContent = "";
        // Load SubAnalyte details
        var hiddenSubAnalyteSMRRefVal = JSON.parse($("#hiddenSubAnalyteSMRRefVal").val());

        hiddenSubAnalyteSMRRefVal.forEach(function (hiddenSubAnalyteSMRRefVal, index) {

            var editSMR = hiddenSubAnalyteSMRRefVal["tsasmId"] + "|" + hiddenSubAnalyteSMRRefVal["specimenName"] + "|" +
                                                    hiddenSubAnalyteSMRRefVal["quantity"] + "|" +
                                                    hiddenSubAnalyteSMRRefVal["timePeriod"] + "|" +
                                                    hiddenSubAnalyteSMRRefVal["methodName"] + "|" +
                                                    hiddenSubAnalyteSMRRefVal["resultType"] + "|" +
                                                    hiddenSubAnalyteSMRRefVal["testSubAnalyteId"];

            var specimenMethod = (hiddenSubAnalyteSMRRefVal["tsasmId"] == "" || hiddenSubAnalyteSMRRefVal["tsasmId"] == null) ? "" : hiddenSubAnalyteSMRRefVal["specimenName"] + " | " +
                                                    hiddenSubAnalyteSMRRefVal["quantity"] + " | " +
                                                    hiddenSubAnalyteSMRRefVal["timePeriod"] + " | " +
                                                    hiddenSubAnalyteSMRRefVal["methodName"] + " | " +
                                                    hiddenSubAnalyteSMRRefVal["resultType"] +
                                                    "<span style='float:right'>" +
                                                        "<a href='javascript:void(0)' id='btnDeleteSubAnalyteSMR' title='" + hiddenSubAnalyteSMRRefVal["tsasmId"] + "' onclick='javascript:removeSMR(this)'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a>  " +
                                                        "<a href='javascript:void(0)' id='btnEditSubAnalyteSMR'  title='" + editSMR + "' onclick='javascript:editSMR(this)'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>" +
                                                    "</span>";

            var addRefValButton = (hiddenSubAnalyteSMRRefVal["tsasmId"] == "" || hiddenSubAnalyteSMRRefVal["tsasmId"] == null) ? "" : "<span style='float:right'>" +
                                                        "<a href='javascript:void(0)' id='btnAddSubAnalyteSMRRefVal' title='" + hiddenSubAnalyteSMRRefVal["tsasmId"] + "'  onclick='javascript:openReferenceValueModal(this)'><i class='fa fa-plus-square-o' aria-hidden='true'></i></a>" +
                                                    "</span><br>";

            tabContent += "<tr>" +
                                "<td scope='col'>" + hiddenSubAnalyteSMRRefVal["analyteName"] + "</td>" +
                                "<td scope='col'>" + hiddenSubAnalyteSMRRefVal["subAnalyteName"] + "</td>" +

                                "<td scope='col'>" + specimenMethod + /*specimenMethod else part was here*/
                                "</td>" +
                                "<td scope='col'>" + addRefValButton /*addRefValButton else part was here*/;


            if (hiddenSubAnalyteSMRRefVal["tsasmId"] != "" && hiddenSubAnalyteSMRRefVal["tsasmId"] != null) {
                for (var i = 0; i < hiddenSubAnalyteSMRRefVal["refVal"].length; i++) {

                    var editSMRRefVal = hiddenSubAnalyteSMRRefVal["tsasmId"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["tsarId"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["refType"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["age"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["male"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["female"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["grade"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["units"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["interpretation"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["lowerLimit"] + "|" +
                                        hiddenSubAnalyteSMRRefVal["refVal"][i]["upperLimit"];

                    var refVal = (hiddenSubAnalyteSMRRefVal["refVal"][i]["tsarId"] == "" || hiddenSubAnalyteSMRRefVal["refVal"][i]["tsarId"] == null) ? "" : hiddenSubAnalyteSMRRefVal["refVal"][i]["refType"] + " | " +
                                    hiddenSubAnalyteSMRRefVal["refVal"][i]["age"] + " | " +
                                    hiddenSubAnalyteSMRRefVal["refVal"][i]["male"] + " | " +
                                    hiddenSubAnalyteSMRRefVal["refVal"][i]["female"] + " | " +
                                    hiddenSubAnalyteSMRRefVal["refVal"][i]["grade"] + " | " +
                                    hiddenSubAnalyteSMRRefVal["refVal"][i]["units"] + " | " +
                                    hiddenSubAnalyteSMRRefVal["refVal"][i]["interpretation"] + " | " +
                                    hiddenSubAnalyteSMRRefVal["refVal"][i]["lowerLimit"] + " | " +
                                    hiddenSubAnalyteSMRRefVal["refVal"][i]["upperLimit"] +
                                    "<span style='float:right'>" +
                                        "<a href='javascript:void(0)' id='btnDeleteSubAnalyteSMRRefVal' title='" + hiddenSubAnalyteSMRRefVal["refVal"][i]["tsarId"] + "' onclick='javascript:removeReferenceValue(this)'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a>  " +
                                        "<a href='javascript:void(0)' id='btnEditSubAnalyteSMRRefVal' title='" + editSMRRefVal + "'  onclick='javascript:editReferenceValue(this)'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>" +
                                    "</span>";


                    tabContent += "<p>" + refVal + /*refVal else part was here */
                                    "</p>";
                }
            }

            tabContent += "</td></tr>";
        });

        $("#tbodyTestAnalyteSubAnalyte").append(tabContent);
        
        //Sort table by analyte name
        sortTable("tabTestAnalyteSubAnalyte");
    });

</script>

<!-- OPEN ADD SMR -->
<script type="text/javascript">
    function openAddSMRModal() {        
        //by default "Select Analyte" option for selAnalyte should be selected and no sub analyte should be loaded
        $('#selAnalyte').find('option[value=""]').prop('selected', true);
        $('#selSubAnalyte').find('option[value=""]').prop('selected', true);
        $("#selSubAnalyte option").each(function (i) {
            if ($(this).val() != "") {
                $(this).hide();
            }
        });

        //refresh testAnalyteId and testSubAnalyteId 
        $("#hiddenTestAnalyteId").val("");
        $("#hiddenTestSubAnalyteId").val("");

        $.ajax({
            type: "POST",
            url: "EditTest.aspx/getSpecimen",
            //data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            //dataType: "text/plain",
            success: function (response) {
                var json = JSON.parse(response.d);

                for (var i = 0; i < json.length; i++) {
                    $("#lstAddSpecimen").append("<option value='" + json[i]["specimenName"] + "' />");
                }

                //console.log("success" + response.d);
            },
            error: function (response) {
                console.log("error" + response.d);
            },
            failure: function (response) {
                //console.log("fail" + response.d);
            }
        });

        $("#modalAddSpecimenMethod").modal('show');
    }
</script>

<!-- ANALYTE SUBANALYTE SELECTED -->
<script type="text/javascript">

    //load sub analytes based on analyte selected in add specimen method modal
    function analyteSelected(obj) {
        var analyteId = $(obj).val();

        var hiddenAnalyteSMRRefVal = JSON.parse($("#hiddenAnalyteSMRRefVal").val());

        //console.log(hiddenAnalyteSMRRefVal);

        for (var i = 0; i < hiddenAnalyteSMRRefVal.length; i++) {
            if (hiddenAnalyteSMRRefVal[i]["analyteId"] == analyteId) {
                //alert(hiddenAnalyteSMRRefVal[i]["testAnalyteId"]);
                $("#hiddenTestAnalyteId").val(hiddenAnalyteSMRRefVal[i]["testAnalyteId"]);
                break;
            }
        }

            $('#selSubAnalyte').find('option').show();
        $('#selSubAnalyte').find('option[value=""]').prop('selected', true);

        $("#selSubAnalyte option").each(function (i) {
            if ($(this).val().split('|')[0] != analyteId && $(this).val() != "") {
                $(this).hide();
            }
        });
    }


    //load sub analytes based on analyte selected in add specimen method modal
    function subAnalyteSelected(obj) {
        var subAnalyteId = $(obj).val();
        
        var hiddenSubAnalyteSMRRefVal = JSON.parse($("#hiddenSubAnalyteSMRRefVal").val());

        for (var i = 0; i < hiddenSubAnalyteSMRRefVal.length; i++) {
            if (hiddenSubAnalyteSMRRefVal[i]["subAnalyteId"] == subAnalyteId.split("|")[1]) {
                //alert(hiddenSubAnalyteSMRRefVal[i]["testSubAnalyteId"]);
                $("#hiddenTestSubAnalyteId").val(hiddenSubAnalyteSMRRefVal[i]["testSubAnalyteId"]);
                break;
            }
        }
    }
</script>

<!-- EDIT SMR -->
<script type="text/javascript">
    function editSMR(obj) {
        var smrId = $(obj).attr('title').split("|")[0];
        var specimenName = $(obj).attr('title').split("|")[1];
        var quantity = $(obj).attr('title').split("|")[2];
        var timePeriod = $(obj).attr('title').split("|")[3];
        var methodName = $(obj).attr('title').split("|")[4];
        var resultType = $(obj).attr('title').split("|")[5];
        var testAnalyteSubAnalyteId = $(obj).attr('title').split("|")[6];

        var hiddenAnalyteSubAnalyteSMR = ($(obj).attr('id') == 'btnEditAnalyteSMR') ? 'analyte' : 'subAnalyte';

        $("#txtSpecimenName").val(specimenName);
        $("#txtQuantity").val(quantity);
        $("#txtTimePeriod").val(timePeriod);
        $("#txtMethod").val(methodName);
        $("#selResultType").val(resultType);
        $("#hiddenSMRId").val(smrId);
        $("#hiddenAnalyteSubAnalyteSMR").val(hiddenAnalyteSubAnalyteSMR);
        $("#hiddenTestAnalyteSubAnalyteId").val(testAnalyteSubAnalyteId);
        $("#hiddenCurrentValues").val($(obj).attr('title'));
        
        $.ajax({
            type: "POST",
            url: "EditTest.aspx/getSpecimen",
            //data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            //dataType: "text/plain",
            success: function (response) {
                var json = JSON.parse(response.d);

                for (var i = 0; i < json.length; i++) {
                    $("#lstSpecimen").append("<option value='" + json[i]["specimenName"] + "' />");
                }

                //console.log("success" + response.d);
            },
            error: function (response) {
                console.log("error" + response.d);
            },
            failure: function (response) {
                //console.log("fail" + response.d);
            }
        });

        $("#modalSpecimenMethod").modal('show');
    }

</script>

<!-- UPDATE SMR -->
<script type="text/javascript">
    // this is called when update button on 
    function updateSMR() {
        
        var smrId = $("#hiddenCurrentValues").val().split("|")[0];
        var specimenName = $("#hiddenCurrentValues").val().split("|")[1];
        var quantity = $("#hiddenCurrentValues").val().split("|")[2];
        var timePeriod = $("#hiddenCurrentValues").val().split("|")[3];
        var methodName = $("#hiddenCurrentValues").val().split("|")[4];
        var resultType = $("#hiddenCurrentValues").val().split("|")[5];

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
        else if ($("#txtSpecimenName").val() == specimenName &&
                 $("#txtQuantity").val() == quantity &&
                 $("#txtTimePeriod").val() == timePeriod &&
                 $("#txtMethod").val() == methodName &&
                 $("#selResultType").val() == resultType
        ) {
            alert("No change in values");
            return false;
        }

        var sampleName = $("#txtSpecimenName").val();
        var quantity = $("#txtQuantity").val();
        var timePeriod = $("#txtTimePeriod").val();
        var method = $("#txtMethod").val();
        var resultType = $("#selResultType").val();

        var parameter = { "insertInto": $("#hiddenAnalyteSubAnalyteSMR").val(), "smrId": $("#hiddenSMRId").val(), "sampleType": sampleName, "quantity": quantity, "timePeriod": timePeriod, "method": method, "resultType": resultType, "testAnalyteSubAnalyteId": $("#hiddenTestAnalyteSubAnalyteId").val() };

        $.ajax({
            type: "POST",
            url: "EditTest.aspx/updateSpecimenMethod",
            data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            //dataType: "text/plain",
            success: function (response) {
                var json = JSON.parse(response.d);

                //if error occurs
                if (json["key"] == 0) {
                    alert("Error occured");
                }
                //if specimen and method updated successfully
                else if (json["key"] == 1) {
                    location.reload(true);
                }
                else if (json["key"] == 2) {
                    alert("Specimen and method already exists");
                }

                console.log("success" + response.d);
            },
            error: function (response) {
                console.log("error" + response.d);
            },
            failure: function (response) {
                //console.log("fail" + response.d);
            }
        });

        //location.reload(true);
    }
</script>

<!-- EDIT REFERENCE VALUES -->
<script type="text/javascript">
    function editReferenceValue(obj) {        
        var testSMId = $(obj).attr('title').split("|")[0];
        var testReferenceId = $(obj).attr('title').split("|")[1];
        var referencetype = $(obj).attr('title').split("|")[2];
        var ageRange = $(obj).attr('title').split("|")[3];
        var male = $(obj).attr('title').split("|")[4];
        var female = $(obj).attr('title').split("|")[5];
        var grade = $(obj).attr('title').split("|")[6];
        var units = $(obj).attr('title').split("|")[7];
        var interpretation = $(obj).attr('title').split("|")[8];
        var lowerLimit = $(obj).attr('title').split("|")[9];
        var upperLimit = $(obj).attr('title').split("|")[10];

        var hiddenAnalyteSubAnalyteSMRRefVal = ($(obj).attr('id') == 'btnEditAnalyteSMRRefVal') ? 'analyte' : 'subAnalyte';
        //alert(hiddenAnalyteSubAnalyteSMRRefVal + " " + testSMId + " " + testReferenceId + " " + referencetype + " " + ageRange + " " + male + " " + female + " " + grade + " " + units + " " + interpretation + " " + lowerLimit + " " + upperLimit);

        $("#selReferenceType").val(referencetype);

        var minAge = (ageRange != "" && ageRange != "NA") ? ageRange.split('-')[0].split(' ')[0] : "";
        var maxAge = (ageRange != "" && ageRange != "NA") ? ageRange.split('-')[1].split(' ')[0] : "";

        var minAgeUnit = (ageRange != "" && ageRange != "NA") ? ageRange.split('-')[0].split(' ')[1] : "";
        var maxAgeUnit = (ageRange != "" && ageRange != "NA") ? ageRange.split('-')[1].split(' ')[1] : "";

        $("#txtMinAge").val(minAge);
        $("#selMinAgeUnit").val(minAgeUnit);
        $("#txtMaxAge").val(maxAge);
        $("#selMaxAgeUnit").val(maxAgeUnit);

        var minMaleValue = male.split('-')[0];
        var maxMaleValue = male.split('-')[1];

        $("#txtMinMaleValue").val(minMaleValue);
        $("#txtMaxMaleValue").val(maxMaleValue);

        var minFemaleValue = female.split('-')[0];
        var maxFemaleValue = female.split('-')[1];

        $("#txtMinFemaleValue").val(minFemaleValue);
        $("#txtMaxFemaleValue").val(maxFemaleValue);

        $("#txtGrade").val(grade);
        $("#txtUnits").val(units);
        $("#txtInterpretation").val(interpretation);
        $("#txtLowerLimit").val(lowerLimit);
        $("#txtUpperLimit").val(upperLimit);
        $("#hiddenTestSMId").val(testSMId);
        $("#hiddenTestReferenceId").val(testReferenceId);
        $("#hiddenAnalyteSubAnalyteSMRRefVal").val(hiddenAnalyteSubAnalyteSMRRefVal);
        $("#hiddenCurrentRefValues").val($(obj).attr('title'));

        $("#btnUpdateReferenceValues").show();
        $("#btnAddReferenceValues").hide();
        $("#modalReferenceValues").modal('show');
    }

</script>

<!-- UPDATE REFERENCE VALUES -->
<script type="text/javascript">
    function updateReferenceValues() {
        if (validateReferenceValues()) {

            if ($("#txtMinAge").val() == "0" && $("#txtMaxAge").val() == "0") {
                $("#txtAge").val("NA");
            }
            else if ($("#txtMinAge").val() == "" && $("#txtMaxAge").val() == "") {
                $("#txtAge").val("NA");
            }
            else {
                $("#txtAge").val($("#txtMinAge").val() + " " + $("#selMinAgeUnit").val() + "-" + $("#txtMaxAge").val() + " " + $("#selMaxAgeUnit").val());
            }

            if ($("#txtMinMaleValue").val() == "" && $("#txtMaxMaleValue").val() == "") {
                $("#txtMaleRange").val("NA");
            }
            else {
                $("#txtMaleRange").val($("#txtMinMaleValue").val() + "-" + $("#txtMaxMaleValue").val());
            }

            if ($("#txtMinFemaleValue").val() == "" && $("#txtMaxFemaleValue").val() == "") {
                $("#txtFemaleRange").val("NA");
            }
            else {
                $("#txtFemaleRange").val($("#txtMinFemaleValue").val() + "-" + $("#txtMaxFemaleValue").val());
            }

            if ($("#txtGrade").val() == "") {
                $("#txtGrade").val("NA");
            }

            if ($("#txtUnits").val() == "") {
                $("#txtUnits").val("NA");
            }

            if ($("#txtInterpretation").val() == "") {
                $("#txtInterpretation").val("NA");
            }

            if ($("#txtLowerLimit").val() == "") {
                $("#txtLowerLimit").val("NA");
            }

            if ($("#txtUpperLimit").val() == "") {
                $("#txtUpperLimit").val("NA");
            }

            var referencetype = $("#hiddenCurrentRefValues").val().split("|")[2];
            var ageRange = $("#hiddenCurrentRefValues").val().split("|")[3];
            var male = $("#hiddenCurrentRefValues").val().split("|")[4];
            var female = $("#hiddenCurrentRefValues").val().split("|")[5];
            var grade = $("#hiddenCurrentRefValues").val().split("|")[6];
            var units = $("#hiddenCurrentRefValues").val().split("|")[7];
            var interpretation = $("#hiddenCurrentRefValues").val().split("|")[8];
            var lowerLimit = $("#hiddenCurrentRefValues").val().split("|")[9];
            var upperLimit = $("#hiddenCurrentRefValues").val().split("|")[10];            
            
            //if new values are same as previous values
            if (referencetype == $("#selReferenceType").val() &&
                ageRange == $("#txtAge").val() &&
                male == $("#txtMaleRange").val() &&
                female == $("#txtFemaleRange").val() &&
                grade == $("#txtGrade").val() &&
                units == $("#txtUnits").val() &&
                interpretation == $("#txtInterpretation").val() &&
                lowerLimit == $("#txtLowerLimit").val() &&
                upperLimit == $("#txtUpperLimit").val()
            ) {
                alert("No change in values");
                return false;
            }
            
            var parameter = { "insertInto": $("#hiddenAnalyteSubAnalyteSMRRefVal").val(), "testSMId": $("#hiddenTestSMId").val(), "testReferenceId": $("#hiddenTestReferenceId").val(), "referenceType": $("#selReferenceType").val(), "ageRange": $("#txtAge").val(), "male": $("#txtMaleRange").val(), "female": $("#txtFemaleRange").val(),"grade":$("#txtGrade").val(),"units":$("#txtUnits").val(),"interpretation":$("#txtInterpretation").val(),"lowerLimit":$("#txtLowerLimit").val(),"upperLimit":$("#txtUpperLimit").val() };

            $.ajax({
                type: "POST",
                url: "EditTest.aspx/updateReferenceValues",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                //dataType: "text/plain",
                success: function (response) {
                    var json = JSON.parse(response.d);

                    //if error occurs
                    if (json["key"] == 0) {
                        alert("Error occured");
                    }
                    //if specimen and method updated successfully
                    else if (json["key"] == 1) {
                        location.reload(true);
                    }
                    else if (json["key"] == 2) {
                        alert("Reference already exists");
                    }

                    console.log("success" + response.d);
                },
                error: function (response) {
                    console.log("error" + response.d);
                },
                failure: function (response) {
                    //console.log("fail" + response.d);
                }
            });
        }
    }
</script>

<!-- ADD REFERENCE VALUES -->
<script type="text/javascript">
    function openReferenceValueModal(obj) {        
        $("#selReferenceType").val("");
        $("#txtMinAge").val("");
        $("#selMinAgeUnit").val("");
        $("#txtMaxAge").val("");
        $("#selMaxAgeUnit").val("");
        $("#txtMinMaleValue").val("");
        $("#txtMaxMaleValue").val("");
        $("#txtMinFemaleValue").val("");
        $("#txtMaxFemaleValue").val("");
        $("#txtGrade").val("");
        $("#txtUnits").val("");
        $("#txtInterpretation").val("");
        $("#txtLowerLimit").val("");
        $("#txtUpperLimit").val("");
        $("#hiddenTestSMId").val($(obj).attr('title'));

        var hiddenAnalyteSubAnalyteSMRRefVal = ($(obj).attr('id') == 'btnAddAnalyteSMRRefVal') ? 'analyte' : 'subAnalyte';
        $("#hiddenAnalyteSubAnalyteSMRRefVal").val(hiddenAnalyteSubAnalyteSMRRefVal);

        $("#btnUpdateReferenceValues").hide();
        $("#btnAddReferenceValues").show();
        $("#modalReferenceValues").modal('show');
    }

    function addReferenceValues() {
        if (validateReferenceValues()) {

            if ($("#txtMinAge").val() == "0" && $("#txtMaxAge").val() == "0") {
                $("#txtAge").val("NA");
            }
            else if ($("#txtMinAge").val() == "" && $("#txtMaxAge").val() == "") {
                $("#txtAge").val("NA");
            }
            else {
                $("#txtAge").val($("#txtMinAge").val() + " " + $("#selMinAgeUnit").val() + "-" + $("#txtMaxAge").val() + " " + $("#selMaxAgeUnit").val());
            }

            if ($("#txtMinMaleValue").val() == "" && $("#txtMaxMaleValue").val() == "") {
                $("#txtMaleRange").val("NA");
            }
            else {
                $("#txtMaleRange").val($("#txtMinMaleValue").val() + "-" + $("#txtMaxMaleValue").val());
            }

            if ($("#txtMinFemaleValue").val() == "" && $("#txtMaxFemaleValue").val() == "") {
                $("#txtFemaleRange").val("NA");
            }
            else {
                $("#txtFemaleRange").val($("#txtMinFemaleValue").val() + "-" + $("#txtMaxFemaleValue").val());
            }

            if ($("#txtGrade").val() == "") {
                $("#txtGrade").val("NA");
            }

            if ($("#txtUnits").val() == "") {
                $("#txtUnits").val("NA");
            }

            if ($("#txtInterpretation").val() == "") {
                $("#txtInterpretation").val("NA");
            }

            if ($("#txtLowerLimit").val() == "") {
                $("#txtLowerLimit").val("NA");
            }

            if ($("#txtUpperLimit").val() == "") {
                $("#txtUpperLimit").val("NA");
            }

            var parameter = { "insertInto": $("#hiddenAnalyteSubAnalyteSMRRefVal").val(), "testSMId": $("#hiddenTestSMId").val(), "referenceType": $("#selReferenceType").val(), "ageRange": $("#txtAge").val(), "male": $("#txtMaleRange").val(), "female": $("#txtFemaleRange").val(), "grade": $("#txtGrade").val(), "units": $("#txtUnits").val(), "interpretation": $("#txtInterpretation").val(), "lowerLimit": $("#txtLowerLimit").val(), "upperLimit": $("#txtUpperLimit").val() };

            $.ajax({
                type: "POST",
                url: "EditTest.aspx/addReferenceValues",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                //dataType: "text/plain",
                success: function (response) {
                    var json = JSON.parse(response.d);

                    //if error occurs
                    if (json["key"] == 0) {
                        alert("Error occured");
                    }
                    //if specimen and method updated successfully
                    else if (json["key"] == 1) {
                        location.reload(true);
                    }
                    else if (json["key"] == 2) {
                        alert("Reference already exists");
                    }

                    console.log("success" + response.d);
                },
                error: function (response) {
                    console.log("error" + response.d);
                },
                failure: function (response) {
                    //console.log("fail" + response.d);
                }
            });
        }
    }

</script>

<!-- LOAD MAX AGE UNIT BASED ON MIN AGE UNIT -->
<script type="text/javascript">
    function loadMaxAgeUnit(obj) {

        $("#selMaxAgeUnit").val("");

        $("#selMaxAgeUnit > option").each(function () {
            $(this).show();
        });

        if ($(obj).val() == "year") {
            $("#selMaxAgeUnit > option").each(function () {
                if (this.value == "day" || this.value == "week" || this.value == "month") {
                    $(this).hide();
                }
            });
        }
        if ($(obj).val() == "month") {
            $("#selMaxAgeUnit > option").each(function () {
                if (this.value == "day" || this.value == "week") {
                    $(this).hide();
                }
            });
        }
        if ($(obj).val() == "week") {
            $("#selMaxAgeUnit > option").each(function () {
                if (this.value == "day") {
                    $(this).hide();
                }
            });
        }
    }
</script>

<!-- REMOVE REFERENCE VALUES -->
<script type="text/javascript">
    function removeReferenceValue(obj) {
        var testReferenceId = $(obj).attr('title');
        var hiddenAnalyteSubAnalyteSMRRefVal = ($(obj).attr('id') == 'btnDeleteAnalyteSMRRefVal') ? 'analyte' : 'subAnalyte';

        //alert(hiddenAnalyteSubAnalyteSMRRefVal + " " + testReferenceId);

        if (confirm("Are you sure you want to delete ?")) {
            var parameter = { "deleteFrom": hiddenAnalyteSubAnalyteSMRRefVal, "testReferenceId": testReferenceId };

            $.ajax({
                type: "POST",
                url: "EditTest.aspx/deleteReferenceValues",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                //dataType: "text/plain",
                success: function (response) {
                    var json = JSON.parse(response.d);

                    //if error occurs
                    if (json["key"] == 0) {
                        alert("Error occured");
                    }
                    //if specimen and method updated successfully
                    else if (json["key"] == 1) {
                        $(obj).closest('p').remove();
                    }

                    //console.log("success" + response.d);
                },
                error: function (response) {
                    console.log("error" + response.d);
                },
                failure: function (response) {
                    //console.log("fail" + response.d);
                }
            });
        }
    }
</script>

<!-- REMOVE SMR VALUES -->
<script type="text/javascript">
    function removeSMR(obj) {
        var testSMId = $(obj).attr('title');
        var hiddenAnalyteSubAnalyteSMR = ($(obj).attr('id') == 'btnDeleteAnalyteSMR') ? 'analyte' : 'subAnalyte';

        //alert(hiddenAnalyteSubAnalyteSMR + " " + testSMId);

        if (confirm("If you delete this specimen, it's corresponding references will also be deleted. Are you sure you want to delete ?")) {
            var parameter = { "deleteFrom": hiddenAnalyteSubAnalyteSMR, "testSMId": testSMId };

            $.ajax({
                type: "POST",
                url: "EditTest.aspx/deleteSMR",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                //dataType: "text/plain",
                success: function (response) {
                    var json = JSON.parse(response.d);

                    //if error occurs
                    if (json["key"] == 0) {
                        alert("Error occured");
                    }
                    //if specimen and method updated successfully
                    else if (json["key"] == 1) {
                        //$(obj).closest('tr').remove();
                        location.reload(true);
                    }

                    //console.log("success" + response.d);
                },
                error: function (response) {
                    console.log("error" + response.d);
                },
                failure: function (response) {
                    //console.log("fail" + response.d);
                }
            });
        }
    }
</script>


<!-- LOAD PROFILE LIST BASED ON SECTION SELECTION-->
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
    }
</script>

<!-- REFERENCE VALUES VALIDATION -->
<script type="text/javascript">
    function validateReferenceValues() {

        if ($("#selReferenceType").val() == "" &&
           $("#txtMinAge").val() == "" &&
           $("#txtMaxAge").val() == "" &&
           $("#selMinAgeUnit").val() == "" &&
           $("#selMaxAgeUnit").val() == "" &&
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

            if ($("#txtMinAge").val() == "" || $("#txtMaxAge").val() == "") {
                alert("Please enter age range. If not applicable enter 0");
                return false;
            }
            else {
                //if minAgeDay start
                if ($("#selMinAgeUnit").val() == "day") {
                    if ($("#selMaxAgeUnit").val() == "day") {
                        if (parseInt($("#txtMinAge").val()) > parseInt($("#txtMaxAge").val())) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                    else if ($("#selMaxAgeUnit").val() == "week") {
                        if (parseInt($("#txtMinAge").val()) > (parseInt($("#txtMaxAge").val()) * 7)) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                    else if ($("#selMaxAgeUnit").val() == "month") {
                        if (parseInt($("#txtMinAge").val()) > (parseInt($("#txtMaxAge").val()) * 31)) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                    else if ($("#selMaxAgeUnit").val() == "year") {
                        if (parseInt($("#txtMinAge").val()) > (parseInt($("#txtMaxAge").val()) * 365)) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                }
                //if minAgeDay end
                //if minAgeWeek start
                else if ($("#selMinAgeUnit").val() == "week") {
                    if ($("#selMaxAgeUnit").val() == "week") {
                        if (parseInt($("#txtMinAge").val()) > parseInt($("#txtMaxAge").val())) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                    else if ($("#selMaxAgeUnit").val() == "month") {
                        if (parseInt($("#txtMinAge").val()) > (parseInt($("#txtMaxAge").val()) * 4)) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                    else if ($("#selMaxAgeUnit").val() == "year") {
                        if (parseInt($("#txtMinAge").val()) > (parseInt($("#txtMaxAge").val()) * 52)) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                }
                //if minAgeWeek end
                //if minAgeMonth start
                else if ($("#selMinAgeUnit").val() == "month") {
                    if ($("#selMaxAgeUnit").val() == "month") {
                        if (parseInt($("#txtMinAge").val()) > parseInt($("#txtMaxAge").val())) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                    else if ($("#selMaxAgeUnit").val() == "year") {
                        if (parseInt($("#txtMinAge").val()) > (parseInt($("#txtMaxAge").val()) * 12)) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                }
                //if minAgeMonth end
                //if minAgeYear start
                else if ($("#selMinAgeUnit").val() == "year") {
                    if ($("#selMaxAgeUnit").val() == "year") {
                        if (parseInt($("#txtMinAge").val()) > parseInt($("#txtMaxAge").val())) {
                            alert("Min age cannot be greater that max age");
                            return false;
                        }
                    }
                }
                //if minAgeYear end
            }

            if ($("#selMinAgeUnit").val() == "") {

                if (parseInt($("#txtMinAge").val()) != 0 || parseInt($("#txtMaxAge").val()) != 0) {
                    alert("Please select unit for min age");
                    return false;
                }
            }
            if ($("#selMaxAgeUnit").val() == "") {

                if (parseInt($("#txtMinAge").val()) != 0 || parseInt($("#txtMaxAge").val()) != 0) {
                    alert("Please select unit for max age");
                    return false;
                }
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


<!-- VALIDATE ADD SMR -->
<script type="text/javascript">
    function validateAddSMR() {
        if ($("#selAnalyte").val() == "" || $("#selAnalyte").val() == null) {
            $("#lblAnalyte").html("Select analyte");
            return false;
        }
        else if ($("#txtAddSpecimenName").val() == "" || $("#txtAddSpecimenName").val() == null) {
            $("#lblAddSpecimenName").html("Enter specimen");
            return false;
        }
        else if ($("#txtAddQuantity").val() == "" || $("#txtAddQuantity").val() == null) {
            $("#lblAddQuantity").html("Enter quantity");
            return false;
        }
        else if ($("#txtAddTimePeriod").val() == "" || $("#txtAddTimePeriod").val() == null) {
            $("#lblAddTimePeriod").html("Enter time period");
            return false;
        }
        else if ($("#txtAddMethod").val() == "" || $("#txtAddMethod").val() == null) {
            $("#lblAddMethod").html("Enter method");
            return false;
        }
        else if ($("#selAddResultType").val() == "" || $("#selAddResultType").val() == null) {
            $("#lblAddResultType").html("Enter result type");
            return false;
        }
        else {
            return true;
        }
    }
</script>

<!-- ADD ANALYTE -->
<script type="text/javascript">
    function openAddAnalyteModal() {
        $("#txtAnalyteName").val("");
        $("#hiddenAnalyteId").val("");

        //$("#lstAnalyte").append("<option value='AbC'/>");
        var parameter = {"testId":$("#hiddenTestId").val()};
        $.ajax({
            type: "POST",
            url: "EditTest.aspx/getAnalytes",
            data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            //dataType: "text/plain",
            success: function (response) {
                var json = JSON.parse(response.d);
                
                for (var i = 0; i < json.length; i++) {
                    $("#lstAnalyte").append("<option value='" + json[i]["analyteName"] + "' data-id='" + json[i]["analyteId"] + "'/>");
                }

                //console.log("success" + response.d);
            },
            error: function (response) {
                console.log("error" + response.d);
            },
            failure: function (response) {
                //console.log("fail" + response.d);
            }
        });

        
         $("#modalAddAnalyte").modal('show');
     }

     function onAnalyteSelect(obj) {
         var analyteName = $(obj).val();
         $("#hiddenAnalyteId").val("");

         if (analyteName != "") {
             $("#lstAnalyte").find("option").each(function () {
                 
                 if ($(this).val().toUpperCase() == analyteName.toUpperCase()) {
                     $("#hiddenAnalyteId").val($(this).attr('data-id'));
                 }
             });
         }
     }

    function addAnalyte() {
        var analyteName=$("#txtAnalyteName").val();
        var analyteId=$("#hiddenAnalyteId").val();
        var testId = $("#hiddenTestId").val();

        var hiddenAnalyteSMRRefVal = JSON.parse($("#hiddenAnalyteSMRRefVal").val());

        for (var i = 0; i < hiddenAnalyteSMRRefVal.length; i++) {
            //console.log(hiddenAnalyteSMRRefVal[i]["analyteName"] + " - " + hiddenAnalyteSMRRefVal[i]["analyteId"]);
            if (hiddenAnalyteSMRRefVal[i]["analyteName"].toUpperCase() == analyteName.toUpperCase()) {
                alert("Analyte already exists");
                return false;
            }
        }


        var parameter = { "testId": $("#hiddenTestId").val(), "analyteId": analyteId, "analyteName": analyteName };
        $.ajax({
            type: "POST",
            url: "EditTest.aspx/addAnalyte",
            data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            //dataType: "text/plain",
            success: function (response) {
                var json = JSON.parse(response.d);

                //if error occurs
                if (json["key"] == 0) {
                    alert("Error occured");
                }
                //if analyte added successfully
                else if (json["key"] == 1) {
                    location.reload(true);
                }

                //console.log("success" + response.d);
            },
            error: function (response) {
                console.log("error" + response.d);
            },
            failure: function (response) {
                //console.log("fail" + response.d);
            }
        });

    }
</script>


<!-- ADD SUB ANALYTE -->
<script type="text/javascript">
    function openAddSubAnalyteModal() {
        $("#selAnalyteName").empty();
        $("#selSubAnalyteName").empty();

        var parameter = { "testId": $("#hiddenTestId").val() };
        $.ajax({
            type: "POST",
            url: "EditTest.aspx/getAnalyteSubAnalyte",
            data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            //dataType: "text/plain",
            success: function (response) {
                var json = JSON.parse(response.d);

                var analytes = new Array();

                $("#selAnalyteName").append("<option value=''>Select Analyte</option>");
                $("#selSubAnalyteName").append("<option value=''>Select SubAnalyte</option>");

                for (var i = 0; i < json.length; i++) {

                    if ($.inArray(json[i]["analyteId"], analytes) == -1) {
                        $("#selAnalyteName").append("<option value='" + json[i]["analyteId"] + "'>" + json[i]["analyteName"] + "</option>");
                        analytes.push(json[i]["analyteId"]);
                    }

                    $("#selSubAnalyteName").append("<option value='" + json[i]["analyteId"] + "|" + json[i]["subAnalyteId"] + "'>" + json[i]["subAnalyteName"] + "</option>");
                }

                $("#selSubAnalyteName option").each(function (i) {
                    if ( $(this).val() != "") {
                        $(this).hide();
                    }
                });

                //console.log("success" + response.d);
            },
            error: function (response) {
                console.log("error" + response.d);
            },
            failure: function (response) {
                //console.log("fail" + response.d);
            }
        });


        $("#modalAddSubAnalyte").modal('show');
    }

    function analyteSelectedToAddSubAnalyte(obj) {
        var analyteId = $(obj).val();

        $('#selSubAnalyteName').find('option').show();
        $('#selSubAnalyteName').find('option[value=""]').prop('selected', true);

        $("#selSubAnalyteName option").each(function (i) {
            if ($(this).val().split('|')[0] != analyteId && $(this).val() != "") {
                $(this).hide();
            }
        });
    }

    function addSubAnalyte() {
        var analyteId = $("#selAnalyteName").val();
        var subAnalyteId = $("#selSubAnalyteName").val().split("|")[1];
        var testId = $("#hiddenTestId").val();

        if (analyteId == "" || analyteId == null) {
            $("#lblAddAnalyteName").html("Select analyte");
            return false;
        }
        else if (subAnalyteId == "" || subAnalyteId == null) {
            $("#lblAddSubAnalyteName").html("Select subanalyte");
            return false;
        }

        var parameter = { "testId": $("#hiddenTestId").val(), "subAnalyteId": subAnalyteId };
        $.ajax({
            type: "POST",
            url: "EditTest.aspx/addSubAnalyte",
            data: JSON.stringify(parameter),
            contentType: "application/json; charset=utf-8",
            //dataType: "text/plain",
            success: function (response) {
                var json = JSON.parse(response.d);

                //if error occurs
                if (json["key"] == 0) {
                    alert("Error occured");
                }
                //if analyte added successfully
                else if (json["key"] == 1) {
                    location.reload(true);
                }

                //console.log("success" + response.d);
            },
            error: function (response) {
                console.log("error" + response.d);
            },
            failure: function (response) {
                //console.log("fail" + response.d);
            }
        });

    }
</script>

</asp:Content>

