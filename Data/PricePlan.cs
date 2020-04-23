using System.Collections.Generic;

namespace DGMLD3.Data
{
    public class PricePlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ChargeAmount { get; set; }
        public string BillingPer { get; set; }
        public string Features { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
