import {
    initClickEventOnButtons, showCartQuantityAfterLoading,
    loadCartItems, attachTemplateToDropdownCart
} from "/js/cartUtils.js";
import { htmlTemplates } from "/js/htmlFactory.js";

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

    cartButton.addEventListener('click', () => {
        event.preventDefault();

        if (shoppingCart.childNodes.length > 1) {
            shoppingCart.innerHTML = "";
            shoppingCart.style.visibility = "hidden";
        }
        else {
            loadDropdownCart();
        }
    })
}

function loadDropdownCart() {
    attachTemplateToDropdownCart(htmlTemplates.filledDropdownCartBody);
    loadCartItems();
    //$(".shopping-cart > .button").on("click", () => { location.href = "/Product/Cart"; });
}


showCartQuantityAfterLoading();
initCartButtonFunctionality();
initBuyButtons();