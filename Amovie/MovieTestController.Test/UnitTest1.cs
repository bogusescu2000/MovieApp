using Amovie.Controllers;
using Behaviour.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MovieTest.Test
{
    public class MovieControllerTest
    {

        Mock<IMovieService> movieService = new Mock<IMovieService>();

        private readonly MovieController _controller;

        public MovieControllerTest()
        {
            var mockMovieService = new Mock<IMovieService>();

            List<LastMovieDto> movies = new List<LastMovieDto>() { new LastMovieDto(), new LastMovieDto(), new LastMovieDto(), new LastMovieDto() };
            mockMovieService.Setup(x => x.GetAll()).ReturnsAsync(new List<LastMovieDto>(movies));

            _controller = new MovieController(mockMovieService.Object);

        }

        [Fact]
        public async Task Get_Returns_Correct_Number_Of_Movies()
        {
            //arrange

            //act
            var result = await _controller.Get();
            var movies = result.Value as List<LastMovieDto>;

            //assert
            Assert.Equal(4, movies.Count);
        }

        [Fact]
        public async Task GetMovieById_Returns_Ok_StatusCode()
        {

            var movieId = 2;

            var movie = _controller.GetMovie(movieId) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, movie.StatusCode);
        }
    }
}