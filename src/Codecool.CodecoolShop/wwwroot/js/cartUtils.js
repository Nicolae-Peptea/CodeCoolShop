export function updateCart(response, htmlElements) {
    let totalItems = 0;

    response.forEach((element) => {
        totalItems += element.Quantity;
    })

    htmlElements.forEach((field) => {
        field.innerHTML = totalItems;
    })

    setSessionStorageForShoppingCart(totalItems, response);
}


function setSessionStorageForShoppingCart(totalItems, jsonResonse) {
    sessionStorage.setItem("shoppingCartQuantity", totalItems);
    sessionStorage.setItem("shoppingCartItems", JSON.stringify(jsonResonse));
}
