$(document).ready(function() {
    $(document).on('click', '.btn-search-people', function() {
        $('#form-search-all').submit();
    });

    $('#form-search-all').submit(function() {
        var input = $('.input-search');
        console.log(input.val().trim());
        if(input.val().trim() === "") {
            return false;
        }
        return true;
    });

    function checkSubmitFormSeach() {
        var input = $('.input-search');
        console.log(input.val().trim());
        if(input.val().trim() === "") {
            return false;
        }
        return true;
    }
});