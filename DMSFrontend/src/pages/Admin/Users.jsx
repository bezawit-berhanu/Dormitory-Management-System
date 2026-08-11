//useState = lets react remember changing information.
//useEffect = lets us perform something when the component loads or changes.

import {useEffect, useState} from "react";

//React router navigation.
import { useNavigate } from "react-router-dom";

//Our backend communication layer.
import userManagementService
 from "../../services/userManagementService";
 const users = () => {
    //This contains our users, intitally there are no users.
    const [users, setUsers] = useState([]);
 //Used while waiting for the backend.
        const [loading, setLoading] = useState(true);

       //Stores an error message if something goes wrong.
        const [error, setError] = useState("");

        //used to navigate to another page.
        const navigate = useNavigate();


        //To load users
        useEffect(() => {
            //We create a separate async function because the useEffect call back itself shouldn't normally be async.
            const loadUsers = async () => {
try {

    setLoading(true);
//Ask our service for all users.
    const data = await userManagementService.getUsers();
const userList = Array.isArray(data) ? data : data?.data || [];

setUsers(userList);

}
catch(err) {
    console.error(err); //Techninal info for us while developing.

    //Message for the user.
    setError(err.response?.data?.message ||
        "unable to load users."
    );
}
finally {
    //Whether successfull or failed, loading is finished.
    setLoading(false);
}

            };
            //Actually call the function.
            loadUsers();
        }, []);


        //Delete User 
        const handleDelete = async (id) => {
            const confirmed = window.confirm("Are you sure you want to delete this user?");

            if(!confirmed) {
                return;
            }
            try {

                //Tell the backened to delete the user
                await userManagementService.deleteUser(id);

//Remove the deleted user from our current scree,. 
                setUsers((previousUsers) => previousUsers.filter((user) =>
                (user.userId ?? user.id) !== id
                ));
            }
            catch(err) {
                console.error(err);
                setError(err.response?.data?.message || "Unable to delete user.");
            }
        };

        //Loading screen
        if(loading) {
            return (
                <div>
                    <h2>Users</h2>
                    <p>Loading users...</p>
                </div>
            );
        }
        return (
            <div className="users-page">

      {/* Page heading */}
      <div className="page-header">

        <div>
          <h1>User Management</h1>

          <p>
            Manage users and their accounts.
          </p>
        </div>


        {/* Button for creating a user.
            We'll connect this to a form later. */}
        <button
          onClick={() =>
            navigate("/admin/users/create")
          }
        >
          Add User
        </button>

      </div>


      {/* Error message */}
      {error && (
        <div className="error-message">
          {error}
        </div>
      )}


      {/* =====================================
          USERS TABLE
          ===================================== */}

      {users.length === 0 ? (

        // No users were returned.
        <p>No users found.</p>

      ) : (

        <table>

          <thead>

            <tr>

              <th>ID</th>

              <th>Name</th>

              <th>Email</th>

              <th>Phone</th>

              <th>Role</th>

              <th>Actions</th>

            </tr>

          </thead>


          <tbody>

            {/* 
              .map() goes through every user
              and creates one table row.
            */}
            {users.map((user) => {

              // Backend naming may differ.
              // We'll adapt this when we see
              // your actual DTO.
              const id =
                user.userId ?? user.id;

              return (

                <tr key={id}>

                  <td>
                    {id}
                  </td>

                  <td>
                    {user.fullName ||
                      user.name ||
                      "N/A"}
                  </td>

                  <td>
                    {user.email ||
                      "N/A"}
                  </td>

                  <td>
                    {user.phoneNumber ||
                      user.phone ||
                      "N/A"}
                  </td>

                  <td>
                    {user.roleName ||
                      user.role ||
                      "N/A"}
                  </td>

                  <td>

                    {/* Edit button */}
                    <button
                      onClick={() =>
                        navigate(
                          `/admin/users/${id}/edit`
                        )
                      }
                    >
                      Edit
                    </button>


                    {/* Delete button */}
                    <button
                      onClick={() =>
                        handleDelete(id)
                      }
                    >
                      Delete
                    </button>

                  </td>

                </tr>
              );
            })}

          </tbody>

        </table>
      )}

    </div>
            );
 };

 export default users;

