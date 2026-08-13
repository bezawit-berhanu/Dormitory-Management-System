import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
    getMaintenanceRequests,
    deleteMaintenanceRequest,
} from "../../services/maintenanceService";

function MaintenanceList() {
    const navigate = useNavigate();

    const [requests, setRequests] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        let cancelled = false;

        const fetchRequests = async () => {
            try {
                const data = await getMaintenanceRequests();

                if (!cancelled) {
                    setRequests(data || []);
                    setLoading(false);
                }
            } catch (err) {
                console.error(err);

                if (!cancelled) {
                    setError("Failed to load maintenance requests.");
                    setLoading(false);
                }
            }
        };

        fetchRequests();

        return () => {
            cancelled = true;
        };
    }, []);

    const handleDelete = async (id) => {
        const confirmed = window.confirm(
            "Are you sure you want to delete this maintenance request?"
        );

        if (!confirmed) {
            return;
        }

        try {
            await deleteMaintenanceRequest(id);

            setRequests((previousRequests) =>
                previousRequests.filter(
                    (request) =>
                        request.maintenanceRequestId !== id
                )
            );
        } catch (err) {
            console.error(err);
            setError("Failed to delete maintenance request.");
        }
    };

    if (loading) {
        return (
            <div>
                <h2>Maintenance Requests</h2>
                <p>Loading maintenance requests...</p>
            </div>
        );
    }

    return (
        <div>
            <h2>Maintenance Requests</h2>

            {error && (
                <p style={{ color: "red" }}>
                    {error}
                </p>
            )}

            {requests.length === 0 ? (
                <p>No maintenance requests found.</p>
            ) : (
                <table>
                    <thead>
                        <tr>
                            <th>Request Number</th>
                            <th>Title</th>
                            <th>Category</th>
                            <th>Room</th>
                            <th>Priority</th>
                            <th>Status</th>
                            <th>Request Date</th>
                            <th>Actions</th>
                        </tr>
                    </thead>

                    <tbody>
                        {requests.map((request) => (
                            <tr
                                key={request.maintenanceRequestId}
                            >
                                <td>
                                    {request.requestNumber}
                                </td>

                                <td>
                                    {request.title}
                                </td>

                                <td>
                                    {request.category}
                                </td>

                                <td>
                                    {request.roomId}
                                </td>

                                <td>
                                    {request.priority}
                                </td>

                                <td>
                                    {request.status}
                                </td>

                                <td>
                                    {request.requestDate
                                        ? new Date(
                                            request.requestDate
                                        ).toLocaleDateString()
                                        : ""}
                                </td>

                                <td>
                                    <button
                                        onClick={() =>
                                            navigate(
                                                `/maintenance/edit/${request.maintenanceRequestId}`
                                            )
                                        }
                                    >
                                        Edit
                                    </button>
                                    <button
                                        onClick={() =>
                                            navigate(
                                                `/maintenance/${request.maintenanceRequestId}`
                                            )
                                        }
                                    >
                                        View
                                    </button>
                                    <button
                                        onClick={() =>
                                            handleDelete(
                                                request.maintenanceRequestId
                                            )
                                        }
                                    >
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
}

export default MaintenanceList;