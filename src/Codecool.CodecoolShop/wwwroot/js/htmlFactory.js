let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });

export const htmlTemplates = {
    formatShoppingCartItem: 1,
    formatShoppingCartPageItem: 2,
    emptyCartFormat: 3,
    filledCartFormat: 4,
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

function formatShoppingCartPageItemBuilder(item) {
    return `
        <div class="filled-cart-item">
            <div class="filled-cart-item-left">
                <img src="/img/${item.Product.Name}.jpg" alt="${item.Product.Name}" />
            </div>
            <div class="filled-cart-item-right">
                <a class="filled-cart-item-name"
                    href="" title="${item.Product.Name}">${item.Product.Name}
                </a>

                <div class="filled-cart-item-quantity">
                    <button class="decrease-quantity" name="decrease-quantity" type="submit" data-product-id="${item.Product.Id}"><i class="arrow down"></i></button>
                    <p><strong>${item.Quantity} ${item.Quantity == 1 ? "Piece" : "Pieces"}</strong></p>
                    <button class="increase-quantity" name="increase-quantity" type="submit"data-product-id="${item.Product.Id}"><i class="arrow up"></i></button>
                </div>

                <button class="filled-cart-item-delete" data-product-id="${item.Product.Id}"><i class="fa fa-trash-o"></i></button>

                <span class="filled-cart-item-price">${formatter.format(item.Product.DefaultPrice * item.Quantity)}</span>
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
                    <span class="total-right"></span>
                </div>
                <button onclick="location.href='/Product/Checkout'">Checkout</button>
            </div>
        </div>`;
}