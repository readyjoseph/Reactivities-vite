using System;

namespace Domain;

public class Activity //Relates to the Activity class in the API project. This is a model class that represents an activity in the system.
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Unique identifier for the activity. Default is a new GUID.
    public required string Title { get; set; }
    public DateTime Date { get; set; }
    public required string Description { get; set; } // Description of the activity.
    public required string Category { get; set; }
    public bool IsCancelled { get; set; }
    //location props
    public required string City { get; set; }
    public required string Venue { get; set; } // Venue of the activity.
    public double Latitude { get; set; } // Latitude of the activity location.
    public double Longitude { get; set; } // Longitude of the activity location.
}
