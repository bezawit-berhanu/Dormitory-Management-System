import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../../constants/api";

const CheckOutHistory = () => {

  const [checkOuts, setCheckOuts] = useState([]);

  const [loading, setLoading] =
    useState(true);

  const [error, setError] =
    useState("");


  useEffect(() => {

    const loadCheckOuts = async () => {

      try {

        const response =
          await api.get("/CheckOut");

        setCheckOuts(response.data);

      } catch (error) {

        console.error(error);

        setError(
          "Unable to load check-out history."
        );

      } finally {

        setLoading(false);

      }

    };

    loadCheckOuts();

  }, []);


  if (loading) {
    return <p>Loading check-out history...</p>;
  }


  if (error) {
    return <p>{error}</p>;
  }


  return (

    <div>

      <h1>Check-Out History</h1>


      <Link to="/check-out">
        Check Out Student
      </Link>


      {checkOuts.length === 0 ? (

        <p>
          No check-out records found.
        </p>

      ) : (

        <table>

          <thead>

            <tr>

              <th>Student ID</th>
              <th>Date</th>
              <th>Reason</th>

            </tr>

          </thead>


          <tbody>

            {checkOuts.map((checkOut) => (

              <tr key={checkOut.checkOutId}>

                <td>
                  {checkOut.studentId}
                </td>

                <td>
                  {checkOut.checkOutDate
                    ? new Date(
                        checkOut.checkOutDate
                      ).toLocaleString()
                    : "-"}
                </td>

                <td>
                  {checkOut.reason || "-"}
                </td>

              </tr>

            ))}

          </tbody>

        </table>

      )}

    </div>

  );
};

export default CheckOutHistory;