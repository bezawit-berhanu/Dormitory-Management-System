import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createMaintenanceRequest } from "../../services/maintenanceService";

function CreateMaintenance() {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        roomId: "",
        category: "",
        title: "",
        description: "",
        priority: "Medium",
        imageUrl: "",
    });

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const handleChange = (event) => {
        const { name, value } = event.target;

        setFormData((previousData) => ({
            ...previousData,
            [name]: value,
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        setError("");

        if (!formData.roomId) {
            setError("Please enter the room ID.");
            return;
        }

        if (!formData.category) {
            setError("Please select a category.");
            return;
        }

        if (!formData.title.trim()) {
            setError("Please enter a title.");
            return;
        }

        if (!formData.description.trim()) {
            setError("Please enter a description.");
            return;
        }

        try {
            setLoading(true);

            const maintenanceData = {
                roomId: Number(formData.roomId),
                requestedBy: 0,
                category: formData.category,
                title: formData.title,
                description: formData.description,
                priority: formData.priority,
                requestDate: new Date().toISOString(),
                status: "Pending",
                imageUrl: formData.imageUrl || null,

                assignedUserId: null,
                assignedDate: null,
                assignmentStatus: null,
            };

            await createMaintenanceRequest(maintenanceData);

            navigate("/maintenance");
        } catch (err) {
            console.error(err);
            setError("Failed to create maintenance request.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h2>Create Maintenance Request</h2>

            {error && (
                <p style={{ color: "red" }}>
                    {error}
                </p>
            )}

            <form onSubmit={handleSubmit}>

                <div>
                    <label htmlFor="roomId">
                        Room ID
                    </label>

                    <input
                        id="roomId"
                        name="roomId"
                        type="number"
                        value={formData.roomId}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label htmlFor="category">
                        Category
                    </label>

                    <select
                        id="category"
                        name="category"
                        value={formData.category}
                        onChange={handleChange}
                        required
                    >
                        <option value="">
                            Select category
                        </option>

                        <option value="Plumbing">
                            Plumbing
                        </option>

                        <option value="Electrical">
                            Electrical
                        </option>

                        <option value="Furniture">
                            Furniture
                        </option>

                        <option value="Cleaning">
                            Cleaning
                        </option>

                        <option value="Other">
                            Other
                        </option>
                    </select>
                </div>

                <div>
                    <label htmlFor="title">
                        Title
                    </label>

                    <input
                        id="title"
                        name="title"
                        type="text"
                        value={formData.title}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label htmlFor="description">
                        Description
                    </label>

                    <textarea
                        id="description"
                        name="description"
                        value={formData.description}
                        onChange={handleChange}
                        rows="5"
                        required
                    />
                </div>

                <div>
                    <label htmlFor="priority">
                        Priority
                    </label>

                    <select
                        id="priority"
                        name="priority"
                        value={formData.priority}
                        onChange={handleChange}
                    >
                        <option value="Low">
                            Low
                        </option>

                        <option value="Medium">
                            Medium
                        </option>

                        <option value="High">
                            High
                        </option>

                        <option value="Urgent">
                            Urgent
                        </option>
                    </select>
                </div>

                <div>
                    <label htmlFor="imageUrl">
                        Image URL
                    </label>

                    <input
                        id="imageUrl"
                        name="imageUrl"
                        type="text"
                        value={formData.imageUrl}
                        onChange={handleChange}
                        placeholder="Optional"
                    />
                </div>

                <div>
                    <button
                        type="submit"
                        disabled={loading}
                    >
                        {loading
                            ? "Submitting..."
                            : "Submit Request"}
                    </button>

                    <button
                        type="button"
                        onClick={() =>
                            navigate("/maintenance")
                        }
                    >
                        Cancel
                    </button>
                </div>

            </form>
        </div>
    );
}

export default CreateMaintenance;