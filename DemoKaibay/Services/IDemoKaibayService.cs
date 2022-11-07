using DemoKaibay.Dtos;
using System.Security.Claims;

namespace DemoKaibay.Services
{
    public interface IDemoKaibayService
    {
        Task CreateAuction(CreateAuctionItemDto value, ClaimsPrincipal claimUser);
        Task<IList<AuctionItemDto>> GetActiveAuctions();
        Task<AuctionDetailsDto> GetAuctionDetails(int id);
        Task PostBid(BidDto bid);
    }
}