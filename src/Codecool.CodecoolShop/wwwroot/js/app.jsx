const App = () => {
    const handleClick = (e) => {
        e.preventDefault();
        console.log("Orders");
    }

    return (
        <div className="App">
            <Header handleClick={handleClick}/>
            <Content />
        </div>
    );
}

const Header = ({ handleClick }) => {
    return (
        <div className="page-header">
            <nav>
                <ul className="admin-menu">
                    <li className="menu-heading">
                        <h3>Admin</h3>
                    </li>
                    <li>
                        <button onClick={handleClick}>My Orders</button>
                    </li>
                </ul>
            </nav>
        </div>
    );
}

const Content = () => {
    return (
        <div className="page-content">
            <OrdersList url="/order/all"/>
        </div>
    );
}

const OrdersList = ({ url }) => {
    const [baseLink, setBaseLink] = React.useState("https://localhost:5001");
    const { data: orders, isLoading, error } = useFetch(baseLink + url);

    React.useEffect(() => {
        const rows = document.querySelectorAll('.row');
        for (const row of rows) {
            row.addEventListener('click', (e) => {
                let orderDetailsContainer =
                    e.currentTarget.querySelector(".order-details");

                orderDetailsContainer.style.display =
                    orderDetailsContainer.style.display === ""
                        ? "flex" : "";
            });
        }
    }, [orders]);

    return (
        <div className="orders-list">
            <div className="table-header">
                <div className="column">Number</div>
                <div className="column">Placed</div>
                <div className="column">Total</div>
            </div>
            {error ?? <div>{error}</div>}
            {isLoading && <div>Loading...</div>}
            {orders && <OrderTableRows orders={orders} />}
        </div>
    );
}

const OrderTableRows = ({ orders }) => {
    return (
        <div className="table-content">
            {orders.map((order, i) =>
                <div className="row" key={i + 1}>
                    <div className="order-preview">
                        <div className="column">{i + 1}</div>
                        <div className="column">{order.orderPlaced}</div>
                        <div className="column">{i + 1}</div>
                    </div>

                    <div className="order-details">
                        <div className="column">1</div>
                        <div className="column">Product 1</div>
                        <div className="column">$49.99</div>
                    </div>
                </div>
            )}
        </div>
    );
}

const useFetch = (url) => {
    const [data, setData] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(true);
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        setTimeout(() => {
            fetch(url)
                .then((response) => {
                    if (!response.ok) {
                        throw Error("Could not fetch the data for that resource...");
                    }
                    return response.json();
                })
                .then((data) => {
                    console.log(data);
                    setIsLoading(false);
                    setError(null);
                    setData(data);
                })
                .catch((error) => {
                    if (error.name === 'AbortError') {
                        console.log('Fetch aborted.');
                    }
                    else {
                        setIsLoading(false);
                        setError(error.message);
                    }
                });
        }, 500);
    }, [url]);

    return { data, isLoading, error }
}

ReactDOM.render(<App />, document.getElementById('content'));