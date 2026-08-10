const API_URL = "http://localhost:5069/api/DormitoryStructure";

export const getDormitories = async () => {
    const response = await fetch(`${API_URL}/dormitories`);

    if (!response.ok) {
        throw new Error("Failed to fetch dormitories");
    }

    return await response.json();
};

export const getDormitoryById = async (id) => {
    const response = await fetch(`${API_URL}/dormitories/${id}`);

    if (!response.ok) {
        throw new Error("Failed to fetch dormitory");
    }

    return await response.json();
};

export const createDormitory = async (dormitory) => {
    const response = await fetch(`${API_URL}/dormitories`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(dormitory),
    });

    if (!response.ok) {
        throw new Error("Failed to create dormitory");
    }

    return await response.json();
};

export const updateDormitory = async (id, dormitory) => {
    const response = await fetch(`${API_URL}/dormitories/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(dormitory),
    });

    if (!response.ok) {
        throw new Error("Failed to update dormitory");
    }
};
export const deactivateDormitory = async (id) => {
    const response = await fetch(
        `${API_URL}/dormitories/${id}/deactivate`,
        {
            method: "PUT",
        }
    );

    if (!response.ok) {
        throw new Error("Failed to deactivate dormitory");
    }
}; export const getBlocks = async () => {
    const response = await fetch(`${API_URL}/blocks`);

    if (!response.ok) {
        throw new Error("Failed to fetch blocks");
    }

    return await response.json();
};

export const createBlock = async (block) => {
    const response = await fetch(`${API_URL}/blocks`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(block),
    });

    if (!response.ok) {
        throw new Error("Failed to create block");
    }

    return await response.json();
};

export const updateBlock = async (id, block) => {
    const response = await fetch(`${API_URL}/blocks/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(block),
    });

    if (!response.ok) {
        throw new Error("Failed to update block");
    }
};

export const getBlockById = async (id) => {
    const response = await fetch(`${API_URL}/blocks/${id}`);

    if (!response.ok) {
        throw new Error("Failed to fetch block");
    }

    return await response.json();
};

export const deactivateBlock = async (id) => {
    const response = await fetch(
        `${API_URL}/blocks/${id}/deactivate`,
        {
            method: "PUT",
        }
    );

    if (!response.ok) {
        throw new Error("Failed to deactivate block");
    }
};
export const getFloors = async () => {
    const response = await fetch(`${API_URL}/floors`);

    if (!response.ok) {
        throw new Error("Failed to fetch floors");
    }

    return await response.json();
};

export const getFloorById = async (id) => {
    const response = await fetch(`${API_URL}/floors/${id}`);

    if (!response.ok) {
        throw new Error("Failed to fetch floor");
    }

    return await response.json();
};

export const createFloor = async (floor) => {
    const response = await fetch(`${API_URL}/floors`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(floor),
    });

    if (!response.ok) {
        throw new Error("Failed to create floor");
    }

    return await response.json();
};

export const updateFloor = async (id, floor) => {
    const response = await fetch(`${API_URL}/floors/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(floor),
    });

    if (!response.ok) {
        throw new Error("Failed to update floor");
    }

    if (response.status === 204) {
        return;
    }

    return await response.json();
};

export const deactivateFloor = async (id) => {
    const response = await fetch(
        `${API_URL}/floors/${id}/deactivate`,
        {
            method: "PUT",
        }
    );

    if (!response.ok) {
        throw new Error("Failed to deactivate floor");
    }
};
export const getRooms = async () => {
    const response = await fetch(`${API_URL}/rooms`);

    if (!response.ok) {
        throw new Error("Failed to fetch rooms");
    }

    return await response.json();
};

export const getRoomById = async (id) => {
    const response = await fetch(`${API_URL}/rooms/${id}`);

    if (!response.ok) {
        throw new Error("Failed to fetch room");
    }

    return await response.json();
};

export const createRoom = async (room) => {
    const response = await fetch(`${API_URL}/rooms`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(room),
    });

    if (!response.ok) {
        throw new Error("Failed to create room");
    }

    return await response.json();
};

export const updateRoom = async (id, room) => {
    const response = await fetch(`${API_URL}/rooms/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(room),
    });

    if (!response.ok) {
        throw new Error("Failed to update room");
    }

    if (response.status === 204) {
        return;
    }

    return await response.json();
};

export const deactivateRoom = async (id) => {
    const response = await fetch(
        `${API_URL}/rooms/${id}/deactivate`,
        {
            method: "PUT",
        }
    );

    if (!response.ok) {
        throw new Error("Failed to deactivate room");
    }
};
export const getBeds = async () => {
    const response = await fetch(`${API_URL}/beds`);

    if (!response.ok) {
        throw new Error("Failed to fetch beds");
    }

    return await response.json();
};

export const getBedById = async (id) => {
    const response = await fetch(`${API_URL}/beds/${id}`);

    if (!response.ok) {
        throw new Error("Failed to fetch bed");
    }

    return await response.json();
};

export const createBed = async (bed) => {
    const response = await fetch(`${API_URL}/beds`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(bed),
    });

    if (!response.ok) {
        throw new Error("Failed to create bed");
    }

    return await response.json();
};

export const updateBed = async (id, bed) => {
    const response = await fetch(`${API_URL}/beds/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(bed),
    });

    if (!response.ok) {
        throw new Error("Failed to update bed");
    }

    if (response.status === 204) {
        return;
    }

    return await response.json();
};

export const deactivateBed = async (id) => {
    const response = await fetch(
        `${API_URL}/beds/${id}/deactivate`,
        {
            method: "PUT",
        }
    );

    if (!response.ok) {
        throw new Error("Failed to deactivate bed");
    }
};