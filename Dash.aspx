<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="Dash.aspx.cs" Inherits="SuperAdmin_Dash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style>

.circle-tile {
    margin-bottom: 15px;
    text-align: center;
}
.circle-tile-heading {
    border: 3px solid rgba(255, 255, 255, 0.3);
    border-radius: 100%;
    color: #FFFFFF;
    height: 80px;
    margin: 0 auto -40px;
    position: relative;
    transition: all 0.3s ease-in-out 0s;
    width: 80px;
}
.circle-tile-heading .fa {
    line-height: 80px;
}
.circle-tile-content {
    padding-top: 50px;
}
.circle-tile-number {
    font-size: 26px;
    font-weight: 700;
    line-height: 1;
    padding: 5px 0 15px;
}
.circle-tile-description {
    text-transform: uppercase;
}
.circle-tile-footer {
    background-color: rgba(0, 0, 0, 0.1);
    color: rgba(255, 255, 255, 0.5);
    display: block;
    padding: 5px;
    transition: all 0.3s ease-in-out 0s;
}
.circle-tile-footer:hover {
    background-color: rgba(0, 0, 0, 0.2);
    color: rgba(255, 255, 255, 0.5);
    text-decoration: none;
}
.circle-tile-heading.dark-blue:hover {
    background-color: #2E4154;
}
.circle-tile-heading.green:hover {
    background-color: #138F77;
}
.circle-tile-heading.orange:hover {
    background-color: #DA8C10;
}
.circle-tile-heading.blue:hover {
    background-color: #2473A6;
}
.circle-tile-heading.red:hover {
    background-color: #CF4435;
}
.circle-tile-heading.purple:hover {
    background-color: #7F3D9B;
}
.tile-img {
    text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
}

.dark-blue {
    background-color: #34495E;
}
.green {
    background-color: #16A085;
}
.blue {
    background-color: #2980B9;
}
.orange {
    background-color: #F39C12;
}
.red {
    background-color: #E74C3C;
}
.purple {
    background-color: #8E44AD;
}
.dark-gray {
    background-color: #7F8C8D;
}
.gray {
    background-color: #95A5A6;
}
.light-gray {
    background-color: #BDC3C7;
}
.yellow {
    background-color: #F1C40F;
}
.text-dark-blue {
    color: #34495E;
}
.text-green {
    color: #16A085;
}
.text-blue {
    color: #2980B9;
}
.text-orange {
    color: #F39C12;
}
.text-red {
    color: #E74C3C;
}
.text-purple {
    color: #8E44AD;
}
.text-faded {
    color: rgba(255, 255, 255, 0.7);
}


</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


<link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css">
<nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Dashboard</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                       </div>
                     
                  </div>
               </div>
            </nav><br />
