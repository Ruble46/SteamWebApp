public class User
{
    public string? steamid { get; set; }
    public int? communityvisibilitystate { get; set; }
    public int? profilestate { get; set; }
    public string? personaname { get; set; }
    public string? profileurl { get; set; }
    public string? avatar { get; set; }
    public string? avatarmedium { get; set; }
    public string? avatarfull { get; set; }
    public string? avatarhash { get; set; }
    public double? lastlogoff { get; set; }
    public int personastate { get; set; }
    public string? primaryclanid { get; set; }
    public double? timecreated { get; set; }
    public int? personastateflags { get; set; }
}

public class UserResponse
{
    public List<User>? players { get; set; }
}