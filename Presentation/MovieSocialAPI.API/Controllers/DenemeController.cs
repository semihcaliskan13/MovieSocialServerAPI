using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSocialAPI.Application.Abstractions.Storage;
using MovieSocialAPI.Application.Features.Commands.Quotes;
using MovieSocialAPI.Application.Features.Queries.GetAllQuotes;
using MovieSocialAPI.Application.Repositories;
using MovieSocialAPI.Application.Services;
using MovieSocialAPI.Domain.Entities;
using System.Net;

namespace MovieSocialAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes="User")]
    public class DenemeController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
       

        readonly private IQuoteReadRepository _quoteReadRepository;
        readonly private IQuoteWriteRepository _quoteWriteRepository;

        readonly private IFileService _fileService;
        readonly private IMovieRepository _movieRepository;


        readonly private IProfileImageFileWriteRepository _profileImageFileWriteRepository;
        readonly private IProfileImageFileReadRepository _profileImageFileReadRepository;
        readonly private IFileReadRepository _fileReadRepository;
        readonly private IFileWriteRepoistory _fileWriteRepository;
        readonly private IProfileBackgroundImageFileReadRepository _profileBackgroundImageFileReadRepository;
        readonly private IProfileBackgroundImageFileWriteRepository _profileBackgroundImageFileWriteRepository;

        readonly IStorageService _storageService;

        readonly IMediator _mediator;


        public DenemeController(IWebHostEnvironment webHostEnvironment,
            
            IQuoteReadRepository quoteReadRepository,
            IMovieRepository movieRepository,
            IQuoteWriteRepository quoteWriteRepository,
            IFileService fileService,
            IProfileImageFileWriteRepository profileImageFileWriteRepository,
            IProfileImageFileReadRepository profileImageFileReadRepository,
            IFileReadRepository fileReadRepository,
            IFileWriteRepoistory fileWriteRepository,
            IProfileBackgroundImageFileReadRepository profileBackgroundImageFileReadRepository,
            IProfileBackgroundImageFileWriteRepository profileBackgroundImageFileWriteRepository
,
            IStorageService storageService
,
            IMediator mediator)
        {
           
            this._webHostEnvironment = webHostEnvironment;
            
            _quoteReadRepository = quoteReadRepository;
            _movieRepository = movieRepository;
            _quoteWriteRepository = quoteWriteRepository;
            _fileService = fileService;
            _profileImageFileWriteRepository = profileImageFileWriteRepository;
            _profileImageFileReadRepository = profileImageFileReadRepository;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _profileBackgroundImageFileReadRepository = profileBackgroundImageFileReadRepository;
            _profileBackgroundImageFileWriteRepository = profileBackgroundImageFileWriteRepository;
            _storageService = storageService;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetData([FromQuery] GetAllQutoesQueryRequest getAllQutoesQueryRequest)
        {
            //IQueryable<Movie> movie = _movieRepository.GetAll();
            //MovieQuoteMasterModel model = new MovieQuoteMasterModel { MovieList = movie, QuoteList = quote };

            //IQueryable<Quote> quotes = _quoteReadRepository.GetAll();//Optimize edilmeli.16kasım
            //foreach (var item in quotes)
            //{
            //    item.Movie = await _movieRepository.GetByIdAsync(item.MovieId);
            //}

            //return Ok(quotes);
           
            return Ok(await _mediator.Send(getAllQutoesQueryRequest));


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataById(string id)
        {

            Quote quote = await _quoteReadRepository.GetByIdAsync(id);
            Movie movie = await _movieRepository.GetByIdAsync(quote.MovieId);
            //MovieQuoteMasterModel model = new() { Movie = movie, Quote = quote };
            return Ok(quote);

        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateQuoteCommandRequest createQuoteCommandRequest)//Postta master model kullanmamak lazım. Çünkü fazlaca veri biz
        {
            //veri tabanına kaydetmeyeceğimiz için fazladan gelecektir. Alıntıyı al, bunu yaparken kullanıcının orası için seçtiği verinin-hatta bu seçme işlemini dropdown üzerinden de yapabilir, arayarak ve dropdown'u filtreleyerek-                                                                 //sadece id'sini al ki alıntı içinde bu id değeri zaten var. Alıntıyı post et ve alıntı kayıt edilsin diyebiliriz.
            CreateQuoteCommandResponse createQuoteCommandResponse= await _mediator.Send(createQuoteCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
       
        [HttpGet("[action]/{userName}")]

        public async Task<IActionResult> UploadDeneme([FromRoute] string userName)
        {//wwwroot/images kaydediyor.
            /*var datas = await _storageService.UploadAsync("files", Request.Form.Files);
            await _profileImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProfileImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage=_storageService.StorageName

            }).ToList());
            await _profileImageFileWriteRepository.SaveAsync();
            AZURE*/


            //LOCAL
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files);
            //await _profileImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProfileImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainerName,
            //    Storage = _storageService.StorageName

            //}).ToList());
            //await _profileImageFileWriteRepository.SaveAsync();

            var datas1 = _storageService.GetFiles(userName);

            return Ok(datas1);

        }
        

        [HttpPost("[action]/{userName}")]       
        public async Task<IActionResult> Upload([FromRoute]string userName)
        {
           
            
            List <(string fileName, string pathOContainerName)> result = await _storageService.UploadAsync(userName, Request.Form.Files);
            await _profileImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProfileImageFile
            {
                FileName = r.fileName,
                Path = r.pathOContainerName,
                Storage = _storageService.StorageName

            }).ToList());
            await _profileImageFileWriteRepository.SaveAsync();
            return Ok();

        }






    }
}
