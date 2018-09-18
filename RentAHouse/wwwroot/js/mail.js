$("#composeForm").submit(function (e) {
    $('modalContact').modal('hide');
    sendMessage($("#modalContact").data().OwnerEmail,
                $("#composeReplyEmail").val(),
                $("#composeSubject").val(),
                $("#composeMessage").val());
    composeTidy()
});

function sendMessage(mailTo, mailReplay, subject, message) {
    emailjs.init("user_G7l9i9QIc10uPu3wdpsMB");

    var service_id = 'gmail';
    var template_id = 'template_XIR57NvG';
    var template_params = {
        email: mailTo,
        replyEmail: mailReplay,
        subject: subject,
        message: message
    };
    emailjs.send(service_id, template_id, template_params);
};

function composeTidy() {
    $('#compose-modal').modal('hide');

    $('#composeReplyEmail').val('');
    $('#composeSubject').val('');
    $('#composeMessage').val('');
}