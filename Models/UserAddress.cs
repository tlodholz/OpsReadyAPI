using System.ComponentModel.DataAnnotations.Schema;

namespace OpsReady.Models
{
    [Table("OpsReady_UserAddress")]
    public class UserAddress
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public bool IsPrimary { get; set; }
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
