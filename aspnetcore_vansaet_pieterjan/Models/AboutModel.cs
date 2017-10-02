using System;

namespace aspnetcore_vansaet_pieterjan.Models
{
    public class AboutModel
    {
        public double DaysUntillBirthday { get; set; }

        public DateTime Now => DateTime.Now;
    }
}