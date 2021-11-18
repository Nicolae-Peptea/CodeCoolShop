﻿import OrderTableRows from '/js/React/OrderTableRows.jsx';
import OrderProductList from '/js/React/OrderProductList.jsx';

const OrdersList = ({ url }) => {
    const [isDetails, setIsDetails] = React.useState(false);
    const [products, setProducts] = React.useState("");

    if (!isDetails) {
        return (
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
        );
    }
    else {
        return (
            <OrderProductList
                products={products}
                setIsDetails={setIsDetails}
            />
        );
    }
}

export default OrdersList;