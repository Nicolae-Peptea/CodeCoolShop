import { render } from "react-dom";
import Header from "/js/React/Header.jsx";
import Content from "/js/React/Content.jsx";

const App = () => (
    <div className="App">
        <Header />
        <Content />
    </div>
);

render(<App />, document.getElementById("content"));
