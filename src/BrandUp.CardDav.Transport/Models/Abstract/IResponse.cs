﻿namespace BrandUp.CardDav.Transport.Models.Abstract
{
    public interface IResponse
    {
        bool IsSuccess { get; init; }
        string StatusCode { get; init; }
        IResponseBody Content { get; }

        static abstract IResponse Create(HttpResponseMessage message);
    }
}
