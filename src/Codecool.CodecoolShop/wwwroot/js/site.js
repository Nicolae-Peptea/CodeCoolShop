
import { updateCart } from "/js/cartUtils.js";
import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";
import { dataHandler } from "/js/dataHandler.js";


let cartButton = document.querySelector("#cart");
let buyButtons = [...document.querySelectorAll("#add-to-cart")];
let shoppingCart = document.querySelector(".shopping-cart");
let shoppingCartItemsContainer = document.querySelector(".shopping-cart-items");
let itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });


function showCartQuantityAfterLoading() {
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    itemsInShoppingCartFields.forEach((element) => {
        element.innerHTML = shoppingCartQuantity;
    })
}


function initBuyButtons() {
    buyButtons.forEach((button) => {
        button.addEventListener("click", async (event) => {
            event.preventDefault();

            let productId = button.getAttribute("data-product-id");
            let data = await dataHandler.addNewItemToCart(productId);

            updateCart(data, itemsInShoppingCartFields)
          
            if (shoppingCart.style.visibility == "visible") {
                loadCartItems(data);
            }
        })
    })
}


function initDeleteCartItemsButtons() {
    let buttons = [...document.querySelectorAll(".delete-cart-item")];
    buttons.forEach((button) => {
        button.addEventListener('click', async (event) => {
            event.preventDefault();

            let productId = button.getAttribute("data-product-id");
            let data = await dataHandler.removeItemFromCart(productId);

            updateCart(data, itemsInShoppingCartFields)

            loadCartItems(data);
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
