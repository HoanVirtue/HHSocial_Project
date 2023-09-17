$(document).ready(function () {
    
    /// <summary>
    /// Event handler: Đăng ký tài khoản
    /// </summary>
    /// <returns>Json</returns>
    /// Authors: Tạ Đức Hoàn
    /// Create: 17/9/2023
    /// Update: 17/9/2023
    $('#btnRegister').on('click', function () {
        var formData = new FormData();
        formData.append("FirstName", $('#firstName').val());
        formData.append("LastName", $('#lastName').val());
        formData.append("Email", $('#email').val());
        formData.append("Password", $('#password').val());
        formData.append("RetyePassword", $('#retyptePassword').val());
        formData.append("Birthday", $('#birthday').val());
        formData.append("GenderName", $('input[name="GenderName"]:checked').val());

        if (formData.get("FirstName"))
            $.ajax({
                url: '/Users/Register',
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.success) {
                        console.log('Add thanh cong');
                    } else {
                        var regError = document.getElementById("reg_error_inner");
                        if (regError !== null) {
                            var message = "";
                            for (var i = 0; i < response.errors.length; i++) {
                                message += response.errors[i];
                                if (i < response.errors.length - 1) {
                                    message += "<br>";
                                }
                            }
                            regError.innerHTML = message;
                            
                        } else {
                            var divReg = document.createElement("div");
                            divReg.id = "reg_error";
                            divReg.classList.add("_58mn", "message_error_register");
                            divReg.role = "alert";
                            divReg.tabIndex = -1;
                            var divItem = document.createElement("div");
                            divReg.id = "reg_error_inner";
                            divReg.classList.add("_58mo");
                            divReg.tabIndex = 0;
                            var message = "";
                            for (var i = 0; i < response.errors.length; i++) {
                                message += response.errors[i];
                                if (i < response.errors.length - 1) {
                                    message += "<br>";
                                }
                            }
                            divItem.innerHTML = message;
                            divReg.appendChild(divItem);

                            var modal = $('#registerModal').find('.modal-body');
                            modal.prepend(divReg);
                        }
                    }
                },
                error: function (error) {
                    console.log("Users Error: ", error);
                }
            })
    });
});