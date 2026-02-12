using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.MovieAPI.Helpers;
using MovieWatchlist.MovieAPI.Models.DTOs;
using MovieWatchlist.MovieAPI.Services.Interface;

namespace MovieWatchlist.MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;
        private readonly JwtHelper _jwtHelper;

        public MovieController(IMovieService service, JwtHelper jwtHelper)
        {
            _service = service;
            _jwtHelper = jwtHelper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login dto)
        {
            // Hardcoded username/password
            if (dto.Username != "admin" || dto.Password != "password")
                return Unauthorized(new { message = "Invalid credentials" });

            // Hardcoded userId = "1"
            var token = _jwtHelper.GenerateToken("1", dto.Username);
            return Ok(new { token });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = _service.GetAll();
            return Ok(movies);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var movie = _service.GetById(id);

            if (movie == null)
                return NotFound(new { message = "Not Found!" });

            return Ok(movie);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreateMovie dto)
        {
            var created = _service.CreateMovie(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}/rating")]
        public IActionResult UpdateRating(int id, [FromBody] UpdateMovieRating dto)
        {
            try
            {
                _service.UpdateRating(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("{id}/update")]
        public IActionResult UpdateMovie(int id, [FromBody] CreateMovie dto)
        {
            try
            {
                _service.UpdateMovie(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.DeleteMovie(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
