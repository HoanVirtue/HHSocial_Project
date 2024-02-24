$(document).ready(function() {
    $(document).on('keypress', 'input[name="input-search-mess"]', function() {
        var inputValue = $(this).val();
        var formData = new FormData();
        formData.append("keySearch", inputValue);

        searchUserInChatsByName(formData, $(this));
    });
    
    function searchUserInChatsByName(formData, element) {
        $.ajax({
            url: '/UserMessages/SearchUserInChats',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                var usersHtml = $(element).closest('.chats-dropdown-menu').find('.card-body .chats-search');
                var eleHtml = "";
                if(response.isSuccess) {
                    usersHtml.html('');
                    response.listUser.forEach(user => {
                        eleHtml += `
                        <a href="" class="sub-card">
                            <div class="d-flex align-items-center">
                                <div class="">
                                    <img src="~/images/user/avatar.png" alt="" class="avatar-40 rounded">
                                </div>
                                <div class="ms-3 w-100">
                                    <h6 style="font-weight: 500;font-size: 15px;">${user.sourceUser.userName}</h6>
                                </div>
                            </div>
                        </a>
                        `;
                    });
                    usersHtml.html(eleHtml);
                }
            }, error: function(err) {

            }
        });
    }

    $(document).on('keyup', '.input-chat', function() {
        var input = $(this).val();
        var footerItem = $(this).closest('.chat-footer').find('.event-mess');
        if(input !== "") {
            console.log($(this).closest('.chat-footer-item'));
            $(this).closest('.chat-footer-item').css('width', '100%');
            $(this).closest('.chat-footer-item').css('background-color', '#fff');
            $(this).css('width', '100%');
            footerItem.removeClass('d-flex').addClass('d-none');
        } else {
            footerItem.removeClass('d-none').addClass('d-flex');
        }
    })
});