import {useEffect,useState} from "react";
import {getTransfers} from "../../services/transferService";


function TransferList(){

const [transfers,setTransfers]=useState([]);


useEffect(()=>{

getTransfers()
.then(data=>setTransfers(data));

},[]);



return(

<div>

<h2>Room Transfer Requests</h2>


{
transfers.map(t=>(

<div key={t.transferId}>

<p>
Student: {t.studentName}
</p>

<p>
Status: {t.status}
</p>


</div>

))

}

</div>

)

}


export default TransferList;