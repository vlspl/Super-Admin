// Call the dataTables jQuery plugin

$(document).ready(function() {
  oTable= $('#dataTable').DataTable({
    
    "bPaginate": true,
    "bLengthChange": false,
    "bFilter": true,
    "bInfo": false,
    "bAutoWidth": false 
             });
 
   $('#myInputTextField').keyup(function(){
    oTable.search($(this).val()).draw() ;
   })
  
});
