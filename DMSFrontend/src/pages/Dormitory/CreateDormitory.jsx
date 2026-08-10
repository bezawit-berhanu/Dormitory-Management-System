import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createDormitory } from "../../services/dormitoryService";
import "../../App.css";

const CreateDormitory = () => {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        dormitoryName: "",
        location: "",
    });

    const [error, setError] = useState("");
    const [saving, setSaving] = useState(false);

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

            await createDormitory({
                dormitoryName: formData.dormitoryName.trim(),
                location: formData.location.trim(),
            });

            navigate("/dormitories");
        } catch {
            setError(
                "Unable to create the dormitory. Please check the server connection and try again."
            );
        } finally {
            setSaving(false);
        }
    };

    return (
        <div className="page-container">
            <div className="form-page-header">
                <div>
                    <h1>Add Dormitory</h1>
                    <p>Create a new dormitory structure.</p>
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
                            placeholder="Enter dormitory name"
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
                            placeholder="Enter dormitory location"
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
                            {saving ? "Saving..." : "Create Dormitory"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default CreateDormitory;