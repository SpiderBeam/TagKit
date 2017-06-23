using Xunit;

namespace TagKit.Markup.Tests
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
        [Fact]
        public static void HasFeatureReturnsTrueForSupportedVersion()
        {
            var target = new TagImplementation();
            Assert.True(target.HasFeature(SupportedFeature, null));
            Assert.True(target.HasFeature(SupportedFeature, "1.0"));
            Assert.True(target.HasFeature(SupportedFeature, "2.0"));
        }
        [Fact]
        public static void HasFeatureReturnsFalseForUnsupportedVersion()
        {
            var target = new TagImplementation();
            Assert.False(target.HasFeature(SupportedFeature, "1.2"));
        }
    }
}
