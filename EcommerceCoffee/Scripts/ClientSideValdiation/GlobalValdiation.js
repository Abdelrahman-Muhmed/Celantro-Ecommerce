$(document).ready(function () {
    $('.form-control').on("copy paste cut", function (e) {
        //$("#messageForGlobal").text("Copy, paste, and cut actions are disabled.");

        e.preventDefault();
    });
    $('.form-control').on("contextmenu", function (e) {
        e.preventDefault();
        //$("#messageForGlobal").text("Right-click is disabled.");
    });
    $('.form-control').on("drop", function (e) {
        e.preventDefault();
        //$("#messageForGlobal").text("Drag and drop actions are disabled.");
    });
})