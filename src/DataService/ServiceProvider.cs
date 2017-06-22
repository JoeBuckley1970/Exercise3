/*--------------------------------------------------------------------------------------+
|
|  $Copyright: (c) 2017 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using AssetWise.Interns.Models;

namespace Interns2017.DataService
    {
    /// <summary>
    /// Provides Person and Message data.
    /// </summary>
    public class ServiceProvider
    {
        /// <summary>
        /// Gets an list of Persons.
        /// </summary>
        /// <returns>An <see cref="IList"/> of <see cref="Person"/>s.</returns>
        public IList<Person> GetPersons ()
            {
            return new List<Person>()
                {
                new Person() { Name = "Alice" },
                new Person() { Name = "Bob" },
                new Person() { Name = "Charlie"},
                new Person() { Name = "Joe" }
                };
            }

        /// <summary>
        /// Gets an list of messages.
        /// </summary>
        /// <returns>An <see cref="IList"/> of messages.</returns>
        public IList<string> GetMessages ()
            {
            return new List<string>()
                {
                "Hello",
                "Hola!",
                "Goeie dag",
                "Sawubona"
                };
            }
        }
}
