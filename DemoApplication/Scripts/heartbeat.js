$(document).ready(function () {
    setInterval(function () {
        $("#status").attr("class", "glyphicon glyphicon-refresh");
        $.get("/api/heartbeat").done(function (data) {
            $("#heartbeat").text(data);
            $("#status").attr("class", "glyphicon glyphicon-ok");
        }).fail(function (xhr, error, status) {
            $("#heartbeat").text("failed to connect");
            $("#status").attr("class", "glyphicon glyphicon-remove");
        });
    }, 1000);
});
