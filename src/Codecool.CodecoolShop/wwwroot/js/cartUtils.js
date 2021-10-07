export function updateCart(response, htmlElements) {
    let totalItems = 0;

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
