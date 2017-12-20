$(function () {
    fillDropdown();

    $('#login').click(function () {
        var user = $('#selectedUser').val();
        signIn(user);
    });

    $('#opennews').click(function () {
        ajaxCall("openNews", "get");
    });

    $('#hiddennews').click(function () {
        ajaxCall("hiddennews", "get");
    });

    $('#hiddenandage').click(function () {
        alert("tja");
        ajaxCall("agelimitnews", "get");
    });

    $('#recreate').click(function () {
        ajaxCall('recreateUsers', "post");
    });

    $('#userswithclaims').click(function () {
        usersWithClaims();
    });

    $('#sportsnews').click(function () {
        ajaxCall("sportsNews", "get");
    });

    $('#culturesnews').click(function () {
        ajaxCall("cultureNews", "get");
    });
});

function usersWithClaims() {
    $.ajax({
        url: "check/usersWithClaims",
        method: "GET",
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
        url: "check/" + url,
        method: method.toUpperCase()
    })
        .done(function (result) {
            alert("success");
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            alert("fail");
            console.log(xhr, static, error);
        })
};

function signIn(user) {
    $.ajax({
        url: "check/signin",
        method: "GET",
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
};

function fillDropdown() {
    $.ajax({
        url: "check/allUsers",
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
            console.log(xhr, static, error);
        })
};

function getHtmlForSelect(content) {
    var html = '<option>' + content + '</option>';
    return html;
};

