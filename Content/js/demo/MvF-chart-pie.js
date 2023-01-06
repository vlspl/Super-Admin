// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Pie Chart Example
var ctx = document.getElementById("MaleVsFemalePieChart");
var labelName=$('#HGender').val();
var array = labelName.split(",");
var one=array[0];
var Two=array[1];

var testCount=$('#HTestCountGender').val();
var array1 = testCount.split(",");
var one1=array1[0];
var Two1=array1[1];
var testTotalCount=$('#HTotalGenderTestCount').val();
var maleVsFemalePieChart = new Chart(ctx, {
  type: 'pie',
  data: {
    labels: [one,Two],
    datasets: [{
       data: [one1,Two1],
      backgroundColor: ['#4e73df', '#00b5b0'],
      hoverBackgroundColor: ['#2e59d9', '#01908c'],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
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
  
  },
});
