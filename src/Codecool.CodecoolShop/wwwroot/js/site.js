
import { ajaxFetch } from "/js/utils.js";

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


function loadShoppingCartItemsFromSessionStorage() {
    let shoppingCartItemsFromSessionStorage = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    loadCartItems(shoppingCartItemsFromSessionStorage);
}


function initBuyButtons() {
    buyButtons.forEach((button) => {
        button.addEventListener("click", async (event) => {

            event.preventDefault();
            let url = "api/add-cart-item";
            let httpRequest = "post";
            let data = await updateCart(button, httpRequest, url);
           
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
            let url = "api/remove-cart-item";
            let httpRequest = "delete";
            let data = await updateCart(button, httpRequest, url);

            loadCartItems(data);
        })
    })
}


async function updateCart(htmlElement, httpRequest, url) {

    let productId = htmlElement.getAttribute("data-product-id");
    let totalItems = 0;
    let response = await ajaxFetch(productId, httpRequest, url);

    response.forEach((element) => {
        totalItems += element.Quantity;
    })

    itemsInShoppingCartFields.forEach((field) => {
        field.innerHTML = totalItems;
    })

    setSessionStorageForShoppingCart(totalItems, response);

    return response;
}


function loadCartItems(items) {
    let itemsFormat = "";
    let totalCartSumField = document.querySelector(".main-color-text");
    let totalCartSum = 0;

    if (items != null) {
        for (let i = 0; i < items.length; i++) {
            itemsFormat += formatShoppingCartItem(items[i]);
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


function setSessionStorageForShoppingCart(totalItems, jsonResonse) {
    sessionStorage.setItem("shoppingCartQuantity", totalItems);
    sessionStorage.setItem("shoppingCartItems", JSON.stringify(jsonResonse));
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


function formatShoppingCartItem(item) {
    return `
        <li class="clearfix">
            <img src="/img/${item.Product.Name}.jpg" alt="${item.Product.Name}" />
            <span class="item-name">${item.Product.Name}</span>
            <div class="cart-details-container">
            <div class=".cart-items-right">
            <span class="item-price">${formatter.format(item.Product.DefaultPrice * item.Quantity)}</span>
            <span class="item-quantity">x ${item.Quantity}</span>
            </div>
            <div class=".cart-items-left">
            <button class="delete-cart-item" data-product-id="${item.Product.Id}"><i class="fa fa-trash-o"></i></button>
             </div>
            </div>
        </li>`;
}

showCartQuantityAfterLoading();
initCartButtonFunctionality();
initBuyButtons();

