$(document).ready(function () {



    $(document).on("click", ".status-slider", function () {

        let clickedBtn = $(this);

        let productId = $(this).attr("data-id");
        let data = { id: productId };

        $.ajax({
            url: "Slider/ChangeStatus",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {
                    if (clickedBtn.hasClass("active-slider")) {
                        clickedBtn.removeClass("active-slider")
                        clickedBtn.addClass("disable-slider")
                    }
                    else {
                        clickedBtn.addClass("active-slider")
                        clickedBtn.removeClass("disable-slider")
                    }
                }
            }

        })

    })










})