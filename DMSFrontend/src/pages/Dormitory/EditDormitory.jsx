import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
    getDormitoryById,
    updateDormitory,
} from "../../services/dormitoryService";
import "../../App.css";

const EditDormitory = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        dormitoryName: "",
        location: "",
    });

    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        const loadDormitory = async () => {
            try {
                const data = await getDormitoryById(id);

                setFormData({
                    dormitoryName: data.dormitoryName || "",
                    location: data.location || "",
                });
            } catch {
                setError("Unable to load the dormitory.");
            } finally {
                setLoading(false);
            }
        };

        loadDormitory();
    }, [id]);

    const handleChange = (event) => {
        const { name, value } = event.target;

        setFormData((previous) => ({
            ...previous,
            [name]: value,
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError("");

        if (!formData.dormitoryName.trim()) {
            setError("Dormitory name is required.");
            return;
        }

        if (!formData.location.trim()) {
            setError("Location is required.");
            return;
        }

        try {
            setSaving(true);

            await updateDormitory(id, {
                dormitoryId: Number(id),
                dormitoryName: formData.dormitoryName.trim(),
                location: formData.location.trim(),
            });

            navigate("/dormitories");
        } catch {
            setError(
                "Unable to update the dormitory. Please check the server connection and try again."
            );
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading dormitory...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="form-page-header">
                <div>
                    <h1>Edit Dormitory</h1>
                    <p>Update the dormitory structure information.</p>
                </div>
            </div>

            <div className="form-card">
                {error && (
                    <div className="form-error">
                        {error}
                    </div>
                )}

                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="dormitoryName">
                            Dormitory Name
                        </label>

                        <input
                            id="dormitoryName"
                            name="dormitoryName"
                            type="text"
                            value={formData.dormitoryName}
                            onChange={handleChange}
                            disabled={saving}
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="location">
                            Location
                        </label>

                        <input
                            id="location"
                            name="location"
                            type="text"
                            value={formData.location}
                            onChange={handleChange}
                            disabled={saving}
                        />
                    </div>

                    <div className="form-actions">
                        <button
                            type="button"
                            className="secondary-button"
                            onClick={() => navigate("/dormitories")}
                            disabled={saving}
                        >
                            Cancel
                        </button>

                        <button
                            type="submit"
                            className="primary-button"
                            disabled={saving}
                        >
                            {saving ? "Updating..." : "Update Dormitory"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EditDormitory;