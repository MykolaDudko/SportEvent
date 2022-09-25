using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator.Models
{
    public class EventModel
    {
        public int ProviderEventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public List<OddsList> OddsList { get; set; }
    }
}
