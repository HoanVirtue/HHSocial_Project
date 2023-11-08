$(document).ready(function() {
    $('.btn-accept-friend').on('click', function() {
        var parentEle = $(this).parent();
        var formData = new FormData();
        formData.append("type", "accept");
        formData.append("sourceId", $(this).val());
        
        
        acceptRequestFriend(formData, parentEle);
    });

    function acceptRequestFriend(formData, parentEle) {
        $.ajax({
            url: '/UserFriends/ReplyRequestFriend',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.successRequest) {
                    parentEle.html("Đã chấp nhận lời mời kết bạn.");
                    console.log("Đã gỡ lời mời kết bạn");
                }
                
            }, error: function(err) {
                console.error(err.toString());
            }
        });
    }

    $('.btn-delete-friend').on('click', function() {
        var parentEle = $(this).parent();
        var formData = new FormData();
        formData.append("type", "delete");
        formData.append("sourceId", $(this).val());
        
        deleteRequestFriend(formData, parentEle);
    });

    function deleteRequestFriend(formData, parentEle) {
        $.ajax({
            url: '/UserFriends/ReplyRequestFriend',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.successRequest) {
                    parentEle.html("Đã gỡ lời mời kết bạn.");
                }
                
            }, error: function(err) {
                console.error(err.toString());
            }
        })
    }
});