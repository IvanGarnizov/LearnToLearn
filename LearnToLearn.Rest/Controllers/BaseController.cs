namespace LearnToLearn.Rest.Controllers
{
    using System.Web.Http;

    using Entities;

    using Services;

    public class BaseController<T> : ApiController
    {
        protected IService<T> service;
        protected IService<User> userService;

        public BaseController(IService<T> service, IService<User> userService)
        {
            this.service = service;
            this.userService = userService;
        }
    }
}