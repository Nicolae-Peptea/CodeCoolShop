import OrderProductList from '/js/React/OrderProductList.jsx';
import useFetch from '/js/React/useFetch.jsx';

const OrderTableRows = ({ url }) => {
    const [baseLink, setBaseLink] = React.useState("https://localhost:5001");
    const { data: orders, isLoading, error } = useFetch(baseLink + url);
    const [products, setProducts] = React.useState("");

    React.useEffect(() => {
        const rows = document.querySelectorAll('.row');
        let orderDetailsContainer = document.querySelector(".order-details");
        for (const row of rows) {
            row.addEventListener('click', (e) => {
                let target = e.currentTarget;

                let currentOrderId = target.dataset.orderId;

                if (orderDetailsContainer.style.display === "") {
                    fetch(baseLink + "/order/getorderproducts?id=" + currentOrderId)
                        .then((response) => {
                            if (!response.ok) {
                                throw Error("Could not fetch the data for that resource...");
                            }
                            return response.json();
                        })
                        .then((data) => {
                            setProducts(data);
                        });
                    orderDetailsContainer.style.display = "flex";
                } else {
                    orderDetailsContainer.style.display = "";
                }
            });
        }
    }, [orders]);

    return (
        <>
            <div className="table-content">
                {error ?? <div>{error}</div>}
                {isLoading && <div>Loading...</div>}
                {orders &&
                    orders.map((order, i) =>
                        <div className="row" key={i + 1} data-order-id={order.Id}>
                            <div className="order-preview">
                                <div className="column">{i + 1}</div>
                                <div className="column">{(order.OrderPlaced).split("T").join(' ')}</div>
                            </div>

                        </div>
                    )
                }
            </div>
            <OrderProductList products={products} />
        </>

    );
}

export default OrderTableRows;