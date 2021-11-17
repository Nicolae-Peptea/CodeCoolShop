import OrdersList from '/js/React/OrdersList.jsx';

const Content = () => {
    return (
        <div className="page-content">
            <OrdersList url="/order/getbyuserid" />
        </div>
    );
}

export default Content;