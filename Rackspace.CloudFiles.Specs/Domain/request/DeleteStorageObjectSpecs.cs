using System;
using System.Net;
using Moq;
using Rackspace.CloudFiles.Request;
using Rackspace.CloudFiles.Request.Interfaces;
using SpecMaker.Core.Matchers;
using SpecMaker.Core;

namespace Rackspace.CloudFiles.Specs.Domain.request
{
    public class DeleteStorageObjectSpecs: BaseSpec
    {
        public void when_deleting_a_storage_item_and_storage_url_is_null()
        {
            should("throw ArgumentNullException",()=>new DeleteStorageObject(null, "containername", "storageitemname"), typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item_and_storage_url_is_emptry_string()
        {
            
            should("throw ArgumentNullException",()=>new DeleteStorageObject("", "containername", "storageitemname"), typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item_and_storage_item_name_is_null()
        {
            should("throw ArgumentNullException", () =>new DeleteStorageObject("http://storageurl", "containername", null),typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item_and_storage_item_name_is_emptry_string()
        {
            should("throw ArgumentNullException", () =>new DeleteStorageObject("http://storageurl", "containername", ""),typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item_and_container_name_is_null()
        {
            should("throw ArgumentNullException", () =>new DeleteStorageObject("http://storageurl", null, "storageitemname"),typeof(ArgumentNullException));
        }

        public void when_deleting_a_storage_item_and_container_name_is_emptry_string()
        {
            should("throw ArgumentNullException", () =>new DeleteStorageObject("http://storageurl", "", "storageitemname"),typeof(ArgumentNullException));
        }
        public void when_deleting_a_storage_item()
        {
            var deleteStorageItem = new DeleteStorageObject("http://storageurl", "containername", "storageitemname");
            var _mockrequest = new Mock<ICloudFilesRequest>();
            _mockrequest.SetupGet(x => x.Headers).Returns(new WebHeaderCollection());
            deleteStorageItem.Apply(_mockrequest.Object);
            
            should("start with storageurl, have container name next, and then end with the item being deleted",
                   ()=>deleteStorageItem.CreateUri().Is("http://storageurl/containername/storageitemname"));
            should("use HTTP DELETE method",()=> _mockrequest.VerifySet(x => x.Method = "DELETE"));
        }
    }
}