namespace Delivery.WebApi.DPTOs
{
    public class PackageDto
    {

        public Guid deliveryId { get; set; }//eliminar 
        public string ContentDescription { get; set; }
        public Double Weight { get; set; }

    }
}
