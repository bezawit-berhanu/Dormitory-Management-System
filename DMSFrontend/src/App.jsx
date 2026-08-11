import { BrowserRouter, Routes, Route } from "react-router-dom";
import {AuthProvider } from "./context/AuthContext";
import AppRoutes from "./routes/AppRoutes";

import Dormitory from "./pages/Dormitory";
import DormitoryList from "./pages/Dormitory/DormitoryList";
import CreateDormitory from "./pages/Dormitory/CreateDormitory";
import EditDormitory from "./pages/Dormitory/EditDormitory";
import Blocks from "./pages/Dormitory/Blocks";
import CreateBlock from "./pages/Dormitory/CreateBlock";
import EditBlock from "./pages/Dormitory/EditBlock";
import Floors from "./pages/Dormitory/floors";
import CreateFloor from "./pages/Dormitory/CreateFloor";
import EditFloor from "./pages/Dormitory/EditFloor";
import Rooms from "./pages/Dormitory/Rooms";
import CreateRoom from "./pages/Dormitory/CreateRoom";
import EditRoom from "./pages/Dormitory/EditRoom";
import Beds from "./pages/Dormitory/Beds";
import CreateBed from "./pages/Dormitory/CreateBed";
import EditBed from "./pages/Dormitory/EditBed";

function App() {
  return (
    <BrowserRouter>
    <AuthProvider>
        <AppRoutes />
         <Routes>
                <Route path="/" element={<Dormitory />} />

                <Route path="/dormitories" element={<DormitoryList />} />

                <Route
                    path="/dormitories/create"
                    element={<CreateDormitory />}
                />

                <Route
                    path="/dormitories/edit/:id"
                    element={<EditDormitory />}
                />

                <Route path="/blocks" element={<Blocks />} />
                <Route
                    path="/blocks/create"
                    element={<CreateBlock />}
                />

                <Route
                    path="/blocks/edit/:id"
                    element={<EditBlock />}
                />
                <Route path="/floors" element={<Floors />} />
                <Route
                    path="/floors/create"
                    element={<CreateFloor />}
                />
                <Route
                    path="/floors/edit/:id"
                    element={<EditFloor />}
                />
                <Route path="/rooms" element={<Rooms />} />
                <Route
                    path="/rooms/create"
                    element={<CreateRoom />}
                />
                <Route
                    path="/rooms/edit/:id"
                    element={<EditRoom />}
                />
                <Route path="/beds" element={<Beds />} />
                <Route
                    path="/beds/create"
                    element={<CreateBed />}
                />

                <Route
                    path="/beds/edit/:id"
                    element={<EditBed />}
                />
            </Routes>
        </AuthProvider>
        </BrowserRouter>
  );
}

export default App;