import OrderTableRows from "/js/React/OrderTableRows.jsx";
import OrderProductList from "/js/React/OrderProductList.jsx";

const OrdersList = ({ url }) => {
    const [isDetails, setIsDetails] = React.useState(false);
    const [products, setProducts] = React.useState("");

    return (
        <>
            {!isDetails && (
                //<div className="orders-list">
                //    <div className="table-header">
                //        <div className="column order-number-header">Number</div>
                //        <div className="column placed-date-header">Placed</div>
                //    </div>
                //    <OrderTableRows
                //        isDetails={isDetails}
                //        setIsDetails={setIsDetails}
                //        url={url}
                //        setProducts={setProducts}
                //    />
                //</div>
                //<table>
                //    <thead>
                //        <tr>
                //            <th>Number</th>
                //        </tr>
                //    </thead>
                //    <tbody>
                //        <tr>
                //            <td>Content</td>
                //        </tr>
                //    </tbody>
                //</table>
                <ul className="order-list" style={{listStyle: 'none'}}>
                    <li className="order-hist-box" key={1}>
                        <div className="order-head">
                            <div className="order-head-info go-left">
                                <h1>
                                    <a className="" href="/history/shoppingdetails/122777789">
                                        Order no.  <span className="order-number">1</span>
                                    </a>
                                </h1>
                                <p>Placed on:
                                    <span className="order-date">17 iulie 2020, 15:06</span>
                                </p>
                            </div>
                            <a
                                href="/history/shoppingdetails/122777789"
                                className="full-white-button go-right gtm_6x2itw"
                            >
                                detalii comanda
                            </a>
                        </div>
                    </li>
                </ul>
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
