namespace Models
{
    using System;

    public class UserScore
    {
        public string Username { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Seconds => (EndTime - StartTime).Seconds;
    }
}
