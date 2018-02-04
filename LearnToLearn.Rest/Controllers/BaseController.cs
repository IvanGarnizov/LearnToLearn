namespace LearnToLearn.Rest.Controllers
{
    using System.Web.Http;

    using Data;

    public class BaseController : ApiController
    {
        protected LearnToLearnContext context;

        public BaseController(LearnToLearnContext context)
        {
            this.context = context;
        }

        public BaseController()
            : this(new LearnToLearnContext())
        {
        }
    }
}