$(document).ready(function() {
    $(".upload-button").on("click", function() {
        $('input[name="fileImage"]').click();
    });

    $('.file-upload-h').on("change", function(e) {
        const file = e.target.files[0];

        if(file) {
            const reader = new FileReader();
            reader.onload = function(event) {
                $('.profile-edit-pic').attr("src", event.target.result);
            }
            reader.readAsDataURL(file);
        }
    });
});