const API_URL = "http://localhost:5069/api/Maintenance";

// Get all maintenance requests
export const getMaintenanceRequests = async () => {
    const response = await fetch(API_URL);

    if (!response.ok) {
        throw new Error("Failed to fetch maintenance requests");
    }

    return await response.json();
};

// Get maintenance request by ID
export const getMaintenanceRequestById = async (id) => {
    const response = await fetch(`${API_URL}/${id}`);

    if (!response.ok) {
        throw new Error("Failed to fetch maintenance request");
    }

    return await response.json();
};

// Create maintenance request
export const createMaintenanceRequest = async (maintenanceData) => {
    const response = await fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(maintenanceData),
    });

    if (!response.ok) {
        throw new Error("Failed to create maintenance request");
    }

    return await response.json();
};

// Update maintenance request
export const updateMaintenanceRequest = async (id, maintenanceData) => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(maintenanceData),
    });

    if (!response.ok) {
        throw new Error("Failed to update maintenance request");
    }

    return await response.json();
};

// Delete maintenance request
export const deleteMaintenanceRequest = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, {
        method: "DELETE",
    });

    if (!response.ok) {
        throw new Error("Failed to delete maintenance request");
    }

    return true;
};