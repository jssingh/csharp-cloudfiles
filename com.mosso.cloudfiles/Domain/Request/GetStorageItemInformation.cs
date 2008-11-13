///
/// See COPYING file for licensing information
///

using System;
using System.Collections.Specialized;
using System.Web;
using com.mosso.cloudfiles.exceptions;

namespace com.mosso.cloudfiles.domain.request
{
    /// <summary>
    /// GetStorageItemInformation
    /// </summary>
    public class GetStorageItemInformation : BaseRequest
    {
        /// <summary>
        /// GetStorageItemInformation constructor
        /// </summary>
        /// <param name="storageUrl">the customer unique url to interact with cloudfiles</param>
        /// <param name="containerName">the name of the container where the storage item is located</param>
        /// <param name="storageItemName">the name of the storage item to add meta information too</param>
        /// <param name="storageToken">the customer unique token obtained after valid authentication necessary for all cloudfiles ReST interaction</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the reference parameters are null</exception>
        /// <exception cref="ArgumentNullException">Thrown when any of the reference parameters are null</exception>
        /// <exception cref="ContainerNameLengthException">Thrown when the container name length exceeds the maximum container length allowed</exception>
        public GetStorageItemInformation(string storageUrl, string containerName, string storageItemName,
                                         string storageToken)
        {
            if (string.IsNullOrEmpty(storageUrl)
                || string.IsNullOrEmpty(storageToken)
                || string.IsNullOrEmpty(containerName)
                || string.IsNullOrEmpty(storageItemName))
                throw new ArgumentNullException();


            if (containerName.Length > Constants.MAXIMUM_CONTAINER_NAME_LENGTH)
                throw new ContainerNameLengthException("The container name length exceeds " + Constants.MAXIMUM_CONTAINER_NAME_LENGTH + " characters.s");

            Uri =
                new Uri(storageUrl + "/" + HttpUtility.UrlEncode(containerName).Replace("+", "%20") + "/" +
                        HttpUtility.UrlEncode(storageItemName).Replace("+", "%20"));
            Method = "HEAD";
            Headers = new NameValueCollection();
            Headers.Add(Constants.X_STORAGE_TOKEN, HttpUtility.UrlEncode(storageToken));
        }
    }
}