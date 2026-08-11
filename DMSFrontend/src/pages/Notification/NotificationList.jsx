import Notification from "../../components/notification/Notification";import {useEffect,useState} from "react";

function NotificationList(){

const [notifications,setNotifications]=useState([]);


useEffect(()=>{

getNotifications()
.then(data=>setNotifications(data));

},[]);



return(

<div>

<h2>Notifications</h2>


{
notifications.map(n=>(

<div key={n.notificationId}>

<h4>{n.title}</h4>

<p>{n.message}</p>

</div>

))

}


</div>

)

}


export default NotificationList;