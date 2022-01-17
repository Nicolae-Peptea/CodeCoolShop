import OrdersList from "/js/React/OrdersList.jsx";

const Content = () => (
    <div className="page-content">
        <OrdersList url="/order/getbyuserinsession" />
    </div>
);

export default Content;
