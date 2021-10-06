import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";

let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });

//function formatShoppingCartPageItem(item) {
//    return `
//        <div class="filled-cart-item">
//            <div class="filled-cart-item-left">
//                <img src="/img/${item.Product.Name}.jpg" alt="${item.Product.Name}" />
//            </div>
//            <div class="filled-cart-item-right">
//                <a class="filled-cart-item-name"
//                    href="" title="${item.Product.Name}">${item.Product.Name}
//                </a>

//                <div class="filled-cart-item-quantity">
//                    <button name="decrease-quantity" type="submit"><i class="arrow down"></i></button>
//                    <p><strong>${item.Quantity} ${item.Quantity == 1 ? "Piece" : "Pieces"}</strong></p>
//                    <button name="increase-quantity" type="submit"><i class="arrow up"></i></button>
//                </div>

//                <button class="filled-cart-item-delete" data-product-id="${item.Product.Id}"><i class="fa fa-trash-o"></i></button>

//                <span class="filled-cart-item-price">${formatter.format(item.Product.DefaultPrice * item.Quantity)}</span>
//            </div>
//        </div>`;
//}

function emptyCartFormat() {
    return `
        <div id="empty-cart">
            <div class="shopping-cart-title">Your cart is empty</div>
            <div class="textBox mrg-btm-sm">
                <div>To add products to cart</div>
                <div>please go back to the store.</div>
            </div>
            <a class="empty-cart-primary-btn pho-btn btn btn-primary" href="/">
                <i class="em em-go-left"></i>Back to Store
            </a>
        </div>`;
}

function filledCartFormat() {
    return `
        <h1>${document.title.split("-").shift()}</h1>

        <div id="filled-cart">
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
    const formatShoppingCartPageItemBuilder = htmlFactory(htmlTemplates.formatShoppingCartPageItem);
    for (let i = 0; i < items.length; i++) {
        const formatShoppingCartPageItem = formatShoppingCartPageItemBuilder(items[i]);
        itemsFormat += formatShoppingCartPageItem;
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
