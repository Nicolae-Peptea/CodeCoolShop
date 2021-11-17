import useFetch from '/js/React/useFetch.jsx';

const OrderProductList = ({ orderIdInUse }) => {
    const [baseLink, setBaseLink] = React.useState("https://localhost:5001");
    const { data: products } = useFetch(baseLink + "/order/getorderproducts?id=" + orderIdInUse);

    return (
        <>
            <div className="order-details">
                {[...Array(10)].map((product, i) =>
                        <div className="order-details-row" key={i + 1}>
                            <div className="column">{i + 1}</div>
                            <div className="column">Product {i + 1}</div>
                            <div className="column">$49.99</div>
                        </div>
                )}
            </div>
        </>
    );
}

export default OrderProductList;