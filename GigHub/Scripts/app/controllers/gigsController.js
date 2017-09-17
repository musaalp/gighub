var GigsController = function (attendanceService) {

    var goingButton;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        goingButton = $(e.target);

        var gigId = goingButton.attr("data-gig-id");

        if (goingButton.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);

    };

    var done = function () {
        var text = (goingButton.text() == "Going") ? "Going?" : "Going";

        goingButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed");
    };

    return {
        init: init
    }

}(AttendanceService);