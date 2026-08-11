import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { getNotifications } from "../../services/notificationService";
function Notification({ notification }) {
    if (!notification) {
        return null;
    }

    return (
        <div>
            <h4>{notification.title}</h4>
            <p>{notification.message}</p>
        </div>
    );
}

export default Notification;