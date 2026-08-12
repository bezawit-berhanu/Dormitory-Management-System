import {useEffect, useRef} from "react";
import {HTML5QrCode } from "html5-qrcode";

const QRScanner = ({ onScan}) => {
    //Keeps a reference to the scanner instance.
    const scannerRef = useRef(null);

    useEffect (() => {
        const scanner = new HTML5Qrcode("qr-reader");

        scannerRef.current = scanner;
//Starting the device camera.
        scanner.start(
            {
                facingMode: "environment"
            },
            {
                fps: 10, 
                qrbox: {
                    width: 250, 
                    hegiht: 250
                }
            },
            //QR successfully scanned.
            (decodedText) => {
                console.log("QR scanned: ", decodedText);

                //Give the scanned value to the parent component.
                if(onScan) {
                    onScan(decodedText)
                }
            },

            () => {}
        ).catch((error) => {
            console.error("unable to start camera: ", error);
        });

        return () => {
            if(scannerRef.current) {
                scannerRef.current.stop().catch(() => {});
            }
        };
    }, [onScan]);

    return (

    <div>

      <h3>
        Scan Student QR Code
      </h3>


      <div id="qr-reader"></div>

    </div>

  );
};

export default QRScanner;

