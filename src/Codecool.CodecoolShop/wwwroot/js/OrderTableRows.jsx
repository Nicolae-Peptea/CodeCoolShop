const OrderTableRows = ({ orders }) => {
    return (
        <div className="table-content">
            {orders.map((order, i) =>
                <div className="row" key={i + 1}>
                    <div className="order-preview">
                        <div className="column">{i + 1}</div>
                        <div className="column">{order.orderPlaced}</div>
                        <div className="column">{i + 1}</div>
                    </div>

                    <div className="order-details">
                        <div className="column">1</div>
                        <div className="column">Product 1</div>
                        <div className="column">$49.99</div>
                    </div>
                </div>
            )}
        </div>
    );
}

export default OrderTableRows;