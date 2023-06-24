$(document).ready(function () {



    $(document).on("submit", "#basket-form", function (e) {
        e.preventDefault();

        let productId = $(this).attr("data-id");

        let data = { id: productId };

        $.ajax({
            url: "cart/addbasket",
            type: "Post",
            data: data,
            success: function (res) {
                $("sup.basket-count").text(res)
            }

        })

    })





})