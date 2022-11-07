using System.ComponentModel.DataAnnotations;

namespace DemoKaibay.Dtos
{
    public class AuctionItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public double InitialPrice { get; set; }
        public double? CurrentPrice { get; set; }
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm:ss}")]
        public DateTime AuctionEnded { get; set; }
    }
}
