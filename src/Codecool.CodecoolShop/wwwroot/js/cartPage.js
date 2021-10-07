import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";

let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });


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
        const filledCartFormatBuilder = htmlFactory(htmlTemplates.filledCartFormat);
        const filledCartFormat = filledCartFormatBuilder();
        shoppingCartPageContainer.innerHTML = filledCartFormat;
        loadShoppingCartPageContainer(items);
    }
    else {
        const emptyCartFormatBuilder = htmlFactory(htmlTemplates.emptyCartFormat);
        const emptyCartFormat = emptyCartFormatBuilder();
        shoppingCartPageContainer.innerHTML = emptyCartFormat;
    }
}

loadShoppingCartPage();
