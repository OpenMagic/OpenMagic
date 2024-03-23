using System;
using System.Net;
using FluentAssertions;
using OpenMagic.Extensions;
using Xunit;

namespace OpenMagic.Tests.Extensions;

public class UriExtensionsTests
{
    public class GetResponseStatusCode
    {
        [Fact]
        public void ShouldReturnHttpStatusCode()
        {
            // Given
            var uri = new Uri("http://google.com");

            // When
            var statusCode = uri.GetResponseStatusCode();

            // Then
            statusCode.Should().Be(HttpStatusCode.OK, "because the world will have ended if '{0}' is down :-)", uri);
        }
    }

    public class ResponseIsSuccessStatusCode
    {
        [Fact]
        public void ShouldReturnTrueWhenWebsiteIsRunning()
        {
            // Given
            var uri = new Uri("http://google.com");

            // When
            var responseIsOK = uri.ResponseIsSuccessStatusCode();

            // Then
            responseIsOK.Should().BeTrue("because the world will have ended if '{0}' is down :-)", uri);
        }

        [Fact]
        public void ShouldReturnFalseWhenWebsiteDoesNotExist()
        {
            // Given
            var uri = new Uri("http://a.a");

            // When
            var responseIsOK = uri.ResponseIsSuccessStatusCode();

            // Then
            responseIsOK.Should().BeFalse("because '{0}' should not exist", uri);
        }
    }
}