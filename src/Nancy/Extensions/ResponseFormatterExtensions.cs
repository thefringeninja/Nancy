namespace Nancy.Extensions
{
	public static class ResponseFormatterExtensions
	{
		public static Response As<TResponseFormat>(this IResponseFormatter extensionPoint, object model)
			where TResponseFormat : IResponseFormat, new()
		{
			var formatter = new TResponseFormat();
			return formatter.Format(model);
		}
	}
}