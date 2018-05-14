//Khi click nut Add To Cart
$("#feature-items").on("click", ".add-to-cart", function (event) {
    event.preventDefault();
    var selected_product = $(this).children('p').text();
    updateCart("add", selected_product, event)
});