// ****************************************
// Alan Wright
// 7/17/14

using System.Text;

namespace NetflixRouletteSharp
{
    public enum RequestType
    {
        Title, TitleYear, Director, Actor
    }

    public class RouletteRequest
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string Director { get; set; }

        public string Actor { get; set; }

        public RequestType ReqType { get; set; }

        /// <summary>
        ///     Returns a formatted API URL containing the request information.
        /// </summary>
        /// <value>The formatted API URL.</value>
        /// <exception cref="T:NetflixRouletteSharp.RouletteRequestException">The request title cannot be null or white space</exception>
        public string ApiUrl
        {
            get
            {
                switch (ReqType)
                {
                    case RequestType.Title:
                        return new StringBuilder(NetflixRoulette.API_URL).AppendFormat("title={0}", Title.Replace(" ", "%20")).ToString();
                    case RequestType.TitleYear:
                        return new StringBuilder(NetflixRoulette.API_URL).AppendFormat("title={0}&year={1}", Title.Replace(" ", "%20"), Year).ToString();
                    case RequestType.Actor:
                        return new StringBuilder(NetflixRoulette.API_URL).AppendFormat("actor={0}", Actor.Replace(" ", "%20"), Year).ToString();
                    case RequestType.Director:
                        return new StringBuilder(NetflixRoulette.API_URL).AppendFormat("director={0}", Director.Replace(" ", "%20"), Year).ToString();
                }
                return null;
            }
        }
    }
}
