import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
    createBlock,
    getDormitories,
} from "../../services/dormitoryService";
import "../../App.css";

const CreateBlock = () => {
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

        const loadDormitories = async () => {
            try {
                const data = await getDormitories();

                if (!cancelled) {
                    setDormitories(data);
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load dormitories.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadDormitories();

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

            await createBlock({
                blockName: formData.blockName.trim(),
                dormitoryId: Number(formData.dormitoryId),
                description: formData.description.trim(),
            });

            navigate("/blocks");
        } catch {
            setError("Unable to create the block.");
        } finally {
            setSaving(false);
        }
    };

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Create Block</h1>
                    <p>Add a new block to a dormitory.</p>
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
                            disabled={loading}
                            required
                        >
                            <option value="">
                                {loading
                                    ? "Loading dormitories..."
                                    : "Select a dormitory"}
                            </option>

                            {dormitories
                                .filter(
                                    (dormitory) =>
                                        dormitory.isActive !== false
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
                                : "Create Block"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default CreateBlock;