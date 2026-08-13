import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getMaintenanceRequestById } from "../../services/maintenanceService";

function MaintenanceDetails() {
    const { id } = useParams();
    const navigate = useNavigate();

    const [request, setRequest] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const loadRequest = async () => {
            try {
                const data = await getMaintenanceRequestById(id);
                setRequest(data);
            } catch (err) {
                console.error(err);
                setError("Failed to load maintenance request.");
            } finally {
                setLoading(false);
            }
        };

        loadRequest();
    }, [id]);

    if (loading) {
        return <p>Loading maintenance request...</p>;
    }

    if (error) {
        return (
            <div>
                <p style={{ color: "red" }}>{error}</p>

                <button onClick={() => navigate("/maintenance")}>
                    Back to Maintenance
                </button>
            </div>
        );
    }

    if (!request) {
        return (
            <div>
                <p>Maintenance request not found.</p>

                <button onClick={() => navigate("/maintenance")}>
                    Back to Maintenance
                </button>
            </div>
        );
    }

    return (
        <div>
            <h2>Maintenance Request Details</h2>

            <p>
                <strong>Request Number:</strong>{" "}
                {request.requestNumber}
            </p>

            <p>
                <strong>Room:</strong>{" "}
                {request.roomId}
            </p>

            <p>
                <strong>Category:</strong>{" "}
                {request.category}
            </p>

            <p>
                <strong>Title:</strong>{" "}
                {request.title}
            </p>

            <p>
                <strong>Description:</strong>{" "}
                {request.description}
            </p>

            <p>
                <strong>Priority:</strong>{" "}
                {request.priority}
            </p>

            <p>
                <strong>Status:</strong>{" "}
                {request.status}
            </p>

            <p>
                <strong>Request Date:</strong>{" "}
                {request.requestDate
                    ? new Date(
                        request.requestDate
                    ).toLocaleDateString()
                    : "N/A"}
            </p>

            <p>
                <strong>Assigned User:</strong>{" "}
                {request.assignedUserId ?? "Not assigned"}
            </p>

            <p>
                <strong>Assignment Status:</strong>{" "}
                {request.assignmentStatus ?? "Not assigned"}
            </p>

            {request.imageUrl && (
                <div>
                    <strong>Image:</strong>

                    <br />

                    <img
                        src={request.imageUrl}
                        alt="Maintenance"
                        style={{
                            maxWidth: "400px",
                            marginTop: "10px",
                        }}
                    />
                </div>
            )}

            <br />

            <button onClick={() => navigate("/maintenance")}>
                Back to Maintenance
            </button>

            <button
                onClick={() =>
                    navigate(
                        `/maintenance/edit/${request.maintenanceRequestId}`
                    )
                }
            >
                Edit
            </button>
        </div>
    );
}

export default MaintenanceDetails;