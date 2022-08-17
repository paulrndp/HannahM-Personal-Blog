using hannahM.Data;
using hannahM.Models;

namespace hannahM.Action
{
    public class VisitorCounterClass
    {
        public static void AddVisitor(int id, string? category, string mainIP, ApplicationDbContext _db)
        {
            var getCurrentIp = _db.Visitor.Where(x => x.postId == id && x.Point == category).Select(all => all.visitorIp).FirstOrDefault();
            //120.29.86.100 != 120.29.86.100
            if (getCurrentIp != mainIP)
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
