let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });

export const htmlTemplates = {
    formatShoppingCartItem: 1,
    formatShoppingCartPageItem: 2,
    emptyCartFormat: 3,
    filledCartFormat: 4,
    formatCheckoutPageCartItem: 5,
    formatCheckoutPageCartTotal: 6,
    filledDropdownCartBody: 7,
    emptyDropdownCartBody: 8,
};

export function htmlFactory(template) {
    switch (template) {
        case htmlTemplates.formatShoppingCartItem:
            return formatShoppingCartItemBuilder;
        case htmlTemplates.formatShoppingCartPageItem:
            return formatShoppingCartPageItemBuilder;
        case htmlTemplates.emptyCartFormat:
            return emptyCartFormatBuilder;
        case htmlTemplates.filledCartFormat:
            return filledCartFormatBuilder;
        case htmlTemplates.formatCheckoutPageCartItem:
            return checkoutPageCartItemBuilder;
        case htmlTemplates.formatCheckoutPageCartTotal:
            return checkoutPageCartTotalBuilder;
        case htmlTemplates.filledDropdownCartBody:
            return filledDropdownCartBuilder;
        case htmlTemplates.emptyDropdownCartBody:
            return emptyDropdownCartBuilder;
        default:
            console.error("Undefined template: " + template);
            return () => {
                return "";
            };
    }
}

function formatShoppingCartItemBuilder(item) {
    return `
        <li class="clearfix">
            <img src="/img/${item.productName}.jpg" alt="${item.productName}" />
            
            <div class="cart-details-container">
                <span class="item-name">${item.productName}</span>
                <span class="item-price">${formatter.format(item.productPrice * item.productQuantity)}</span>
            </div>

            <span class="item-quantity">x ${item.productQuantity}</span>
            <button class="delete-cart-item" data-product-id="${item.productId}"><i class="fa fa-trash-o"></i></button>
        </li>`;
}

function formatShoppingCartPageItemBuilder(item) {
    return `
        <div class="filled-cart-item">
            <div class="filled-cart-item-left">
                <img src="/img/${item.productName}.jpg" alt="${item.productName}" />
            </div>
            <div class="filled-cart-item-right">
                <a class="filled-cart-item-name"
                    href="" title="${item.productName}">${item.productName}
                </a>

                <div class="filled-cart-item-quantity">
                    <button class="decrease-quantity" name="decrease-quantity" type="submit" data-product-id="${item.productId}"><i class="arrow down"></i></button>
                    <p><strong>${item.productQuantity} ${item.productQuantity == 1 ? "Piece" : "Pieces"}</strong></p>
                    <button class="increase-quantity" name="increase-quantity" type="submit"data-product-id="${item.productId}"><i class="arrow up"></i></button>
                </div>

                <button class="filled-cart-item-delete delete-cart-item" data-product-id="${item.productId}"><i class="fa fa-trash-o"></i></button>

                <span class="filled-cart-item-price">${formatter.format(item.productPrice * item.productQuantity)}</span>
            </div>
        </div>`;
}

function emptyCartFormatBuilder() {
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

function filledCartFormatBuilder() {
    return `
        <h1>${document.title.split("-").shift()}</h1>

        <div id="filled-cart">
            <div class="items"></div>
            <div class="shopping-cart-summary">
                <div class="shopping-cart-page-total">
                    <span class="total-left"><strong>Total:</strong></span>
                    <span class="total-right total-cart-amount"></span>
                </div>
                <button onclick="location.href='/Product/Checkout'">Proceed to Checkout</button>
            </div>
        </div>`;
}

function checkoutPageCartItemBuilder(item) {
    return `
        <li class="list-group-item d-flex justify-content-between lh-condensed">
            <div>
                <h6 class="my-0">${item.productName}</h6>
                <small class="text-muted">x ${item.productQuantity}</small>
            </div>
            <span class="text-muted">${formatter.format(item.productPrice * item.productQuantity)}</span>
        </li>`;
}

function checkoutPageCartTotalBuilder(total) {
    return `
        <li class="list-group-item d-flex justify-content-between">
            <span>Total (USD)</span>
            <strong>${formatter.format(total)}</strong>
        </li>`;
}

function filledDropdownCartBuilder() {
    return `<div class="triangle"></div>
            <ul class="shopping-cart-items fixed-content"></ul>

            <div class="shopping-cart-total">
                <span class="lighter-text">Total:</span>
                <span class="main-color-text total-cart-amount"></span>
            </div>

            <a class="button">Go to Cart</a>`;
}

function emptyDropdownCartBuilder() {
    return `<div class="triangle"></div>
            <p class="card-text text-center">The shopping cart is empty.</p>
            <a class="button">Go to Cart</a>`;
}
