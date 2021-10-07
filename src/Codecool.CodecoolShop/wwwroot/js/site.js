import { updateCartItems, deleteCartItems } from "/js/cartUtils.js";
import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";
import { dataHandler } from "/js/dataHandler.js";


let cartButton = document.querySelector("#cart");
let buyButtons = [...document.querySelectorAll("#add-to-cart")];
let shoppingCart = document.querySelector(".shopping-cart");
let shoppingCartItemsContainer = document.querySelector(".shopping-cart-items");
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });


function showCartQuantityAfterLoading() {

    let itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    itemsInShoppingCartFields.forEach((element) => {
        element.innerHTML = shoppingCartQuantity;
    })
}


function initBuyButtons() {
    buyButtons.forEach((button) => {
        button.addEventListener("click", async (event) => {
            event.preventDefault();

            let quantity = 1;
            let functionToHandleUpdate = dataHandler.addNewItemToCart;
            await updateCartItems(button, quantity, functionToHandleUpdate)
          
            if (shoppingCart.style.visibility == "visible") {
                loadShoppingCartItemsFromSessionStorage()
            }
        })
    })
}


function initDeleteCartItemsButtons() {
    let buttons = [...document.querySelectorAll(".delete-cart-item")];
    buttons.forEach((button) => {
        button.addEventListener('click', async (event) => {
            event.preventDefault();

            let functionToHandleDelete = dataHandler.removeItemFromCart;

            await deleteCartItems(button, functionToHandleDelete);
            loadShoppingCartItemsFromSessionStorage()
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
            loadShoppingCartItemsFromSessionStorage();
        }
    })
}


function loadShoppingCartItemsFromSessionStorage() {
    let shoppingCartItemsFromSessionStorage = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    loadCartItems(shoppingCartItemsFromSessionStorage);
}


function loadCartItems(items) {
    let itemsFormat = "";
    let totalCartSumField = document.querySelector(".main-color-text");
    let totalCartSum = 0;
    const formatShoppingCartItemBuilder = htmlFactory(htmlTemplates.formatShoppingCartItem);

    if (items != null) {
        for (let i = 0; i < items.length; i++) {
            const formatShoppingCartItem = formatShoppingCartItemBuilder(items[i]);

            itemsFormat += formatShoppingCartItem;
            totalCartSum += items[i].Product.DefaultPrice * items[i].Quantity;
        }
    }
    else {
        itemsFormat = "The shopping cart is empty.";
    }

    totalCartSumField.innerHTML = formatter.format(totalCartSum);
    shoppingCartItemsContainer.innerHTML = itemsFormat;
    initDeleteCartItemsButtons();
}


showCartQuantityAfterLoading();
initCartButtonFunctionality();
initBuyButtons();
