namespace ChessGame
{
    public class Utility
    {
        public string connString;
        public string[] pNames = new string[2];
        public int timeControl = 600; // 10 minutes by default

        public static string FormatTime(int seconds)
        {
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60; 
            string formattedTime = $"{minutes}:{remainingSeconds:D2}";
            return formattedTime;
        }

    }
}
