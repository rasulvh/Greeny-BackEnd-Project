$(document).ready(function () {



    $(document).on("click", ".show-more-btn", function () {

        let parent = $("#parent-elem");

        let skiptCount = $(parent).children().length;

        let productsCount = $("#products").attr("data-count");

        $.ajax({
            url: `home/showmoreorless?skip=${skiptCount}`,
            type: "Get",
            success: function (res) {
                debugger
                $(parent).append(res);
                skiptCount = $(parent).children().length;
                if (skiptCount >= productsCount) {
                    $(".show-more-btn").addClass("d-none")
                }
            }

        })
    })


})