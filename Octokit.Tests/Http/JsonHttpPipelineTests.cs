﻿using System;
using System.IO;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Http
{
    public class JsonHttpPipelineTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new JsonHttpPipeline(null));
            }
        }

        public class TheSerializeRequestMethod
        {
            [Fact]
            public void SetsRequestAcceptHeader()
            {
                var request = new Request();
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Contains("Accept", request.Headers.Keys);
                Assert.Equal("application/vnd.github.v3+json; charset=utf-8", request.Headers["Accept"]);
            }

            [Fact]
            public void DoesNotChangeExistingAcceptsHeader()
            {
                var request = new Request();
                request.Headers.Add("Accept", "application/vnd.github.manifold-preview; charset=utf-8");
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Contains("Accept", request.Headers.Keys);
                Assert.Equal("application/vnd.github.manifold-preview; charset=utf-8", request.Headers["Accept"]);
            }

            [Fact]
            public void LeavesStringBodyAlone()
            {
                const string json = "just some string data";
                var request = new Request { Body = json };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Equal(json, request.Body);
            }

            [Fact]
            public void LeavesStreamBodyAlone()
            {
                var stream = new MemoryStream();
                var request = new Request { Body = stream };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Same(stream, request.Body);
            }

            [Fact]
            public void LeavesNullBodyAlone()
            {
                var request = new Request { Body = null };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Null(request.Body);
            }

            [Fact]
            public void EncodesObjectBody()
            {
                var request = new Request { Body = new { test = "value" } };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Equal("{\"test\":\"value\"}", request.Body);
            }

            [Fact]
            public void EnsuresArguments()
            {
                var jsonPipeline = new JsonHttpPipeline();

                Assert.Throws<ArgumentNullException>(() => jsonPipeline.SerializeRequest(null));
            }
        }

        public class TheDeserializeResponseMethod
        {
            [Fact]
            public void DeserializesResponse()
            {
                const string data = "works";
                var response = new ApiResponse<string>
                {
                    Body = SimpleJson.SerializeObject(data),
                    ContentType = "application/json"
                };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.DeserializeResponse(response);

                Assert.NotNull(response.BodyAsObject);
                Assert.Equal(data, response.BodyAsObject);
            }

            [Fact]
            public void IgnoresResponsesNotIdentifiedAsJson()
            {
                const string data = "works";
                var response = new ApiResponse<string>
                {
                    Body = SimpleJson.SerializeObject(data),
                    ContentType = "text/html"
                };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.DeserializeResponse(response);

                Assert.Null(response.BodyAsObject);
            }


            [Fact]
            public void PerformsDataMemberAttributeMapping()
            {
                const string data = @"{ ""tag"":""tag-name"",
                                        ""sha"": ""tag-sha"",
                                        ""url"": ""tag-url"",
                                        ""message"": ""initial version\n"",
                                        ""tagger"": {
                                            ""name"": ""tagger-name"",
                                            ""email"": ""tagger-email"",
                                            ""date"": ""2011-06-17T14:53:35-07:00""
                                        },
                                        ""object"": {
                                            ""type"": ""commit"",
                                            ""sha"": ""object-sha"",
                                            ""url"": ""object-url""
                                        }}";
                //const string data = @"{""name"":""tag-name"",""url"":""url""}";

                var response = new ApiResponse<Tag>
                {
                    Body = data,
                    ContentType = "application/json"
                };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.DeserializeResponse(response);

                Assert.NotNull(response.BodyAsObject);
                Assert.Equal("tag-name", response.BodyAsObject.Name);
                Assert.Equal("tag-sha", response.BodyAsObject.Sha);
                Assert.Equal("tag-url", response.BodyAsObject.Url);
                Assert.Equal("tag-message", response.BodyAsObject.Message);
                Assert.Equal("tagger.name", response.BodyAsObject.Tagger.Name);
                Assert.Equal("tagger.email", response.BodyAsObject.Tagger.Email);
            }
        }
    }
}
