import {useEffect,useState} from "react";
import {getInspections} from "../../services/inspectionService";


function InspectionList(){

const [inspections,setInspections]=useState([]);


useEffect(()=>{

getInspections()
.then(data=>setInspections(data));

},[]);



return(

<div>

<h2>Inspections</h2>


{
inspections.map(i=>(

<div key={i.inspectionId}>

<p>
Room: {i.roomId}
</p>

<p>
Status: {i.status}
</p>

</div>

))

}


</div>

)

}

export default InspectionList;