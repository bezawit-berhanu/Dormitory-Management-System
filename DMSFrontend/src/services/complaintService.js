import axios from "axios";

const API_URL = "http://localhost:5069/api/Complaint";


export const getComplaints = async () => {
    const response = await axios.get(API_URL);
    return response.data;
};


export const getComplaintById = async (id) => {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
};


export const createComplaint = async (data) => {
    const response = await axios.post(API_URL, data);
    return response.data;
};


export const updateComplaint = async (id,data)=>{
    const response = await axios.put(`${API_URL}/${id}`,data);
    return response.data;
};


export const deleteComplaint = async(id)=>{
    const response = await axios.delete(`${API_URL}/${id}`);
    return response.data;
};