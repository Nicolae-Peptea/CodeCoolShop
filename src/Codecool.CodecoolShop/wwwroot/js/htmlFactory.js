let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });

export const htmlTemplates = {
    formatShoppingCartItem: 1,
};

export function htmlFactory(template) {
    switch (template) {
        case htmlTemplates.formatShoppingCartItem:
            return formatShoppingCartItemBuilder;
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