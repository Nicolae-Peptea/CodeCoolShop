// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let cartButton = document.querySelector("#cart");
let buyButtons = [...document.querySelectorAll("#add-to-cart")];
let shoppingCart = document.querySelector(".shopping-cart");
let shoppingCartItemsContainer = document.querySelector(".shopping-cart-items");
let itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });


function showCartQuantityWhenLoading() {
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    itemsInShoppingCartFields.forEach((element) => {
        element.innerHTML = shoppingCartQuantity;
    })
}


function loadShoppingCartItemsFromSessionStorage() {
    let shoppingCartItemsFromSessionStorage = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    loadCartItems(shoppingCartItemsFromSessionStorage);
}


function initBuyButtonsFunctionality() {
    buyButtons.forEach((element) => {
        element.addEventListener('click', async function () {

            event.preventDefault();
            let productId = element.getAttribute("data-product-id");

            let routeUrl = 'api/buy';
            let cartItemsTotal = 0;

            try {
                const response = await $.ajax({
                    url: routeUrl,
                    data: { id: productId },
                    method: "post",
                    dataType: "json",
                })

                response.forEach((element) => {
                    cartItemsTotal += element.Quantity;
                })

                sessionStorage.setItem("shoppingCartQuantity", cartItemsTotal);
                sessionStorage.setItem("shoppingCartItems", JSON.stringify(response));

                itemsInShoppingCartFields.forEach((field) => {
                    field.innerHTML = cartItemsTotal;
                })

                if (shoppingCart.style.visibility == "visible") {
                    loadCartItems(response);
                }
            } catch (e) {
                console.log("Error" + e);
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
            loadShoppingCartItemsFromSessionStorage();
        }
    })
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

function initDeleteCartItemsButtons() {
    let buttons = [...document.querySelectorAll(".delete-cart-item")];
    buttons.forEach((button) => {
        button.addEventListener('click', async (event) => {
            event.preventDefault();
            let productId = button.getAttribute("data-product-id");
            let routeUrl = 'api/remove-cart-item';

            try {
                const response = await $.ajax({
                    url: routeUrl,
                    data: { id: productId },
                    method: "delete",
                    dataType: "json",
                })
                console.log(response);

            } catch (e) {
                console.log("Error" + e);
            }
        })
    })
}

function formatShoppingCartItem(item) {
    return `
        <li class="clearfix">
            <img src="img/${item.Product.Name}.jpg" alt="${item.Product.Name}" />
            <span class="item-name">${item.Product.Name}</span>
            <span class="item-price">${formatter.format(item.Product.DefaultPrice * item.Quantity)}</span>
            <span class="item-quantity">x ${item.Quantity}</span> <button class="delete-cart-item" data-product-id="${item.Product.Id}">x</button>
        </li>`;
}

showCartQuantityWhenLoading();
initCartButtonFunctionality();
initBuyButtonsFunctionality();

