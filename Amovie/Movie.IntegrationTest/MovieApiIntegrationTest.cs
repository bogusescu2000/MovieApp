using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Movie.IntegrationTest
{
    public class MovieApiIntegrationTest
    {
        [Fact]
        public async Task Get_Returns_Ok_StatusCode()
        {
            var client = new TestClientProvider().Client;

            var response = await client.GetAsync("/api/Movie/allmovies");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}