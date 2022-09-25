using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class Event
    {
        public int Id { get; set; }
        public int ProviderEventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        
    }
}
