import Header from '/js/React/Header.jsx';
import Content from '/js/React/Content.jsx';


const App = () => {
    return (
        <div className="App">
            <Header />
            <Content />
        </div>
    );
}

ReactDOM.render(<App />, document.getElementById('content'));