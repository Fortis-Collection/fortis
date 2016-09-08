using System;
using System.Linq;
using Fortis.Model.Fields;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// Test Methods Syntax: [Testing method/property name]_[given condition/input parameters]_[Expected behavior]
    /// </summary>
    /// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.NameValueListFieldWrapper}" />
    public class NameValueListFieldWrapperTests : FieldWrapperTestClass<NameValueListFieldWrapper>
    {
        [Fact]
        public void Value_ValidRawValue_AllKeysCorrespondsToValues()
        {
            this.Field.Value = "key1=value1&key2=value2";

            var actual = this.FieldWrapper.Value;

            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
            Assert.Equal("value1", actual["key1"]);
            Assert.Equal("value2", actual["key2"]);
        }

        [Fact]
        public void Value_EmptyRawValue_ReturnsEmptyCollection()
        {
            this.Field.Value = string.Empty;

            var actual = this.FieldWrapper.Value;

            Assert.NotNull(actual);
            Assert.Equal(0, actual.Count);
        }


        [Fact]
        public void Value_KeyWithoutValue_KeyWithoutValueGivesNull()
        {
            this.Field.Value = "key1=value1&key2";

            var actual = this.FieldWrapper.Value;

            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
            Assert.Equal("value1", actual["key1"]);
            Assert.False(actual.AllKeys.Contains("key2"));
            Assert.Equal(null, actual["key2"]);
        }

        [Fact]
        public void HasValue_EmptyRawValue_ReturnsFalse()
        {
            this.Field.Value = string.Empty;

            var actual = this.FieldWrapper.HasValue;

            Assert.False(actual);
        }

        [Fact]
        public void HasValue_ValidRawValue_ReturnsTrue()
        {
            this.Field.Value = "key1=value1&key2=value2";

            var actual = this.FieldWrapper.HasValue;

            Assert.True(actual);
        }

        [Fact]
        public void HasValue_SingleKeyWithoutValue_ReturnsTrue()
        {
            this.Field.Value = "key1";

            var actual = this.FieldWrapper.HasValue;

            Assert.True(actual);
        }
    }
}
