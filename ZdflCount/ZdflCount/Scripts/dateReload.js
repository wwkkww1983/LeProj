$(".date-btn-query").click(function () {
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;
    window.location.href = location.href + "?startDate=" + startDate + "&endDate=" + endDate;
})