<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="LabCalendar.aspx.cs" Inherits="LabCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Lab Calender</a>
      </div>

    <%--  <div class="mr-5">
        <ul class="navbar-nav ml-auto">
         <li class="nav-item pt-1">
            <button class="btn btn-color"><span><i class="fa fa-arrow-left mr-2"
                  area-hidden="true"></i></span>Back</button>
          </li>
        </ul>
      </div>--%>
    </div>
  </nav>
    <div class="container-fluid">
        <div class="points">
            <p>
                <h5>
                    Selected Checkup Location :</h5>
                <h6>
                    <asp:RadioButton ID="atclinic" Checked="true" GroupName="radioCheckup" Text="" CssClass="radio-lab"
                        runat="server" />&nbsp;&nbsp;At Clinic &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                            ID="atHome" GroupName="radioCheckup" Text="" CssClass="radio-lab" runat="server" />&nbsp;&nbsp;At
                    Home</h6>
            </p>
        </div>
    </div>
    <br />
    <!-- ADD SLOTS -->
    <div class="container-fluid">
        <div class="points">
            <p>
                <h5>
                Add Slots</p>
            </h5>
            <div class="table-responsive">
                <table class="table  table-bordered" id="tabSlots">
                    <tbody id="tBodySlots" runat="server">
                        <thead>
                            <tr id="row0">
                                <th style="background-color: #f2f2f2; color: Black; text-align: center;">
                                    Day
                                </th>
                                <th style="background-color: #f2f2f2; color: Black; text-align: center;">
                                    From
                                </th>
                                <th style="background-color: #f2f2f2; color: Black; text-align: center;">
                                    To
                                </th>
                                <th style="background-color: #f2f2f2; color: Black; text-align: center;">
                                    Action
                                </th>
                            </tr>
                        </thead>
                    </tbody>
                </table>
                <nav class="navbar navbar-expand-sm navbar-header p-0 m-0">    
         <ul class="navbar-nav">
               <li class="nav-item mr-3">
                   <asp:HiddenField ID="hiddenSlots" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hiddenTotalAddCount" runat="server" ClientIDMode="Static" />
                  <input id="btnAddSlots" class="btn btn-color" type="button" value="Add" />
                </li>
                <li class="nav-item">
                    <a href="javascript:void(0)" class="btn btn-color" id="HideAddbtn"
                            runat="server" onclick="javascript:submitSlots()">Submit</a>
                        <asp:Button ID="btnSubmitSlots" runat="server" Text="Submit Slots" OnClick="btnSubmitSlots_Click"
                            Style="display: none" ClientIDMode="Static" />
                </li>
              </ul>
            </nav>

            <div class="container">
        <div class="row">
          
            <div class="col-lg-12">
  <div class="table-responsive">
                        <table class="table table-bordered text-small" id="dataTable" width="100%" style="color: #56549b"
                            cellspacing="0">
                            <thead>
                                <tr>
                                 <th style="text-align:center;">
                                        Sr No
                                    </th>
                                    <th style="text-align:center;">
                                      Day
                                    </th>
                                    <th style="text-align:center;">
                                        From
                                    </th>
                                     <th style="text-align:center;">
                                        To
                                    </th>
                                      <th style="text-align:center;">
                                        Type
                                    </th>
                                      <th style="text-align:center;">
                                        Action
                                    </th>
                                   
                                   
                                </tr>
                            </thead>
                            <tbody id="tbodyLabSlots" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
