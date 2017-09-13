using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UsersDiosna.Alarms.Models
{
    public class AlarmFormModel
    {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "PageNumber")]
            public int PageNumber { get; set; }

            public int someId { get; set; }
            public int NumberOfRecords { get; set; }
    }

    public class AlarmViewModel {
        
        public string Id { get; set; }
        public string Label { get; set; }
        public string originTime { get; set; }
        public string expTime { get; set; }
        
    }

    public class AlarmFilter
    {
        public List<int> alarms { get; set; }
    }

    public class AlarmGraphConfig
    {
        public List<string> EN { get; set; }
        public List<string> CZ { get; set; }
        public List<string> DE { get; set; }
        public List<string> PL { get; set; }
    }
    public class AlarmGraphData
    {
        public short id { get; set; }
        public string title { get; set; }

        public int originTime { get; set; } // in pktime
        public int expiryTime { get; set; } // in pkTime
    }
}