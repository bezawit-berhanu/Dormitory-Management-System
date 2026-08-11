import axios from "axios";

const API_URL="http://localhost:5069/api/Transfer";


export const getTransfers=async()=>{

const response=await axios.get(API_URL);

return response.data;

};


export const createTransfer=async(data)=>{

const response=await axios.post(API_URL,data);

return response.data;

};


export const getTransferById=async(id)=>{

const response=await axios.get(`${API_URL}/${id}`);

return response.data;

};