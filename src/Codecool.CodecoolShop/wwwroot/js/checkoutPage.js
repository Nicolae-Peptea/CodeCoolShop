import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";

let shoppingCartPageContainer = document.querySelector(".shopping-cart-page-container");
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });


let cartTotalQuantityBadgeContainer = document.querySelector(".badge-pill");

let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");

cartTotalQuantityBadgeContainer.innerHTML = shoppingCartQuantity;
