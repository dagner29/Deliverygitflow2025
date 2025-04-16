using System.ComponentModel.DataAnnotations;

namespace Delivery.WebApi.DPTOs
{
    public class CreateDeliveryRequestDto
    {
        [Required]
        public DateTime ScheduledDate { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"^\d{5,10}$", ErrorMessage = "El código postal debe contener entre 5 y 10 dígitos.")]
        public string PostalCode { get; set; }
        [Required]
        public int DeliveryRouteId { get; set; }
        [Required]
        public DateTime FechaEntrega { get; set; }
        

        //public int DeliveryRouteId { get; set; }
        /*

        public string Status { get; set; }

        // Lista de paradas de la ruta como parte del DTO
        public List<AddressDto> Stops { get; set; }

        public string OptimalPath { get; set; }

        // Lista de identificadores de paquetes
        public List<Guid> PackageIds { get; set; }

        */
    }


    // Address DTO para representar las paradas de la ruta
    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

}
