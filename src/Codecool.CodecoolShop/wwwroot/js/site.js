// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let cartButton = document.querySelector("#cart");
let buyButtons = [...document.querySelectorAll("#add-to-cart")];
let shoppingCart = document.querySelector(".shopping-cart");
let itemsInShoppingCart = document.querySelector(".badge");


function showCartQuantityWhenLoading() {
    let shoppingCartItems = sessionStorage.getItem("shoppingCartQuantity");
    itemsInShoppingCart.innerHTML = shoppingCartItems;
}


buyButtons.forEach((element) => {
    element.addEventListener('click', async function() {

        event.preventDefault();
        let productId = element.getAttribute("data-product-id");
        
        let routeUrl = 'api/buy';
        let cartItemsTotal = 0;

        try {
            const response = await $.ajax({
                url: routeUrl,
                data: { id: productId },
                method: "post",
                dataType: "json",
            })

            response.forEach((element) => {
                cartItemsTotal += element.Quantity;
            })

            sessionStorage.setItem("shoppingCartQuantity", cartItemsTotal)
            itemsInShoppingCart.innerHTML = cartItemsTotal;

        } catch (e) {
            console.log("Error" + e);
        }
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

showCartQuantityWhenLoading();
