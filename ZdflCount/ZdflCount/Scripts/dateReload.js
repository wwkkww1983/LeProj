$(".date-btn-query").click(function () {
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    window.location.href = location.protocol + "//" + location.host + "/" + location.pathname + "?startDate=" + startDate + "&endDate=" + endDate;
})