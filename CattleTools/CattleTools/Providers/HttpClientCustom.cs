using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CattleTools
{
    class HttpClientCustom
    {

        private static CookieContainer _cookieJar;
        private static CookieContainer cookieJar { get { return _cookieJar = _cookieJar ?? new CookieContainer(); } }
        private static HttpClientHandler _clientHandler;
        private static HttpClientHandler clientHandler { get { return _clientHandler = _clientHandler ?? new HttpClientHandler() { CookieContainer = cookieJar }; } }
        private static HttpClient _httpClient;
        private static HttpClient httpClient { get { return _httpClient = _httpClient ?? new HttpClient(clientHandler); } set { _httpClient = value; } }

        public static void reset()
        {
            _cookieJar = new CookieContainer();
            _clientHandler = new HttpClientHandler { CookieContainer = _cookieJar };
            _httpClient = new HttpClient(_clientHandler);
        }

        

        public static HttpResponseMessage MakeRequest(HttpMethod method, Uri uri, IRequestModel content = null, CancellationTokenSource cts = null)
        {
            try
            {
                httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(method, uri);
                if (content != null)
                {
                    request.Content = new StringContent(content.ToJson(), Encoding.Default, "application/json");
                    var contentString = request.Content.ReadAsStringAsync();
                }
                if (cts != null)
                {
                    return httpClient.SendAsync(request, cts.Token).Result;
                }
                else
                {
                    return httpClient.SendAsync(request).Result;
                }
            }
            catch (HttpRequestException hre)
            {
                throw;
            }
            catch (AggregateException ae)
            {
                throw;
            }
            catch (Exception ex)
            {
                reset();
                throw;
            }
            finally
            {
                httpClient = null;
            }
        }

    }
}
