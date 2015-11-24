$(function () {
    // Declare a proxy to reference the hub.
    var chat = $.connection.chatHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (name, message) {
        var encodedMsg = $('<div style="display:block;"/>').text(message);
        // Add the message to the page.

        if ($('#discussion > div').length > 18){
            $('#discussion div:first-child').remove();
        }

        $('#discussion').append(encodedMsg);
    };
    // Set initial focus to message input box.
    $('#message').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            chat.server.send("user", $('#message').val());
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
    });
});

function getPackages(searchTerm) {
    $.get("/api/packages?q=" + searchTerm);
}

$(function () {
    $('#search_1').click(function () { getPackages("Microsoft.ApplicationInsights.Web"); });
    $('#search_2').click(function () { getPackages("Microsoft.ApplicationInsights"); });
    $('#search_3').click(function () { getPackages("Microsoft"); });
});
