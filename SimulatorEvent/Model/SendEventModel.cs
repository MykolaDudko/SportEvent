using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorEvent.Model
{
    public class SendEventModel
    {
        public int ProviderEventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int ProviderOddsID { get; set; }
        public string OddsName { get; set; }
        public double OddsRate { get; set; }
        public string Status { get; set; }
    }
}
