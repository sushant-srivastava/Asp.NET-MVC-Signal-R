using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApplication5.Models;
using System.Web.Script.Serialization;

namespace WebApplication5.Hubs
{
    public class User
    {
        public string UserName { get; set; }
        public int LevelId { get; set; }
    }
    public class LeaderBoardHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void RefreshLeaderBoard()
        {
            var users = db.Users.OrderByDescending(x => x.LevelId).ThenBy(x => x.Updated);
            var leaderBoard = new List<User>();
            foreach (var user in users)
                leaderBoard.Add(new User() { UserName = user.UserName, LevelId = user.LevelId });
            
            var jsonSerialiser = new  JavaScriptSerializer();
            Clients.All.sendTopUsers(jsonSerialiser.Serialize(leaderBoard));
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