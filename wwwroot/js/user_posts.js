$(document).ready(function () {
    $(document).on('click', '.btn-create-post', function () {
        calculatorTimeDifferent();
        return;
        var formData = new FormData();
        var imageFile = $('.input-media');
        var hasImage = new Boolean(false);

        if (imageFile.val()) {
            hasImage = true;
        }
        formData.append("Content", $('textarea[name="Content"]').val());
        formData.append("HasImage", hasImage);
        if (hasImage) {
            formData.append("ImagePost", imageFile[0].files[0]);
        }

        createPost(formData);
    });

    function createPost(formData) {
        $.ajax({
            url: '/UserPosts/CreatePost',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.isSuccess) {
                    createElementPost(response.post, response.imagePost);
                } else {
                    console.log(response.error);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    function createElementPost(post, image) {
        var elementPost = `
        <div class="card">
            <div class="card-body">
                <div class="post-item">
                    <div class="user-post-data pb-3">
                        <div class="d-flex align-items-center">
                            <div class="me-3">
                                <img src="data:image/jpeg;base64,${image.ImageData}" alt=""
                                    class="img-fluid rounded-circle avatar-60">
                            </div>
                            <div
                                class="d-flex align-items-center justify-content-between w-100">
                                <div class="">
                                    <h5 class="d-inline-block mb-0">
                                        <a href="">${post.User.UserName}</a>
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
                    <div class="content-post mb-2 ${post.Content == "" ? "view-hidden" : ""}">
                        ${post.Content}
                    </div>
                    ${post.HasImage ? `<div class="user-post"><a href=""><img src="data:image/jpeg;base64,${image.ImageData}" alt="" class="img-fluid w-100"></a></div>` : ""}
                    <div class="comment-post mt-3">
                        <div class="d-flex align-items-center justify-content-between">
                            <div class="d-flex">
                                <div class="d-flex align-items-center">
                                    <div class="like-post">
                                        <div class="dropdown">
                                            <span class="dropdown-toggle"
                                                data-bs-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false" role="button">
                                                <img src="/images/icon/01.png" alt=""
                                                    class="img-fluid">
                                            </span>
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
                                            <span class="dropdown-toggle"
                                                data-bs-toggle="dropdown" aria-haspopup="true"
                                                aria-expanded="false" role="button">140
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
                                        <span class="dropdown-toggle" data-bs-toggle="dropdown"
                                            aria-haspopup="true" aria-expanded="false"
                                            role="button">
                                            20 Comment
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
                        <form action="" class="mt-3 d-flex align-items-center comment-text">
                            <input type="text" class="form-control rounded"
                                placeholder="Viết bình luận...">
                            <div class="comment-attagement d-flex">
                                <a href="#"><i class="ri-link"></i></a>
                                <a href="#"><i class="ri-user-smile-line"></i></a>
                                <a href="#"><i class="ri-camera-line"></i></a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        `;
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

});