</div>
</div>




                
             
                <script type="text/javascript" src="js/jquery.js"></script>
                <script type="text/javascript" src="js/code.js"></script>
                <script type="text/javascript" src="js/pagination.js"></script>
                <script type="text/javascript">
                    $(document).ready(function () {

                        var totalAddCount = 1;

                        $("#btnAddSlots").click(function () {
                            var rowNumber = ($("#tabSlots tr").length);
                            totalAddCount += 1;

                            $("#hiddenTotalAddCount").val(totalAddCount);
                            var id1 = "selDay" + rowNumber;
                            var id2 = "selFrom" + rowNumber;
                            var id3 = "selTo" + rowNumber;
                            var row = '<tr id="row' + rowNumber + '">' +
                              '<td><select id="' + id1 + '" style="width:100%;height:30px">' +
                                        '<option value="Monday">Monday</option>' +
                                        '<option value="Tuesday">Tuesday</option>' +
                                        '<option value="Wednesday">Wednesday</option>' +
                                        '<option value="Thursday">Thursday</option>' +
                                        '<option value="Friday">Friday</option>' +
                                        '<option value="Saturday">Saturday</option>' +
                                        '<option value="Sunday">Sunday</option>' +
                                    '</select></td>' +

                              '<td><select id="' + id2 + '" style="width:100%;height:30px">' +
                                        '<option  value="0:00">0:00</option>' +
                                         '<option  value="0:30">0:30</option>' +
                                        '<option Value="1:00">1:00</option>' +
                                        '<option Value="1:30">1:30</option>' +
                                        '<option Value="2:00">2:00</option>' +
                                        '<option Value="2:30">2:30</option>' +
                                        '<option Value="3:00">3:00</option>' +
                                        '<option Value="3:30">3:30</option>' +
                                        '<option Value="4:00">4:00</option>' +
                                        '<option Value="4:30">4:30</option>' +
                                        '<option Value="5:00">5:00</option>' +
                                        '<option Value="5:30">5:30</option>' +
                                        '<option  value="6:00">6:00</option>' +
                                        '<option Value="6:30">6:30</option>' +
                                        '<option Value="7:00">7:00</option>' +
                                        '<option Value="7:30">7:30</option>' +
                                        '<option Value="8:00">8:00</option>' +
                                        '<option Value="8:30">8:30</option>' +
                                        '<option Value="9:00">9:00</option>' +
                                        '<option Value="9:30">9:30</option>' +
                                        '<option Value="10:00">10:00</option>' +
                                        '<option Value="10:30">10:30</option>' +
                                        '<option Value="11:00">11:00</option>' +
                                        '<option Value="11:30">11:30</option>' +
                                        '<option Value="12:00">12:00</option>' +
                                        '<option Value="12:30">12:30</option>' +
                                        '<option Value="13:00">13:00</option>' +
                                        '<option Value="13:30">13:30</option>' +
                                        '<option Value="14:00">14:00</option>' +
                                        '<option Value="14:30">14:30</option>' +
                                        '<option Value="15:00">15:00</option>' +
                                        '<option Value="15:30">15:30</option>' +
                                        '<option Value="16:00">16:00</option>' +
                                        '<option Value="16:30">16:30</option>' +
                                        '<option Value="17:00">17:00</option>' +
                                        '<option Value="17:30">17:30</option>' +
                                        '<option Value="18:00">18:00</option>' +
                                        '<option Value="18:30">18:30</option>' +
                                        '<option Value="19:00">19:00</option>' +
                                        '<option Value="19:30">19:30</option>' +
                                        '<option Value="20:00">20:00</option>' +
                                        '<option Value="20:30">20:30</option>' +
                                        '<option Value="21:00">21:00</option>' +
                                        '<option Value="21:30">21:30</option>' +
                                        '<option Value="22:00">22:00</option>' +
                                        '<option Value="22:30">22:30</option>' +
                                         '<option Value="23:00">23:00</option>' +
                                         '<option Value="23:30">23:30</option>' +
                                    '</select></td>' +

                                '<td><select id="' + id3 + '" style="width:100%;height:30px">' +
                                        '<option  value="0:00">0:00</option>' +
                                         '<option  value="0:30">0:30</option>' +
                                        '<option Value="1:00">1:00</option>' +
                                        '<option Value="1:30">1:30</option>' +
                                        '<option Value="2:00">2:00</option>' +
                                        '<option Value="2:30">2:30</option>' +
                                        '<option Value="3:00">3:00</option>' +
                                        '<option Value="3:30">3:30</option>' +
                                        '<option Value="4:00">4:00</option>' +
                                        '<option Value="4:30">4:30</option>' +
                                        '<option Value="5:00">5:00</option>' +
                                        '<option Value="5:30">5:30</option>' +
                                        '<option  value="6:00">6:00</option>' +
                                        '<option Value="6:30">6:30</option>' +
                                        '<option Value="7:00">7:00</option>' +
                                        '<option Value="7:30">7:30</option>' +
                                        '<option Value="8:00">8:00</option>' +
                                        '<option Value="8:30">8:30</option>' +
                                        '<option Value="9:00">9:00</option>' +
                                        '<option Value="9:30">9:30</option>' +
                                        '<option Value="10:00">10:00</option>' +
                                        '<option Value="10:30">10:30</option>' +
                                        '<option Value="11:00">11:00</option>' +
                                        '<option Value="11:30">11:30</option>' +
                                        '<option Value="12:00">12:00</option>' +
                                        '<option Value="12:30">12:30</option>' +
                                        '<option Value="13:00">13:00</option>' +
                                        '<option Value="13:30">13:30</option>' +
                                        '<option Value="14:00">14:00</option>' +
                                        '<option Value="14:30">14:30</option>' +
                                        '<option Value="15:00">15:00</option>' +
                                        '<option Value="15:30">15:30</option>' +
                                        '<option Value="16:00">16:00</option>' +
                                        '<option Value="16:30">16:30</option>' +
                                        '<option Value="17:00">17:00</option>' +
                                        '<option Value="17:30">17:30</option>' +
                                        '<option Value="18:00">18:00</option>' +
                                        '<option Value="18:30">18:30</option>' +
                                         '<option Value="19:00">19:00</option>' +
                                          '<option Value="19:30">19:30</option>' +
                                           '<option Value="20:00">20:00</option>' +
                                            '<option Value="20:30">20:30</option>' +
                                             '<option Value="21:00">21:00</option>' +
                                              '<option Value="21:30">21:30</option>' +
                                               '<option Value="22:00">22:00</option>' +
                                                '<option Value="22:30">22:30</option>' +
                                                '<option Value="23:00">23:00</option>' +
                                                '<option Value="23:30">23:30</option>' +
                                '</select></td>' +
                                '<td class="text-center removebtnclass"><span><input type="button" id="btn' + rowNumber + '" class="removebtn"value="" onclick="javascript:removeRow(this)"/></span></td>' +
                        '</tr>';

                            $("#tabSlots").append(row);
                        });
                    });


                    function submitSlots() {
                        var slots = "";

                        for (i = 1; i < $("#tabSlots tr").length; i++) {
                            if ($("#row" + i).is(":visible")) {
                                var ss = $("#tabSlots tr").length;
                                slots += $("#selDay" + i).val() + "|" + $("#selFrom" + i).val() + "|" + $("#selTo" + i).val() + "@";
                            }
                        }
                        $("#hiddenSlots").val(slots);
                        $("#btnSubmitSlots").click();
                    }

                    function removeRow(row) {
                        $(row).closest('tr').hide();
                    }
                    function removeTimeSlot(obj) {
                        var rowId = "rowSlot" + $(obj).attr('id');
                        $("#" + rowId).hide();

                        var parameter = { "slotId": $(obj).attr('id') };
                        $.ajax({
                            type: "POST",
                            url: "LabCalendar.aspx/removeSlot",
                            data: JSON.stringify(parameter),
                            contentType: "application/json; charset=utf-8",
                            //dataType: "text/plain",
                            success: function (response) {

                                if (response.d == "0") {
                                    $("#" + rowId).show();

                                }
                                console.log("success" + response.d);
                                location.reload(true);
                            },
                            error: function (response) {
                                console.log("error" + response.d);
                            },
                            failure: function (response) {
                                console.log("fail" + response.d);
                            }
                        });
                    }
                </script>
</asp:Content>
