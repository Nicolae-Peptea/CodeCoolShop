export let dataHandler = {
    addNewItemToCart: async function (itemId) {

        let url = "api/add-cart-item";
        let data = {
            'id': itemId,
        };
        return await apiPost(url, data);
    },
    removeItemFromCart: async function (itemId) {
        let url = "api/remove-cart-item";
        let data = {
            'id': itemId,
        };
        return await apiDelete(url, data);
    },
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


async function apiPut(url, data) {
    try {
        let response = await $.ajax({
            url: url,
            data: data,
            method: "put",
            dataType: "json",
        })

        return response;

    } catch (e) {
        console.log("Error" + e);
    }
}