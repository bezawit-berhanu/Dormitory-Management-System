import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
    getRooms,
    deactivateRoom,
} from "../../services/dormitoryService";
import "../../App.css";

const Rooms = () => {
    const [rooms, setRooms] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [searchTerm, setSearchTerm] = useState("");
    const [deactivatingId, setDeactivatingId] = useState(null);

    useEffect(() => {
        let cancelled = false;

        const loadRooms = async () => {
            try {
                const data = await getRooms();

                if (!cancelled) {
                    setRooms(data);
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load room records.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadRooms();

        return () => {
            cancelled = true;
        };
    }, []);

    const handleDeactivate = async (id, roomNumber) => {
        const confirmed = window.confirm(
            `Are you sure you want to deactivate Room "${roomNumber}"?`
        );

        if (!confirmed) {
            return;
        }

        try {
            setError("");
            setDeactivatingId(id);

            await deactivateRoom(id);

            setRooms((currentRooms) =>
                currentRooms.map((room) =>
                    room.roomId === id
                        ? { ...room, isActive: false }
                        : room
                )
            );
        } catch {
            setError("Unable to deactivate the room.");
        } finally {
            setDeactivatingId(null);
        }
    };

    const filteredRooms = rooms.filter((room) =>
        `${room.roomNumber} ${room.floorId} ${room.capacity} ${room.status}`
            .toLowerCase()
            .includes(searchTerm.toLowerCase())
    );

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading room records...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Rooms</h1>
                    <p>
                        Manage rooms within the dormitory floors.
                    </p>
                </div>

                <div className="action-buttons">
                    <Link
                        to="/floors"
                        className="secondary-button"
                    >
                        ← Back to Floors
                    </Link>

                    <Link
                        to="/rooms/create"
                        className="primary-button"
                    >
                        + Add Room
                    </Link>

                    <Link
                        to="/beds"
                        className="secondary-button"
                    >
                        View Beds →
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
                        <h2>Room Records</h2>

                        <span className="record-count">
                            {rooms.length} record
                            {rooms.length !== 1 ? "s" : ""}
                        </span>
                    </div>

                    <input
                        type="text"
                        className="search-input"
                        placeholder="Search by room, floor, capacity, or status..."
                        value={searchTerm}
                        onChange={(event) =>
                            setSearchTerm(event.target.value)
                        }
                    />
                </div>

                {filteredRooms.length === 0 ? (
                    <div className="empty-state">
                        <h3>No rooms found</h3>

                        <p>
                            {searchTerm
                                ? "Try a different search term."
                                : "There are no room records yet."}
                        </p>
                    </div>
                ) : (
                    <div className="table-wrapper">
                        <table className="data-table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Room Number</th>
                                    <th>Floor ID</th>
                                    <th>Capacity</th>
                                    <th>Status</th>
                                    <th>Active Status</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>

                            <tbody>
                                {filteredRooms.map((room) => (
                                    <tr key={room.roomId}>
                                        <td>{room.roomId}</td>

                                        <td className="name-cell">
                                            {room.roomNumber}
                                        </td>

                                        <td>{room.floorId}</td>

                                        <td>{room.capacity}</td>

                                        <td>{room.status}</td>

                                        <td>
                                            {room.isActive !== false ? (
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
                                                {room.isActive !== false && (
                                                    <>
                                                        <Link
                                                            to={`/rooms/edit/${room.roomId}`}
                                                            className="edit-button"
                                                        >
                                                            Edit
                                                        </Link>

                                                        <button
                                                            type="button"
                                                            className="deactivate-button"
                                                            onClick={() =>
                                                                handleDeactivate(
                                                                    room.roomId,
                                                                    room.roomNumber
                                                                )
                                                            }
                                                            disabled={
                                                                deactivatingId ===
                                                                room.roomId
                                                            }
                                                        >
                                                            {deactivatingId ===
                                                                room.roomId
                                                                ? "Deactivating..."
                                                                : "Deactivate"}
                                                        </button>
                                                    </>
                                                )}

                                                {room.isActive === false && (
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

export default Rooms;