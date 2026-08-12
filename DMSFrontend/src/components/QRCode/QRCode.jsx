

import { QRCodeCanvas } from "qrcode.react";


const QRCode = ({ student }) => {

  const qrValue = student?.studentId || "";

  if (!qrValue) {

    return (
      <p>
        No student ID available for QR code.
      </p>
    );

  }


  return (

    <div className="qr-code">

      <h3>
        Student QR Code
      </h3>
// Generates the actual QRImage by taking the value
      <QRCodeCanvas
        value={qrValue}
        size={220}
      />
      <p>
        Student ID: {student.studentId}
      </p>

    </div>

  );

};


export default QRCode;