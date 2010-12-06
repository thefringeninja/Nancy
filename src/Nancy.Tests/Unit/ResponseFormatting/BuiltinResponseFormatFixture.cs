using System;
using Nancy.Extensions;
using Nancy.Formatting;
using Xunit;

namespace Nancy.Tests.Unit.ResponseFormatting
{
	public class BuiltinResponseFormatFixture
	{
		[Serializable]
		class SerializableModel
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
			Response.As<XmlSerializableFormat>(model).Contents
		}
	}
}