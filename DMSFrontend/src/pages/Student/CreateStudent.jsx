import {useState} from "react";
import {useNavigate} from "react-router-dom";
import studentService from "../../services/studentService";

const createStudent = () => {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
    userId: "",

      name: "",

      studentId: "",

      departmentId: "",

      gender: "",

      dateOfBirth: "",

      yearOfStudy: "",

      emergencyContactNumber: ""
    });

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");



    const handleChange = (event) => {
        const {name, value}= event.target;
            setFormData((previous) => ({
                  ...previous, [name]: value
            }));
    };


    const handleSubmit = async (event) => {
        event.preventDefault();
        setError("");

    if(
        !formData.userId ||
        !formData.name ||
        !formData.studentId ||
        !formData.departmentId
    ) {
        setError ("Please fill the required fields!");
        return;
    }


    try {
        setLoading(true);
        
    const studentData = {
        userId: Number(formData.UserId),
        name: formData.name,
        studentId: formData.studentId,
        departmentId: Number(formData.departmentId),
        gender: formData.gender,
        yearOfStudy: formData.yearOfStudy,
        emergencyContactNumber: formData.emergencyContactNumber
    };
    await studentService.createStudent(
        studentData
    );
    navigate("/students");
    } catch(err) {
        console.error(err);
        setError(err?.response?.data?.message || "Failed to create students.");
    }
    finally {
        setLoading(false);
    }
};


return (

    <div className="student-form-page">

      <h1>
        Create Student
      </h1>

      <p>
        Add a new student to the system.
      </p>


      {error && (

        <div className="error-message">
          {error}
        </div>

      )}


      <form
        onSubmit={handleSubmit}
        className="student-form"
      >

        {/* User ID */}
        <div className="form-group">

          <label htmlFor="userId">
            User ID *
          </label>

          <input
            id="userId"
            name="userId"
            type="number"
            value={formData.userId}
            onChange={handleChange}
          />

        </div>


        {/* Name */}
        <div className="form-group">

          <label htmlFor="name">
            Full Name *
          </label>

          <input
            id="name"
            name="name"
            type="text"
            value={formData.name}
            onChange={handleChange}
            placeholder="Enter full name"
          />

        </div>


        {/* Student ID */}
        <div className="form-group">

          <label htmlFor="studentId">
            Student ID *
          </label>

          <input
            id="studentId"
            name="studentId"
            type="text"
            value={formData.studentId}
            onChange={handleChange}
            placeholder="Enter student ID"
          />

        </div>


        {/* Department */}
        <div className="form-group">

          <label htmlFor="departmentId">
            Department ID *
          </label>

          <input
            id="departmentId"
            name="departmentId"
            type="number"
            value={formData.departmentId}
            onChange={handleChange}
          />

        </div>


        {/* Gender */}
        <div className="form-group">

          <label htmlFor="gender">
            Gender
          </label>

          <select
            id="gender"
            name="gender"
            value={formData.gender}
            onChange={handleChange}
          >

            <option value="">
              Select gender
            </option>

            <option value="Female">
              Female
            </option>

            <option value="Male">
              Male
            </option>

          </select>

        </div>


        {/* Date of birth */}
        <div className="form-group">

          <label htmlFor="dateOfBirth">
            Date of Birth
          </label>

          <input
            id="dateOfBirth"
            name="dateOfBirth"
            type="date"
            value={formData.dateOfBirth}
            onChange={handleChange}
          />

        </div>


        {/* Year */}
        <div className="form-group">

          <label htmlFor="yearOfStudy">
            Year of Study
          </label>

          <input
            id="yearOfStudy"
            name="yearOfStudy"
            type="text"
            value={formData.yearOfStudy}
            onChange={handleChange}
            placeholder="e.g. 3"
          />

        </div>


        {/* Emergency contact */}
        <div className="form-group">

          <label htmlFor="emergencyContactNumber">
            Emergency Contact
          </label>

          <input
            id="emergencyContactNumber"
            name="emergencyContactNumber"
            type="text"
            value={
              formData.emergencyContactNumber
            }
            onChange={handleChange}
            placeholder="Emergency contact number"
          />

        </div>


        {/* ==================================
            FORM ACTIONS
            ================================== */}

        <div className="form-actions">

          <button
            type="button"
            onClick={() =>
              navigate("/students")
            }
          >
            Cancel
          </button>


          <button
            type="submit"
            disabled={loading}
          >

            {loading
              ? "Creating..."
              : "Create Student"}

          </button>

        </div>

      </form>

    </div>
  );
};

export default CreateStudent;