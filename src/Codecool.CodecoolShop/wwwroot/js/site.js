import { updateCartItems, showCartQuantityAfterLoading, loadCartItems } from "/js/cartUtils.js";
import { dataHandler } from "/js/dataHandler.js";


let cartButton = document.querySelector("#cart");
let buyButtons = [...document.querySelectorAll("#add-to-cart")];
let shoppingCart = document.querySelector(".shopping-cart");
let shoppingCartItemsContainer = document.querySelector(".shopping-cart-items");


function initBuyButtons() {
    buyButtons.forEach((button) => {
        button.addEventListener("click", async (event) => {
            event.preventDefault();

            let quantity = 1;
            let functionToHandleUpdate = dataHandler.addNewItemToCart;
            await updateCartItems(button, quantity, functionToHandleUpdate)
          
            if (shoppingCart.style.visibility == "visible") {
                loadCartItems()
            }
        })
    })
}


function initCartButtonFunctionality() {
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
