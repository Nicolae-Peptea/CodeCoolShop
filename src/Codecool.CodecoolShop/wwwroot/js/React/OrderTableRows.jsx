import useFetch from '/js/React/useFetch.jsx';

const OrderTableRows = ({ url, isDetails, setIsDetails, setProducts }) => {
    const [baseLink, setBaseLink] = React.useState("https://localhost:5001");
    const { data: orders, isLoading, error } = useFetch(baseLink + url);

    React.useEffect(() => {
        const rows = document.querySelectorAll('.row');
        let back = document.querySelector(".back");

        for (const row of rows) {
            row.addEventListener('click', (e) => {
                let target = e.currentTarget;

                let currentOrderId = target.dataset.orderId;

                fetch(baseLink + "/order/getorderproducts?id=" + currentOrderId)
                    .then((response) => {
                        if (!response.ok) {
                            throw Error("Could not fetch the data for that resource...");
                        }
                        return response.json();
                    })
                    .then((data) => {
                        console.log(data);
                        setProducts(data);
                    });

                setIsDetails(true);
            });
        }

        if (back !== null) {
            back.addEventListener("click", () => {
                setIsDetails(false);
            })
        }
    }, [orders, isDetails]);

    return (
        <>
            <div className="table-content">
                {error ?? <div>{error}</div>}
                {isLoading && <div>Loading...</div>}
                {orders &&
                    orders.map((order, i) =>
                        <div className="row" key={i + 1} data-order-id={order.Id}>
                            <div className="order-preview">
                                <div className="column order-number">{i + 1}</div>
                                <div className="column order-date-preview">{(order.OrderPlaced).split("T").join(' ')}</div>
                            </div>

                        </div>
                    )
                }
            </div>
        </>

    );
}

export default OrderTableRows;