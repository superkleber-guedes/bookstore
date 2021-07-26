using bookstore.Integration;
using Kleber.Bookstore;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.API.ComponentTests.Setup
{
    public class ApiClientFixture : WebAppFactory<Startup>
    {
        private readonly IBookRepository _mockedBookRepository;
        private readonly HttpClient _httpClient;

        public HttpResponseMessage LastResponse { get; protected set; }

        //this is not susteinable long term and should be replace by abstract classes and injection on tests
        public ApiClientFixture(IBookRepository mockedBookRepository) : base(mockedBookRepository)
        {
            _mockedBookRepository = mockedBookRepository;
            _httpClient = CreateClient();
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url)
        {
            LastResponse = null;

            HttpRequestMessage msg = new HttpRequestMessage(method, url);

            LastResponse = await _httpClient.SendAsync(msg);

            return LastResponse;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, string bodyJson)
        {
            LastResponse = null;

            HttpRequestMessage msg = new HttpRequestMessage(method, url);

            var content =  new ByteArrayContent(Encoding.UTF8.GetBytes(bodyJson));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            msg.Content = content;

            LastResponse = await _httpClient.SendAsync(msg);

            return LastResponse;
        }
    }
}
