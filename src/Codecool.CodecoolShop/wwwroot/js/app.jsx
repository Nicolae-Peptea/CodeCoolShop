const App = () => {
    const handleClick = (e) => {
        e.preventDefault();
        console.log("Orders");
    }

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
    }, []);

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
            <OrdersList url="/orders"/>
        </div>
    );
}

const OrdersList = ({ url }) => {
    const [orders, setOrders] = React.useState(null);

    return (
        <div className="orders-list">
            <div className="table-header">
                <div className="column">Column1</div>
                <div className="column">Column2</div>
                <div className="column">Column3</div>
            </div>
            <OrderTableRows/>
        </div>
    );
}

const OrderTableRows = () => {
    return (
        <div className="table-content">
            {[...Array(10)].map((x, i) =>
                <div className="row" key={i}>
                    <div className="order-preview">
                        <div className="column">Column1</div>
                        <div className="column">Column2</div>
                        <div className="column">Column3</div>
                    </div>

                    <div className="order-details">
                        <div className="column">Column1</div>
                        <div className="column">Column2</div>
                        <div className="column">Column3</div>
                    </div>
                </div>
            )}
        </div>
    );
}

ReactDOM.render(<App />, document.getElementById('content'));