
export async function updateCartItems(htmlWithAttribute, quantity, functionToUpdate) {
    let productId = htmlWithAttribute.getAttribute("data-product-id");
    let data = await functionToUpdate(productId, quantity);

    updateCart(data)
}


export async function deleteCartItems(htmlWithAttribute, functionToUpdate) {

    let productId = htmlWithAttribute.getAttribute("data-product-id");
    let data = await functionToUpdate(productId);

    updateCart(data)
}

export function showCartQuantityAfterLoading() {

    let itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    itemsInShoppingCartFields.forEach((element) => {
        element.innerHTML = shoppingCartQuantity;
    })
}


function updateCart(response) {
    let totalItems = 0;
    let totalItemsInShoppingCart = [...document.querySelectorAll(".badge")];

    response.forEach((element) => {
        totalItems += element.Quantity;
    })

    totalItemsInShoppingCart.forEach((field) => {
        field.innerHTML = totalItems;
    })

    setSessionStorage(totalItems, response);
}


function setSessionStorage(totalItems, jsonResonse) {
    if (totalItems === 0) {
        sessionStorage.clear();
    }
    else {
        sessionStorage.setItem("shoppingCartQuantity", totalItems);
        sessionStorage.setItem("shoppingCartItems", JSON.stringify(jsonResonse));
    }
}


