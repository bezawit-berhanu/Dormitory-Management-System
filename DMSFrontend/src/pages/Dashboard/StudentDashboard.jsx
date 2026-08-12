import {useState, useEffect } from "react"
import {Link} from "react-router-dom";
import studentService from "../../services/studentService";
import registrarService from "../../services/registrarService";


const StudentDashboard = () => {
    const [students, setStudents] = useState([]);

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    //Load students
    useEffect (()=> {
    const loadStudents = async () => {
        try  {
            const data = await studentService.getAllStudents();
            setStudents(Array.isArray(data)? data : []);
        }
        catch(err) {
            console.error(err);
            setError("Unable to load student statistics.");
        }
        finally {
            setLoading (false);
        }
    };
    loadStudents();
 }, []);

 //Statistics

 const totalStudents = students.length;
 const activeStudents = students.filter((student) => Number(student.status) ===1 ).length;
 const inactiveStudents = students.filter((student) => Number(student.status) === 2).length;
 const Graduated =students.filter((student)=> Number(student.status) === 3).length;
 const suspended = students.filter((student) => Number(student.status) === 4).length;
if (loading) {

    return (
      <div>
        Loading dashboard...
      </div>
    );
  }


  return (

    <div className="student-dashboard">

      <h1>
        Student Dashboard
      </h1>

      <p>
        Overview of student management.
      </p>


      {error && (

        <div className="error-message">
          {error}
        </div>

      )}a

{/* ==========================================
    STUDENT STATISTICS
    ========================================== */}

<div className="dashboard-cards">

  {/* Total students */}
  <div className="dashboard-card">
    <h2>{totalStudents}</h2>
    <p>Total Students</p>
  </div>


  {/* Active students */}
  <div className="dashboard-card">
    <h2>{activeStudents}</h2>
    <p>Active Students</p>
  </div>


  {/* Inactive students */}
  <div className="dashboard-card">
    <h2>{inactiveStudents}</h2>
    <p>Inactive Students</p>
  </div>


  {/* Graduated students */}
  <div className="dashboard-card">
    <h2>{Graduated}</h2>
    <p>Graduated Students</p>
  </div>


  {/* Suspended students */}
  <div className="dashboard-card">
    <h2>{suspendedStudents}</h2>
    <p>Suspended Students</p>
  </div>

</div>

      {/* ==================================
          QUICK ACTION
          ================================== */}

      <div className="dashboard-actions">

        <Link to="/students">
          View All Students
        </Link>

      </div>

    </div>
  );
};


export default StudentDashboard;