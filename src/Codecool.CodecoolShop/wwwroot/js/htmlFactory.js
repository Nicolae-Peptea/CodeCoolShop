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
    return `<tr>
                <td id="img">
                <img src="/img/${item.productName}.jpg" class="img-fluid img-thumbnail" alt="${item.productName}">
                </td>
                <td id="name">${item.productName}</td>
                <td id="price">${formatter.format(item.productPrice)}</td>
                <td class="qty" id="qty">
                    <button class="decrease-quantity" name="decrease-quantity" type="submit" data-product-id="${item.productId}">-</button>
                    <span class="item-quantity">${item.productQuantity}</span>
                    <button class="increase-quantity" name="increase-quantity" type="submit"data-product-id="${item.productId}">+</button>
                </td>
                <td id="total">${formatter.format(item.productPrice * item.productQuantity)}</td>
                <td id="actions">
                    <button class="delete-cart-item" data-product-id="${item.productId}"><i class="fa fa-trash-o"></i></button>
                </td>
            </tr>`;
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
                    <p><strong>${item.productQuantity} x ${formatter.format(item.productPrice)}</strong></p>
                </div>
                
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
                <button id="checkout-button">Proceed to Checkout</button>
                <input id="cart-items" type="hidden" name="cartItems">
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
    let modalBody = `<table class="table table-image">
                        <thead>
                        <tr>
                            <th id="img" scope="col"></th>
                            <th id="name" scope="col">Name</th>
                            <th id="price" scope="col">Price</th>
                            <th id="qty" scope="col">Qty</th>
                            <th id="total" scope="col">Total</th>
                            <th id="actions" scope="col">Actions</th>
                        </tr>
                        </thead>

                        <tbody></tbody>
                    </table>

                    <div class="d-flex justify-content-end">
                        <h5>Total: <span class="price text-success"></span></h5>
                    </div>`;
    return modalCartBuilder(modalBody, true);
}

function emptyDropdownCartBuilder() {
    let modalBody = `<p class="card-text text-center">The shopping cart is empty.</p>`;
    return modalCartBuilder(modalBody);
}

function addModalCheckoutButton() {
    return `<input id="total" type="hidden" name="total-value">
            <input class="button btn btn-success" type="submit" value="Checkout">`;
}

function modalCartBuilder(modalBody, filled = false) {
    return `<div class="modal-dialog modal-lg modal-dialog-centered" role="document" >
                <div class="modal-content">

                    <div class="modal-header border-bottom-0">
                        <h5 class="modal-title" id="exampleModalLabel">
                            Your Shopping Cart
                        </h5>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        ${modalBody}
                    </div>

                    <div class="modal-footer border-top-0 d-flex justify-content-between">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        ${filled ? addModalCheckoutButton() : ""}
                    </div>
                </div>
            </div>`;
}
