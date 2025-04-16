namespace Delivery.WebApi.DPTOs
{
    public class DeliveryDto
    {
   
        public Guid Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Address { get; set; }
        public string AssignedPerson { get; set; }
        public List<PackageDto> Packages { get; set; }

    }

}
