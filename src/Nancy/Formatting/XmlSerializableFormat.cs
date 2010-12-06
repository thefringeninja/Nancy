using System;
using System.Xml.Serialization;

namespace Nancy.Formatting
{
	public class XmlSerializableFormat
		: IResponseFormat
	{
		#region IResponseFormat Members

		public Response Format(object model)
		{
			if (model == null)
				throw new ArgumentNullException("model");
			var serializer = new XmlSerializer(model.GetType());
			return new Response
				{
					ContentType = "application/xml",
					Contents = stream => serializer.Serialize(stream, model)
				};
		}

		#endregion
	}
}