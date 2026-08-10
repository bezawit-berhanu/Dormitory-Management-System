import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
    createRoom,
    getFloors,
} from "../../services/dormitoryService";
import "../../App.css";

const CreateRoom = () => {
    const navigate = useNavigate();

    const [floors, setFloors] = useState([]);
    const [formData, setFormData] = useState({
        roomNumber: "",
        floorId: "",
        capacity: "",
        status: "",
    });

    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        let cancelled = false;

        const loadFloors = async () => {
            try {
                const data = await getFloors();

                if (!cancelled) {
                    setFloors(data);
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load floors.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadFloors();

        return () => {
            cancelled = true;
        };
    }, []);

    const handleChange = (event) => {
        const { name, value } = event.target;

        setFormData((current) => ({
            ...current,
            [name]: value,
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (!formData.roomNumber.trim()) {
            setError("Room number is required.");
            return;
        }

        if (!formData.floorId) {
            setError("Please select a floor.");
            return;
        }

        if (!formData.capacity.trim()) {
            setError("Capacity is required.");
            return;
        }

        if (!formData.status.trim()) {
            setError("Room status is required.");
            return;
        }

        try {
            setError("");
            setSaving(true);

            await createRoom({
                roomNumber: formData.roomNumber.trim(),
                floorId: Number(formData.floorId),
                capacity: formData.capacity.trim(),
                status: formData.status.trim(),
            });

            navigate("/rooms");
        } catch {
            setError("Unable to create the room.");
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading floors...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Add Room</h1>
                    <p>Create a new room for a dormitory floor.</p>
                </div>

                <Link
                    to="/rooms"
                    className="secondary-button"
                >
                    Back to Rooms
                </Link>
            </div>

            <div className="content-card form-card">
                {error && (
                    <div className="error-state">
                        {error}
                    </div>
                )}

                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="roomNumber">
                            Room Number
                        </label>

                        <input
                            id="roomNumber"
                            name="roomNumber"
                            type="text"
                            value={formData.roomNumber}
                            onChange={handleChange}
                            placeholder="Enter room number"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="floorId">
                            Floor
                        </label>

                        <select
                            id="floorId"
                            name="floorId"
                            value={formData.floorId}
                            onChange={handleChange}
                            required
                        >
                            <option value="">
                                Select a floor
                            </option>

                            {floors
                                .filter(
                                    (floor) =>
                                        floor.isActive !== false
                                )
                                .map((floor) => (
                                    <option
                                        key={floor.floorId}
                                        value={floor.floorId}
                                    >
                                        Floor {floor.floorNumber}
                                    </option>
                                ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label htmlFor="capacity">
                            Capacity
                        </label>

                        <input
                            id="capacity"
                            name="capacity"
                            type="text"
                            value={formData.capacity}
                            onChange={handleChange}
                            placeholder="Enter room capacity"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="status">
                            Room Status
                        </label>

                        <select
                            id="status"
                            name="status"
                            value={formData.status}
                            onChange={handleChange}
                            required
                        >
                            <option value="">
                                Select status
                            </option>
                            <option value="Available">
                                Available
                            </option>
                            <option value="Occupied">
                                Occupied
                            </option>
                            <option value="Maintenance">
                                Maintenance
                            </option>
                        </select>
                    </div>

                    <div className="form-actions">
                        <Link
                            to="/rooms"
                            className="secondary-button"
                        >
                            Cancel
                        </Link>

                        <button
                            type="submit"
                            className="primary-button"
                            disabled={saving}
                        >
                            {saving
                                ? "Saving..."
                                : "Create Room"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default CreateRoom;