namespace Delivery.WebApi.DPTOs
{
    public class CreateDeliveryPersonDto
    {
        public string Name { get; set; }
        public int DeliveryId { get; set; } // ID de la entrega donde se asignará
    }
}
