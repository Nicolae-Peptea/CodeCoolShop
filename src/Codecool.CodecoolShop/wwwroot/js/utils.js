



async function ajaxFetch(data, httpRequestType, urlRoute) {
    try {
        let response = await $.ajax({
            url: urlRoute,
            data: { id: data },
            method: httpRequestType,
            dataType: "json",
        })

        return response;

    } catch (e) {
        console.log("Error" + e);
    }
}