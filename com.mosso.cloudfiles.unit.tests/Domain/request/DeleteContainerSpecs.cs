using System;
using System.Net;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rackspace.CloudFiles.domain.request;
using Rackspace.CloudFiles.domain.request.Interfaces;
using SpecMaker.Core;
using SpecMaker.Core.Matchers;

namespace Rackspace.CloudFiles.unit.tests.Domain.request.DeleteContainerSpecs
{
    public class DeleteContainerSpecs : BaseSpec
    {
        public void when_deleting_a_container_and_storage_url_is_null()
        {
            should("throw ArgumentNullException", () => new DeleteContainer(null, "containername"),
                typeof(ArgumentNullException));
        }
        public void when_deleting_a_container_and_storage_url_is_empty_string()
        {
            should("throw ArgumentNullException", () => new DeleteContainer("", "containername"),
                typeof(ArgumentNullException));
        }
        public void when_deleting_a_container_and_container_name_is_null()
        {
            should("throw ArgumentNullException", () => new DeleteContainer("http://storageurl", null),
                   typeof(ArgumentNullException));
        }
        public void when_deleting_a_container_and_container_name_is_empty_string()
        {
            should("throw ArgumentNullException", () => new DeleteContainer("http://storageUrl", ""),
                   typeof(ArgumentNullException));
        }
        public void when_deleting_a_container()
        {
            var deleteContainer = new DeleteContainer("http://storageurl", "containername");
            var mockrequest = new Mock<ICloudFilesRequest>();
            deleteContainer.Apply(mockrequest.Object);
            should("have url made of storage url and container name", 
                ()=>deleteContainer.CreateUri().ToString().Is("http://storageurl/containername"));
            should("have http delete method", ()=>
                mockrequest.VerifySet(x => x.Method = "DELETE")
                );
        }
    }

   
}