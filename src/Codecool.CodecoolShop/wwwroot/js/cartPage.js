import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";
import { dataHandler } from "/js/dataHandler.js";
import { updateCart } from "/js/cartUtils.js";


let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });
let itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];

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
        initQuantityModifyingButtons();
        initDeleteShoppingCartItemsButtonsFunctionality();
    }
    else {
        const emptyCartFormatBuilder = htmlFactory(htmlTemplates.emptyCartFormat);
        const emptyCartFormat = emptyCartFormatBuilder();
        shoppingCartPageContainer.innerHTML = emptyCartFormat;
    }
}

function initQuantityModifyingButtons() {
    let increaseQuantityButtons = [...document.querySelectorAll(".increase-quantity")];
    let decreaseQuantityButtons = [...document.querySelectorAll(".decrease-quantity")];

    increaseQuantityButtons.forEach((button) => {
        button.addEventListener('click', async function () {
            event.preventDefault();

            const increaseQuantity = 1;
            updateCartItems(button, increaseQuantity);
        })
    })
    decreaseQuantityButtons.forEach((button) => {
        button.addEventListener('click', async function () {
            event.preventDefault();

            const decreaseQuantity = -1;
            updateCartItems(button, decreaseQuantity);
        })
    })
  
}

function initDeleteShoppingCartItemsButtonsFunctionality() {
    let deleteShoppingCartItemsButtons = [...document.querySelectorAll(".filled-cart-item-delete")];

    deleteShoppingCartItemsButtons.forEach((button) => {
        button.addEventListener('click', async (event) => {
            event.preventDefault();
            let productId = button.getAttribute("data-product-id");
            console.log(productId);
            //let url = "api/remove-cart-item";
            //let httpRequest = "delete";
            //let data = await updateCart(button, httpRequest, url);

            //loadCartItems(data);
        })
    })
}


async function updateCartItems(htmlElement, quantity) {
    let productId = htmlElement.getAttribute("data-product-id");
    let data = await dataHandler.addNewItemToCart(productId, quantity);

    updateCart(data, itemsInShoppingCartFields)
    loadShoppingCartPage();
}



loadShoppingCartPage();
