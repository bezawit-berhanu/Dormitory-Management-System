import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createComplaint } from "../../services/complaintService";

function CreateComplaint() {

    const navigate = useNavigate();

    const [complaint, setComplaint] = useState({
        sId: 1,
        title: "",
        description: "",
        priority: "",
        status: "Pending"
    });

    const handleChange = (e) => {
        setComplaint({
            ...complaint,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {

            await createComplaint({
                sId: Number(complaint.sId),
                title: complaint.title,
                description: complaint.description,
                priority: complaint.priority,
                status: complaint.status,
                complaintDate: new Date().toISOString()
            });

            alert("Complaint submitted successfully");

            navigate("/complaints");

        } catch (error) {

            console.error("Error creating complaint:", error);

            alert("Failed to create complaint");
        }
    };

    return (
        <div className="container">

            <h2>Create Complaint</h2>

            <form onSubmit={handleSubmit}>

                <div className="mb-3">

                    <label>Student ID</label>

                    <input
                        type="number"
                        name="sId"
                        value={complaint.sId}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="mb-3">

                    <label>Title</label>

                    <input
                        type="text"
                        name="title"
                        value={complaint.title}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="mb-3">

                    <label>Description</label>

                    <textarea
                        name="description"
                        value={complaint.description}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="mb-3">

                    <label>Priority</label>

                    <input
                        type="text"
                        name="priority"
                        value={complaint.priority}
                        onChange={handleChange}
                        placeholder="e.g. High, Medium, Low"
                    />

                </div>

                <button
                    type="submit"
                    className="btn btn-primary"
                >
                    Submit Complaint
                </button>

            </form>

        </div>
    );
}

export default CreateComplaint;