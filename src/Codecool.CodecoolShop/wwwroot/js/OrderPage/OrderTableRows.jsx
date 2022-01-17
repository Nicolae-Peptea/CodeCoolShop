import useFetch from "/js/React/useFetch.jsx";

const OrderTableRows = ({ url, isDetails, setIsDetails, setProducts }) => {
    //  const baseLink = process.env.REACT_APP_BACKEND_LINK;
    const baseLink = "https://localhost:5001";
    const { data: orders, isLoading, error } = useFetch(baseLink + url);

    const rowClickHandler = (e) => {
        e.preventDefault();

        const target = e.currentTarget;
        const currentOrderId = target.dataset.orderId;

        fetch(baseLink + "/order/getorderproducts?id=" + currentOrderId)
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                setProducts(data);
            })
            .catch((error) => {
                throw Error(
                    error.message ||
                        "Could not fetch the data for that resource..."
                );
            });

        setIsDetails(true);
    };

    React.useEffect(() => {
        let back = document.querySelector(".back");

        if (back !== null) {
            back.addEventListener("click", () => {
                setIsDetails(false);
            });
        }
    }, [orders, isDetails]);

    return (
        <div className="table-content">
            <div><h1>Orders</h1></div>
            <div className="solid" />
            {error ?? <div>{error}</div>}
            {!!isLoading &&
                <div className="lds-spinner"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>}
            {orders &&
                <ul className="order-list">
                    {orders.map((order, i) => (
                        <li
                            className="order-hist-box"
                            key={i + 1}
                            data-order-id={order.Id}
                            onClick={rowClickHandler}
                        >
                            <div className="order-head">
                                <div className="order-head-info go-left">
                                    <h2>
                                        Order no.  <span className="order-number">{i + 1}</span>
                                    </h2>
                                    <p>Placed on: 
                                        <span className="order-date"> {new Date(order.OrderPlaced).toLocaleString() }</span>
                                    </p>
                                </div>
                                <button
                                    className="full-white-button go-right"
                                >
                                    View
                                </button>
                            </div>
                        </li>
                    ))}
                </ul>
            }
        </div>
    );
};

export default OrderTableRows;

//orders.map((order, i) => (
//    <div
//        className="row"
//        key={i + 1}
//        data-order-id={order.Id}
//        onClick={rowClickHandler}
//    >
//        <div className="order-preview">
//            <div className="column order-number">{i + 1}</div>
//            <div className="column order-date-preview">
//                {order.OrderPlaced.split("T").join(" ")}
//            </div>
//        </div>
//    </div>
//))}