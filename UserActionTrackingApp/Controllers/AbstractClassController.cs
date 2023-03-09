using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserActionTrackingApp.Controllers
{
    public class AbstractClassController : Controller
    {

        private static string cookieCount = "getCookies";


        private static string sessionsCount = "getSessions";

        public string TrackingMessage(string namePage)
        {
            int visits = IncrementVisits(namePage);
            int sessionVisits = IncrementCookies(namePage);
            return $"Session Visits : {sessionVisits} | Number of Sessions: {sessionVisits}"; 
        }

        private int IncrementCookies(string pageName )
        {
            string sessionKey = sessionsCount + pageName;
            int pageCount = HttpContext.Session.GetInt32(sessionKey) ?? 0;
            pageCount++;
            HttpContext.Session.SetInt32(sessionKey , pageCount);
            return pageCount;

        }
        private int IncrementVisits(string namePage)
        {
           
            string visits;
            if (Request.Cookies.ContainsKey(cookieCount))
            {
                visits = Request.Cookies[cookieCount];  
            }
            else
            {
                visits = "{}";
            }
            Dictionary<string, int> visitCounts = JsonConvert.DeserializeObject<Dictionary<string, int>>(visits);

            visitCounts.TryGetValue(namePage, out int current);
            current++;
            visitCounts[namePage] = current;

            visits = JsonConvert.SerializeObject(visitCounts);

            CookieOptions cookieOptions = new CookieOptions() { Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append(cookieCount, visits, cookieOptions);



            return current;
        }

        
    }

}
