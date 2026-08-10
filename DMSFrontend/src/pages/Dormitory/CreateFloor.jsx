import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
    createFloor,
    getBlocks,
} from "../../services/dormitoryService";
import "../../App.css";

const CreateFloor = () => {
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

        const loadBlocks = async () => {
            try {
                const data = await getBlocks();

                if (!cancelled) {
                    setBlocks(data);
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load blocks.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadBlocks();

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

            await createFloor({
                floorNumber: formData.floorNumber.trim(),
                blockId: Number(formData.blockId),
                description: formData.description.trim(),
            });

            navigate("/floors");
        } catch {
            setError("Unable to create the floor.");
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading blocks...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Add Floor</h1>
                    <p>Create a new floor for a dormitory block.</p>
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
                                        block.isActive !== false
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
                                : "Create Floor"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default CreateFloor;