import { htmlFactory, htmlTemplates } from "/js/htmlFactory.js";

let checkoutPageContainer = document.querySelector(".checkout-page-container");
let formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', });
let countriesApiUrl = "https://countriesnow.space/api/v0.1/countries";
let countries;

async function getCountriesApi() {
    try {
        let response = await $.ajax({
            method: "get",
            url: countriesApiUrl,
            dataType: "json",
        })

        countries = response.data;
        localStorage.setItem("countries", JSON.stringify(countries));

    } catch (e) {
        console.log("Error" + e);
    }
}

function displayCartQuantityInForm() {
    let cartTotalQuantityBadgeContainer = document.querySelector(".badge-pill");
    let shoppingCartQuantity = sessionStorage.getItem("shoppingCartQuantity");
    cartTotalQuantityBadgeContainer.innerHTML = shoppingCartQuantity;
}


function displayItemsFromCart() {
    let shoppingCartItemsFromSessionStorage = JSON.parse(sessionStorage.getItem("shoppingCartItems"));
    loadItemsinCheckoutPageItemsContainer(shoppingCartItemsFromSessionStorage);
}

function loadItemsinCheckoutPageItemsContainer(items) {
    let checkoutPageItemsContainer = document.querySelector(".list-group.mb-3");
    let itemsFormat = "";
    let totalCartSum = 0;
    const cartItemTemplateBuilder = htmlFactory(htmlTemplates.formatCheckoutPageCartItem);
    const cartTotalTemplateBuilder = htmlFactory(htmlTemplates.formatCheckoutPageCartTotal);

    for (let i = 0; i < items.length; i++) {
        const cartItemTemplate = cartItemTemplateBuilder(items[i]);

        itemsFormat += cartItemTemplate;
        totalCartSum += items[i].Product.DefaultPrice * items[i].Quantity;
    }
    const cartTotalTemplate = cartTotalTemplateBuilder(totalCartSum);
    itemsFormat += cartTotalTemplate;
    checkoutPageItemsContainer.innerHTML = itemsFormat;
}

function initIntlPhoneNumber() {
    const phoneInputField = document.querySelector("#phone");
    const phoneInput = window.intlTelInput(phoneInputField, {
        utilsScript:
            "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    });
    phoneInputField.addEventListener("change", () => {
        const invalidFeedbackContainer = document.querySelector(".mb-3.phone").querySelector(".invalid-feedback");
        const SelectedCountryData = (phoneInput.getSelectedCountryData()).name;
        const isValidNumber = phoneInput.isValidNumber();
        invalidFeedbackContainer.style.display = "block";

        if (isValidNumber) {
            invalidFeedbackContainer.innerHTML = "";
        }
        else {
            invalidFeedbackContainer.innerHTML = `Ivalid phone number format for <strong>${SelectedCountryData}</strong>! (Country code + Number).`;
        }

    });
}

function initCountriesInSelector() {
    let countrySelector = document.querySelector("#country");
    let format = "";
    format += `<option value="choose">Choose...</option>`;
    for (let i = 0; i < countries.length; i++) {
        format += `<option value="${countries[i].country}">${countries[i].country}</option>`;
    }
    countrySelector.innerHTML = format;
    initCountrySelectorOnChangeFunctionality();
}

function initCountrySelectorOnChangeFunctionality() {
    let countrySelector = document.querySelector("#country");
    countrySelector.addEventListener("change", (event) => {
        event.preventDefault();
        initCitiesInSelector();
    })
}

function initCitiesInSelector() {
    let countrySelector = document.getElementById("country");
    let citySelector = document.getElementById("city");
    let countryName = countrySelector.value;
    let countryCities = countries.filter(obj => {
        return obj.country === countryName;
    })[0].cities;
    let format = "";
    format += `<option value="choose">Choose...</option>`;
    for (let i = 0; i < countryCities.length; i++) {
        format += `<option value="${countryCities[i]}">${countryCities[i]}</option>`;
    }
    citySelector.innerHTML = format;
}

function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function validate() {
    const email = $("#email").val();

    if (validateEmail(email)) {
        $("#email").css("border", "2px solid green");
        $(".mb-3.email").children(".invalid-feedback").css("display", "");
    } else {
        $(".mb-3.email").children(".invalid-feedback").css("display", "block");
        $("#email").css("border", "2px solid red");
    }
    return false;
}

function initGoToPaymentEvent() {
    let fieldsContainerParents = [...document.querySelectorAll("div.mb-3")];
    $(".btn").on("click", () => {
        event.preventDefault();
        let counter = 0;
        fieldsContainerParents.forEach((container) => {
            if (container.querySelector("input")) {
                if (container.querySelector("input").value == "") {
                    container.querySelector(".invalid-feedback").style.display = "block";
                    counter++;
                }
                else {
                    container.querySelector(".invalid-feedback").style.display = "";
                    container.querySelector("input").style.border = "2px solid green";
                }
            }
            else if (container.querySelector("select")) {
                if (container.querySelector("select").value == "Choose...") {
                    container.querySelector(".invalid-feedback").style.display = "block";
                    counter++;
                }
                else {
                    container.querySelector(".invalid-feedback").style.display = "";
                    container.querySelector("select").style.border = "2px solid green";
                }
            }
        })
        if (counter == 0) {
            console.log("Going to payment");
        }
    });
}


$("#email").on("change", validate);

displayCartQuantityInForm();
displayItemsFromCart();
initIntlPhoneNumber();
if (localStorage.getItem("countries")) {
    countries = JSON.parse(localStorage.getItem("countries"));
}
else {
    await getCountriesApi();
}
initCountriesInSelector();
initGoToPaymentEvent();
