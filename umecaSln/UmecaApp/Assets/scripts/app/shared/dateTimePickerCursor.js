/**
 * Created by Vmware on 09/01/2015.
 */
jQuery(function ($) {

    $(".date-picker, .input-group-addon").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });


    $('.umeca-time-picker').mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
});

//<script src="${pageContext.request.contextPath}/assets/scripts/app/shared/dateTimePickerCursor.js"></script>