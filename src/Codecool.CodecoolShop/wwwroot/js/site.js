import {
    initClickEventOnButtons, showCartQuantityAfterLoading,
    loadCartItems,
} from "/js/cartUtils.js";

let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");
let buyButtons = [...document.querySelectorAll("#add-to-cart")];
let shoppingCart = document.querySelector(".shopping-cart");


function initBuyButtons() {
    const quantity = 1;
    buyButtons.forEach((button) => {
        initClickEventOnButtons(button, quantity);
    })
}


function initCartButtonFunctionality() {
    let cartButton = document.querySelector("#cart");

    cartButton.addEventListener('click', async function () {
        event.preventDefault();
        let visibility = shoppingCart.style.visibility;
        if (visibility == "visible") {
            shoppingCart.style.visibility = "hidden";
            shoppingCartItemsContainer.innerHTML = "";
        }
        else {
            shoppingCart.style.visibility = "visible";
            loadCartItems();
        }
    })
}

showCartQuantityAfterLoading();
initCartButtonFunctionality();
initBuyButtons();