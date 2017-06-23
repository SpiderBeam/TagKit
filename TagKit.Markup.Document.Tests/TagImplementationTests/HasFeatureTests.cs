using Xunit;

namespace TagKit.Markup.Tests.TagImplementationTests
{
    public class HasFeatureTests
    {
        // Feature supported by the TagImplementation
        private const string SupportedFeature = "XML";
        [Fact]
        public static void HasFeatureReturnsTrueForSupportedFeature()
        {
            var target = new TagImplementation();
            // verify that it's also case-insensitive
            Assert.True(target.HasFeature(SupportedFeature.ToUpper(), null));
            Assert.True(target.HasFeature(SupportedFeature.ToLower(), null));
        }
        [Fact]
        public static void HasFeatureReturnsFalseForUnsupportedFeature()
        {
            var target = new TagImplementation();
            Assert.False(target.HasFeature("Unsupported", null));
        }
    }
}
