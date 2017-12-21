$(function () {
    fillDropdown();

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
        usersWithClaims();
    });

    $('#sportsnews').click(function () {
        ajaxCall("news/sportsNews", "get");
    });

    $('#culturenews').click(function () {
        ajaxCall("news/cultureNews", "get");
    });
});

function usersWithClaims() {
    $.ajax({
        url: "user/usersWithClaims",
        method: "GET"
    })
        .done(function (result) {
            alert("success");
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            alert("fail");
            console.log(xhr, status, error);
        });
}

function ajaxCall(url, method) {
    $.ajax({
        url: url,
        method: method.toUpperCase()
    })
        .done(function (result) {
            alert("success");
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            alert("fail");
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
            alert("success");
            console.log("Inloggad användare: ", result);
        })
        .fail(function (xhr, status, error) {
            alert("fail");
            console.log(xhr, status, error);
        });
}

function fillDropdown() {
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
            alert("fail");
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

