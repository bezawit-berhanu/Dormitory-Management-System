import api from "../constants/api";



const registrarService = {


  async getStudents() {
    const response =
      await api.get("/Registrar/students");

    return response.data;
  },


  async getStudentById(studentId) {
    const response =
      await api.get(
        `/Registrar/students/${studentId}`
      );

    return response.data;
  },

 
  async searchStudents(query) {
    const response =
      await api.get(
        `/Registrar/students/search?query=${encodeURIComponent(query)}`
      );

    return response.data;
  }
};

export default registrarService;