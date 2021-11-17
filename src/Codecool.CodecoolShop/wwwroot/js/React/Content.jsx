import OrdersList from '/js/React/OrdersList.jsx';

const Content = () => {
    return (
        <div className="page-content">
            <OrdersList url="/order/all" />
        </div>
    );
}

export default Content;