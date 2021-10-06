// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });
let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");

function formatShoppingCartPageItem(item) {
    return `
        <div class="filled-cart-item">
            <img src="/img/${item.Product.Name}.jpg" alt="${item.Product.Name}" />
            <span class="item-name">${item.Product.Name}</span>
            <span class="item-price">${formatter.format(item.Product.DefaultPrice * item.Quantity)}</span>
            <span class="item-quantity">x ${item.Quantity}</span> <button class="delete-cart-item" data-product-id="${item.Product.Id}">x</button>
        </div>`;
}

function emptyCartFormat() {
    return `
        <div id="empty-cart">
            <div class="shopping-cart-title">Your cart is empty</div>
            <div class="textBox mrg-btm-sm">
                <div>To add products to cart</div>
                <div>please go back to the store.</div>
            </div>
            <a class="empty-cart-primary-btn pho-btn btn btn-primary" asp-area="" asp-controller="Product" asp-action="Index">
                <i class="em em-go-left"></i>Back to Store
            </a>
        </div>`;
}

function filledCartFormat() {
    return `
        <div id="filled-cart">
            <h1>${document.title}</h1>
            <div class="items"></div>
            <div class="shopping-cart-summary">
                <div class="shopping-cart-page-total">
                    <span class="total-left"><strong>Total:</strong></span>
                    <span class="total-right"></span>
                </div>
                <button>Checkout</button>
            </div>
        </div>`;
}

function loadShoppingCartPageContainer(items) {
    let itemsFormat = "";
    let totalCartSum = 0;
    for (let i = 0; i < items.length; i++) {
        itemsFormat += formatShoppingCartPageItem(items[i]);
        totalCartSum += items[i].Product.DefaultPrice * items[i].Quantity;
    }

    let filledCart = document.querySelector("#filled-cart");
    let filledCartItemsContainer = filledCart.querySelector(".items");
    let totalCartSumField = filledCart.querySelector(".shopping-cart-page-total .total-right");
    filledCartItemsContainer.innerHTML = itemsFormat;
    totalCartSumField.innerHTML = formatter.format(totalCartSum);
}

function loadShoppingCartPage() {
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));

    if (items) {
        shoppingCartPageContainer.innerHTML = filledCartFormat();
        loadShoppingCartPageContainer(items);
    }
    else {
        shoppingCartPageContainer.innerHTML = emptyCartFormat();
    }
}



loadShoppingCartPage();
