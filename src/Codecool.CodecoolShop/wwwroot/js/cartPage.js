import { loadShoppingCartPage } from "/js/cartUtils.js";
import { dataHandler } from "/js/dataHandler.js";


loadShoppingCartPage();

let checkoutButton = document.querySelector("#checkout-button");

checkoutButton.addEventListener('click', () => {
   /* event.preventDefault();*/

    const items = sessionStorage.getItem("shoppingCartItems");
/*    dataHandler.sendCartItems(items)*/


})
