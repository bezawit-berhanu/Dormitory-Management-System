import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import {
    getBlockById,
    updateBlock,
    getDormitories,
} from "../../services/dormitoryService";
import "../../App.css";

const EditBlock = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [dormitories, setDormitories] = useState([]);
    const [formData, setFormData] = useState({
        blockName: "",
        dormitoryId: "",
        description: "",
    });

    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        let cancelled = false;

        const loadBlock = async () => {
            try {
                const [block, dormitoryData] = await Promise.all([
                    getBlockById(id),
                    getDormitories(),
                ]);

                if (!cancelled) {
                    setDormitories(dormitoryData);

                    setFormData({
                        blockName: block.blockName ?? "",
                        dormitoryId: block.dormitoryId?.toString() ?? "",
                        description: block.description ?? "",
                    });
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load the block.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadBlock();

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

        if (!formData.blockName.trim()) {
            setError("Block name is required.");
            return;
        }

        if (!formData.dormitoryId) {
            setError("Please select a dormitory.");
            return;
        }

        try {
            setError("");
            setSaving(true);

            await updateBlock(id, {
                blockId: Number(id),
                blockName: formData.blockName.trim(),
                dormitoryId: Number(formData.dormitoryId),
                description: formData.description.trim(),
            });

            navigate("/blocks");
        } catch {
            setError("Unable to update the block.");
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading block...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Edit Block</h1>
                    <p>Update the block information.</p>
                </div>

                <Link
                    to="/blocks"
                    className="secondary-button"
                >
                    Back to Blocks
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
                        <label htmlFor="blockName">
                            Block Name
                        </label>

                        <input
                            id="blockName"
                            name="blockName"
                            type="text"
                            value={formData.blockName}
                            onChange={handleChange}
                            placeholder="Enter block name"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="dormitoryId">
                            Dormitory
                        </label>

                        <select
                            id="dormitoryId"
                            name="dormitoryId"
                            value={formData.dormitoryId}
                            onChange={handleChange}
                            required
                        >
                            <option value="">
                                Select a dormitory
                            </option>

                            {dormitories
                                .filter(
                                    (dormitory) =>
                                        dormitory.isActive !== false ||
                                        dormitory.dormitoryId ===
                                        Number(formData.dormitoryId)
                                )
                                .map((dormitory) => (
                                    <option
                                        key={dormitory.dormitoryId}
                                        value={dormitory.dormitoryId}
                                    >
                                        {dormitory.dormitoryName}
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
                            placeholder="Enter block description"
                            rows="4"
                        />
                    </div>

                    <div className="form-actions">
                        <Link
                            to="/blocks"
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
                                : "Update Block"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EditBlock;