import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../../constants/api";

const CheckInHistory = () => {

  // Stores the check-in records returned by the API.
  const [checkIns, setCheckIns] = useState([]);

  // Shows while we're loading data.
  const [loading, setLoading] = useState(true);

  // Stores an error message.
  const [error, setError] = useState("");


  useEffect(() => {

    const loadCheckIns = async () => {

      try {

        // Get check-in records from ASP.NET.
        const response = await api.get("/CheckIn");

        setCheckIns(response.data);

      } catch (error) {

        console.error(error);

        setError("Unable to load check-in history.");

      } finally {

        setLoading(false);

      }

    };

    loadCheckIns();

  }, []);


  if (loading) {
    return <p>Loading check-in history...</p>;
  }


  if (error) {
    return <p>{error}</p>;
  }


  return (

    <div>

      <h1>Check-In History</h1>


      <Link to="/check-in">
        Check In Student
      </Link>


      {checkIns.length === 0 ? (

        <p>
          No check-in records found.
        </p>

      ) : (

        <table>

          <thead>

            <tr>

              <th>Student ID</th>
              <th>Check-In Date</th>
              <th>Status</th>

            </tr>

          </thead>


          <tbody>

            {checkIns.map((checkIn) => (

              <tr key={checkIn.checkInId}>

                <td>
                  {checkIn.studentId}
                </td>

                <td>
                  {checkIn.checkInDate
                    ? new Date(
                        checkIn.checkInDate
                      ).toLocaleString()
                    : "-"}
                </td>

                <td>
                  {checkIn.status || "Checked In"}
                </td>

              </tr>

            ))}

          </tbody>

        </table>

      )}

    </div>

  );
};

export default CheckInHistory;