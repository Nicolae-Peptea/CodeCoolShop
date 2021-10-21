import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";

let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });
let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");


export function getStorage() {
    return sessionStorage.getItem("shoppingCartItems");
}


export function initClickEventOnButtons(button, quantity) {
    button.addEventListener('click', () => {
        event.preventDefault();
        updateCart(button, quantity);

        let shoppingCart = document.querySelector(".modal");
        let filledCart = document.querySelector("#filled-cart");

        if (shoppingCart.style.display == "block") {
            loadCartModal();
        }

        if (filledCart) {
            loadShoppingCartPage();
        }
    })
}


export function updateCart(htmlElement, quantity) {

    const itemtoAdd = createItemForSessionStorage(htmlElement)
    const sessionStorageItems = getStorage();
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

    return {
        "productId": productId,
        "productName": productName,
        "productPrice": productPrice,
        "productQuantity": 1,
    }
}


export function loadCartModal() {
    let items = JSON.parse(getStorage());

    if (items != null) {
        loadFilledCartModal();
    }
    else {
        loadEmptyCartModal();
    }
}


function loadFilledCartModal() {
    let filledModalBodyTemplate = htmlTemplates.filledDropdownCartBody;
    let itemTemplate = htmlTemplates.formatShoppingCartItem;
    let itemsContainerClass = "#cartModal > div > div > div.modal-body > table > tbody";
    let totalCartContainerClass = "#cartModal > div > div > div.modal-body > div > h5 > span";

    attachTemplateToDropdownCart(filledModalBodyTemplate);
    cartItemsLoader(itemTemplate, itemsContainerClass, totalCartContainerClass);
    initQuantityModifyingButtons();

    $("#cartModal > div > div > div.modal-footer.border-top-0.d-flex.justify-content-between > input.button.btn.btn-success")
        .on("click", () => {
            let inputValue = document.querySelector("input#total");
            let total = 0;
            items.forEach(element => total += element.productQuantity * element.productPrice);
            inputValue.value = total;
        });
}

function loadEmptyCartModal() {
    let emptyModalBodyTemplate = htmlTemplates.emptyDropdownCartBody;
    attachTemplateToDropdownCart(emptyModalBodyTemplate);
}


function cartItemsLoader(template, itemsContainerClass, totalCartContainerClass) {
    let items = JSON.parse(getStorage());
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
}


export function attachTemplateToDropdownCart(template) {
    let shoppingCart = document.querySelector(".modal");
    const cartDropdownBuilder = htmlFactory(template);
    const cartDropdownBodyTemplate = cartDropdownBuilder();
    shoppingCart.innerHTML = cartDropdownBodyTemplate;
}


export function loadShoppingCartPage() {
    let items = JSON.parse(getStorage());
    (items) ? loadFilledCartPage(items) : loadEmptyCartPage();
}


function loadFilledCartPage(cartItems) {
    const filledCartFormatBuilder = htmlFactory(htmlTemplates.filledCartFormat);
    const filledCartFormat = filledCartFormatBuilder();

    shoppingCartPageContainer.innerHTML = filledCartFormat;
    loadItemsInShoppingCartPageContainer();

    $("#checkout-button").on("click", () => {
        let inputValue = document.querySelector("#cart-items");
        inputValue.value = JSON.stringify(cartItems);
    });
}


function loadItemsInShoppingCartPageContainer() {
    let items = JSON.parse(getStorage());
    if (items) {
        cartItemsLoader(htmlTemplates.formatShoppingCartPageItem,
            "#filled-cart > .items", ".total-cart-amount");
    }
}


function initQuantityModifyingButtons() {
    let increaseQuantityButtons = [...document.querySelectorAll(".increase-quantity")];
    let decreaseQuantityButtons = [...document.querySelectorAll(".decrease-quantity")];
    let deleteItemButtons = [...document.querySelectorAll(".delete-cart-item")];

    increaseQuantityButtons.forEach((button) => {
        const quantity = 1;
        initQuantityModifyingButton(button, quantity);
    })

    decreaseQuantityButtons.forEach((button) => {
        const quantity = -1;
        initQuantityModifyingButton(button, quantity);
    })

    deleteItemButtons.forEach((button) => {
        deleteShoppingCartItemsButtonsFunctionality(button);
    })
}


function initQuantityModifyingButton(button, quantity) {
    button.addEventListener("click", () => {
        event.preventDefault();
        updateCart(button, quantity);

        if (getStorage() !== null) {
            modifyCartItemQuantity(button);
        }
        else {
            loadEmptyCartModal();
        }
    })
}


function modifyCartItemQuantity(button) {
    let id = button.getAttribute('data-product-id');
    let htmlItem = document.querySelector(`tr[data-product-id="${id}"]`);
    let items = JSON.parse(getStorage());

    let [cartItem] = items.filter(item => item.productId === id);

    if (cartItem === undefined) {
        htmlItem.remove();
    }
    else {
        let currentQuantity = htmlItem.querySelector("td .item-quantity");
        let currenTotal = htmlItem.querySelector("td#total");
        currentQuantity.innerHTML = cartItem.productQuantity;
        currenTotal.innerHTML = formatter.format(parseFloat(cartItem.productQuantity) * parseFloat(cartItem.productPrice));
    }

    let totalPriceField = document.querySelector("span.price");
    let totalValue = getStorageCartTotalValue();
    totalPriceField.innerHTML = formatter.format(totalValue);
}


function loadEmptyCartPage() {
    const emptyCartFormatBuilder = htmlFactory(htmlTemplates.emptyCartFormat);
    const emptyCartFormat = emptyCartFormatBuilder();
    shoppingCartPageContainer.innerHTML = emptyCartFormat;
}

//Section: Delete cart item
function deleteShoppingCartItemsButtonsFunctionality(button) {
    button.addEventListener("click", () => {
        event.preventDefault();
        let quantity = getItemQuantity(event.currentTarget) * (- 1);
        updateCart(button, quantity);
        let isStorageEmpty = getStorage() !== null;

        if (isStorageEmpty) {
            deleteCartItem(event.currentTarget);
        }
        else {
            loadEmptyCartModal();
        }
    })
}


function deleteCartItem(button) {
    let id = button.getAttribute('data-product-id');
    let htmlItem = document.querySelector(`tr[data-product-id="${id}"]`);
    htmlItem.remove();

    let totalPriceField = document.querySelector("span.price");
    let totalValue = getStorageCartTotalValue();
    totalPriceField.innerHTML = formatter.format(totalValue);
}
//Section-End: Delete cart item

function getItemQuantity(htmlElement) {

    const sessionStorageObjects = JSON.parse(getStorage());
    let id = htmlElement.getAttribute("data-product-id");

    const [first] = sessionStorageObjects
        .filter(element => element.productId === id);

    return first.productQuantity;
}


function getStorageCartTotalValue() {
    let items = JSON.parse(getStorage());
    let total = 0;

    for (let item of items) {
        total += item.productPrice * item.productQuantity;
    }
    return total;
}