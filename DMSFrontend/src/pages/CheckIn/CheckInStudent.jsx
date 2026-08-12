import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../constants/api";

const CheckInStudent = () => {

  // Student ID entered by the staff.
  const [studentId, setStudentId] = useState("");

  // Optional room assignment ID.
  const [roomAssignmentId, setRoomAssignmentId] = useState("");

  // Used while the request is being sent.
  const [loading, setLoading] = useState(false);

  // Used to show success/error messages.
  const [message, setMessage] = useState("");

  const navigate = useNavigate();


  const handleSubmit = async (e) => {

    e.preventDefault();

    setMessage("");

    // Don't send an empty request.
    if (!studentId) {
      setMessage("Student ID is required.");
      return;
    }

    try {

      setLoading(true);

      // Send the check-in information to ASP.NET.
      //
      // IMPORTANT:
      // Adjust the URL/property names later if your
      // backend CheckInController uses different names.
      await api.post("/CheckIn", {
        studentId: studentId,
        roomAssignmentId: roomAssignmentId || null,
        checkInDate: new Date().toISOString()
      });

      setMessage("Student checked in successfully.");

      // Clear the form after successful check-in.
      setStudentId("");
      setRoomAssignmentId("");

    } catch (error) {

      console.error(error);

      setMessage(
        error.response?.data?.message ||
        "Failed to check in student."
      );

    } finally {

      setLoading(false);

    }
  };


  return (

    <div>

      <h1>Check In Student</h1>

      <p>
        Record the arrival of a student into the dormitory.
      </p>


      {message && (
        <p>{message}</p>
      )}


      <form onSubmit={handleSubmit}>

        <div>

          <label>
            Student ID
          </label>

          <input
            type="text"
            value={studentId}
            onChange={(e) =>
              setStudentId(e.target.value)
            }
            placeholder="Enter student ID"
          />

        </div>


        <div>

          <label>
            Room Assignment ID
          </label>

          <input
            type="text"
            value={roomAssignmentId}
            onChange={(e) =>
              setRoomAssignmentId(e.target.value)
            }
            placeholder="Optional"
          />

        </div>


        <button
          type="submit"
          disabled={loading}
        >

          {loading
            ? "Checking in..."
            : "Check In"}

        </button>

      </form>


      <button
        type="button"
        onClick={() =>
          navigate("/check-in/history")
        }
      >
        View Check-In History
      </button>

    </div>

  );
};

export default CheckInStudent;