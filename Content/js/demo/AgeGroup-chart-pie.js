// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Pie Chart Example
var ctx = document.getElementById("ageGroupPieChart");
var labelName=$('#HAgegroupAmt').val();
var array = labelName.split(",");
var one=array[0];
var Two=array[1];
var Three=array[2];
var Four=array[3];
var Five=array[4];
var testTotalCount=$('#HAgeGroupTotalAmount').val();
var ageGroupPieChart = new Chart(ctx, {
  type: 'pie',
  data: {
    labels: ["0-20", "21-40", "41-60", "61-80", "81-100"],
    datasets: [{         
      backgroundColor: ["#4e73df","#5a89cc","#f27900","#6faf00","#00b5b0"],
      hoverBackgroundColor: ["#2e59d9","#457dcc","#cc6601","#588a02","#01908c"],
      data: [one,Two,Three,Four,Five],  
    }],
  },
  options: {
    
    maintainAspectRatio: false,
    tooltips: {
      backgroundColor: "rgb(255,255,255)",
      bodyFontColor: "#858796",
      borderColor: '#dddfeb',
      borderWidth: 1,
      xPadding: 15,
      yPadding: 15,
      displayColors: false,
      caretPadding: 10,
    },
  
    legend: {
      display: false
    },
  }
});
