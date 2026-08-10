import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import {
    getFloorById,
    updateFloor,
    getBlocks,
} from "../../services/dormitoryService";
import "../../App.css";

const EditFloor = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [blocks, setBlocks] = useState([]);
    const [formData, setFormData] = useState({
        floorNumber: "",
        blockId: "",
        description: "",
    });

    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        let cancelled = false;

        const loadFloor = async () => {
            try {
                const [floor, blockData] = await Promise.all([
                    getFloorById(id),
                    getBlocks(),
                ]);

                if (!cancelled) {
                    setBlocks(blockData);

                    setFormData({
                        floorNumber: floor.floorNumber ?? "",
                        blockId: floor.blockId?.toString() ?? "",
                        description: floor.description ?? "",
                    });
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load the floor.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadFloor();

        return () => {
            cancelled = true;
        };
    }, [id]);

    const handleChange = (event) => {
        const { name, value } = event.target;

        setFormData((current) => ({
            ...current,
            [name]: value,
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (!formData.floorNumber.trim()) {
            setError("Floor number is required.");
            return;
        }

        if (!formData.blockId) {
            setError("Please select a block.");
            return;
        }

        try {
            setError("");
            setSaving(true);

            await updateFloor(id, {
                floorId: Number(id),
                floorNumber: formData.floorNumber.trim(),
                blockId: Number(formData.blockId),
                description: formData.description.trim(),
            });

            navigate("/floors");
        } catch {
            setError("Unable to update the floor.");
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading floor...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Edit Floor</h1>
                    <p>Update the floor information.</p>
                </div>

                <Link
                    to="/floors"
                    className="secondary-button"
                >
                    Back to Floors
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
                        <label htmlFor="floorNumber">
                            Floor Number
                        </label>

                        <input
                            id="floorNumber"
                            name="floorNumber"
                            type="text"
                            value={formData.floorNumber}
                            onChange={handleChange}
                            placeholder="Enter floor number"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="blockId">
                            Block
                        </label>

                        <select
                            id="blockId"
                            name="blockId"
                            value={formData.blockId}
                            onChange={handleChange}
                            required
                        >
                            <option value="">
                                Select a block
                            </option>

                            {blocks
                                .filter(
                                    (block) =>
                                        block.isActive !== false ||
                                        block.blockId ===
                                        Number(formData.blockId)
                                )
                                .map((block) => (
                                    <option
                                        key={block.blockId}
                                        value={block.blockId}
                                    >
                                        {block.blockName}
                                    </option>
                                ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label htmlFor="description">
                            Description
                        </label>

                        <textarea
                            id="description"
                            name="description"
                            value={formData.description}
                            onChange={handleChange}
                            placeholder="Enter floor description"
                            rows="4"
                        />
                    </div>

                    <div className="form-actions">
                        <Link
                            to="/floors"
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
                                : "Update Floor"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EditFloor;