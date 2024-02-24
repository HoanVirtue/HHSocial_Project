$(document).ready(function() {
    $(".chats-dropdown .chats-dropdown-menu").click(function(e){
        e.stopPropagation();
     });
    $(document).on('click', '.btn-back-chats', function() {
        console.log(1);
    });
    $('input[name="input-search-mess"]').on('click', function() {
        searchChats(true);
    });
    $('.btn-back-chats').on('click', function() {
        searchChats(false);
    });
    function searchChats(search) {
        if(search) {
            var chatDropMenu = $('.chats-dropdown .chats-dropdown-menu');
            chatDropMenu.find('.btn-back-chats').css('display', 'inline');
            chatDropMenu.find('.card-body .chats-message').css('display', 'none');
            chatDropMenu.find('.card-body .chats-search').css('display', 'inline');
        } else {
            var chatDropMenu = $('.chats-dropdown .chats-dropdown-menu');
            chatDropMenu.find('.btn-back-chats').css('display', 'none');
            chatDropMenu.find('.card-body .chats-message').css('display', 'inline');
            chatDropMenu.find('.card-body .chats-search').css('display', 'none');
        }
    }
})