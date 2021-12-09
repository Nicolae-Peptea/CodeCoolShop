import Total from "/js/React/Total.jsx";

const OrderProductList = ({ products, setIsDetails }) => {
    const backHandler = (e) => {
        e.preventDefault();
        setIsDetails(false);
    };

    return (
        <>
            <div><h1>Details</h1></div>
            <div className="solid" />

            <button className="back" onClick={backHandler}>
                <svg stroke="currentColor" fill="currentColor" strokeWidth="0" viewBox="0 0 512 512" height="1em" width="1em" xmlns="http://www.w3.org/2000/svg"><path d="M217.9 256L345 129c9.4-9.4 9.4-24.6 0-33.9-9.4-9.4-24.6-9.3-34 0L167 239c-9.1 9.1-9.3 23.7-.7 33.1L310.9 417c4.7 4.7 10.9 7 17 7s12.3-2.3 17-7c9.4-9.4 9.4-24.6 0-33.9L217.9 256z"></path></svg>
                Back
            </button>

            {!!products && (
                <div className="order-details">
                    {products.map((product, i) => (
                        <div className="order-details-row" key={i + 1}>
                            <div className="column product-image-container">
                                <img
                                    src={`/img/${product.Product.Name}.jpg`}
                                    className="product-image"
                                    alt={`Image for ${product.Product.Name}`}
                                />
                            </div>
                            <div className="column product-details">
                                {product.Product.Name}
                            </div>
                            <div className="column product-price-details">
                                <div className="product-price">
                                    ${product.PricePerProduct}
                                </div>
                                <div className="quantity">
                                    x {product.productQuantity}
                                </div>
                            </div>
                        </div>
                    ))}
                    <div className="thin" />
                    <Total
                        products={products}
                    />
                </div>
            )}
        </>
    );
};

export default OrderProductList;
