$(document).ready(function()
{
    $('#loading-indicator').spin({
        lines: 10,              // The number of lines to draw
        length: 8,              // The length of each line
        width: 4,               // The line thickness
        radius: 8,              // The radius of the inner circle
        scale: 1,               // Scales overall size of the spinner
        corners: 1,             // Corner roundness (0..1)
        color: '#1C2EEF',       // #rgb or #rrggbb or array of colors
        opacity: 0.25,          // Opacity of the lines
        rotate: 0,              // The rotation offset
        direction: 1,           // 1: clockwise, -1: counterclockwise
        speed: 1,               // Rounds per second
        trail: 60,              // Afterglow percentage
        fps: 20,                // Frames per second when using setTimeout() as a fallback for CSS
        zIndex: 2e9,            // The z-index (defaults to 2000000000)
        className: 'spinner',   // The CSS class to assign to the spinner
        top: '50%',             // Top position relative to parent
        left: '50%',            // Left position relative to parent
        shadow: false,          // Whether to render a shadow
        hwaccel: false,         // Whether to use hardware acceleration
        position: 'fixed'       // Element positioning
    });  // spinner initialization
});


// FUNCTIONS //////////////////////////////////////////////////////////////////////////////////////
function BlockUi()
{
    $.blockUI({ message: null });
    $("#loading-indicator").removeAttr("style");
};
function UnblockUi()
{
    $.unblockUI();  // unblock UI when ajax activity stops
    $("#loading-indicator").attr("style", "display: none;"); // hide loading indicator when ajax activity stops
};