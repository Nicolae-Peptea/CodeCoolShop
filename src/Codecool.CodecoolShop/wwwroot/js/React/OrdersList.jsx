import OrderTableRows from '/js/React/OrderTableRows.jsx';

const OrdersList = ({ url }) => {
    return (
        <div className="orders-list">
            <div className="table-header">
                <div className="column">Number</div>
                <div className="column">Placed</div>
            </div>
            <OrderTableRows url={url} />
        </div>
    );
}

export default OrdersList;