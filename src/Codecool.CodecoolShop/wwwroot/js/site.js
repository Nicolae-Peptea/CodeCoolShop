﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let cartButton = document.querySelector("#cart");
let shoppingCart = document.querySelector(".shopping-cart");
let buyButtons = [...document.querySelectorAll("#add-to-cart")];

buyButtons.forEach((element) => {
    element.addEventListener('click', async function() {
        event.preventDefault();
        let productId = element.getAttribute("data-product-id");
        let routeUrl = 'api/buy';

        const response = $.ajax({
            url: routeUrl,
            data: { id: productId },
            method: "post",
        })
        
    })
})

cartButton.addEventListener('click', async function () {
    event.preventDefault();
    let visibility = shoppingCart.style.visibility;
    if (visibility == "visible") {
        shoppingCart.style.visibility = "hidden";
    }
    else {
        shoppingCart.style.visibility = "visible";
    }
})
