/*useEffect- lets you perform side effects in react functional componenets
---- It helps in fetching data from API, Adding event listeners or timers, 
updating the document title, or changiing the manual DOM.

Noarray -> runs after every single render.
[] -> runs only once the componenet first loads.
[prop, state] -> runs on the first load and whenever thos specific value changes.
prop -> data passed down from a parent component to a child component.
(Read only).

state-> data manged internally within a single component.

*/


import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";

import studentService from "../../services/studentServices";
import studentCard from "../../components/StudentCard/StudentCard";

const StudentList = () => {

    //Stores the students returned by the API.
    const [students, setStudents] = useState([]);

    //Shows while students are being loaded.
    const [loading, setLoading] = useState(true);

    //stores an error message if the API fails.
    const [error, setError] = useState("");

    //used to navigate to pages.
    const navigate = useNavigate();

//Load Students
//Use effect runs when this page appears.
useEffect(() => {
    loadStudents();
}, []);


//Get students from backend.
const loadStudents = async () => {
//Put the code that might fail here (Fetching network data, parsing a file)
    try {
        setLoading(true);
        setError("");

        const data = await studentService.getAllStudents();

//Backend might return an array so the fall back prevents the page from breaking.
        setStudents(Array.isArray(data) ? data : []);
    }
    //If error occurs control jums to this block.
    catch(err) {
        console.error("Failed to load students", err); 

        setError(err.response?.data?.message || "Unable to load students.");
    }
    finally {
        setLoading(false);
    }
};
//DELETE STUDENT
/* 
Asynchronous vs Synchronous - differ in how they handle time and blocking during execution.

Async - tells the function it is going to execute asynchronous operations. It automatically forces the function to return a promise.
Await - between async and promise - it pauses the execution of the function until that promise
resolves.
Promise: represents the eventual completion or failure of an asynchronous operation and its resulting value.
*/
const handleDelete = async (id) => {
    const confirmed = window.confirm("Are you sure you want to delete this student?");

    if(!confirmed) {
        return;
    }

    try {
        await studentService.deleteStudent(id);

        setStudents((previousStudent) => previousStudents.filter((student) => 
        student.sId !== id));
    }
    catch(err) {
        console.error(err);

        setError(err?.response?.data?.message || "Unable to delete Students");
    }
};

 if (loading) {

    return (
      <div>
        <h2>Students</h2>
        <p>Loading students...</p>
      </div>
    );
  }


  return (

    <div className="student-list-page">

      {/* Page heading */}
      <div className="page-header">

        <div>

          <h1>
            Students
          </h1>

          <p>
            Manage registered students.
          </p>

        </div>


        {/* Add student button */}
        <button
          type="button"
          onClick={() =>
            navigate("/students/create")
          }
        >
          + Add Student
        </button>

      </div>


      {/* Error */}
      {error && (

        <div className="error-message">
          {error}
        </div>

      )}


      {/* ==================================
          STUDENT CARDS
          ================================== */}

      {students.length === 0 ? (

        <div>

          <h3>
            No students found.
          </h3>

          <p>
            Add a student to get started.
          </p>

        </div>

      ) : (

        <div className="student-grid">

          {students.map((student) => (

            <StudentCard
              key={student.sId}
              student={student}

              onView={(id) =>
                navigate(
                  `/students/${id}`
                )
              }

              onEdit={(id) =>
                navigate(
                  `/students/${id}/edit`
                )
              }

              onDelete={handleDelete}
            />

          ))}

        </div>

      )}

    </div>
  );
};


export default StudentList;