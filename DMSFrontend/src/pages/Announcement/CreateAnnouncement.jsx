import { useState } from "react";
import { createAnnouncement } from "../../services/announcementService";


function CreateAnnouncement(){

const [announcement,setAnnouncement]=useState({
    createdBy:1,
    title:"",
    message:"",
    publishedDate:"",
    expiryDate:"",
    status:"Active"
});


const handleChange=(e)=>{
    setAnnouncement({
        ...announcement,
        [e.target.name]:e.target.value
    });
};


const handleSubmit=async(e)=>{
    e.preventDefault();

    try{

        await createAnnouncement(announcement);

        alert("Announcement created successfully");

    }
    catch(error){

        console.log(error);
        alert("Failed to create announcement");

    }

};


return(

<div>

<h2>Create Announcement</h2>


<form onSubmit={handleSubmit}>


<input
name="title"
placeholder="Title"
value={announcement.title}
onChange={handleChange}
/>


<textarea
name="message"
placeholder="Message"
value={announcement.message}
onChange={handleChange}
/>


<input
type="datetime-local"
name="publishedDate"
onChange={handleChange}
/>


<input
type="datetime-local"
name="expiryDate"
onChange={handleChange}
/>


<select
name="status"
onChange={handleChange}
>

<option value="Active">
Active
</option>

<option value="Expired">
Expired
</option>

</select>


<button type="submit">
Create
</button>


</form>


</div>

);

}


export default CreateAnnouncement;