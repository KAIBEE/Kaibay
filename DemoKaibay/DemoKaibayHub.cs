using DemoKaibay.Dtos;
using DemoKaibay.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace DemoKaibay
{
    
    public class DemoKaibayHub : Hub
    {
        private readonly IDemoKaibayService kaibayService;
        private readonly UserManager<IdentityUser> user;

        public DemoKaibayHub(IDemoKaibayService kaibayService, UserManager<IdentityUser> user)
        {
            this.kaibayService=kaibayService;
            this.user=user;
        }

        [Authorize]
        [HubMethodName("AddToGroup")]
        public async Task AddToGroup(int groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
        }

        [Authorize]
        [HubMethodName("RemoveFromGroup")]
        public async Task RemoveFromGroup(int groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());
        }

        [Authorize]
        [HubMethodName("BidRequest")]
        public async Task BidRequest(int auctionId, double priceBid)
        {
            var bid = new BidDto
            {
                Price = priceBid,
                AuctionId = auctionId,
                BuyerId = user.GetUserId(Context.User),
                BuyerName = user.GetUserName(Context.User)
            };

            try
            {
                await kaibayService.PostBid(bid);

                await Clients.Group(auctionId.ToString()).SendAsync("UpdatePriceForGroup", auctionId, priceBid);
                await Clients.OthersInGroup(auctionId.ToString()).SendAsync("UpdateNotification", priceBid);

                await Clients.All.SendAsync("UpdatePrice", auctionId, priceBid);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Reject", auctionId, ex.Message);
            }
        }
    }
}
