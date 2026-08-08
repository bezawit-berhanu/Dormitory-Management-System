import { BrowserRouter, Routes, Route, Navigate} from "react-router-dom";
import Login from "../pages/Authentication/Login";
import ForgotPassword from "../pages/Authentication/ForgotPassword";
import Profile from "../pages/Authentication/Profile";
import ProtectedRoute from "../Componenets/ProtectedRoute";
function AppRoutes() {
    return (
        <BrowserRouter>
        <Routes>
           <Route path="/" element={<Navigate to="/login" replace />} />
            <Route path="/login" element={<Login />} />
            <Route path="/forgot-password" element= {<ForgotPassword />} />
            <Route element= {<ProtectedRoute/>}/>
            <Route path = "/profile" element = {<profile />}/>
            <Route path="*" element={<h1>404 - Page Not Found</h1>} />
            
        </Routes>
        </BrowserRouter>
    )
}

export default AppRoutes;