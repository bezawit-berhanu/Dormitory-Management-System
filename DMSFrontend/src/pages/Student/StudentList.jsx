import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import registrarService from "../../services/registrarService";

const StudentList = () => {

  const navigate = useNavigate();

  // Students received from Registrar
  const [students, setStudents] = useState([]);

  // Search box
  const [search, setSearch] = useState("");

  // Loading indicator
  const [loading, setLoading] = useState(true);

  // Error message
  const [error, setError] = useState("");

  // ==========================================
  // LOAD STUDENTS
  // ==========================================

  useEffect(() => {
    loadStudents();
  }, []);

  const loadStudents = async () => {

    try {

      setLoading(true);
      setError("");

      const data =
        await registrarService.getStudents();

      setStudents(data);

    } catch (err) {

      console.error(err);

      setError(
        "Could not load students from Registrar."
      );

    } finally {

      setLoading(false);
    }
  };

  // ==========================================
  // SEARCH
  // ==========================================

  const handleSearch = async (e) => {

    e.preventDefault();

    try {

      setLoading(true);
      setError("");

      // Empty search = get everyone
      const data = search.trim()
        ? await registrarService.searchStudents(search)
        : await registrarService.getStudents();

      setStudents(data);

    } catch (err) {

      console.error(err);

      setError("Student search failed.");

    } finally {

      setLoading(false);
    }
  };

  // ==========================================
  // STATUS TEXT
  // ==========================================

  const getStatusName = (status) => {

    switch (Number(status)) {

      case 1:
        return "Active";

      case 2:
        return "Inactive";

      case 3:
        return "Graduated";

      case 4:
        return "Suspended";

      default:
        return "Unknown";
    }
  };

  // ==========================================
  // UI
  // ==========================================

  return (
    <div>

      <h1>Students</h1>

      <p>
        Students are retrieved from the Registrar.
      </p>

      {/* Search */}
      <form onSubmit={handleSearch}>

        <input
          type="text"
          value={search}
          onChange={(e) =>
            setSearch(e.target.value)
          }
          placeholder="Search by ID, name or department"
        />

        <button type="submit">
          Search
        </button>

      </form>

      {/* Error */}
      {error && (
        <p>{error}</p>
      )}

      {/* Loading */}
      {loading && (
        <p>Loading students...</p>
      )}

      {/* Student table */}
      {!loading && (

        <table>

          <thead>
            <tr>
              <th>Student ID</th>
              <th>Name</th>
              <th>Department</th>
              <th>Year</th>
              <th>Status</th>
              <th>Action</th>
            </tr>
          </thead>

          <tbody>

            {students.map((student) => (

              <tr key={student.studentId}>

                <td>
                  {student.studentId}
                </td>

                <td>
                  {student.fullName}
                </td>

                <td>
                  {student.department}
                </td>

                <td>
                  {student.yearOfStudy}
                </td>

                <td>
                  {getStatusName(student.status)}
                </td>

                <td>

                  <button
                    onClick={() =>
                      navigate(
                        `/students/${student.studentId}`
                      )
                    }
                  >
                    View
                  </button>

                </td>

              </tr>

            ))}

          </tbody>

        </table>

      )}

    </div>
  );
};

export default StudentList;