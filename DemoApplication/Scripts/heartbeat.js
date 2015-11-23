$(document).ready(function () {
    setInterval(function () {
        $.get("/api/heartbeat").done(function (data) {
            $(heartbeat).text(data);
        }).fail(function (xhr, error, status) {
            $(heartbeat).text("failed to connect");
        });
    }, 1000);
});
