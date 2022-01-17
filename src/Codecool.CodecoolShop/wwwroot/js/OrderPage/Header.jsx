const Header = () => {
    const handleClick = (e) => {
        e.preventDefault();
        window.location.reload(false);
    };

    return (
        <div className="sidebar">
            <ul className="admin-menu">
                <li>
                    <a href="#" onClick={handleClick}>
                        Orders
                    </a>
                </li>
            </ul>
        </div>
    );
};

export default Header;
