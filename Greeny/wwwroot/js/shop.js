$(document).ready(function () {

    $('.category-checkbox').on('click', function (e) {
        e.preventDefault();

        var categoryId = $(this).data('category-id');

        filterByCategory(categoryId);
    });

    function filterByCategory(categoryId) {
        $.ajax({
            url: `/Shop/FilterByCategory`,
            type: 'GET',
            data: { categoryId },
            success: function (data) {
                displayProducts(data);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function displayProducts(products) {
        debugger

        var productList = $('.products');
        productList.html("");

        if (products.length > 0) {
            var listItem = "";

            $.each(products, function (index, product) {
                listItem = '<div class="col">' +
                    '<div class="product-card">' +
                    '<div class="product-media">' +
                    '<div class="product-label">' +
                    '<label class="label-text new">new</label>' +
                    '</div>' +
                    '<button class="product-wish wish">' +
                    '<i class="fas fa-heart"></i>' +
                    '</button>' +
                    '<a class="product-image" href="#">' +
                    '<img src="' + product.Image + '" alt="product">' +
                    '</a>' +
                    '<div class="product-widget">' +
                    '<a title="Product Compare" href="#" class="fas fa-random"></a>' +
                    '<a title="Product Video" href="#" class="venobox fas fa-play" data-autoplay="true" data-vbtype="video"></a>' +
                    '<a title="Product View" href="#" class="fas fa-eye" data-bs-toggle="modal" data-bs-target="#product-view"></a>' +
                    '</div>' +
                    '</div>' +
                    '<div class="product-content">' +
                    '<div class="product-rating">';

                for (var i = 0; i < product.Rating; i++) {
                    listItem += '<i class="active icofont-star"></i>';
                }
                for (var i = 0; i < (5 - product.Rating); i++) {
                    listItem += '<i class="icofont-star"></i>';
                }

                listItem += '<a href="#">(' + product.ReviewCount + ')</a>' +
                    '</div>' +
                    '<h6 class="product-name"><a href="#">' + product.Name + '</a></h6>' +
                    '<h6 class="product-price">';

                if (product.Discount !== 0) {
                    listItem += '<del>$' + product.Price + '</del>' +
                        '<span>$' + (product.Price * (100 - product.Discount) / 100) + '<small>/piece</small></span>';
                } else {
                    listItem += '<span>$' + product.Price + '<small>/piece</small></span>';
                }

                listItem += '</h6>' +
                    '<button class="product-add" title="Add to Cart">' +
                    '<i class="fas fa-shopping-basket"></i><span>add</span>' +
                    '</button>' +
                    '<div class="product-action">' +
                    '<button class="action-minus" title="Quantity Minus">' +
                    '<i class="icofont-minus"></i>' +
                    '</button>' +
                    '<input class="action-input" title="Quantity Number" type="text" name="quantity" value="1">' +
                    '<button class="action-plus" title="Quantity Plus">' +
                    '<i class="icofont-plus"></i>' +
                    '</button>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';

                productList.html(listItem)
            });
        }
        else {
            productList.append('<p>No products found.</p>');
        }
    }
})