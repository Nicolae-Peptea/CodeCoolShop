﻿import { useState } from "react";
import OrderTableRows from "/js/React/OrderTableRows.jsx";
import OrderProductList from "/js/React/OrderProductList.jsx";

const OrdersList = ({ url }) => {
    const [isDetails, setIsDetails] = useState(false);
    const [products, setProducts] = useState("");
    const [total, setTotal] = useState(0);

    useEffect(() => {
        setTotal(
            products
                .map((o) => o.PricePerProduct * o.productQuantity)
                .reduce((a, v) => a + v, 0)
        );
    }, [products]);

    return (
        <>
            {!isDetails && (
                <div className="orders-list">
                    <div className="table-header">
                        <div className="column order-number-header">Number</div>
                        <div className="column placed-date-header">Placed</div>
                    </div>
                    <OrderTableRows
                        isDetails={isDetails}
                        setIsDetails={setIsDetails}
                        url={url}
                        setProducts={setProducts}
                    />
                </div>
            )}
            {!!isDetails && (
                <OrderProductList
                    products={products}
                    setIsDetails={setIsDetails}
                />
            )}
            <p>
                <strong>Total is {total}</strong>
            </p>
        </>
    );
};

export default OrdersList;
