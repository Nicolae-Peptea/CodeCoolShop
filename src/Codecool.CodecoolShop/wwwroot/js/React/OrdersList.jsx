import OrderTableRows from '/js/React/OrderTableRows.jsx';
import useFetch from '/js/React/useFetch.jsx';

const OrdersList = ({ url }) => {
    const [baseLink, setBaseLink] = React.useState("https://localhost:5001");
    const { data: orders, isLoading, error } = useFetch(baseLink + url);

    React.useEffect(() => {
        const rows = document.querySelectorAll('.row');
        for (const row of rows) {
            row.addEventListener('click', (e) => {
                let orderDetailsContainer =
                    e.currentTarget.querySelector(".order-details");

                orderDetailsContainer.style.display =
                    orderDetailsContainer.style.display === ""
                        ? "flex" : "";
            });
        }
    }, [orders]);

    return (
        <div className="orders-list">
            <div className="table-header">
                <div className="column">Number</div>
                <div className="column">Placed</div>
                <div className="column">Total</div>
            </div>
            {error ?? <div>{error}</div>}
            {isLoading && <div>Loading...</div>}
            {orders && <OrderTableRows orders={orders} />}
        </div>
    );
}

export default OrdersList;