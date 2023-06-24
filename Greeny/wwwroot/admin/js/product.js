$(document).ready(function () {
    getSubCategoryByCategoryId();

    $(document).on("click", ".images .image button", function () {

        let productImageId = $(this).attr("data-id");

        let removedElem = $(this).parent();

        let data = { id: productImageId };

        $.ajax({
            url: "/admin/product/DeleteProductImage",
            type: "Post",
            data: data,
            success: function (res) {
                $(removedElem).remove();
            }
        })
    })

    $(document).on("click", ".images .image .change-ismain", function () {

        let productImageId = $(this).attr("data-id");

        let changedElem = $(this);

        var changeBtns = document.querySelectorAll(".images .image .change-ismain")

        let data = { id: productImageId };

        $.ajax({
            url: "/admin/product/ChangeImageIsMain",
            type: "Post",
            data: data,
            success: function (res) {
                for (var i = 0; i < changeBtns.length; i++) {
                    var btn = changeBtns[i]

                    if (btn.classList.contains("main-image")) {
                        btn.classList.remove("main-image")
                    }
                }
                $(changedElem).addClass("main-image")
            }
        })
    })

})


$("#categoryId").change(function () {
    getSubCategoryByCategoryId();
})

var getSubCategoryByCategoryId = function () {
    $.ajax({
        url: "/Admin/Product/GetSubCategoryByCategoryId",
        type: 'Get',
        data: {
            categoryId: $('#categoryId').val(),
        },
        success: function (data) {
            $('#subCatogId').find('option').remove()
            $(data).each(
                function (index, item) {
                    $('#subCatogId').append(`<option value="${item.id}">${item.name}</option>`)
                }
            );
        }
    })
}