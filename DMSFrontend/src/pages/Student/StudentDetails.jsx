import { useEffect, useState } from "react";
import {
  useNavigate,
  useParams
} from "react-router-dom";

import registrarService from "../../services/registrarService";

const StudentDetails = () => {

  const { studentId } = useParams();

  const navigate = useNavigate();

  const [student, setStudent] = useState(null);

  const [loading, setLoading] = useState(true);

  const [error, setError] = useState("");

  // ==========================================
  // LOAD STUDENT FROM REGISTRAR
  // ==========================================

  useEffect(() => {

    loadStudent();

  }, [studentId]);

  const loadStudent = async () => {

    try {

      setLoading(true);

      const data =
        await registrarService
          .getStudentById(studentId);

      setStudent(data);

    } catch (err) {

      console.error(err);

      setError(
        "Student could not be found."
      );

    } finally {

      setLoading(false);
    }
  };

  // ==========================================
  // STATUS
  // ==========================================

  const getStatusName = (status) => {

    const statuses = {
      1: "Active",
      2: "Inactive",
      3: "Graduated",
      4: "Suspended"
    };

    return statuses[Number(status)]
      || "Unknown";
  };

  if (loading)
    return <p>Loading student...</p>;

  if (error)
    return <p>{error}</p>;

  if (!student)
    return <p>Student not found.</p>;

  return (

    <div>

      <button
        onClick={() =>
          navigate("/students")
        }
      >
        ← Back to Students
      </button>

      <h1>
        {student.fullName}
      </h1>

      <p>
        Student ID: {student.studentId}
      </p>

      <p>
        Department: {student.department}
      </p>

      <p>
        Year of Study: {student.yearOfStudy}
      </p>

      <p>
        Gender: {student.gender}
      </p>

      <p>
        Date of Birth:
        {" "}
        {new Date(
          student.dateOfBirth
        ).toLocaleDateString()}
      </p>

      <p>
        Status:
        {" "}
        {getStatusName(student.status)}
      </p>

      {/* ======================================
          THIS IS WHERE RESIDENCE MANAGEMENT
          STARTS.
          ====================================== */}

      <button
        onClick={() =>
          navigate(
            `/assignments/${student.studentId}`
          )
        }
      >
        Assign Room
      </button>

    </div>
  );
};

export default StudentDetails;