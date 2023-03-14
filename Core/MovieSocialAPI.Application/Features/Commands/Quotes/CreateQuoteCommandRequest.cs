using MediatR;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Commands.Quotes
{

    public class CreateQuoteCommandRequest : IRequest<CreateQuoteCommandResponse>
    {
        public string MovieId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string ImagePath { get; set; }

    }
    public class CreateQuoteCommandResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
    public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommandRequest, CreateQuoteCommandResponse>
    {
        readonly IQuoteWriteRepository _quoteWriteRepository;

        public CreateQuoteCommandHandler(IQuoteWriteRepository quoteWriteRepository)
        {
            _quoteWriteRepository = quoteWriteRepository;
        }

        public async Task<CreateQuoteCommandResponse> Handle(CreateQuoteCommandRequest request, CancellationToken cancellationToken)
        {

            Quote quote = new Quote
            {
                MovieId = request.MovieId,
                Description = request.Description,
                UserId = request.UserId,
                ImagePath = request.ImagePath
            };
            await _quoteWriteRepository.AddAsync(quote);
            
            int result = await _quoteWriteRepository.SaveAsync();
            int id = quote.Id;
            if (result==1)
            {
                CreateQuoteCommandResponse response = new() { Message = "Alıntı paylaşılması başarılı!",Id=id };
                return response;
            }
            
            
            return new();
        }
    }
}
