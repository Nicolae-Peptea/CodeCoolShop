export function updateCart(response, htmlElements) {
    let totalItems = 0;
    let itemsInShoppingCartFields = [...document.querySelectorAll(".badge")];

    response.forEach((element) => {
        totalItems += element.Quantity;
    })

    htmlElements.forEach((field) => {
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



export async function updateCartItems(htmlWithAttribute, quantity, functionToUpdate) {
    let productId = htmlWithAttribute.getAttribute("data-product-id");
    let data = await functionToUpdate(productId, quantity);

    updateCart(data, itemsInShoppingCartFields)
}


async function deleteCartItems(htmlElement) {

    let productId = htmlElement.getAttribute("data-product-id");
    let data = await dataHandler.removeItemFromCart(productId);

    updateCart(data, itemsInShoppingCartFields)
}
