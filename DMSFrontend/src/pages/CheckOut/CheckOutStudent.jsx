import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../constants/api";

const CheckOutStudent = () => {

  const [studentId, setStudentId] = useState("");

  const [reason, setReason] = useState("");

  const [loading, setLoading] = useState(false);

  const [message, setMessage] = useState("");

  const navigate = useNavigate();


  const handleSubmit = async (e) => {

    e.preventDefault();

    setMessage("");

    if (!studentId) {

      setMessage("Student ID is required.");

      return;
    }


    try {

      setLoading(true);

      // Send checkout information to ASP.NET.
      await api.post("/CheckOut", {

        studentId: studentId,

        checkOutDate:
          new Date().toISOString(),

        reason: reason

      });


      setMessage(
        "Student checked out successfully."
      );


      setStudentId("");

      setReason("");

    } catch (error) {

      console.error(error);

      setMessage(
        error.response?.data?.message ||
        "Failed to check out student."
      );

    } finally {

      setLoading(false);

    }

  };


  return (

    <div>

      <h1>Check Out Student</h1>


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
            Reason
          </label>

          <input
            type="text"
            value={reason}
            onChange={(e) =>
              setReason(e.target.value)
            }
            placeholder="Reason for checkout"
          />

        </div>


        <button
          type="submit"
          disabled={loading}
        >

          {loading
            ? "Checking out..."
            : "Check Out"}

        </button>

      </form>


      <button
        type="button"
        onClick={() =>
          navigate("/check-out/history")
        }
      >
        View Check-Out History
      </button>

    </div>

  );
};

export default CheckOutStudent;