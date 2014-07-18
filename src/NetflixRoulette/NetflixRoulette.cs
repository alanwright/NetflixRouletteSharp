//Alan Wright
// 7/17/14

using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;

namespace NetflixRouletteSharp
{
    /// <summary>
    ///     Class NetflixRoulette.
    /// </summary>
    public class NetflixRoulette
    {
        /// <summary>
        ///     The NetflixRoulette API URL;
        /// </summary>
        public const string API_URL = "http://netflixroulette.net/api/api.php?";

        /// <summary>
        ///     Creates a request with the specified <paramref name="title" /> value.
        /// </summary>
        /// <param name="title">The request title.</param>
        /// <returns>RouletteResponse</returns>
        public static RouletteResponse TitleRequest(string title)
        {
            return CreateRequest(new RouletteRequest
            {
                ReqType = RequestType.Title,
                Title = title
            });
        }

        /// <summary>
        ///     Creates a request with the specified <paramref name="title" /> and <paramref name="year" /> values.
        /// </summary>
        /// <param name="title">The request title.</param>
        /// <param name="year">The year.</param>
        /// <returns>RouletteResponse</returns>
        public static RouletteResponse TitleAndYearRequest(string title, int year)
        {
            return CreateRequest(new RouletteRequest
            {
                ReqType = RequestType.TitleYear,
                Title = title,
                Year = year
            });
        }

        public static List<RouletteResponse> ActorRequest(string actor)
        {
            return CreateListRequest(new RouletteRequest
            {
                ReqType = RequestType.Actor,
                Actor = actor
            });
        }

        public static List<RouletteResponse> DirectorRequest(string director)
        {
            return CreateListRequest(new RouletteRequest
            {
                ReqType = RequestType.Director,
                Director = director
            });
        }

        /// <summary>
        ///     Creates a request with the values specified in the <paramref name="requestData" /> object.
        /// </summary>
        /// <param name="requestData">The request data.</param>
        /// <returns>RouletteResponse</returns>
        /// <exception cref="T:NetflixRoulette.RouletteRequestException">
        ///     The HTTP request returned a Bad Request status code
        ///     or
        ///     An Exception was caught while creating the HTTP request.
        /// </exception>
        public static RouletteResponse CreateRequest(RouletteRequest requestData)
        {
            try
            {
                var httpWebReq = (HttpWebRequest) WebRequest.Create(requestData.ApiUrl);
                using (var httpWebResp = (HttpWebResponse) httpWebReq.GetResponse())
                {
                    if (httpWebResp.StatusCode == HttpStatusCode.OK)
                    {
                        return (RouletteResponse) new DataContractJsonSerializer(typeof(RouletteResponse)).ReadObject(httpWebResp.GetResponseStream());
                    }

                    throw new RouletteRequestException("Unexpected HTTP Status Code ({0}: {1})", httpWebResp.StatusCode, httpWebResp.StatusDescription);
                }
            }
            catch (Exception exception)
            {
                throw new RouletteRequestException("{0} {1}", exception.Message, requestData.ToString());
            }
        }

        public static List<RouletteResponse> CreateListRequest(RouletteRequest requestData)
        {
            try
            {
                var httpWebReq = (HttpWebRequest) WebRequest.Create(requestData.ApiUrl);
                using (var httpWebResp = (HttpWebResponse) httpWebReq.GetResponse())
                {
                    if (httpWebResp.StatusCode == HttpStatusCode.OK)
                    {
                        return (List<RouletteResponse>)new DataContractJsonSerializer(typeof(List<RouletteResponse>)).ReadObject(httpWebResp.GetResponseStream());
                    }

                    throw new RouletteRequestException("Unexpected HTTP Status Code ({0}: {1})", httpWebResp.StatusCode, httpWebResp.StatusDescription);
                }
            }
            catch (Exception exception)
            {
                throw new RouletteRequestException("{0} {1}", exception.Message, requestData.ToString());
            }
        }
    }
}
