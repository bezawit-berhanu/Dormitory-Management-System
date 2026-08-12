import api from "../constants/api";
const qrCodeService = {
  async getStudentQRCode(studentId) {

    const response =
      await api.get(
        `/QRCode/student/${studentId}`
      );

    return response.data;
  },

  async verifyQRCode(qrValue) {

    const response =
      await api.post(
        "/QRCode/verify",
        {
          qrValue
        }
      );

    return response.data;
  }

};

export default qrCodeService;