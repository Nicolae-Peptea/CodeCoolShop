import {
    displayCartQuantityOnDesignatedFields,
    updateCart,
    loadCartModal,
} from "/js/cartUtils.js";

$("body").on("click", "#cart", (event) => {
    event.preventDefault();

    $(document).ready(function () {
        $("#cartModal").modal("show");
    });

    loadCartModal();
});

function initBuyButtons() {
    const quantity = 1;
    let buyButtons = [...document.querySelectorAll("#add-to-cart")];
    buyButtons.forEach((button) => {
        initClickEventOnButtons(button, quantity);
    });
}

function initClickEventOnButtons(button, quantity) {
    button.addEventListener("click", (event) => {
        event.preventDefault();
        updateCart(button, quantity);
    });
}

displayCartQuantityOnDesignatedFields();
initBuyButtons();
