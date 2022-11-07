using System.ComponentModel.DataAnnotations;

namespace DemoKaibay.Dtos
{
    public class AuctionDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public double InitialPrice { get; set; }
        public double? CurrentPrice { get; set; }
        public double FinalPrice { get; set; }
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm:ss}")]
        public DateTime AuctionEnded { get; set; }
        public bool IsEnded { get; set; }
        public ICollection<BidDto> Bids { get; set; }

    }
}
