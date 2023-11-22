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

    $('#btn-change-password').on('click', function() {
        var form = $('#form-change-password');
        var formData = new FormData(form[0]);
        var divError = $('.show-error-message');
        validateData(formData);
        changePassword(formData);
    });

    function validateData(formData) {
        var divError = $('.show-error-message');
        if(formData.get("CurrentPassword") == "" || formData.get("NewPassword") == "" || formData.get("NewPassword") == "") {
            divError.html('Dữ liệu được để trống. Vui lòng nhập dữ liệu trước khi cập nhật');
        } else {
            divError.html('');
        }
    }

    function changePassword(formData) {
        $.ajax({
            url: '/Users/ChangePassword',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.isSuccess) {
                    $.toast({
                        heading: 'Success',
                        text: 'Thay đổi mật khẩu thành công.',
                        showHideTransition: 'slide',
                        icon: 'success',
                        position: 'bottom-right'
                    });
                } else {
                    var divError = $('.show-error-message');
                    if(divError.html() != 'Dữ liệu được để trống. Vui lòng nhập dữ liệu trước khi cập nhật') {
                        var strError = "";
                        for(let i = 0; i < response.errors.length; i++) {
                            strError += response.errors[0] + "<br>";
                        }
                        divError.html(strError);
                    }
                    
                    $.toast({
                        heading: 'Cảnh báo',
                        text: 'Lỗi! Thay đổi mật khẩu thất bại.',
                        showHideTransition: 'slide',
                        icon: 'warning',
                        position: 'bottom-right'
                    });
                }
            },
            error: function(error) {

            }
        });
    }

    $(document).on('click', '.upload-media', function() {
        $('.input-media').click();
    })
});