namespace Delivery.WebApi.DPTOs
{
    public class AddPackageRequestDto
    {
        public Guid PackageId { get; set; }
        public string Address { get; set; }
        public float Weight { get; set; }
    }
}
