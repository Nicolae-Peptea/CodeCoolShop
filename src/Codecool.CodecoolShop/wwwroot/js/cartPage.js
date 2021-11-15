import { loadShoppingCartPage, getStorage } from "/js/cartUtils.js";

$("body").on("click", "#checkout-button", () => {
    let cartItems = JSON.parse(getStorage());
    let inputValue = document.querySelector("#cart-items");
    inputValue.value = JSON.stringify(cartItems);
});


loadShoppingCartPage();