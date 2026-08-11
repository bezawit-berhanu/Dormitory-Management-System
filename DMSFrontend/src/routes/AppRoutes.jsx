import { Routes, Route } from "react-router-dom";

import {
  Routes,
  Route,
  Navigate
} from "react-router-dom";
import AnnouncementList from "../pages/Announcement/AnnouncementList";
import CreateAnnouncement from "../pages/Announcement/CreateAnnouncement";
import CreateComplaint from "../pages/Complaint/CreateComplaint";
import ComplaintDetails from "../pages/Complaint/ComplaintDetails";
import ComplaintList from "../pages/Complaint/ComplaintList";
import TransferList from "../pages/Transfer/TransferList";
import InspectionList from "../pages/Inspection/InspectionList";
import NotificationList from "../pages/Notification/NotificationList";



import ProtectedRoute
  from "../routes/ProtectedRoute";

import Login
  from "../pages/Authentication/Login";

import ForgotPassword
  from "../pages/Authentication/ForgotPassword";

import Profile
  from "../pages/Authentication/Profile";

function AppRoutes() {
  return (
    <Routes>

      <Route
        path="/"
        element={<h1>Dormitory Management System</h1>}
      />
         <Route
        path="/announcements"
        element={<AnnouncementList />}
      />
      <Route
        path="/create-announcement"
        element={<CreateAnnouncement />}
      />
      <Route 
 path="/create-complaint" 
 element={<CreateComplaint/>}
/>


<Route
 path="/complaints/:id"
 element={<ComplaintDetails/>}
/>
<Route path="/complaints" element={<ComplaintList/>}/>

<Route path="/transfers" element={<TransferList/>}/>

<Route path="/inspections" element={<InspectionList/>}/>

<Route path="/notifications" element={<NotificationList/>}/>

      <Route
        path="/login"
        element={<Login />}
      />
      <Route
        path="/forgot-password"
        element={<ForgotPassword />}
      />


      <Route
        path="/profile"
        element={
          <ProtectedRoute>
            <Profile />
          </ProtectedRoute>
        }
      />

      <Route
        path="/"
        element={
          <Navigate
            to="/login"
            replace
          />
        }
      />


      
      <Route
        path="*"
        element={
          <Navigate
            to="/login"
            replace
          />
        }
      />

    </Routes>
  );
}


export default AppRoutes;