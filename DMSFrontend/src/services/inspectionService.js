import axios from "axios";


const API_URL="http://localhost:5069/api/Inspection";


export const getInspections=async()=>{

const response=await axios.get(API_URL);

return response.data;

};


export const createInspection=async(data)=>{

const response=await axios.post(API_URL,data);

return response.data;

};