using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace WorkshopApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class WatchListController : ControllerBase
    {
        private static List<string> movies = new List<string>();

        [HttpGet(Name = "GetWatchList")]
        public IEnumerable<string> Get()
        {
            return movies;
        }
        [HttpPost(Name = "AddMovie")]
        public bool Post(string movieName) 
        {
            movies.Add(movieName);
            return true;
        }
        [HttpPut(Name = "UpdateMovie")]
        public bool Put(string oldMovieName, string newMovieName)
        {
            int index =movies.IndexOf(oldMovieName);
            if (index >= 0)
            {
                movies[index] = newMovieName;
            }
            return true;
        }
        [HttpDelete(Name = "DeleteMovie")]
        public bool Delete(string movieName)
        {   
            return movies.Remove(movieName);
        }

    }
}
