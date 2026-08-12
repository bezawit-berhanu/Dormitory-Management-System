import axios from "axios";

const registrarApi = axios.create({
  baseURL: "http://localhost:5100/api",
});

export default registrarApi;
