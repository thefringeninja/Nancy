using System.Net;
using Nancy.Routing;
using Nancy.Tests.Extensions;
using Nancy.Tests.Fakes;
using Xunit;

namespace Nancy.Tests.Unit
{
	public class InterceptionFixture
	{
		[Fact]
		public void Should_execute_after_filter_after_request()
		{
			// given
			dynamic parameters = new RouteParameters();
			parameters.value = 100;
			//when
			Response response = new FakeNancyModuleWithInterception().Get["/after"].Invoke(parameters);
			// then
			response.GetStringContentsFromResponse().ShouldEqual("100100");
		}

		[Fact]
		public void Should_chain_after_filters_in_order()
		{
			// given
			dynamic parameters = new RouteParameters();
			parameters.value = 100;
			//when
			Response response = new FakeNancyModuleWithInterception().Get["/after/chained"].Invoke(parameters);
			// then
			response.StatusCode.ShouldEqual(HttpStatusCode.SeeOther);
		}

		[Fact]
		public void Should_execute_before_filter_before_request()
		{
			// given
			var module = new FakeNancyModuleWithInterception();
			// when
			var response = module.Get["/before"].Invoke(new RouteParameters());
			//then
			module.SomethingHappenedFirst.ShouldBeTrue();
		}

		[Fact]
		public void Should_cancel_execution_when_before_filter_requests_it()
		{
			// given
			var module = new FakeNancyModuleWithInterception();
			// when
			var response = module.Get["/before/chained"].Invoke(new RouteParameters());
			//then
			response.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
			module.SomethingHappenedFirst.ShouldBeFalse();
		}
	}
}