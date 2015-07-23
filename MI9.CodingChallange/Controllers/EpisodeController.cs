using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MI9.CodingChallange.Models;

namespace MI9.CodingChallange.Controllers
{
    public class EpisodeController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get([FromBody]EpisodeInfoWrapper request)
        {
            try
            {
                if (!ValidateRequest(request))
                {
                    var err = new ErrorResponse { Error = Common.Constants.ErrorParsingInput };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }
                return Request.CreateResponse(HttpStatusCode.OK, ProcessRequest(request));
            }
            catch 
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    Common.Constants.ErrorInternalServerError);
            }
        }

        
        [Route("")]
        public HttpResponseMessage Post([FromBody]EpisodeInfoWrapper request)
        {
            try
            {
                if (!ValidateRequest(request))
                {
                    var err = new ErrorResponse {Error = Common.Constants.ErrorParsingInput};
                    return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                }
                var data = ProcessRequest(request);
                return Request.CreateResponse(HttpStatusCode.OK,data );
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,Common.Constants.ErrorInternalServerError);
            }
        }


        private bool ValidateRequest(EpisodeInfoWrapper request) {
            if (request?.Payload == null)
            { return false; }
            return true;
        }

        private FilteredEpisodeInfoWrapper ProcessRequest(EpisodeInfoWrapper request)
        {
            var response = request.Payload.Where(x => (x.Drm && x.EpisodeCount > 0))
                                               .Select(c => new FilteredEpisodeInfo
                                               {
                                                   Image = c.Image?.ShowImage,
                                                   Slug = c.Slug,
                                                   Title = c.Title
                                               });
            return new FilteredEpisodeInfoWrapper { Response= response.ToArray() };
        }
    }
}