<div class="container">
  <div class="row">
  <div class="col-md-12" style="width:96%;">
      <asp:HiddenField ID="hdnrollMasterId" runat="server" />
  <div id="lb" runat="server" visible="false">
    <div class="col-sm-3">
      <div class="circle-tile ">
        <a href="#"><div class="circle-tile-heading dark-blue"><i class="fa fa-flask fa-3x" aria-hidden="true"></i></div></a>
        <div class="circle-tile-content dark-blue">
          <div class="circle-tile-description text-faded"> PathLabs</div>
          <div class="circle-tile-number text-faded "> <asp:Label ID="lbllabs" runat="server" Text="0"></asp:Label></div>
          <a class="circle-tile-footer" href="labsdrilldown.aspx">View More <i class="fa fa-chevron-circle-right"></i></a>
        </div>
      </div>
    </div>
     <div class="col-sm-3">
      <div class="circle-tile ">
        <a href="#"><div class="circle-tile-heading dark-blue"><i class="fa fa-file fa-3x"></i></div></a>
        <div class="circle-tile-content dark-blue">
          <div class="circle-tile-description text-faded"> Pending Approval</div>
          <div class="circle-tile-number text-faded "><asp:Label ID="lblrequestLab" runat="server" Text="0"></asp:Label></div>
          <a class="circle-tile-footer" href="PedingLabApproval.aspx">View More <i class="fa fa-chevron-circle-right"></i></a>
        </div>
      </div>
    </div> 
       <div class="col-sm-3">
      <div class="circle-tile ">
        <a href="#"><div class="circle-tile-heading dark-blue"><i class="fa fa-tasks fa-3x"></i></div></a>
        <div class="circle-tile-content dark-blue">
          <div class="circle-tile-description text-faded"> Packages</div>
          <div class="circle-tile-number text-faded "><asp:Label ID="lblpackages" runat="server" Text="0"></asp:Label></div>
          <a class="circle-tile-footer" href="ViewPackages.aspx">View More <i class="fa fa-chevron-circle-right"></i></a>
        </div>
      </div>
    </div> 
     <div class="col-sm-3">
      <div class="circle-tile ">
        <a href="#"><div class="circle-tile-heading dark-blue"><i class="fa fa-edit fa-3x"></i></div></a>
        <div class="circle-tile-content dark-blue">
          <div class="circle-tile-description text-faded">Assign Packages</div>
          <div class="circle-tile-number text-faded "><asp:Label ID="lblassignpkg" runat="server" Text="0"></asp:Label></div>
          <a class="circle-tile-footer" href="ViewAssignPackages.aspx">View More <i class="fa fa-chevron-circle-right"></i></a>
        </div>
      </div>
    </div>
    </div>
    <div id="org" runat="server" visible="false">
      <div class="col-sm-3">
      <div class="circle-tile ">
        <a href="#"><div class="circle-tile-heading dark-blue"><i class="fa fa-building-o fa-3x"></i></div></a>
        <div class="circle-tile-content dark-blue">
          <div class="circle-tile-description text-faded"> Organizations</div>
          <div class="circle-tile-number text-faded "><asp:Label ID="lblorganizations" runat="server" Text="0"></asp:Label></div>
          <a class="circle-tile-footer" href="orgDilldown.aspx">View More <i class="fa fa-chevron-circle-right"></i></a>
        </div>
      </div>
    </div>
      <div class="col-sm-3">
      <div class="circle-tile ">
        <a href="#"><div class="circle-tile-heading dark-blue"><i class="fa fa-h-square fa-3x"></i></div></a>
        <div class="circle-tile-content dark-blue">
          <div class="circle-tile-description text-faded"> Health Camp</div>
          <div class="circle-tile-number text-faded "><asp:Label ID="lblhealthcamp" runat="server" Text="0"></asp:Label></div>
          <a class="circle-tile-footer" href="healthcampDilldown.aspx">View More <i class="fa fa-chevron-circle-right"></i></a>
        </div>
      </div>
    </div>
     </div>
     <div class="col-sm-3" id="gov" runat="server" visible="false">
      <div class="circle-tile ">
        <a href="#"><div class="circle-tile-heading dark-blue"><i class="fa fa-home fa-3x"></i></div></a>
        <div class="circle-tile-content dark-blue">
          <div class="circle-tile-description text-faded"> Government</div>
          <div class="circle-tile-number text-faded "><asp:Label ID="lblgov" runat="server" Text="0"></asp:Label></div>
          <a class="circle-tile-footer" href="govDilldown.aspx">View More <i class="fa fa-chevron-circle-right"></i></a>
        </div>
      </div>
    </div> 
    <div class="col-sm-3" id="Div1" runat="server" visible="false">
      <div class="circle-tile ">
        <a href="#"><div class="circle-tile-heading dark-blue"><i class="fa fa-file-o fa-3x"></i></div></a>
        <div class="circle-tile-content dark-blue">
          <div class="circle-tile-description text-faded"> Demo Request</div>
          <div class="circle-tile-number text-faded "><asp:Label ID="lbldemorequest" runat="server" Text="0"></asp:Label></div>
          <a class="circle-tile-footer" href="DemoList.aspx">View More <i class="fa fa-chevron-circle-right"></i></a>
        </div>
      </div>
    </div>
    </div>
  </div> 
</div> 
<br />
<div class="container">
  <div class="row">
  <div class="col-md-12">
    
  </div>
  </div>
  </div>
</asp:Content>
