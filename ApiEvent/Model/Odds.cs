using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Model
{
    public class Odds
    {
        public int Id { get; set; }
        public int ProviderOddsID { get; set; }
        public int ProviderEventID { get; set; }
        public string OddsName { get; set; }
        public double OddsRate { get; set; }
        public string Status { get; set; }
    }
}
