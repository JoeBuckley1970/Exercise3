/*--------------------------------------------------------------------------------------+
|
|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
using System.Runtime.Serialization;

namespace AssetWise.Interns.Models
    {
    /// <summary>
    /// A greeting message.
    /// </summary>
    [DataContract]
    public class Greeting
        {
        /// <summary>
        /// Gets or sets the <see cref="Person"/> who sent the <see cref="Greeting"/>.
        /// </summary>
        [DataMember]
        public Person From
            {
            get; set;
            }

        /// <summary>
        /// Gets or sets the <see cref="Person"/> to whom the <see cref="Greeting"/> should go.
        /// </summary>
        [DataMember]
        public Person To
            {
            get; set;
            }

        /// <summary>
        /// Gets or sets the text message of the <see cref="Greeting"/>.
        /// </summary>
        [DataMember]
        public string Message
            {
            get; set;
            }
        }
    }
