$(document).ready(function() {
    $(document).on('click', '.btn-accept-friend', function() {
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

    $(document).on('click', '.btn-delete-friend', function() {
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

    $(document).on('click', '.btn-add-friend', function() {
        var formData = new FormData();
        formData.append("type", "add_friend");
        formData.append("sourceId", $(this).val());

        addFriend(formData, $(this));
    });

    function addFriend(formData, element) {
        $.ajax({
            url: '/UserFriends/ReplyRequestFriend',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.successRequest) {
                    $(element).toggleClass('btn-add-friend btn-remove-intivate').text('Hủy lời mời');
                }
            }
        })
    }

    $(document).on('click', '.btn-remove-intivate', function() {
        var formData = new FormData();
        formData.append("type", "cancel_invate");
        formData.append("sourceId", $(this).val());
        
        cancelInvate(formData, $(this));
    });

    function cancelInvate(formData, element) {
        $.ajax({
            url: '/UserFriends/ReplyRequestFriend',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.successRequest) {
                    $(element).toggleClass('btn-remove-intivate btn-add-friend').text('Thêm bạn bè');
                }
            }
        });
    }
});