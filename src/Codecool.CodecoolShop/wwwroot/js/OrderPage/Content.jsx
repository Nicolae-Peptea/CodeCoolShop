import OrdersList from "/js/OrderPage/OrdersList.jsx";

const Content = () => (
    <div className="page-content">
        <OrdersList url="/order/getbyuserinsession" />
    </div>
);

export default Content;
