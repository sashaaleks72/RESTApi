namespace Net7.WebApi.Test.Models
{
    public class Teapot
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string ImgUrl { get; set; } = string.Empty;

        public string ManufacturerCountry { get; set; } = string.Empty;

        public double Capacity { get; set; }

        public int WarrantyInMonths { get; set; }
    }
}
