import Header from '/js/Header.jsx';
import Content from '/js/Content.jsx';


const App = () => {
    return (
        <div className="App">
            <Header />
            <Content />
        </div>
    );
}

ReactDOM.render(<App />, document.getElementById('content'));