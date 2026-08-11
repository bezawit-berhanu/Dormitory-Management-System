import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { getComplaintById } from "../../services/complaintService";

const ComplaintDetails = () => {
    const { id } = useParams();

    const [complaint, setComplaint] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        loadComplaint();
    }, [id]);

    const loadComplaint = async () => {
        try {
            const data = await getComplaintById(id);

            setComplaint(data);
        } catch (error) {
            console.error("Error loading complaint:", error);
        } finally {
            setLoading(false);
        }
    };

    if (loading) {
        return <h2>Loading...</h2>;
    }

    if (!complaint) {
        return <h2>Complaint not found</h2>;
    }

    return (
        <div className="container">
            <h2>Complaint Details</h2>

            <div className="card p-3">

                <p>
                    <strong>ID:</strong>{" "}
                    {complaint.complaintId}
                </p>

                <p>
                    <strong>Student ID:</strong>{" "}
                    {complaint.sId}
                </p>

                <p>
                    <strong>Title:</strong>{" "}
                    {complaint.title}
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
                    <strong>Complaint Date:</strong>{" "}
                    {complaint.complaintDate
                        ? new Date(
                              complaint.complaintDate
                          ).toLocaleString()
                        : ""}
                </p>

            </div>

            <Link
                to="/complaints"
                className="btn btn-secondary mt-3"
            >
                Back
            </Link>
        </div>
    );
};

export default ComplaintDetails;