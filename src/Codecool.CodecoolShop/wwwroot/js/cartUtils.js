import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";

let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });
let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");


export function initClickEventOnButtons(button, quantity) {
    button.addEventListener('click', () => {
        event.preventDefault();
        updateCart(button, quantity);

        let shoppingCart = document.querySelector(".shopping-cart");
        let filledCart = document.querySelector("#filled-cart");

        if (shoppingCart.style.visibility == "visible") {
            loadCartItems();
        }
        if (filledCart) {
            loadShoppingCartPage();
        }
    })
}


export function updateCart(htmlElement, quantity) {

    const itemtoAdd = createItemForSessionStorage(htmlElement)
    const sessionStorageItems = sessionStorage.getItem("shoppingCartItems");
    let cartItems = [];

    if (sessionStorageItems !== null) {
        cartItems = JSON.parse(sessionStorageItems);
    }

    const itemAlreadyInCart = cartItems
        .filter(item => item.productId === itemtoAdd.productId);

    if (itemAlreadyInCart.length > 0) {
        itemAlreadyInCart[0].productQuantity += quantity;
        cartItems = cartItems.filter(item => item.productQuantity > 0);
    }
    else {
        cartItems.push(itemtoAdd);
    }

    setSessionStorage(cartItems);
    showCartQuantityAfterLoading();
}


function setSessionStorage(items) {

    if (items.length > 0) {
        sessionStorage.setItem("shoppingCartItems", JSON.stringify(items));
    } else {
        sessionStorage.clear();
    }
}


export function showCartQuantityAfterLoading() {
    const itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];
    const shoppingCartItems = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    let quantity = 0;

    if (shoppingCartItems !== null) {
        shoppingCartItems.forEach(item => quantity += item.productQuantity)
    }

    itemsInShoppingCartFields.forEach((element) => {
        element.innerHTML = quantity ? quantity : "";
    })
}


function createItemForSessionStorage(htmlElement) {

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


export function loadCartItems() {
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));

    if (items != null) {
        attachTemplateToDropdownCart(htmlTemplates.filledDropdownCartBody);
        cartItemsLoader(htmlTemplates.formatShoppingCartItem, ".shopping-cart-items", ".main-color-text");
        $(".shopping-cart > .button").on("click", () => { location.href = "/Product/Cart"; });
    }
    else {
        attachTemplateToDropdownCart(htmlTemplates.emptyDropdownCartBody);
        $(".shopping-cart > .button").on("click", () => { location.href = "/Product/Cart"; });
    }
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
        const quantity = getItemQuantity(button) * - 1;
        initClickEventOnButtons(button, quantity);
    })
}


function getItemQuantity(htmlElement) {

    const sessionStorageObjects = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    const productId = htmlElement.getAttribute("data-product-id");

    const [first] = sessionStorageObjects
        .filter(element => element.productId === productId);

    return first.productQuantity;
}


export function attachTemplateToDropdownCart(template) {
    let shoppingCart = document.querySelector(".shopping-cart");
    const cartDropdownBuilder = htmlFactory(template);
    const cartDropdownBodyTemplate = cartDropdownBuilder();
    shoppingCart.innerHTML = cartDropdownBodyTemplate;
}


export function loadShoppingCartPage() {
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    (items) ? loadFilledCartPage() : loadEmptyCartPage();
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


function initQuantityModifyingButtons() {
    let increaseQuantityButtons = [...document.querySelectorAll(".increase-quantity")];
    let decreaseQuantityButtons = [...document.querySelectorAll(".decrease-quantity")];

    increaseQuantityButtons.forEach((button) => {
        const increaseQuantity = 1;
        initClickEventOnButtons(button, increaseQuantity);
    })

    decreaseQuantityButtons.forEach((button) => {
        const increaseQuantity = -1;
        initClickEventOnButtons(button, increaseQuantity);
    })
}


function loadEmptyCartPage() {
    const emptyCartFormatBuilder = htmlFactory(htmlTemplates.emptyCartFormat);
    const emptyCartFormat = emptyCartFormatBuilder();
    shoppingCartPageContainer.innerHTML = emptyCartFormat;
}
