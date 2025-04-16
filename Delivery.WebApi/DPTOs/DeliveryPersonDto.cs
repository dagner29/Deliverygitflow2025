namespace Delivery.WebApi.DPTOs
{
    public class DeliveryPersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ActiveDeliveriesCount { get; set; }
    }
}
