using System;
using System.Net;

namespace Backend.Exceptions;
public static class CustomExceptions
{
    public abstract class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected ApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class UserUnauthorizedException : ApiException
    {
        public UserUnauthorizedException() : base("You are not authorized", HttpStatusCode.Unauthorized) { }
    }

    public class InvalidAuthentication : ApiException
    {
        public InvalidAuthentication() : base("The token is invalid", HttpStatusCode.Unauthorized) { }
    }

    public class InvalidPasswordException : ApiException
    {
        public InvalidPasswordException() : base("The password is invalid", HttpStatusCode.Unauthorized) { }
    }

    public class CompanyNotFoundException : ApiException
    {
        public CompanyNotFoundException() : base("Company not found", HttpStatusCode.NotFound) { }
    }

    public class UserNotFoundException : ApiException
    {
        public UserNotFoundException() : base("User not found", HttpStatusCode.NotFound) { }
    }
    
    public class FlightNotFoundException : ApiException
    {
        public FlightNotFoundException() : base("Flight not found", HttpStatusCode.NotFound) { }
    }

    public class BookingNotFoundException : ApiException
    {
        public BookingNotFoundException() : base("Booking not found", HttpStatusCode.NotFound) { }
    }

    public class UserAlreadyExistsException : ApiException
    {
        public UserAlreadyExistsException() : base("The user is already registered", HttpStatusCode.BadRequest) { }
    }

    public class EmailAlreadyRegisteredException : ApiException
    {
        public EmailAlreadyRegisteredException() : base("The email is already registered", HttpStatusCode.BadRequest) { }
    }

    public class InvalidCompanyRequestException : ApiException
    {
        public InvalidCompanyRequestException() : base("Invalid company registration request", HttpStatusCode.BadRequest) { }
    }

    public class LinkageNotFoundException : ApiException
    {
        public LinkageNotFoundException() : base("The request was not found", HttpStatusCode.BadRequest) { }
    }
}