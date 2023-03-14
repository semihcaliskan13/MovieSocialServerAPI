using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSocialAPI.Application.Abstractions.Storage;
using MovieSocialAPI.Application.Features.Commands.Quotes;
using MovieSocialAPI.Application.Features.Queries.GetAllQuotes;
using MovieSocialAPI.Application.Features.Queries.GetQuotesBuUserId;
using MovieSocialAPI.Application.Features.Queries.GetQuotesById;
using MovieSocialAPI.Application.Features.Queries.Quotes.GetQuotesByMovieId;

namespace MovieSocialAPI.API.Controllers    
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IStorageService _storageService;


        public QuotesController(IMediator mediator, IStorageService storageService)
        {
            _mediator = mediator;
            _storageService = storageService;
        }
        [HttpGet]
        public async Task<ActionResult> GetQuotes([FromQuery] GetAllQutoesQueryRequest getAllQutoesQueryRequest)
        {
            //! Bu yapı parametresiz de olabilir gayet, ama request nesnesi göndermek için en azından onu ya parametrede
            //! ya da metot içinde tanımlamak gerekir.
            GetAllQuotesQueryResponse getAllQuotesQueryResponse = await _mediator.Send(getAllQutoesQueryRequest);
            return Ok(getAllQuotesQueryResponse);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetQuotesById([FromRoute] GetQuotesByIdQueryRequest getQuotesByIdQueryRequest)
        {
            //! Alıntıları id'ye göre çekme metodu.
            GetQuotesByIdQueryResponse getQuotesByIdQueryResponse = await _mediator.Send(getQuotesByIdQueryRequest);
            return Ok(getQuotesByIdQueryResponse);
        }
        [HttpGet("[action]/{UserId}")]
        public async Task<ActionResult> GetQuotesByUserId([FromRoute] GetQuotesByUserIdQueryRequest getQuotesByUserIdQueryRequest)
        {
            //! Kullanıcının alıtılarını çekme metodu.
            GetQuotesByUserIdQueryResponse getQuotesByUserIdQueryResponse = await _mediator.Send(getQuotesByUserIdQueryRequest);            
            return Ok(getQuotesByUserIdQueryResponse);
        }
        [HttpPost]
        public async Task<ActionResult> PostQuotes(CreateQuoteCommandRequest createQuoteCommandRequest)
        {
            //! Alıntı post etme metodu.
            CreateQuoteCommandResponse createQuoteCommandResponse = await _mediator.Send(createQuoteCommandRequest);    
            return Ok(createQuoteCommandResponse);
        }
        [HttpPost("[action]/{userName}")]
        public async Task<ActionResult> PostQuotesImage([FromRoute] string userName, [FromQuery] string quoteId)
        {

            List<(string fileName, string pathOContainerName)> result = await _storageService.UploadAsync(userName, Request.Form.Files,quoteId);
            return Ok();

        }
        [HttpGet("[action]/{userName}")]
        public IActionResult GetQuoteImage([FromRoute] string userName, [FromQuery] int quoteId)
        {
            var datas = _storageService.GetFiles(userName);
            var lastData = "";
            foreach (var data in datas)
            {
                if (data.StartsWith(quoteId.ToString()+"-"))
                {
                    lastData = data;
                }

            }
            return Ok(lastData);
        }
        [HttpGet("[action]/{MovieId}")]
        public async Task<ActionResult> GetQuotesByMovieId([FromRoute]GetQuotesByMovieIdQueryRequest getQuotesByMovieIdQueryRequest)
        {
            //! MovieId'ye göre alıntı çekme metodu.
            GetQuotesByMovieIdQueryResponse getQuotesByMovieIdQueryResponse =await _mediator.Send(getQuotesByMovieIdQueryRequest);  
            return Ok(getQuotesByMovieIdQueryResponse);
        }

    }
}
