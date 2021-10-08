import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";
import { dataHandler } from "/js/dataHandler.js";

let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });


export async function updateCartItems(htmlWithAttribute, quantity, functionToUpdate) {
    let productId = htmlWithAttribute.getAttribute("data-product-id");
    let data = await functionToUpdate(productId, quantity);

    updateCart(data)
}


export async function deleteCartItems(htmlWithAttribute, functionToUpdate) {

    let productId = htmlWithAttribute.getAttribute("data-product-id");
    let data = await functionToUpdate(productId);

    updateCart(data)
}


export function showCartQuantityAfterLoading() {

    let itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    itemsInShoppingCartFields.forEach((element) => {
        element.innerHTML = shoppingCartQuantity;
    })
}


export function loadCartItems() {
    let shoppingCartItemsContainer = document.querySelector(".shopping-cart-items");
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
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
    initDeleteShoppingCartItemsButtonsFunctionality(".delete-cart-item", loadCartItems);
}


export function initDeleteShoppingCartItemsButtonsFunctionality(buttonsClass, itemsFormatLoadingMethod) {
    let buttons = [...document.querySelectorAll(buttonsClass)];

    buttons.forEach((button) => {
        button.addEventListener('click', async (event) => {
            event.preventDefault();
            await deleteCartItems(button, dataHandler.removeItemFromCart);
            itemsFormatLoadingMethod();
        })
    })
}


function updateCart(response) {
    let totalItems = 0;
    let totalItemsInShoppingCart = [...document.querySelectorAll(".badge")];

    response.forEach((element) => {
        totalItems += element.Quantity;
    })

    totalItemsInShoppingCart.forEach((field) => {
        field.innerHTML = totalItems;
    })

    setSessionStorage(totalItems, response);
}


function setSessionStorage(totalItems, jsonResonse) {
    if (totalItems === 0) {
        sessionStorage.clear();
    }
    else {
        sessionStorage.setItem("shoppingCartQuantity", totalItems);
        sessionStorage.setItem("shoppingCartItems", JSON.stringify(jsonResonse));
    }
}
