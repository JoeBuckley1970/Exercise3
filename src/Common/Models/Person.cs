/*--------------------------------------------------------------------------------------+
|
|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
using System.Runtime.Serialization;

namespace AssetWise.Interns.Models
    {
    /// <summary>
    /// A Person.
    /// </summary>
    [DataContract]
    public class Person
        {
        /// <summary>
        /// Gets or sets the Name of a <see cref="Person"/>.
        /// </summary>
        [DataMember]
        public string Name
            {
            get; set;
            }
        }
    }
