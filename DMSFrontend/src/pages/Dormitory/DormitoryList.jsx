import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
    getDormitories,
    deactivateDormitory,
} from "../../services/dormitoryService";
import "../../App.css";

const DormitoryList = () => {
    const [dormitories, setDormitories] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [searchTerm, setSearchTerm] = useState("");
    const [deactivatingId, setDeactivatingId] = useState(null);

    const loadDormitories = async () => {
        try {
            setError("");

            const data = await getDormitories();
            setDormitories(data);
        } catch {
            setError("Unable to load dormitory records.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        let cancelled = false;

        const loadInitialDormitories = async () => {
            try {
                const data = await getDormitories();

                if (!cancelled) {
                    setDormitories(data);
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load dormitory records.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadInitialDormitories();

        return () => {
            cancelled = true;
        };
    }, []);

    const handleDeactivate = async (id, name) => {
        const confirmed = window.confirm(
            `Are you sure you want to deactivate "${name}"?`
        );

        if (!confirmed) {
            return;
        }

        try {
            setError("");
            setDeactivatingId(id);

            await deactivateDormitory(id);

            await loadDormitories();
        } catch {
            setError("Unable to deactivate the dormitory.");
        } finally {
            setDeactivatingId(null);
        }
    };

    const filteredDormitories = dormitories.filter((dormitory) =>
        `${dormitory.dormitoryName} ${dormitory.location}`
            .toLowerCase()
            .includes(searchTerm.toLowerCase())
    );

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading dormitory records...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Dormitory Structures</h1>
                    <p>
                        Manage dormitories and their structure.
                    </p>
                </div>

                <div className="action-buttons">
                    <Link
                        to="/dormitories/create"
                        className="primary-button"
                    >
                        + Add Dormitory
                    </Link>

                    <Link
                        to="/blocks"
                        className="secondary-button"
                    >
                        View Blocks
                    </Link>
                </div>
            </div>

            <div className="content-card">
                {error && (
                    <div className="error-state">
                        {error}
                    </div>
                )}

                <div className="table-toolbar">
                    <div>
                        <h2>Dormitories</h2>

                        <span className="record-count">
                            {dormitories.length} record
                            {dormitories.length !== 1 ? "s" : ""}
                        </span>
                    </div>

                    <input
                        type="text"
                        className="search-input"
                        placeholder="Search by name or location..."
                        value={searchTerm}
                        onChange={(event) =>
                            setSearchTerm(event.target.value)
                        }
                    />
                </div>

                {filteredDormitories.length === 0 ? (
                    <div className="empty-state">
                        <h3>No dormitories found</h3>

                        <p>
                            {searchTerm
                                ? "Try a different search term."
                                : "There are no dormitory records yet."}
                        </p>
                    </div>
                ) : (
                    <div className="table-wrapper">
                        <table className="data-table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Dormitory Name</th>
                                    <th>Location</th>
                                    <th>Status</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>

                            <tbody>
                                {filteredDormitories.map((dormitory) => (
                                    <tr key={dormitory.dormitoryId}>
                                        <td>
                                            {dormitory.dormitoryId}
                                        </td>

                                        <td className="name-cell">
                                            {dormitory.dormitoryName}
                                        </td>

                                        <td>
                                            {dormitory.location}
                                        </td>

                                        <td>
                                            {dormitory.isActive !== false ? (
                                                <span className="status-active">
                                                    Active
                                                </span>
                                            ) : (
                                                <span className="status-inactive">
                                                    Inactive
                                                </span>
                                            )}
                                        </td>

                                        <td>
                                            <div className="action-buttons">
                                                {dormitory.isActive !== false && (
                                                    <>
                                                        <Link
                                                            to={`/dormitories/edit/${dormitory.dormitoryId}`}
                                                            className="edit-button"
                                                        >
                                                            Edit
                                                        </Link>

                                                        <button
                                                            type="button"
                                                            className="deactivate-button"
                                                            onClick={() =>
                                                                handleDeactivate(
                                                                    dormitory.dormitoryId,
                                                                    dormitory.dormitoryName
                                                                )
                                                            }
                                                            disabled={
                                                                deactivatingId ===
                                                                dormitory.dormitoryId
                                                            }
                                                        >
                                                            {deactivatingId ===
                                                                dormitory.dormitoryId
                                                                ? "Deactivating..."
                                                                : "Deactivate"}
                                                        </button>
                                                    </>
                                                )}

                                                {dormitory.isActive === false && (
                                                    <span className="inactive-label">
                                                        Deactivated
                                                    </span>
                                                )}
                                            </div>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
};

export default DormitoryList;