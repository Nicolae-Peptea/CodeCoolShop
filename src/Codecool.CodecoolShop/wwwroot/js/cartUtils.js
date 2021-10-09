import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";
import { dataHandler } from "/js/dataHandler.js";

let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });
let shoppingCart = document.querySelector(".shopping-cart");
let shoppingCartItemsContainer = document.querySelector(".shopping-cart-items");
let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");


export async function updateCartItems(htmlWithAttribute, quantity, functionToUpdate) {
    let productId = htmlWithAttribute.getAttribute("data-product-id");
    let data = await functionToUpdate(productId, quantity);

    updateCart(data);
}


export async function deleteCartItems(htmlWithAttribute, functionToUpdate) {

    let productId = htmlWithAttribute.getAttribute("data-product-id");
    let data = await functionToUpdate(productId);

    updateCart(data);
}


export function showCartQuantityAfterLoading() {
    let itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    itemsInShoppingCartFields.forEach((element) => {
        element.innerHTML = shoppingCartQuantity;
    })
}


export function loadCartItems() {
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));

    if (items != null) {
        cartItemsLoader(htmlTemplates.formatShoppingCartItem, ".shopping-cart-items", ".main-color-text");
    }
    else {
        let totalCartSumField = document.querySelector(".main-color-text");
        totalCartSumField.innerHTML = formatter.format(0);
        shoppingCartItemsContainer.innerHTML = "The shopping cart is empty.";
    }
}


export function initCartButtonFunctionality() {
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


export function loadShoppingCartPage() {
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));

    if (items) {
        loadFilledCartPage();
    }
    else {
        loadEmptyCartPage();
    }
}


function loadFilledCartPage() {
    const filledCartFormatBuilder = htmlFactory(htmlTemplates.filledCartFormat);
    const filledCartFormat = filledCartFormatBuilder();

    shoppingCartPageContainer.innerHTML = filledCartFormat;
    loadItemsInShoppingCartPageContainer();
    initQuantityModifyingButtons();
}


function loadItemsInShoppingCartPageContainer() {
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    if (items) {
        cartItemsLoader(htmlTemplates.formatShoppingCartPageItem, "#filled-cart > .items", ".total-cart-amount");
    }
}


function loadEmptyCartPage() {
    const emptyCartFormatBuilder = htmlFactory(htmlTemplates.emptyCartFormat);
    const emptyCartFormat = emptyCartFormatBuilder();
    shoppingCartPageContainer.innerHTML = emptyCartFormat;
}


function cartItemsLoader(template, itemsContainerClass, totalCartContainerClass) {
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    const itemsFormatBuilder = htmlFactory(template);
    let itemsFormat = "";
    let totalCartSum = 0;

    for (let i = 0; i < items.length; i++) {
        const formatShoppingCartItem = itemsFormatBuilder(items[i]);

        itemsFormat += formatShoppingCartItem;
        totalCartSum += items[i].Product.DefaultPrice * items[i].Quantity;
    }

    let shoppingCartItemsContainer = document.querySelector(itemsContainerClass);
    shoppingCartItemsContainer.innerHTML = itemsFormat;
    let totalCartSumField = document.querySelector(totalCartContainerClass);
    totalCartSumField.innerHTML = formatter.format(totalCartSum);
    initDeleteShoppingCartItemsButtonsFunctionality();
}


function initDeleteShoppingCartItemsButtonsFunctionality() {
    let buttons = [...document.querySelectorAll(".delete-cart-item")];

    buttons.forEach((button) => {
        button.addEventListener('click', async (event) => {
            event.preventDefault();

            await deleteCartItems(button, dataHandler.removeItemFromCart);
            
            let filledCart = document.querySelector("#filled-cart");
            if (shoppingCart.style.visibility == "visible") {
                loadCartItems();
            }
            if (filledCart) {
                loadShoppingCartPage();
            }
        })
    })
}


function initQuantityModifyingButtons() {
    let increaseQuantityButtons = [...document.querySelectorAll(".increase-quantity")];
    let decreaseQuantityButtons = [...document.querySelectorAll(".decrease-quantity")];

    increaseQuantityButtons.forEach((button) => {
        button.addEventListener('click', async function () {
            event.preventDefault();

            const increaseQuantity = 1;
            updateCartQuantity(button, increaseQuantity);
        })
    })
    decreaseQuantityButtons.forEach((button) => {
        button.addEventListener('click', async function () {
            event.preventDefault();

            const decreaseQuantity = -1;
            updateCartQuantity(button, decreaseQuantity);
        })
    })
}


async function updateCartQuantity(htmlElement, quantity) {
    const functionToUpdate = dataHandler.addNewItemToCart;
    await updateCartItems(htmlElement, quantity, functionToUpdate);

    loadShoppingCartPage();
    if (shoppingCart.style.visibility == "visible") {
        loadCartItems();
    }
}


function updateCart(response) {
    if (response) {
        let itemsNumber = 0;
        let totalItemsInShoppingCart = [...document.querySelectorAll(".badge")];

        response.forEach((element) => {
            itemsNumber += element.Quantity;
        })

        totalItemsInShoppingCart.forEach((field) => {
            field.innerHTML = itemsNumber != 0 ? itemsNumber : "";
        })

        setSessionStorage(itemsNumber, response);
    }
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
