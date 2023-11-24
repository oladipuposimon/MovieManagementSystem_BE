using Microsoft.AspNetCore.Http;
using MoviesManagement.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Common.Utilities
{
    public class ResponseHandler
    {
        public ResponseModel Success(string? message = null, object? data = null, int? statusCode = null)
        {
            return new ResponseModel()
            {
                isSuccessful = true,
                Message = message ?? "Request was Successful",
                StatusCode = statusCode ?? StatusCodes.Status200OK,
                Data = data
            };
        }

        public ResponseModel Failure(string? message = null, int? statusCode = null)
        {
            return new ResponseModel()
            {
                isSuccessful = false,
                Message = message ?? "Request was not completed",
                StatusCode = statusCode ?? StatusCodes.Status400BadRequest
            };
        }

        

        public ResponseModel InternalServerError()
        {
            return new ResponseModel()
            {
                isSuccessful = false,
                Message = "Something went wrong!",
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
