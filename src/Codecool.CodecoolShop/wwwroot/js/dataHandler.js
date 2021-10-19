export let dataHandler = {
    sendCartItems: async function (items) {

        let url = "/api/checkout";
        let data = {
            'cartItems': items,
        };
        await apiPost(url, data);
    },
}

async function apiPost(url, data) {
    try {
        let response = await $.ajax({
            method: "post",
            url: url,
            data: JSON.stringify(data),
            dataType: "json",
        })

    } catch (e) {
        debugger;
        console.log("Error" + e);
    }
}
