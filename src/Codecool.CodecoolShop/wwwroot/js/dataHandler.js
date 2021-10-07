﻿export let dataHandler = {
    addNewItemToCart: async function (itemId, quantity) {

        let url = "/api/update-cart-item";
        let data = {
            'id': itemId,
            'quantity': quantity,
        };
        return await apiPost(url, data);
    },
    removeItemFromCart: async function (itemId) {
        let url = "/api/remove-cart-item";
        let data = {
            'id': itemId,
        };
        return await apiDelete(url, data);
    },
    getData: async function (url) {
        return await apiGet(url);
    }
}

async function apiGet(url) {
    try {
        let response = await $.ajax({
            method: "get",
            url: url,
            dataType: "json",
        })

        return response;

    } catch (e) {
        console.log("Error" + e);
    }
}

async function apiPost(url, data) {
    try {
        let response = await $.ajax({
            method: "post",
            url: url,
            data: data,
            dataType: "json",
        })

        return response;

    } catch (e) {
        console.log("Error" + e);
    }
}


async function apiDelete(url, data) {
    try {
        let response = await $.ajax({
            url: url,
            data: data,
            method: "delete",
            dataType: "json",
        })

        return response;

    } catch (e) {
        console.log("Error" + e);
    }
}
