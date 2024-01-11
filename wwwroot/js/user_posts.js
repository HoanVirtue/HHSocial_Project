$(document).ready(function () {
    $(document).on('click', '.btn-create-post', function () {
        var formData = new FormData();
        var imageFile = $('.input-media');
        var hasImage = new Boolean(false);
        var content = $('textarea[name="Content"]');

        if(imageFile[0].files[0] != "" || content.val() != "") {
            if (imageFile.val()) {
                hasImage = true;
            }
            formData.append("Content", content.val());
            formData.append("HasImage", hasImage);
            if (hasImage) {
                formData.append("ImagePost", imageFile[0].files[0]);
            }
    
            createPost(formData);
    
            // clear bài đăng cũ
            content.val('');
            $('#feedpost-modal').modal('toggle');
            imageFile.val('');
            $('.images-post').addClass('view-hidden');
        }
        
    });

    function createPost(formData) {
        $.ajax({
            url: '/UserPosts/CreatePost',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                response = JSON.parse(response);
                if (response.isSuccess) {
                    createElementPost(response.post, response.imagePost);
                } else {
                    console.log(response.error);
                }
            },
            error: function (err) {
                console.log("Error Msg: ", err);
            }
        });
    }

    function createElementPost(post, image) {
        let avatar = $('#avatar-user-layout').attr('src');
        let username = $('#username-layout').text();
        var elementPost = `
        <div class="card">
            <div class="card-body">
                <div class="post-item">
                    <div class="user-post-data pb-3">
                        <div class="d-flex align-items-center">
                            <div class="me-3">
                                <img src="${avatar}" alt=""
                                    class="img-fluid rounded-circle avatar-60">
                            </div>
                            <div
                                class="d-flex align-items-center justify-content-between w-100">
                                <div class="">
                                    <h5 class="d-inline-block mb-0">
                                        <a href="">${username}</a>
                                    </h5>
                                    <p class="d-inline-block mb-0 ms-1">Bài viết mới</p>
                                    <p class="mb-0">${calculatorTimeDifferent(post.UpdatedAt)}</p>
                                </div>
                                <div class="card-post-dropdown">
                                    <div class="dropdown">
                                        <span class="dropdown-toggle" data-bs-toggle="dropdown"
                                            aria-haspopup="true" aria-expanded="false"
                                            role="button">
                                            <i class="ri-more-fill"></i>
                                        </span>
                                        <div class="dropdown-menu">
                                            <a href="" class="dropdown-item p-3">
                                                <div class="d-flex">
                                                    <i class="ri-save-line h4"></i>
                                                    <div class="ms-2">
                                                        <h6>Lưu bài viết</h6>
                                                        <p class="mb-0">Add this to your saved
                                                            item
                                                        </p>
                                                    </div>
                                                </div>
                                            </a>
                                            <a href="" class="dropdown-item p-3">
                                                <div class="d-flex">
                                                    <i class="ri-pencil-line h4"></i>
                                                    <div class="data ms-2">
                                                        <h6>Edit Post</h6>
                                                        <p class="mb-0">Update your post and
                                                            saved
                                                            items
                                                        </p>
                                                    </div>
                                                </div>
                                            </a>
                                            <a href="" class="dropdown-item p-3">
                                                <div class="d-flex">
                                                    <i class="ri-close-circle-line h4"></i>
                                                    <div class="data ms-2">
                                                        <h6>Hide From Timeline</h6>
                                                        <p class="mb-0">See fewer posts like
                                                            this.
                                                        </p>
                                                    </div>
                                                </div>
                                            </a>
                                            <a href="" class="dropdown-item p-3">
                                                <div class="d-flex">
                                                    <i class="ri-delete-bin-7-line h4"></i>
                                                    <div class="data ms-2">
                                                        <h6>Delete</h6>
                                                        <p class="mb-0">Remove thids Post on
                                                            Timeline
                                                        </p>
                                                    </div>
                                                </div>
                                            </a>
                                            <a class="dropdown-item p-3" href="#">
                                                <div class="d-flex align-items-top">
                                                    <i class="ri-notification-line h4"></i>
                                                    <div class="data ms-2">
                                                        <h6>Notifications</h6>
                                                        <p class="mb-0">Turn on notifications
                                                            for
                                                            this
                                                            post</p>
                                                    </div>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="content-post mb-2 ${post.Content === null ? "view-hidden" : ""}">
                        ${post.Content}
                    </div>`;

                    if(post.HasImage) {
                        elementPost += `<div class="user-post mb-3"><a href=""><img src="data:image/jpeg;base64,${image.ImageData}" alt="" class="img-fluid w-100"></a></div>`;
                    }

                    elementPost += 
                        `<div class="d-flex align-items-center justify-content-between">
                            <div class="d-flex align-items-center">
                                <div class="d-flex align-items-center">
                                    <div class="like-post">
                                        <div class="dropdown">
                                            <button type="button" class="btn-like-post btn" value="${post.UserPostId}">
                                                <span class="dropdown-toggle"
                                                    data-bs-toggle="dropdown" aria-haspopup="true"
                                                    aria-expanded="false" role="button">
                                                    <img src="/images/icon/like_empty.png" alt=""
                                                        class="img-fluid ic-24 ic-like-post">
                                                </span>
                                            </button>
                                            <div class="dropdown-menu py-2">
                                                <a class="ms-2 me-2" href="#"
                                                    data-bs-toggle="tooltip"
                                                    data-bs-placement="top" title="Like">
                                                    <img src="/images/icon/01.png"
                                                        class="img-fluid" alt="">
                                                </a>
                                                <a class="me-2" href="#"
                                                    data-bs-toggle="tooltip"
                                                    data-bs-placement="top" title="Love"><img
                                                        src="/images/icon/02.png"
                                                        class="img-fluid" alt=""></a>
                                                <a class="me-2" href="#"
                                                    data-bs-toggle="tooltip"
                                                    data-bs-placement="top" title="Happy"><img
                                                        src="/images/icon/03.png"
                                                        class="img-fluid" alt=""></a>
                                                <a class="me-2" href="#"
                                                    data-bs-toggle="tooltip"
                                                    data-bs-placement="top" title="HaHa"><img
                                                        src="/images/icon/04.png"
                                                        class="img-fluid" alt=""></a>
                                                <a class="me-2" href="#"
                                                    data-bs-toggle="tooltip"
                                                    data-bs-placement="top" title="Think"><img
                                                        src="/images/icon/05.png"
                                                        class="img-fluid" alt=""></a>
                                                <a class="me-2" href="#"
                                                    data-bs-toggle="tooltip"
                                                    data-bs-placement="top" title="Sade"><img
                                                        src="/images/icon/06.png"
                                                        class="img-fluid" alt=""></a>
                                                <a class="me-2" href="#"
                                                    data-bs-toggle="tooltip"
                                                    data-bs-placement="top" title="Lovely"><img
                                                        src="/images/icon/07.png"
                                                        class="img-fluid" alt=""></a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="total-like-post ms-2 me-3">
                                        <div class="dropdown">
                                            <span class="dropdown-toggle count-like-post"
                                                data-bs-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false" role="button">0
                                                Likes</span>
                                            <div class="dropdown-menu">
                                                <a class="dropdown-item" href="#">Max Emum</a>
                                                <a class="dropdown-item" href="#">Bill Yerds</a>
                                                <a class="dropdown-item" href="#">Hap E.
                                                    Birthday</a>
                                                <a class="dropdown-item" href="#">Tara Misu</a>
                                                <a class="dropdown-item" href="#">Midge Itz</a>
                                                <a class="dropdown-item" href="#">Sal Vidge</a>
                                                <a class="dropdown-item" href="#">Other</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="total-comment-post">
                                    <div class="dropdown">
                                        <span class="dropdown-toggle count-comment-post" data-bs-toggle="dropdown"
                                            aria-haspopup="true" aria-expanded="false"
                                            role="button">
                                            0 Comment
                                        </span>
                                        <div class="dropdown-menu">
                                            <a class="dropdown-item" href="#">Max Emum</a>
                                            <a class="dropdown-item" href="#">Bill Yerds</a>
                                            <a class="dropdown-item" href="#">Hap E.
                                                Birthday</a>
                                            <a class="dropdown-item" href="#">Tara Misu</a>
                                            <a class="dropdown-item" href="#">Midge Itz</a>
                                            <a class="dropdown-item" href="#">Sal Vidge</a>
                                            <a class="dropdown-item" href="#">Other</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="feather-icon">
                                <a href="">
                                    <i class="ri-share-line"></i>
                                    <span class="ms-1">99 Share</span>
                                </a>
                            </div>
                        </div>
                        <hr>
                        <div class="mt-3 d-flex align-items-center comment-text position-relative">
                            <input type="text" class="form-control rounded input-comment"
                                placeholder="Viết bình luận...">
                            <div class="comment-attagement d-flex">
                                <button class="btn btn-send-comment" style="padding: 0 10px;">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-send fa-bold" viewBox="0 0 16 16">
                                        <path d="M15.854.146a.5.5 0 0 1 .11.54l-5.819 14.547a.75.75 0 0 1-1.329.124l-3.178-4.995L.643 7.184a.75.75 0 0 1 .124-1.33L15.314.037a.5.5 0 0 1 .54.11ZM6.636 10.07l2.761 4.338L14.13 2.576zm6.787-8.201L1.591 6.602l4.339 2.76 7.494-7.493Z"/>
                                    </svg>
                                </button>
                                <a href="#"><i class="ri-link"></i></a>
                                <a href="#"><i class="ri-user-smile-line"></i></a>
                                <a href="#"><i class="ri-camera-line"></i></a>
                            </div>       
                        </div>
                    </div>
                </div>
            </div>
        </div>
        `;

        $('.feed-posts').prepend(elementPost);
        
    }

    function calculatorTimeDifferent(date) {
        date = new Date(date);
        let now = new Date();
        let timeDiffMiliseconds = now - date;

        let minutes = timeDiffMiliseconds / (1000 * 60);
        let hours = minutes / 60;
        let days = hours / 24;
        if(days >= 1) {
            return days + " ngày trước";
        } else if(hours >= 1) {
            return hours + " tiếng trước";
        } else if(minutes >= 1) {
            return minutes + "tiếng trước";
        } else {
            return "1 phút trước";
        }
    }

    $(document).on('click', '.btn-like-post', function() {
        let postId = $(this).val();
        let formData = new FormData();
        formData.append("postId", postId);
        
        handleLikePost(formData, $(this));
    });

    function handleLikePost(formData, element) {
        $.ajax({
            url: '/UserPosts/LikePost',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.isSuccess) {
                    var icLike = element.find('.ic-like-post');
                    var countLike = element.closest('.d-flex').find('.count-like-post');
                    var likers = element.closest('.d-flex').find('.like-dropdown');
                    // like
                    if(response.typeLike == 1) {
                        icLike.attr('src', '/images/icon/liked_blue.png');
                    } else {
                        // don't like
                        icLike.attr('src', '/images/icon/like_empty.png');
                    }
                    countLike.html(`${response.countLike} Likes`);

                    likers.html('');
                    response.likers.forEach(user => {
                        var liker = `<a class="dropdown-item" href="/Users/Profile?userId=${user.userId}">${user.userName}</a>`;
                        likers.append(liker);
                    });
                } else {
                    console.log(response.error);
                }
            },
            error: function(err) {
                console.log(err);
            }
        });
    }

    $(document).on('click', '.btn-send-comment', function() {
        var input = $(this).closest('.comment-text').find('.input-comment').val();
        if(input != "") {
            var formData = new FormData();
            var postId = $(this).val();
            formData.append("UserPostId", postId);
            formData.append("Content", input);
            handleCommentPost(formData, $(this));
        }
    });

    $(document).on('change', '.input-comment', function() {
        var input = $(this).val();
        if(input != "") {
            var formData = new FormData();
            var postId = $(this).closest('.comment-text').find('.btn-send-comment').val();
            formData.append("UserPostId", postId);
            formData.append("Content", input);
            handleCommentPost(formData, $(this));
        }
    });

    function handleCommentPost(formData, element) {
        $.ajax({
            url: '/UserPosts/CommentPost',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.isSuccess) {
                    var itemComment = `
                    <li class="mb-2 item-comment">
                        <div class="d-flex flex-wrap">
                            <div class="user-image">
                                <img src="data:image/jpeg;base64,${response.commentator.avatarImage}" alt=""
                                    class="img-fluid avatar-35 rounded-circle">
                            </div>
                            <div class="ms-3">
                                <div class="d-flex">
                                    <div class="">
                                        <a href="/Users/Profile?userId=${response.commentator.senderId}">
                                            <h6>${response.commentator.userName}</h6>
                                        </a>
                                        <p class="mb-0">${response.commentator.content}</p>
                                    </div>
                                    <div class="ms-3">
                                        <div class="dropdown">
                                            <button id="handle-comment-dropdown" class="btn btn-more-comment" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="bi bi-three-dots"></i>
                                            </button>
                                            <ul class="sub-drop dropdown-menu" aria-labelledby="handle-comment-dropdown">
                                            `;
                                            if(response.userCurrent.userId != response.commentator.senderId)
                                            {
                                                itemComment += `
                                                <li class="dropdown-item">
                                                    <a href="">Ẩn bình luận</a>
                                                </li>
                                                <li class="dropdown-item">
                                                    <a href="">Báo cáo bình luận</a>
                                                </li>
                                                `;
                                            }
                                            else
                                            {
                                                itemComment += `
                                                <li class="dropdown-item">
                                                    <a href="">Chỉnh sửa</a>
                                                </li>
                                                <li class="dropdown-item">
                                                    <a href="#" data-bs-toggle="modal" data-bs-target="#modal-delete-comment-${response.commentator.commentDetailId}">
                                                        Xóa
                                                    </a>
                                                </li>
                                                `;
                                            }
                                            itemComment += `
                                            </ul>
                                        </div>
                                        `;
                                        if(response.userCurrent.userId == response.commentator.senderId)
                                        {
                                            itemComment += `
                                            <div class="modal fade" id="modal-delete-comment-${response.commentator.commentDetailId}" data-bs-backdrop="false" role="dialog"  tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered">
                                                    <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h4 class="modal-title text-center w-100" id="staticBackdropLabel"><b>Xóa bình luận?</b></h4>
                                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                    </div>
                                                    <div class="modal-body">
                                                        Bạn có chắc muốn xóa bình luận này không?
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-cancel" data-bs-dismiss="modal">Không</button>
                                                        <button type="button" class="btn btn-primary btn-delete-comment" value="${response.commentator.commentDetailId}">Xóa</button>
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                            `;
                                        }
                                        itemComment += `
                                    </div>
                                </div>
                                <div class="d-flex flex-wrap align-items-center comment-activity">
                                    <span class="time-comment-post"> ${calculatorTimeDifferent(response.commentator.updatedAt)} </span>
                                    <a href="#">Thích</a>
                                    <a href="#">Phản hồi</a>
                                </div>
                            </div>
                        </div>
                    </li>
                    `;

                    $(element).val('');

                    var parentEle = element.closest('.comment-post');
                    var comments = parentEle.find('.post-comments');
                    comments.prepend(itemComment);
                    var countComment = parentEle.find('.count-comment-post');
                    countComment.text(`${response.countComment} Comments`);
                    var commentator = parentEle.find('.commentator-dropdown');
                    commentator.html('');
                    response.userComments.forEach(user => {
                        var cmt = `<a class="dropdown-item" href="/Users/Profile?userId=${user.userId}">${user.userName}</a>`;
                        commentator.append(cmt);
                    });
                } else {
                    console.log(response.errorMsg);
                }
            },
            error: function(err) {
                console.log(err);
            }
        });
    }

    $(document).on('click', '.btn-disable-en', function() {
        var element = $(this);
        $.ajax({
            url: '/Users/Disable_Enable',
            type: 'POST',
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.disable) {
                    $(element).text('Disable');
                } else {
                    $(element).text('Enable');
                }
            },
            error: function(err) {

            }
        }); 
    });

    $(document).on('click', '.btn-delete-comment', function() {
        var commentId = $(this).val();
        var formData = new FormData();
        formData.append("CommentDetailId", commentId);
        deleteComment(formData, $(this));
    });

    function deleteComment(formData, element) {
        $.ajax({
            url: '/UserPosts/DeleteComment',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function(response) {
                if(response.isSuccess) {
                    // close modal
                    var dialog = $(`#modal-delete-comment-${formData.get("CommentDetailId")}`);
                    dialog.modal('hide');
                    dialog.attr("data-bs-backdrop", "false");
                    dialog.removeClass("fade");
                    element.closest('.item-comment').remove();
                    $('body').addClass("overflow-scroll");

                } else {
                    console.log("Comment Detail: ", response.errorMsg);
                }
            },
            error: function(err) {

            }
        })
    }

    $(".modal").on("show", function () {
        $("body").addClass("modal-open");
    }).on("hidden", function () {
        $("body").removeClass("modal-open");
    });
});

