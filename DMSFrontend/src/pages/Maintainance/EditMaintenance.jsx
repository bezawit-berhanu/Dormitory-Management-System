import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
    getMaintenanceRequestById,
    updateMaintenanceRequest,
} from "../../services/maintenanceService";

function EditMaintenance() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        maintenanceRequestId: 0,
        requestNumber: "",
        roomId: "",
        requestedBy: "",
        category: "",
        title: "",
        description: "",
        priority: "",
        requestDate: "",
        status: "",
        imageUrl: "",
        assignedUserId: "",
        assignedDate: "",
        assignmentStatus: "",
    });

    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        const loadRequest = async () => {
            try {
                setLoading(true);
                setError("");

                const data = await getMaintenanceRequestById(id);

                setFormData({
                    maintenanceRequestId:
                        data.maintenanceRequestId ?? 0,
                    requestNumber:
                        data.requestNumber ?? "",
                    roomId:
                        data.roomId ?? "",
                    requestedBy:
                        data.requestedBy ?? "",
                    category:
                        data.category ?? "",
                    title:
                        data.title ?? "",
                    description:
                        data.description ?? "",
                    priority:
                        data.priority ?? "",
                    requestDate:
                        data.requestDate
                            ? data.requestDate.substring(0, 10)
                            : "",
                    status:
                        data.status ?? "",
                    imageUrl:
                        data.imageUrl ?? "",
                    assignedUserId:
                        data.assignedUserId ?? "",
                    assignedDate:
                        data.assignedDate
                            ? data.assignedDate.substring(0, 10)
                            : "",
                    assignmentStatus:
                        data.assignmentStatus ?? "",
                });
            } catch (err) {
                console.error(err);
                setError("Failed to load maintenance request.");
            } finally {
                setLoading(false);
            }
        };

        loadRequest();
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

        try {
            setSaving(true);
            setError("");

            const dataToSend = {
                ...formData,
                maintenanceRequestId: Number(
                    formData.maintenanceRequestId
                ),
                roomId: Number(formData.roomId),
                requestedBy: Number(formData.requestedBy),

                assignedUserId:
                    formData.assignedUserId === ""
                        ? null
                        : Number(formData.assignedUserId),

                assignedDate:
                    formData.assignedDate === ""
                        ? null
                        : formData.assignedDate,
            };

            await updateMaintenanceRequest(id, dataToSend);

            navigate("/maintenance");
        } catch (err) {
            console.error(err);
            setError("Failed to update maintenance request.");
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return <p>Loading maintenance request...</p>;
    }

    return (
        <div>
            <h2>Edit Maintenance Request</h2>

            {error && (
                <p style={{ color: "red" }}>
                    {error}
                </p>
            )}

            <form onSubmit={handleSubmit}>
                <div>
                    <label>Request Number</label>
                    <input
                        type="text"
                        name="requestNumber"
                        value={formData.requestNumber}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>Room ID</label>
                    <input
                        type="number"
                        name="roomId"
                        value={formData.roomId}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>Requested By</label>
                    <input
                        type="number"
                        name="requestedBy"
                        value={formData.requestedBy}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>Category</label>
                    <input
                        type="text"
                        name="category"
                        value={formData.category}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>Title</label>
                    <input
                        type="text"
                        name="title"
                        value={formData.title}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>Description</label>
                    <textarea
                        name="description"
                        value={formData.description}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>Priority</label>
                    <select
                        name="priority"
                        value={formData.priority}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Select priority</option>
                        <option value="Low">Low</option>
                        <option value="Medium">Medium</option>
                        <option value="High">High</option>
                        <option value="Urgent">Urgent</option>
                    </select>
                </div>

                <div>
                    <label>Request Date</label>
                    <input
                        type="date"
                        name="requestDate"
                        value={formData.requestDate}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div>
                    <label>Status</label>
                    <select
                        name="status"
                        value={formData.status}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Select status</option>
                        <option value="Pending">Pending</option>
                        <option value="Assigned">Assigned</option>
                        <option value="InProgress">
                            In Progress
                        </option>
                        <option value="Completed">Completed</option>
                        <option value="Rejected">Rejected</option>
                    </select>
                </div>

                <div>
                    <label>Image URL</label>
                    <input
                        type="text"
                        name="imageUrl"
                        value={formData.imageUrl}
                        onChange={handleChange}
                    />
                </div>

                <div>
                    <label>Assigned User ID</label>
                    <input
                        type="number"
                        name="assignedUserId"
                        value={formData.assignedUserId}
                        onChange={handleChange}
                    />
                </div>

                <div>
                    <label>Assigned Date</label>
                    <input
                        type="date"
                        name="assignedDate"
                        value={formData.assignedDate}
                        onChange={handleChange}
                    />
                </div>

                <div>
                    <label>Assignment Status</label>
                    <select
                        name="assignmentStatus"
                        value={formData.assignmentStatus}
                        onChange={handleChange}
                    >
                        <option value="">Select assignment status</option>
                        <option value="Pending">Pending</option>
                        <option value="Assigned">Assigned</option>
                        <option value="InProgress">
                            In Progress
                        </option>
                        <option value="Completed">Completed</option>
                    </select>
                </div>

                <br />

                <button type="submit" disabled={saving}>
                    {saving ? "Saving..." : "Save Changes"}
                </button>

                <button
                    type="button"
                    onClick={() => navigate("/maintenance")}
                    disabled={saving}
                    style={{ marginLeft: "10px" }}
                >
                    Cancel
                </button>
            </form>
        </div>
    );
}

export default EditMaintenance;