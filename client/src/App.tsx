import { List, ListItem, ListItemText, Typography } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";

function App() {
  //hook useState
  const [activities, setActivities] = useState<Activity[]>([]);
  //hook useEffect
  useEffect(() => {
    //fetch data from api
    axios("https://localhost:50062/api/activities")
      .then((response) => setActivities(response.data))
      .catch((error) => console.error("Error fetching activities:", error));
  }, []) //empty array means it will only run once when the component mounts
  
  return (
      <>
        <Typography variant="h3">Reactivities</Typography>
        <List>
          {activities.map((activity) => (
            <ListItem key={activity.id}>
              <ListItemText primary={activity.title} />
            </ListItem>
          ))}
        </List>
      </>
  )
}

export default App
