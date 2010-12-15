using System;
using System.IO;
using System.Net;
using Nancy.Extensions;

namespace Nancy.Tests.Fakes
{
    public class FakeNancyModuleWithInterception : NancyModule
    {
    	public bool SomethingHappenedFirst;
        public FakeNancyModuleWithInterception()
        {
			Get["/after/chained"] = x => (string)x.value;
			Get["/after/chained"] = Get["/after/chained"].After(DoubleResponse).After(RedirectResponse);
			
			Get["/after"] = x => (string)x.value;
        	Get["/after"] = Get["/after"].After(DoubleResponse);

        	Get["/before/chained"] = x => "something happened";
        	Get["/before/chained"] = Get["/before/chained"].Before(DoSomethingFirst).Before(UnAuthorized).Before(Authenticate);

        	Get["/before"] = x => "something happened";
			Get["/before"] = Get["/before"].Before(DoSomethingFirst);
        }

    	protected bool DoSomethingFirst(dynamic parameters, ref Response response)
    	{
    		SomethingHappenedFirst = true;
    		return true;
    	}

		protected bool Authenticate(dynamic parameters, ref Response response)
		{
			return true;
		}

		protected bool UnAuthorized(dynamic parameters, ref Response response)
		{
			response = HttpStatusCode.Unauthorized;
			return false;
		}
		protected void RedirectResponse(dynamic parameters, ref Response response)
		{
			response = HttpStatusCode.SeeOther;
		}
		protected void DoubleResponse(dynamic parameters, ref Response response)
		{
			var action = response.Contents;

			response.Contents = stream =>
			{
				action(stream);
				action(stream);
			};
		}
    }
}