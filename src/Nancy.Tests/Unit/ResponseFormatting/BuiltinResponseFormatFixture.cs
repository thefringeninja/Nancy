using System;
using Nancy.Extensions;
using Nancy.Formatting;
using Xunit;

namespace Nancy.Tests.Unit.ResponseFormatting
{
	public class BuiltinResponseFormatFixture
	{
		[Serializable]
		public class SerializableModel
		{
			public string Name { get; set; }
			public int Id { get; set; }
		}

		private static IResponseFormatter Response;
		[Fact]
		public void Xml()
		{
			var model = new SerializableModel
				{
					Id = 12,
					Name = "Mister Charlie"
				};
			var result = Response.As<XmlSerializableFormat>(model).StringContents();
			result.ShouldEqual(@"<?xml version=""1.0""?>
<SerializableModel xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>Mister Charlie</Name>
  <Id>12</Id>
</SerializableModel>");
		}
	}
}