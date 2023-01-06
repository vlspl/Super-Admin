// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

function number_format(number, decimals, dec_point, thousands_sep) {
  // *     example: number_format(1234.56, 2, ',', ' ');
  // *     return: '1 234,56'
  number = (number + '').replace(',', '').replace(' ', '');
  var n = !isFinite(+number) ? 0 : +number,
    prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
    sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
    dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
    s = '',
    toFixedFix = function(n, prec) {
      var k = Math.pow(10, prec);
      return '' + Math.round(n * k) / k;
    };
  // Fix for IE parseFloat(0.55).toFixed(0) = 0;
  s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
  if (s[0].length > 3) {
    s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
  }
  if ((s[1] || '').length < prec) {
    s[1] = s[1] || '';
    s[1] += new Array(prec - s[1].length + 1).join('0');
  }
  return s.join(dec);
}

// Bar Chart Example
var ctx = document.getElementById("ageGroupDonutChart");
var labelName=$('#HAgegroupAmt').val();
var array = labelName.split(",");
var one=array[0];
var Two=array[1];
var Three=array[2];
var Four=array[3];
var Five=array[4];
var testTotalCount=$('#HAgeGroupTotalAmount').val();
var ageGroupDonutChart = new Chart(ctx, {
  type: 'doughnut',
  data: {
    labels: ["0-20", "21-40", "41-60", "61-80", "81-100"],
    datasets: [{
      backgroundColor: ["#4e73df","#5a89cc","#f27900","#6faf00","#00b5b0"],
      hoverBackgroundColor: ["#2e59d9","#457dcc","#cc6601","#588a02","#01908c"],
      // borderColor: "#4e73df",
      data: [one,Two,Three,Four,Five ],
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
    cutoutpercentage:60
  }
});
