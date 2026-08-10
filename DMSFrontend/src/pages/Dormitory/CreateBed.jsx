import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
    createBed,
    getRooms,
} from "../../services/dormitoryService";
import "../../App.css";

const CreateBed = () => {
    const navigate = useNavigate();

    const [rooms, setRooms] = useState([]);
    const [formData, setFormData] = useState({
        bedNumber: "",
        roomId: "",
        status: "",
    });

    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

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
                    setError("Unable to load rooms.");
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

    const handleChange = (event) => {
        const { name, value } = event.target;

        setFormData((current) => ({
            ...current,
            [name]: value,
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (!formData.bedNumber.trim()) {
            setError("Bed number is required.");
            return;
        }

        if (!formData.roomId) {
            setError("Please select a room.");
            return;
        }

        if (!formData.status.trim()) {
            setError("Bed status is required.");
            return;
        }

        try {
            setError("");
            setSaving(true);

            await createBed({
                bedNumber: formData.bedNumber.trim(),
                roomId: Number(formData.roomId),
                status: formData.status.trim(),
            });

            navigate("/beds");
        } catch {
            setError("Unable to create the bed.");
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading rooms...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Add Bed</h1>
                    <p>Create a new bed for a room.</p>
                </div>

                <Link
                    to="/beds"
                    className="secondary-button"
                >
                    Back to Beds
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
                        <label htmlFor="bedNumber">
                            Bed Number
                        </label>

                        <input
                            id="bedNumber"
                            name="bedNumber"
                            type="text"
                            value={formData.bedNumber}
                            onChange={handleChange}
                            placeholder="Enter bed number"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="roomId">
                            Room
                        </label>

                        <select
                            id="roomId"
                            name="roomId"
                            value={formData.roomId}
                            onChange={handleChange}
                            required
                        >
                            <option value="">
                                Select a room
                            </option>

                            {rooms
                                .filter(
                                    (room) =>
                                        room.isActive !== false
                                )
                                .map((room) => (
                                    <option
                                        key={room.roomId}
                                        value={room.roomId}
                                    >
                                        Room {room.roomNumber}
                                    </option>
                                ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label htmlFor="status">
                            Bed Status
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
                            to="/beds"
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
                                : "Create Bed"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default CreateBed;