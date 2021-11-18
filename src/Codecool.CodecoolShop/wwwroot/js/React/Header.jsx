const Header = () => {
    const handleClick = (e) => {
        e.preventDefault();
        window.location.reload(false);
    }

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

export default Header;