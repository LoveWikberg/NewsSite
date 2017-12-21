$(function () {
    fillDropdownWithUserNames();

    $('#login').click(function () {
        var user = $('#selectedUser').val();
        signIn(user);
    });

    $('#opennews').click(function () {
        ajaxCall("news/openNews", "get");
    });

    $('#hiddennews').click(function () {
        ajaxCall("news/hiddennews", "get");
    });

    $('#hiddenandage').click(function () {
        ajaxCall("news/agelimitnews", "get");
    });

    $('#recreate').click(function () {
        ajaxCall('user/recreateUsers', "post");
    });

    $('#userswithclaims').click(function () {
        getUsersWithClaims();
    });

    $('#sportsnews').click(function () {
        ajaxCall("news/sportsNews", "get");
    });

    $('#culturenews').click(function () {
        ajaxCall("news/cultureNews", "get");
    });

    $('#disableAlerts').change(function () {
        if ($('#disableAlerts').is(":checked"))
            disableAlert = true;
        else
            disableAlert = false;
    })
});

var disableAlert = false;

function ajaxCall(url, method) {
    $.ajax({
        url: url,
        method: method.toUpperCase()
    })
        .done(function (result) {
            alertMsg("success");
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            alertMsg("fail");
            console.log(xhr, status, error);
        });
}

function getUsersWithClaims() {
    $.ajax({
        url: "user/usersWithClaims",
        method: "GET"
    })
        .done(function (result) {
            alertMsg("success | Resultatet finns i konsolen");
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            alertMsg("fail");
            console.log(xhr, status, error);
        });
}

function signIn(user) {
    $.ajax({
        url: "user/signin",
        method: "POST",
        data: { userName: user }
    })
        .done(function (result) {
            alertMsg("success");
        })
        .fail(function (xhr, status, error) {
            alertMsg("fail");
            console.log(xhr, status, error);
        });
}

function fillDropdownWithUserNames() {
    $.ajax({
        url: "user/allUsers",
        method: "GET"
    })
        .done(function (result) {
            console.log(result);
            var html = "";
            result.forEach(function (username) {
                html += getHtmlForSelect(username);
            });
            $("select").html(html);
        })
        .fail(function (xhr, status, error) {
            alertMsg("Failed to load usernames");
            console.log(xhr, status, error);
        })
        .always(function () {
            $('#usersLoader').remove();
        });
}

function getHtmlForSelect(content) {
    var html = '<option>' + content + '</option>';
    return html;
}

function alertMsg(message) {
    if (!disableAlert)
        alert(message)
}