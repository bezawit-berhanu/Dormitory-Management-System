import studentService from "../../services/studentService";

const StudentDetails = () => {
    const {id} = useParams();
    const navigate = useNavigate();

    //StudentInformation
    const [student, setStudents] = useState([]);
    const [loading, setLoaading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        loadStudent()
    }, []);

    const loadStudent = async () => {
        try {
            setLoading(true);

            const data = await getStudentById();

            setStudents(data);
        }
        catch(err) {
            console.error(err);
        setError(err?.response?.data?.message || "Unable to load student detail.");
        }
        finally {
            setLoading(false);
        }
    };

    
  if (loading) {
     return (
      <div>
        Loading student...
      </div>
    );
  }


  if (error) {

    return (
      <div>

        <h2>
          Student not found
        </h2>

        <p>
          {error}
        </p>

        <button
          onClick={() =>
            navigate("/students")
          }
        >
          Back to Students
        </button>

      </div>
    );
  }


  if (!student) {

    return (
      <div>
        Student not found.
      </div>
    );
  }


  return (

    <div className="student-details-page">

      <div className="page-header">

        <div>

          <h1>
            Student Details
          </h1>

          <p>
            View student information.
          </p>

        </div>


        <button
          type="button"
          onClick={() =>
            navigate(
              `/students/${student.sId}/edit`
            )
          }
        >
          Edit Student
        </button>

      </div>


      <div className="student-details-card">

        <h2>
          {student.name ||
            student.fullName ||
            "Student"}
        </h2>


        <p>
          <strong>Student ID:</strong>{" "}
          {student.studentId || "N/A"}
        </p>


        <p>
          <strong>Department:</strong>{" "}
          {student.department ||
            student.departmentName ||
            student.departmentId ||
            "N/A"}
        </p>


        <p>
          <strong>Gender:</strong>{" "}
          {student.gender || "N/A"}
        </p>


        <p>
          <strong>Date of Birth:</strong>{" "}
          {student.dateOfBirth
            ? new Date(
                student.dateOfBirth
              ).toLocaleDateString()
            : "N/A"}
        </p>


        <p>
          <strong>Year of Study:</strong>{" "}
          {student.yearOfStudy || "N/A"}
        </p>


        <p>
          <strong>Emergency Contact:</strong>{" "}
          {student.emergencyContact ||
            student.emergencyContactNumber ||
            "N/A"}
        </p>


        <p>
          <strong>Status:</strong>{" "}
          {student.status ?? "N/A"}
        </p>

      </div>


      <Link to="/students">
        ← Back to Students
      </Link>

    </div>
  );
};

export default StudentDetails;