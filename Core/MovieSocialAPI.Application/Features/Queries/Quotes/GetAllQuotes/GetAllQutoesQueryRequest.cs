using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSocialAPI.Application.Features.Queries.GetAllQuotes
{
    public class GetAllQutoesQueryRequest:IRequest<GetAllQuotesQueryResponse>
    {
    }
}
