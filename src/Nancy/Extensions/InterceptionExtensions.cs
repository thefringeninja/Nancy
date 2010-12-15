using System;

namespace Nancy.Extensions
{
	public delegate void AfterDelegate(dynamic parameters, ref Response response);

	public delegate bool BeforeDelegate(dynamic parameters, ref Response response);

	public static class InterceptionExtensions
	{
		public static Func<dynamic, Response> Before(this Func<dynamic, Response> routeAction, BeforeDelegate filter)
		{
			return parameters =>
			{
				Response response = null;
				return filter(parameters, ref response)
				       	? routeAction(parameters)
				       	: response;
			};
		}

		public static Func<dynamic, Response> After(this Func<dynamic, Response> routeAction, AfterDelegate filter)
		{
			return parameters =>
			{
				Response response = routeAction(parameters);
				filter(parameters, ref response);
				return response;
			};
		}
	}
}