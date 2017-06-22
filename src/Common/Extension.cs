/*--------------------------------------------------------------------------------------+
|
|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AssetWise.Interns
    {
    /// <summary>
    /// A static class containing all Extension methods
    /// </summary>
    public static class Extension
        {
        #region List Extensions

        // Use by the FindRandom method
        private static Random random = new Random();

        /// <summary>
        /// Returns the random element in the list.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="list">The <see cref="System.Collections.Generic.IList"/> from there the random element will be drawn.</param>
        /// <returns>A random element from the list.</returns>
        public static T FindRandom<T> (this IList<T> list)
            {
            return list[random.Next(list.Count)];
            }

        #endregion

        #region  Serialization Extensions

        /// <summary>
        /// Serializes an object to Json.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A Json serialized representation of the object.</returns>
        public static string ToJson<T> (this T obj)
            {
            var settings = new DataContractJsonSerializerSettings
                {
                UseSimpleDictionaryFormat = true
                };

            var serializer = new DataContractJsonSerializer(typeof(T), settings);

            using ( var stream = new MemoryStream() )
                {
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
                }
            }


        /// <summary>
        /// Deserialize an Json string into an instance of an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize</typeparam>
        /// <param name="json">The json string to deserialize.</param>
        /// <returns>An object of type T.</returns>
        public static T FromJson<T> (this string json)
            {
            var settings = new DataContractJsonSerializerSettings
                {
                UseSimpleDictionaryFormat = true
                };

            var serializer = new DataContractJsonSerializer(typeof(T), settings);

            using ( var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)) )
                {
                return (T) serializer.ReadObject(stream);
                };
            }

        /// <summary>
        /// Serializes an object to Json, compresses it and converts the compressed bytes into Base64 encoding.
        /// </summary>
        /// <remarks>
        /// The function uses <see cref="GZipStream"/> to do the compression.
        /// </remarks>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A Base64 string containing the compressed data.</returns>
        public static string ToJsonCompressed<T> (this T obj)
            {
            var settings = new DataContractJsonSerializerSettings
                {
                UseSimpleDictionaryFormat = true
                };

            var serializer = new DataContractJsonSerializer(typeof(T), settings);

            using ( var destinationStream = new MemoryStream() )
                {
                using ( var compressor = new GZipStream(destinationStream, CompressionLevel.Fastest, true) )
                    {
                    serializer.WriteObject(compressor, obj);
                    }
                return Convert.ToBase64String(destinationStream.ToArray());
                }
            }

        /// <summary>
        /// Deserialize an Base64 encoded string that represents a compresses Json string to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize</typeparam>
        /// <param name="json">The Base64 encoded, compresses json string to deserialize.</param>
        /// <returns>An object of type T.</returns>
        public static T FromJsonCompressed<T> (this string json)
            {
            var settings = new DataContractJsonSerializerSettings
                {
                UseSimpleDictionaryFormat = true
                };

            var serializer = new DataContractJsonSerializer(typeof(T), settings);

            using ( var sourceStream = new MemoryStream(Convert.FromBase64String(json)) )
                {
                using ( var compressor = new GZipStream(sourceStream, CompressionMode.Decompress) )
                    {
                    return (T) serializer.ReadObject(compressor);
                    }
                }
            }

        #endregion
        }
    }


