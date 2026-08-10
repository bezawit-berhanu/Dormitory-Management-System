import { useEffect, useState} from "react";

import userManagementService from "../../services/userManagementService";

const Roles = () => {
    const [roles, setRoles] = useState([]); //List of roles from back.

    const [roleName, setRoleName] = useState(""); //New role name by admin.
    const [laoding, setLoading] = useState(true);

    const [error, setError] = useState("");


    //LOAD ROLES

    useEffect(() => {
        const loadRoles = async () => {
            try {
                setLoading(true);

                const data = await userManagementService.getRoles();

                const roleList = Array.isArray(data) ? data : data?.data || [];
                setRoles(roleList);
            }
            catch(err) {
                console.error(err);
                setError(err.response?.data?.message || "Unable to load roles.");
            }
            finally {
                setLoading(false);
            }
        };
        loadRoles();
    }, []);

    const handleCreateRole = async (e) => {
        e.preventDefault();

        if(!roleName.trim()) {
            setError("Role name is required.");
            return;
        }

        try {
            setError("");
//Send role to backend.
            const created = await userManagementService.createRole({roleName: roleName.trim()});

            const newRole = created?.data || created;// Add new role to the screen

            setRoles((previousRoles) => [...previousRoles, newRole]);

            setRoleName("");
        }
        catch(err) {
            console.error(err);

            setError(err.repsonse?.data?.message || "Unable to create role.");
        }
    };
    
  const handleDelete = async (id) => {

    const confirmed =
      window.confirm(
        "Are you sure you want to delete this role?"
      );


    if (!confirmed) {
      return;
    }


    try {

      await userManagementService.deleteRole(id);


      // Remove role from current UI.
      setRoles((previousRoles) =>
        previousRoles.filter(
          (role) =>
            (role.roleId ?? role.id) !== id
        )
      );

    } catch (err) {

      console.error(err);

      setError(
        err.response?.data?.message ||
        "Unable to delete role."
      );
    }
  };


  // ==========================================
  // LOADING
  // ==========================================

  if (loading) {

    return (
      <div>
        Loading roles...
      </div>
    );
  }


  // ==========================================
  // UI
  // ==========================================

  return (

    <div className="roles-page">

      <h1>Role Management</h1>

      <p>
        Manage system roles.
      </p>


      {/* Show errors when needed */}
      {error && (
        <div className="error-message">
          {error}
        </div>
      )}


      {/* =====================================
          CREATE ROLE FORM
          ===================================== */}

      <form
        onSubmit={handleCreateRole}
      >

        <input
          type="text"

          placeholder="Role name"

          // Input displays roleName.
          value={roleName}

          // Update roleName when typing.
          onChange={(e) =>
            setRoleName(e.target.value)
          }
        />


        <button type="submit">
          Add Role
        </button>

      </form>


      {/* =====================================
          ROLE LIST
          ===================================== */}

      <div className="role-list">

        {roles.length === 0 ? (

          <p>No roles found.</p>

        ) : (

          <ul>

            {roles.map((role) => {

              const id =
                role.roleId ?? role.id;

              return (

                <li key={id}>

                  <span>
                    {role.roleName ||
                      role.name ||
                      "Unnamed Role"}
                  </span>


                  <button
                    onClick={() =>
                      handleDelete(id)
                    }
                  >
                    Delete
                  </button>

                </li>

              );
            })}

          </ul>
        )}

      </div>

    </div>
  );
};
export default Roles;