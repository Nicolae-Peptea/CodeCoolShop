const OrderProductList = ({ products }) => {
    return (
        <>
            <div className="order-details">
                {products &&
                    products.map((product, i) =>
                        <div className="order-details-row" key={i + 1}>
                            <div className="column">{i + 1}</div>
                            <div className="column">Product {i + 1}</div>
                            <div className="column">${product.PricePerProduct}</div>
                            <div className="column">x {product.productQuantity}</div>
                        </div>
                )}
            </div>
        </>
    );
}

export default OrderProductList;