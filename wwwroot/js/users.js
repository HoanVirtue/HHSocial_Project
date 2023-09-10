$(document).ready(function() {
    $('#btnRegister').on('click', function() {
        var formData = new FormData();
        formData.append("FirstName", $('#firstName').val());
        formData.append("LastName", $('#lastName').val());
        formData.append("Email", $('#email').val());
        formData.append("Password", $('#password').val());
        formData.append("RetyePassword", $('#retyptePassword').val());
        formData.append("GenderName", $('input[name="GenderName"]:checked').val());
        
        if(formData.get("FirstName"))
        $.ajax({
            url: '/Users/Register',
            type: 'POST',
            contentType: false,
            processData: false,
            cache: false,
            data: formData,
            success: function(response) {
                if(response.success) {
                    console.log('Add thanh cong');
                } else {
                    console.log('Error them: ', response.errors[0]);
                }
            },
            error: function(error) {
                console.log("Users Error: ", error);
            }
        })
    });

    function validateData(data) {
        if(data == "") {
            console.log('Vui lòng nhập trường ...');
        }
    }
});