using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CharactersAPI.Tests.Integration
{
    [CollectionDefinition("Characters API")]
    public class SharedTestCollection : ICollectionFixture<CharactersApiFactory>
    {
    }
}
