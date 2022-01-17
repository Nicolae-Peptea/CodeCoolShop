import OrderTableRows from "/js/React/OrderTableRows.jsx";
import OrderProductList from "/js/React/OrderProductList.jsx";

const OrdersList = ({ url }) => {
    const [isDetails, setIsDetails] = React.useState(false);
    const [products, setProducts] = React.useState("");

    return (
        <>
            {!isDetails && (
                <OrderTableRows
                    isDetails={isDetails}
                    setIsDetails={setIsDetails}
                    url={url}
                    setProducts={setProducts}
                />
            )}
            {!!isDetails && (
                <>
                    <OrderProductList
                        products={products}
                        setIsDetails={setIsDetails}
                    />
                </>
            )}
        </>
    );
};

export default OrdersList;
