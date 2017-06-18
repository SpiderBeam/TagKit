﻿using System;
using TagKit.Services;

namespace TagKit.Xml.Tests.Mocks
{
    sealed class MockEntityProvider : IEntityProvider
    {
        readonly Func<String, String> _resolver;

        public MockEntityProvider(Func<String, String> resolver)
        {
            _resolver = resolver;
        }

        public String GetSymbol(String name)
        {
            return _resolver.Invoke(name);
        }
    }
}
