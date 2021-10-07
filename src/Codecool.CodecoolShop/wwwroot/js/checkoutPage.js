import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";

let checkoutPageContainer = document.querySelector(".checkout-page-container");
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });


function displayCartQuantityInForm() {
    let cartTotalQuantityBadgeContainer = document.querySelector(".badge-pill");
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    cartTotalQuantityBadgeContainer.innerHTML = shoppingCartQuantity;
}


function displayItemsFromCart() {
    let shoppingCartItemsFromSessionStorage = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    loadItemsinCheckoutPageItemsContainer(shoppingCartItemsFromSessionStorage);
}

function loadItemsinCheckoutPageItemsContainer(items) {
    let checkoutPageItemsContainer = document.querySelector(".list-group.mb-3");
    let itemsFormat = "";
    let totalCartSum = 0;
    const cartItemTemplateBuilder = htmlFactory(htmlTemplates.formatCheckoutPageCartItem);
    const cartTotalTemplateBuilder = htmlFactory(htmlTemplates.formatCheckoutPageCartTotal);

    for (let i = 0; i < items.length; i++) {
        const cartItemTemplate = cartItemTemplateBuilder(items[i]);

        itemsFormat += cartItemTemplate;
        totalCartSum += items[i].Product.DefaultPrice * items[i].Quantity;
    }
    const cartTotalTemplate = cartTotalTemplateBuilder(totalCartSum);
    itemsFormat += cartTotalTemplate;
    checkoutPageItemsContainer.innerHTML = itemsFormat;
}

displayCartQuantityInForm();
displayItemsFromCart();