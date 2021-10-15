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

    for (let item of items) {
        const formatShoppingCartItem = itemsFormatBuilder(item);

        itemsFormat += formatShoppingCartItem;
        totalCartSum += item.productPrice * item.productQuantity;
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

        AddItemToSessionStorage(itemsNumber, response);
    }
}


//function setSessionStorage(totalItems, jsonResonse) {
//    if (totalItems === 0) {
//        sessionStorage.clear();
//    }
//    else {
//        sessionStorage.setItem("shoppingCartQuantity", totalItems);
//        sessionStorage.setItem("shoppingCartItems", JSON.stringify(jsonResonse));
//    }
//}


export function AddItemToSessionStorage(htmlElement, quantity) {

    const itemtoAdd = createItemToStoreInSessionStorage(htmlElement)
    const sessionStorageItems = sessionStorage.getItem("shoppingCartItems");
    let sessionStorageItemsAsObjects = [];
    if (sessionStorageItems !== null) {
        sessionStorageItemsAsObjects = JSON.parse(sessionStorageItems);
    }

    const itemInSessionStorage = sessionStorageItemsAsObjects
        .filter(element => element.productId === itemtoAdd.productId);

    if (itemInSessionStorage.length > 0) {
        itemInSessionStorage[0].productQuantity += quantity;
    }
    else {
        sessionStorageItemsAsObjects.push(itemtoAdd);
    }

    sessionStorage.setItem("shoppingCartItems", JSON.stringify(sessionStorageItemsAsObjects));
}

function createItemToStoreInSessionStorage(htmlElement) {

    const productId = htmlElement.getAttribute(["data-product-id"])
    const productName = htmlElement.getAttribute(["data-product-name"])
    const productPrice = htmlElement.getAttribute(["data-price"])
    const productCurrency = htmlElement.getAttribute(["data-currency"])

    return {
        "productId": productId,
        "productName": productName,
        "productPrice": productPrice,
        "productCurrency": productCurrency,
        "productQuantity": 1,
    }
}
