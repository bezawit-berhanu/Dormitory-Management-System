import { Link } from "react-router-dom";

const Dormitory = () => {
    return (
        <div>
            <h1>Dormitory Structure</h1>

            <nav>
                <ul>
                    <li>
                        <Link to="/dormitories">Dormitory List</Link>
                    </li>

                    <li>
                        <Link to="/dormitories/create">Create Dormitory</Link>
                    </li>

                    <li>
                        <Link to="/blocks">Blocks</Link>
                    </li>

                    <li>
                        <Link to="/floors">Floors</Link>
                    </li>
                </ul>
            </nav>
        </div>
    );
};

export default Dormitory;