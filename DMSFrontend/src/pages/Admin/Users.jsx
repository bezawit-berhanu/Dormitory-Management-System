//useState = lets react remember changing information.
//useEffect = lets us perform something when the component loads or changes.

import {useEffect, useState} from "react";

//React router navigation.
import { useNavigate } from "react-router-dom";

//Our backend communication layer.
import userManagementService
 from "../../services/userManagementService";
 const users = () => {
    //This contains our users, intitally there are no users.
    const [users, setUsers] = useState([]);
 //Used while waiting for the backend.
        const [loading, setLoading] = useState(true);

       //Stores an error message if something goes wrong.
        const [error, setError] = useState("");

        //used to navigate to another page.
        const navigate = useNavigate();
 }

