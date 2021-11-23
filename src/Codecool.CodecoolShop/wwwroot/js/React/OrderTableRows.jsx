import { useState, useEffect } from "react";
import useFetch from "/js/React/useFetch.jsx";

const OrderTableRows = ({ url, isDetails, setIsDetails, setProducts }) => {
    //  const baseLink = process.env.REACT_APP_BACKEND_LINK;
    const baseLink = "https://localhost:5001";
    const { data: orders, isLoading, error } = useFetch(baseLink + url);

    const [total, setTotal] = useState(0);

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

    useEffect(() => {
        let back = document.querySelector(".back");

        if (back !== null) {
            back.addEventListener("click", () => {
                setIsDetails(false);
            });
        }
    }, [orders, isDetails]);

    useEffect(() => {
        setTotal(
            orders
                .map((o) => o.PricePerProduct * o.productQuantity)
                .reduce((a, v) => a + v, 0)
        );
    }, []);

    return (
        <div className="table-content">
            {error ?? <div>{error}</div>}
            {!!isLoading && <div>Loading...</div>}
            {orders &&
                orders.map((order, i) => (
                    <div
                        className="row"
                        key={i + 1}
                        data-order-id={order.Id}
                        onClick={rowClickHandler}
                    >
                        <div className="order-preview">
                            <div className="column order-number">{i + 1}</div>
                            <div className="column order-date-preview">
                                {order.OrderPlaced.split("T").join(" ")}
                            </div>
                        </div>
                    </div>
                ))}
            <p>
                <strong>Total is {total}</strong>
            </p>
        </div>
    );
};

export default OrderTableRows;
