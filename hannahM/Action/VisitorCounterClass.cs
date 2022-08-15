using Microsoft.AspNetCore.Mvc;

using System.Web;
using hannahM.Data;
using hannahM.Models;

namespace hannahM.Action
{
    public static class VisitorCounterClass
    {
        public static void AddVisitor(int id, string category, ApplicationDbContext _db)
        {

            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] ipAddressWithText = response.Split(':');
            string ipAddressWithHTMLEnd = ipAddressWithText[1].Substring(1);
            string[] ipAddress = ipAddressWithHTMLEnd.Split('<');
            string mainIP = ipAddress[0];

            var getCurrentIp = _db.Visitor.Where(x => x.Id == id).Select(all => all.visitorIp).FirstOrDefault();

            if (getCurrentIp == mainIP)
            {
            }
            else
            {
                Visitor v = new Visitor();
                v.postId = id;
                v.Point = category;
                v.visitorIp = mainIP; 
                _db.Visitor.Add(v);
                _db.SaveChanges();
            }
        }
    }
}
