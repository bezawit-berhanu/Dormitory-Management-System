import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
    getBeds,
    deactivateBed,
} from "../../services/dormitoryService";
import "../../App.css";

const Beds = () => {
    const [beds, setBeds] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [searchTerm, setSearchTerm] = useState("");
    const [deactivatingId, setDeactivatingId] = useState(null);

    useEffect(() => {
        let cancelled = false;

        const loadBeds = async () => {
            try {
                const data = await getBeds();

                if (!cancelled) {
                    setBeds(data);
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load bed records.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadBeds();

        return () => {
            cancelled = true;
        };
    }, []);

    const handleDeactivate = async (id, bedNumber) => {
        const confirmed = window.confirm(
            `Are you sure you want to deactivate Bed "${bedNumber}"?`
        );

        if (!confirmed) {
            return;
        }

        try {
            setError("");
            setDeactivatingId(id);

            await deactivateBed(id);

            setBeds((currentBeds) =>
                currentBeds.map((bed) =>
                    bed.bedId === id
                        ? { ...bed, isActive: false }
                        : bed
                )
            );
        } catch {
            setError("Unable to deactivate the bed.");
        } finally {
            setDeactivatingId(null);
        }
    };

    const filteredBeds = beds.filter((bed) =>
        `${bed.bedNumber} ${bed.roomId} ${bed.status}`
            .toLowerCase()
            .includes(searchTerm.toLowerCase())
    );

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading bed records...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Beds</h1>
                    <p>
                        Manage beds within the dormitory rooms.
                    </p>
                </div>

                <div className="action-buttons">
                    <Link
                        to="/rooms"
                        className="secondary-button"
                    >
                        ← Back to Rooms
                    </Link>

                    <Link
                        to="/beds/create"
                        className="primary-button"
                    >
                        + Add Bed
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
                        <h2>Bed Records</h2>

                        <span className="record-count">
                            {beds.length} record
                            {beds.length !== 1 ? "s" : ""}
                        </span>
                    </div>

                    <input
                        type="text"
                        className="search-input"
                        placeholder="Search by bed, room ID, or status..."
                        value={searchTerm}
                        onChange={(event) =>
                            setSearchTerm(event.target.value)
                        }
                    />
                </div>

                {filteredBeds.length === 0 ? (
                    <div className="empty-state">
                        <h3>No beds found</h3>

                        <p>
                            {searchTerm
                                ? "Try a different search term."
                                : "There are no bed records yet."}
                        </p>
                    </div>
                ) : (
                    <div className="table-wrapper">
                        <table className="data-table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Bed Number</th>
                                    <th>Room ID</th>
                                    <th>Status</th>
                                    <th>Active Status</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>

                            <tbody>
                                {filteredBeds.map((bed) => (
                                    <tr key={bed.bedId}>
                                        <td>{bed.bedId}</td>

                                        <td className="name-cell">
                                            {bed.bedNumber}
                                        </td>

                                        <td>{bed.roomId}</td>

                                        <td>{bed.status}</td>

                                        <td>
                                            {bed.isActive !== false ? (
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
                                                {bed.isActive !== false && (
                                                    <>
                                                        <Link
                                                            to={`/beds/edit/${bed.bedId}`}
                                                            className="edit-button"
                                                        >
                                                            Edit
                                                        </Link>

                                                        <button
                                                            type="button"
                                                            className="deactivate-button"
                                                            onClick={() =>
                                                                handleDeactivate(
                                                                    bed.bedId,
                                                                    bed.bedNumber
                                                                )
                                                            }
                                                            disabled={
                                                                deactivatingId ===
                                                                bed.bedId
                                                            }
                                                        >
                                                            {deactivatingId ===
                                                                bed.bedId
                                                                ? "Deactivating..."
                                                                : "Deactivate"}
                                                        </button>
                                                    </>
                                                )}

                                                {bed.isActive === false && (
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

export default Beds;