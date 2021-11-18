const OrderProductList = ({ products, setIsDetails }) => {
    React.useEffect(() => {
        let back = document.querySelector(".back");

        back.addEventListener("click", () => {
            setIsDetails(false);
        })
    }, []);

    return (
        <>
            <button className="back">&#8249; Back to orders</button>

            <div className="order-details">
                {products &&
                    products.map((product, i) =>
                        <div className="order-details-row" key={i + 1}>
                            <div className="column product-image-container">
                                <img src={`/img/${product.Product.Name}.jpg`} className="product-image" alt={`Image for ${product.Product.Name}.jpg`} />
                            </div>
                            <div className="column product-details">{product.Product.Name}</div>
                            <div className="column product-price-details">
                                <div className="product-price">${product.PricePerProduct}</div>
                                <div className="quantity">x {product.productQuantity}</div>
                            </div>
                        </div>
                    )}
            </div>
        </>
    );
}

export default OrderProductList;