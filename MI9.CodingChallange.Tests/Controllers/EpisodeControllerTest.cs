using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FluentAssertions;
using FluentValidation.WebApi;
using MI9.CodingChallange.Controllers;
using MI9.CodingChallange.Models;
using NUnit.Framework;

namespace MI9.CodingChallange.Tests.Controllers
{
    [TestFixture]
    public class EpisodeControllerTest
    {
        private EpisodeController _episodeController;
        [SetUp]
        public void Start()
        {
            var config = new HttpConfiguration();
            FluentValidationModelValidatorProvider.Configure(config);

            _episodeController = new EpisodeController()
            {
                Request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:50236/")
                },
                Configuration = config
            };
        }

        [Test]
        public void GetShouldReturnBadRequestonInvalidInput()
        {
            EpisodeInfoWrapper req = new EpisodeInfoWrapper();
            var response = _episodeController.Get(req);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void GetShouldReturnZeroItemsifCriteriaNotMatched()
        {
            EpisodeInfoWrapper req = new EpisodeInfoWrapper
            {
                Payload = new[] {new EpisodeInfo
                                        {
                                            Drm = true,
                                            EpisodeCount = 0
                                        }}
            };
            CheckResponseForZeroRecords(req);
        }

        [Test]
        public void GetShouldReturnItemsifCriteriaMatched()
        {
            EpisodeInfoWrapper req = new EpisodeInfoWrapper
            {
                Payload = new[] {new EpisodeInfo
                                        {
                                            Drm = true,
                                            EpisodeCount = 2
                                        }}
            };
           CheckResponseForOneOrMoreRecords(req); 
        }

        [Test]
        public void PostShouldReturnBadRequestonInvalidInput()
        {
            EpisodeInfoWrapper req = new EpisodeInfoWrapper();
            var response = _episodeController.Post(req);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void PostShouldReturnZeroItemsifCriteriaNotMatched()
        {
            EpisodeInfoWrapper req = new EpisodeInfoWrapper
            {
                Payload = new[] {new EpisodeInfo
                                        {
                                            Drm = true,
                                            EpisodeCount = 0
                                        }}
            };
            CheckResponseForZeroRecords(req);
        }

        private void CheckResponseForZeroRecords(EpisodeInfoWrapper req)
        {
            var response = _episodeController.Post(req);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            FilteredEpisodeInfoWrapper jsonResponse;
            HttpResponseMessageExtensions.TryGetContentValue(response, out jsonResponse).Should().BeTrue();
            jsonResponse.Response.Length.Should().Be(0);
        }

        [Test]
        public void PostShouldReturnItemsifCriteriaMatched()
        {
            EpisodeInfoWrapper request = new EpisodeInfoWrapper
            {
                Payload = new[] {new EpisodeInfo
                                        {
                                            Drm = true,
                                            EpisodeCount = 2
                                        }}
            };
            CheckResponseForOneOrMoreRecords(request);
        }

        private void CheckResponseForOneOrMoreRecords(EpisodeInfoWrapper request)
        {
            var response = _episodeController.Post(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            FilteredEpisodeInfoWrapper jsonResponse;
            HttpResponseMessageExtensions.TryGetContentValue(response, out jsonResponse).Should().BeTrue();
            jsonResponse.Response.Length.Should().BeGreaterThan(0);
        }
    }
}
