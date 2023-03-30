using Microsoft.AspNetCore.SignalR;
using PNP.ViewModels;

namespace PNP.Hubs
{
    public class StatisticsHub : Hub
    {
        public async Task SendUpdates(StatisticsVM json)
        {
            await Clients.All.SendAsync("ReceiveUpdates", json);
        }
    }
}
