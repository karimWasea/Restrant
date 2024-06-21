$(document).ready(function () {
    $('select').select2();

    $('.nav-link').click(function () {
        $('.nav-link').removeClass('active'); // Remove active class from all links
        $(this).addClass('active'); // Add active class to the clicked link
        $('.nav-link').css('border-color', 'transparent'); // Reset border color for all links
        $(this).css('border-color', 'green'); // Set border color to green for the clicked link
    });

});