namespace LearnToLearn.Rest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    
    using Models;

    public class UsersController : ApiController
    {
        [Route("Login")]
        public async Task<IHttpActionResult> Login(UserBindingModel model)
        {
            HttpClient client = new HttpClient();
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var requestParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", model.Username),
                new KeyValuePair<string, string>("password", model.Password)
            };
            var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
            var response = await client.PostAsync(baseUrl + "/Token", requestParamsFormUrlEncoded);

            return ResponseMessage(response);
        }
    }
}