import {
    initClickEventOnButtons, showCartQuantityAfterLoading,
    loadCartModal
} from "/js/cartUtils.js";

let buyButtons = [...document.querySelectorAll("#add-to-cart")];

function initBuyButtons() {
    const quantity = 1;
    buyButtons.forEach((button) => {
        initClickEventOnButtons(button, quantity);
    })
}

function initCartButtonFunctionality() {
    let cartButton = document.querySelector("#cart");

    cartButton.addEventListener('click', () => {
        event.preventDefault();

        $(document).ready(function () {
            $('#cartModal').modal('show');
        });

        loadCartModal();
    })
}


showCartQuantityAfterLoading();
initCartButtonFunctionality();
initBuyButtons();