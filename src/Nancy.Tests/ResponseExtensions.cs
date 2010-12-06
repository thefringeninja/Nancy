using System.IO;

namespace Nancy.Tests
{
	public static class ResponseExtensions
	{
		public static string StringContents(this Response response)
		{
			var memory = new MemoryStream();
			response.Contents.Invoke(memory);
			memory.Position = 0;
			using (var reader = new StreamReader(memory))
			{
				return reader.ReadToEnd();
			}
		}
	}
}