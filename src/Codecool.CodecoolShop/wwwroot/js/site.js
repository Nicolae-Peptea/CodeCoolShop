import {
    updateCartItems, showCartQuantityAfterLoading,
    loadCartItems, initCartButtonFunctionality,
    AddItemToSessionStorage,
} from "/js/cartUtils.js";
import { dataHandler } from "/js/dataHandler.js";


let buyButtons = [...document.querySelectorAll("#add-to-cart")];
let shoppingCart = document.querySelector(".shopping-cart");


function initBuyButtons() {
    buyButtons.forEach((button) => {
        button.addEventListener("click", async (event) => {
            event.preventDefault();
            console.log(event.target);
            const htmlElement = event.target;
            const quantity = 1;
            AddItemToSessionStorage(htmlElement, quantity);
            



            //let functionToHandleUpdate = dataHandler.addNewItemToCart;
            //await updateCartItems(button, quantity, functionToHandleUpdate)

            if (shoppingCart.style.visibility == "visible") {
                loadCartItems()
            }
        })
    })
}


showCartQuantityAfterLoading();
initCartButtonFunctionality();
initBuyButtons();
