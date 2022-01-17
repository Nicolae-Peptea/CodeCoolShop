const Total = ({ products }) => (
    <h4>
        Total: $
        {!!products &&
            products
                .map((item) => item.PricePerProduct * item.productQuantity)
                .reduce((previousValue, currentValue) => previousValue + currentValue, 0)
        }
    </h4>
)

export default Total;