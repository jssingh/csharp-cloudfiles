using System;
using System.Net;
using Moq;
using NUnit.Framework;
using Rackspace.CloudFiles.Domain.Request;
using Rackspace.CloudFiles.Domain.Request.Interfaces;


namespace Rackspace.CloudFiles.Unit.Tests.Domain.Request.SetPublicContainerDetailsSpecs
{
    [TestFixture]
    public class when_setting_public_container_details_and_cdn_management_url_is_null
    {
        [Test]
        public void should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new SetPublicContainerDetails(null, "containername", true, false, -1));
        }
    }

    [TestFixture]
    public class when_setting_public_container_details_and_cdn_management_is_emptry_string
    {
        [Test]
        public void should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new SetPublicContainerDetails("", "containername", true, false, -1));
        }
    }


    [TestFixture]
    public class when_setting_public_container_details_and_container_name_is_null
    {
        [Test]
        public void should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new SetPublicContainerDetails("http://cdnmanagementurl", null, true, false, -1));
        }
    }

    [TestFixture]
    public class when_setting_public_container_details_and_container_name_is_emptry_string
    {
        [Test]
        public void should_throw_argument_null_exception()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>new SetPublicContainerDetails("http://cdnmanagementurl", "", true, false,-1));
        }
    }

    [TestFixture]
    public class when_setting_public_container_details_and_container_ttl_is_less_than_zero
    {
        [Test]
        public void should_throw_argument_null_exception()
        {
            var setPublicContainerDetails = new SetPublicContainerDetails("http://cdnmanagementurl", "containername", true,false, -1);
          
            Asserts.AssertHeaders(setPublicContainerDetails,CloudFiles.Utils.Constants.X_CDN_TTL,null );
            
        }
    }

    [TestFixture]
    public class when_setting_public_container_details
    {
        private SetPublicContainerDetails setPublicContainerDetails;

        [SetUp]
        public void setup()
        {
            setPublicContainerDetails = new SetPublicContainerDetails("http://cdnmanagementurl", "containername", true,false ,12345);
        }

        [Test]
        public void should_have_properly_formmated_request_url()
        {
            Assert.That(setPublicContainerDetails.CreateUri().ToString(), Is.EqualTo("http://cdnmanagementurl/containername"));
        }

        [Test]
        public void should_have_a_http_post_method()
        {
            var mock = new Mock<ICloudFilesRequest>();
            mock.SetupGet(x => x.Headers).Returns(new WebHeaderCollection());
            setPublicContainerDetails.Apply(mock.Object);
            mock.VerifySet(x => x.Method = "POST");
        }


        [Test]
        public void should_have_cdn_enabled_in_the_headers()
        {
            Asserts.AssertHeaders(setPublicContainerDetails, CloudFiles.Utils.Constants.X_CDN_ENABLED, "True");
        }

        [Test]
        public void should_have_time_to_live_aka_ttl_in_the_headers()
        {
            Asserts.AssertHeaders(setPublicContainerDetails, CloudFiles.Utils.Constants.X_CDN_TTL, "12345");
        }
    }
}