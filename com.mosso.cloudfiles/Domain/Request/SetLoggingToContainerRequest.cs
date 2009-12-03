using System;
using Rackspace.CloudFiles.domain.request.Interfaces;
using Rackspace.CloudFiles.exceptions;
using Rackspace.CloudFiles.utils;

namespace Rackspace.CloudFiles.domain.request
{
    public class SetLoggingToContainerRequest : IAddToWebRequest
    {
        private readonly string _publiccontainer;
        private readonly string _cdnManagmentUrl;
        private readonly bool _loggingenabled;

        public SetLoggingToContainerRequest(string publiccontainer, string cdnManagmentUrl, bool loggingenabled)
        {
            _publiccontainer = publiccontainer;
            _cdnManagmentUrl = cdnManagmentUrl;
            _loggingenabled = loggingenabled;
            if (String.IsNullOrEmpty(publiccontainer))
                throw new ArgumentNullException();

            if (!ContainerNameValidator.Validate(publiccontainer)) throw new ContainerNameException();
           
        }

        public Uri CreateUri()
        {
            return  new Uri(_cdnManagmentUrl + "/" + _publiccontainer.Encode());
        }

        public void Apply(ICloudFilesRequest request)
        {
            request.Method = "POST";
            string enabled = "False";
            if (_loggingenabled)
                enabled = "True";
            request.Headers.Add("X-Log-Retention", enabled);
           
        }
    }
}
