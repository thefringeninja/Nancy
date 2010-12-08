﻿namespace Nancy.Tests.Unit
{
    using System.IO;
    using System.Net;
    using Nancy;
    using Xunit;

    public class ResponseFixture
    {
        [Fact]
        public void Should_set_empty_content_on_new_instance()
        {
            // Given
            var response = new Response();

            // When 
            var content = GetStringContentsFromResponse(response);

            // Then
            content.ShouldBeEmpty();
        }

        [Fact]
        public void Should_set_content_type_to_text_html_on_new_instance()
        {
            // Given, When
            var response = new Response();

            // Then
            response.ContentType.ShouldEqual("text/html");
        }

        [Fact]
        public void Should_set_status_code_ok_on_new_instance()
        {
            // Given, When
            var response = new Response();

            // Then
            response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Should_not_make_headers_null_on_new_instance()
        {
            // Given, When
            var response = new Response();

            // Then
            response.Headers.ShouldNotBeNull();
        }

        [Fact]
        public void Should_set_status_code_when_implicitly_cast_from_int()
        {
            // Given, When
            Response response = 200;
            
            // Then
            response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Should_set_empty_content_when_implicitly_cast_from_int()
        {
            // Arrange
            Response response = 200;

            // Act
            var output = GetStringContentsFromResponse(response);

            // Assert
            output.ShouldBeEmpty();
        }

        [Fact]
        public void Should_set_content_type_to_text_html_when_implicitly_cast_from_int()
        {
            // Arrange, Act
            Response response = 200;

            // Assert
            response.ContentType.ShouldEqual("text/html");
        }

        [Fact]
        public void Should_set_status_code_when_implicitly_cast_from_http_status_code()
        {
            // Given, When
            Response response = HttpStatusCode.NotFound;

            // Then
            response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
        }

        [Fact]
        public void Should_set_empty_content_when_implicitly_cast_from_http_status_code()
        {
            // Arrange
            Response response = HttpStatusCode.OK;

            // Act
            var output = GetStringContentsFromResponse(response);

            // Assert
            output.ShouldBeEmpty();
        }

        [Fact]
        public void Should_set_content_type_to_text_html_when_implicitly_cast_from_http_status_code()
        {
            // Arrange, Act
            Response response = HttpStatusCode.OK;

            // Assert
            response.ContentType.ShouldEqual("text/html");
        }

        [Fact]
        public void Should_set_status_code_to_ok_when_implicitly_cast_from_string()
        {
            // Given, When
            const string value = "test value";
            Response response = value;

            // Then
            response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Fact]
        public void Should_set_content_when_implicitly_cast_from_string()
        {
            // Given
            const string value = "test value";
            Response response = value;

            // When
            var output = GetStringContentsFromResponse(response);

            // Then
            output.ShouldEqual(value);
        }

        [Fact]
        public void Should_set_content_type_to_text_html_when_implicitly_cast_from_string()
        {
            // Given, When
            const string value = "test value";
            Response response = value;

            // Then
            response.ContentType.ShouldEqual("text/html");
        }

        private static string GetStringContentsFromResponse(Response response)
        {
            var memory = new MemoryStream();
            response.Contents.Invoke(memory);
            memory.Position = 0;
            using(var reader = new StreamReader(memory))
            {
                return reader.ReadToEnd();
            }
        }
    }
}