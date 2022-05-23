using Amovie.Controllers;
using Behaviour.Interfaces;
using Entities.Models.MovieDto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MovieTest.Test
{
    public class MovieControllerTest
    {

        private readonly MovieController _controller;

        public MovieControllerTest()
        {
            var mockMovieService = new Mock<IMovieService>();

            List<MoviesDto> movies = new List<MoviesDto>() { new MoviesDto(), new MoviesDto(), new MoviesDto(), new MoviesDto()};
            mockMovieService.Setup(x => x.GetAll()).ReturnsAsync(new List<MoviesDto>(movies));

            
            //_controller = new MovieController(mockMovieService.Object);

        }

        [Fact]
        public async Task Get_Returns_Correct_Number_Of_Movies()
        {
            //arrange

            //act
            var result = await _controller.Get();
            var movies = result.Value as List<MoviesDto>;

            //assert
            Assert.Equal(4, movies.Count);
        }

        //[Fact]
        //public async Task GetMovieById_Returns_Ok_StatusCode()
        //{
        //    var movieId = 2;

        //    var movie = await _controller.GetMovie(movieId) as ObjectResult;
            
        //    Assert.Equal((int)HttpStatusCode.OK, movie.StatusCode);
        //}

        //[Fact]
        //public async Task GetMovieByInexistentId_Returns_BadRequest()
        //{
        //    var movieId = -2;

        //    var movie = await _controller.GetMovie(movieId) as ObjectResult;

        //    Assert.Equal((int)HttpStatusCode.BadRequest, movie.StatusCode);
        //}
    }
}