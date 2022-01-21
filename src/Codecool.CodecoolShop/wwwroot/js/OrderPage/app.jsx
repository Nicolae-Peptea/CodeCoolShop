import Header from "/js/OrderPage/Header.jsx";
import Content from "/js/OrderPage/Content.jsx";

const App = () => (
    <div className="App">
        <Header />
        <Content />
    </div>
);

ReactDOM.render(<App />, document.getElementById("content"));
