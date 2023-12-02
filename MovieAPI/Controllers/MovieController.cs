using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Data;
using MovieAPI.Models;
using MovieAPI.Models.Dto;
using MovieAPI.ViewModels;
using System;
using System.Globalization;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MainDbContext dbContext;
        private ResponseDto _res;
        CultureInfo indoKultur = CultureInfo.GetCultureInfo("id-ID");

        public MovieController(MainDbContext _dbc)
        {
            dbContext = _dbc;
            _res = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                _res.Result = dbContext.Movies.Select(x => new MovieViewModel() {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Rating = x.Rating.ToString("F2", indoKultur),
                    Image = x.Image,
                    CreatedDate = x.CreatedDate.HasValue ? x.CreatedDate.Value.ToString("dd-MM-yyyy") : "",
                    UpdatedDate = x.UpdatedDate.HasValue ? x.UpdatedDate.Value.ToString("dd-MM-yyyy") : "",

                }).ToList();
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = $"Get All Movies Failed. Error: {e.Message}. Stack Trace: {e.StackTrace}";
            }

            return _res;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var obj = dbContext.Movies.Select(x => new MovieViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Rating = x.Rating.ToString("F2", indoKultur),
                    Image = x.Image,
                    CreatedDate = x.CreatedDate.HasValue ? x.CreatedDate.Value.ToString("dd-MM-yyyy") : "",
                    UpdatedDate = x.UpdatedDate.HasValue ? x.UpdatedDate.Value.ToString("dd-MM-yyyy") : "",

                }).FirstOrDefault(x => x.Id == id);

                _res.Result = obj;
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = $"Select Movie Failed. Error: {e.Message}. Stack Trace: {e.StackTrace}";
            }

            return _res;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] MovieAddViewModel mvm)
        {
            try
            {
                Movie movie = new Movie();
                //movie.Id = 0;
                movie.Title = mvm.Title ?? "";
                movie.Description = mvm.Description;
                movie.Rating = mvm.Rating;
                movie.Image = mvm.Image;
                movie.CreatedDate = DateTime.UtcNow;
                dbContext.Movies.Add(movie);
                dbContext.SaveChanges();

                _res.Result = mvm;
                _res.Message = "Insert Movie Success";
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = $"Insert Movie Failed. Error: {e.Message}. Stack Trace: {e.StackTrace}";
            }

            return _res;
        }

        [HttpPatch]
        [Route("{id:int}")]
        public ResponseDto Update(int id, [FromBody] MovieAddViewModel mvm)
        {
            try
            {
                Movie movie = dbContext.Movies.FirstOrDefault(x => x.Id == id);
                if (movie != null)
                {
                    movie.Title = mvm.Title ?? "";
                    movie.Description = mvm.Description;
                    movie.Rating = mvm.Rating;
                    movie.Image = mvm.Image;
                    movie.UpdatedDate = DateTime.UtcNow;

                    dbContext.Movies.Update(movie);
                    dbContext.SaveChanges();

                    _res.Result = mvm;
                    _res.Message = "Update Movie Success";
                }
                else
                {
                    _res.IsSuccess = false;
                    _res.Message = "Data Not Found!";
                }
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = $"Update Movie Failed. Error: {e.Message}. Stack Trace: {e.StackTrace}";
            }
            return _res;
        }

        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                Movie movie = dbContext.Movies.FirstOrDefault(x => x.Id == id);
                if (movie != null)
                {
                    dbContext.Movies.Remove(movie);
                    dbContext.SaveChanges();

                    _res.Result = movie;
                    _res.Message = "Delete Movie Success";
                }
                else
                {
                    _res.IsSuccess = false;
                    _res.Message = "Data Not Found!";
                }
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = $"Delete Movie Failed. Error: {e.Message}. Stack Trace: {e.StackTrace}";
            }
            return _res;
        }


    }
}
