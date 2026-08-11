import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { getComplaints } from "../../services/complaintService";

function ComplaintList() {
    const [complaints, setComplaints] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        loadComplaints();
    }, []);

    const loadComplaints = async () => {
        try {
            const data = await getComplaints();

            setComplaints(data);
        } catch (error) {
            console.error("Error loading complaints:", error);
        } finally {
            setLoading(false);
        }
    };

    if (loading) {
        return <h2>Loading complaints...</h2>;
    }

    return (
        <div className="container">
            <h2>Complaints</h2>

            <Link
                to="/complaints/create"
                className="btn btn-primary mb-3"
            >
                Create Complaint
            </Link>

            {complaints.length === 0 ? (
                <p>No complaints found.</p>
            ) : (
                complaints.map((complaint) => (
                    <div
                        className="card p-3 mb-3"
                        key={complaint.complaintId}
                    >
                        <h4>{complaint.title}</h4>

                        <p>
                            <strong>ID:</strong>{" "}
                            {complaint.complaintId}
                        </p>

                        <p>
                            <strong>Student ID:</strong>{" "}
                            {complaint.sId}
                        </p>

                        <p>
                            <strong>Description:</strong>{" "}
                            {complaint.description}
                        </p>

                        <p>
                            <strong>Priority:</strong>{" "}
                            {complaint.priority || "Not specified"}
                        </p>

                        <p>
                            <strong>Status:</strong>{" "}
                            {complaint.status}
                        </p>

                        <p>
                            <strong>Date:</strong>{" "}
                            {complaint.complaintDate
                                ? new Date(
                                      complaint.complaintDate
                                  ).toLocaleDateString()
                                : ""}
                        </p>

                        <Link
                            to={`/complaints/${complaint.complaintId}`}
                            className="btn btn-secondary"
                        >
                            View Details
                        </Link>
                    </div>
                ))
            )}
        </div>
    );
}

export default ComplaintList;