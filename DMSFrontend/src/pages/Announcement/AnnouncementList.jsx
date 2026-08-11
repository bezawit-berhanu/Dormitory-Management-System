import { useEffect, useState } from "react";
import { getAnnouncements } from "../../services/announcementService";

function AnnouncementList() {
    const [announcements, setAnnouncements] = useState([]);

    useEffect(() => {
        loadAnnouncements();
    }, []);

    const loadAnnouncements = async () => {
        try {
            const data = await getAnnouncements();
            console.log("API DATA:", data);
            setAnnouncements(data);
        } catch (error) {
            console.error("Error",error);
        }
    };

    return (
        <div>
            <h2>Announcements</h2>

            {announcements.map(item => (
                <div key={item.announcementId}>
                    <h3>{item.title}</h3>
                    <p>{item.message}</p>
                </div>
            ))}
        </div>
    );
}

export default AnnouncementList;