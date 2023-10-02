using System.Collections.Generic;
public class User 
{
    public string UserName;
    public Dictionary<string,int> CategoryProgress { get; set; }
    public int HighScore;
    public string DeviceId;
    public User(){}
    private User(string userName, Dictionary<string, int> categoryProgress, int score)
    {
        UserName = userName;
        CategoryProgress = categoryProgress;
        HighScore = score;
    }
}