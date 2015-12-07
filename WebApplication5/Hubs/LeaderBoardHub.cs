using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApplication5.Models;

namespace WebApplication5.Hubs
{
    public class LeaderBoardHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void RefreshLeaderBoard()
        {
            var users = db.Users.OrderByDescending(x => x.LevelId).Take(5);
            var leaderBoard = string.Empty;
            foreach (var user in users)
                leaderBoard += string.Format("User {0}: Level {1}", user.UserName, user.LevelId);
            Clients.All.sendTopUsers(leaderBoard);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}