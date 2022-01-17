import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";
import { dataHandler } from "/js/dataHandler.js";

let countriesApiUrl = "https://countriesnow.space/api/v0.1/countries";

async function getCountriesApi() {
    if (!localStorage.getItem("countries")) {
        let getCountriesFromApiHandler = dataHandler.getData;
        let apiData = await getCountriesFromApiHandler(countriesApiUrl);
        let countries = apiData.data;
        localStorage.setItem("countries", JSON.stringify(countries));
    }
}

function displayCartQuantityInForm() {
    let cartTotalQuantityBadgeContainer = document.querySelector(".badge-pill");
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    cartTotalQuantityBadgeContainer.innerHTML = shoppingCartQuantity;
}

function loadItemsinCheckoutPageItemsContainer() {
    let items = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    let checkoutPageItemsContainer = document.querySelector(".list-group.mb-3");
    let itemsFormat = "";
    let totalCartSum = 0;
    const cartItemTemplateBuilder = htmlFactory(
        htmlTemplates.formatCheckoutPageCartItem
    );
    const cartTotalTemplateBuilder = htmlFactory(
        htmlTemplates.formatCheckoutPageCartTotal
    );

    for (let i = 0; i < items.length; i++) {
        const cartItemTemplate = cartItemTemplateBuilder(items[i]);

        itemsFormat += cartItemTemplate;
        totalCartSum += items[i].productPrice * items[i].productQuantity;
    }
    const cartTotalTemplate = cartTotalTemplateBuilder(totalCartSum);
    itemsFormat += cartTotalTemplate;
    checkoutPageItemsContainer.innerHTML = itemsFormat;
}

function initIntlPhoneNumber() {
    const phoneInputField = document.querySelector(".phone input");
    const phoneInput = window.intlTelInput(phoneInputField, {
        utilsScript:
            "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    });
    phoneInputField.addEventListener("input", () => {
        const invalidFeedbackContainer = document
            .querySelector(".mb-3.phone")
            .querySelector(".invalid-feedback");
        const SelectedCountryData = phoneInput.getSelectedCountryData().name;
        const isValidNumber = phoneInput.isValidNumber();
        invalidFeedbackContainer.style.display = "block";

        if (isValidNumber || phoneInputField.value == "") {
            invalidFeedbackContainer.innerHTML = "";
        } else {
            invalidFeedbackContainer.innerHTML = `Ivalid phone number format for <strong>${SelectedCountryData}</strong>!`;
        }
    });
}

function initCountriesInSelector() {
    let countries = JSON.parse(localStorage.getItem("countries"));
    let countrySelector = document.querySelector("#Country");
    let format = "";
    format += `<option value="">Choose...</option>`;
    for (let i = 0; i < countries.length; i++) {
        format += `<option value="${countries[i].country}">${countries[i].country}</option>`;
    }
    countrySelector.innerHTML = format;
    initCountrySelectorOnChangeFunctionality();
}

function initCountrySelectorOnChangeFunctionality() {
    let countrySelector = document.querySelector("#Country");
    countrySelector.addEventListener("change", (event) => {
        event.preventDefault();

        let countryName = countrySelector.value;
        if (countryName != "") {
            let countries = JSON.parse(localStorage.getItem("countries"));
            let countryCities = countries.filter((obj) => {
                return obj.country === countryName;
            })[0].cities;

            initCitiesInSelector(countryCities);
        } else {
            initCitiesInSelector(-1);
        }
    });
}

function initCitiesInSelector(countryCities = -1) {
    let citySelector = document.getElementById("City");

    let format = "";
    format += `<option value="choose">Choose...</option>`;
    if (countryCities != -1) {
        for (let i = 0; i < countryCities.length; i++) {
            format += `<option value="${countryCities[i]}">${countryCities[i]}</option>`;
        }
    }

    citySelector.innerHTML = format;
}

function initGoToPaymentEvent() {
    $(".btn").on("click", () => {
        console.log("Going to payment");
    });
}

await getCountriesApi();
displayCartQuantityInForm();
loadItemsinCheckoutPageItemsContainer();
initIntlPhoneNumber();
initCountriesInSelector();
initGoToPaymentEvent();
