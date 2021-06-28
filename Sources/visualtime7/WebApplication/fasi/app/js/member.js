var memberSupport = new function () {
    this.UserFirstVisitChange = function (state) {
        $.ajax({
            type: "POST",
            async: false,
            url: constants.fasiApi.members + 'UserFirstVisitChange?state=' + state,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({}),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
            },
            success: function (data) {
                if (data.Successfully) {
                    console.log("Successfully UserFirstVisitChange");
                }
                else {
                    console.log("Not Successfully UserFirstVisitChange");
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };

    
};