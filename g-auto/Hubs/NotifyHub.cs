using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using g_auto.DAL;
using g_auto.Models;
using Microsoft.AspNet.SignalR;

namespace g_auto.Hubs
{
    public class NotifyHub : Hub
    {
        DBContext db = new DBContext();

        public override Task OnConnected()
        {
            if (Context.Request.Cookies.TryGetValue("hbsgnlrver", out Cookie nsns))
            {
                int AdminId = Convert.ToInt32(Context.Request.Cookies["hbsgnlrver"].Value);

                Admin xdb = db.Admin.Find(AdminId);
                xdb.ConnectionId = Context.ConnectionId;
                db.SaveChanges();
            }

           

            return base.OnConnected();
           
        }

    }
}