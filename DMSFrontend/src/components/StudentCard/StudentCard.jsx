const studentCard = ({student, onView, onEdit, onDelete}) => {
    const studentName = student.name ||
    student.fullName ||
    "Unknown Student";

    const studentId = student.studentId || "N/A";

    const department = student.department ||
    student.departmentId ||
    student.departmentName || 
    "N/A";


    const status = student.status ?? "N/A";

    return  (

        <div className="student-card">

      <h3>
        {studentName}
      </h3>


      <p>
        <strong>Student ID:</strong>{" "}
        {studentId}
      </p>


      {/* Department */}
      <p>
        <strong>Department:</strong>{" "}
        {department}
      </p>


      {/* Gender */}
      <p>
        <strong>Gender:</strong>{" "}
        {student.gender || "N/A"}
      </p>


      {/* Year */}
      <p>
        <strong>Year:</strong>{" "}
        {student.yearOfStudy || "N/A"}
      </p>


      {/* Status */}
      <p>
        <strong>Status:</strong>{" "}
        {status}
      </p>


      <div className="student-card-actions">

        <button
          type="button"
          onClick={() => onView(student.sId)}
        >
          View
        </button>


        <button
          type="button"
          onClick={() => onEdit(student.sId)}
        >
          Edit
        </button>


        <button
          type="button"
          onClick={() => onDelete(student.sId)}
        >
          Delete
        </button>

      </div>

    </div>
    );
};