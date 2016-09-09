using System.Linq;
using Fortis.Model.Fields;
using Sitecore.Data;
using Xunit;

namespace Fortis.Tests.Model.Fields
{
    /// <summary>
    /// Test Methods Syntax: [Testing method/property name]_[given condition/input parameters]_[Expected behavior]
    /// </summary>
    /// <seealso cref="Fortis.Tests.Model.Fields.FieldWrapperTestClass{Fortis.Model.Fields.ListFieldWrapper}" />
    public class ListFieldWrapperTests : FieldWrapperTestClass<ListFieldWrapper>
    {
        protected readonly ID Id1 = ID.NewID;
        protected readonly ID Id2 = ID.NewID;
        protected readonly ID Id3 = ID.NewID;

        [Fact]
        public void Value_EmptyRawValue_ReturnsEmptyEnumerable()
        {
            this.Field.Value = string.Empty;

            var actual = this.FieldWrapper.Value;

            Assert.NotNull(actual);
            Assert.Equal(0, actual.Count());
        }

        [Fact]
        public void Value_EmptyOrInvalidIds_ReturnsEmptyEnumerable()
        {
            this.Field.Value = "|this-is-not-valid-guid||||";

            var actual = this.FieldWrapper.Value;

            Assert.NotNull(actual);
            Assert.Equal(0, actual.Count());
        }

        [Fact]
        public void Value_ValidRawIdList_ReturnsExpectedGuidEnumerable()
        {
            this.Field.Value = string.Join("|", this.Id1, this.Id2, this.Id3);

            var actual = this.FieldWrapper.Value;

            Assert.NotNull(actual);
            Assert.Equal(3, actual.Count());
            Assert.Equal(this.Id1.Guid, actual.ElementAt(0));
            Assert.Equal(this.Id2.Guid, actual.ElementAt(1));
            Assert.Equal(this.Id3.Guid, actual.ElementAt(2));
        }

        [Fact]
        public void Value_ContainsValidAndInvalidIds_InvalidIdIsNotAddedIntoEnumerable()
        {
            this.Field.Value = string.Join("|", this.Id1, "this-is-not-valid-guid", this.Id2);

            var actual = this.FieldWrapper.Value;

            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count());
            Assert.Equal(this.Id1.Guid, actual.ElementAt(0));
            Assert.Equal(this.Id2.Guid, actual.ElementAt(1));
        }

        [Fact]
        public void HasValue_EmptyRawValue_ReturnsFalse()
        {
            this.Field.Value = string.Empty;

            var actual = this.FieldWrapper.HasValue;

            Assert.False(actual);
        }

        [Fact]
        public void HasValue_EmptyOrInvalidIds_ReturnsFalse()
        {
            this.Field.Value = "|this-is-not-valid-guid||||";

            var actual = this.FieldWrapper.HasValue;

            Assert.False(actual);
        }

        [Fact]
        public void HasValue_ValidRawIdList_ReturnsTrue()
        {
            this.Field.Value = string.Join("|", this.Id1, this.Id2, this.Id3);

            var actual = this.FieldWrapper.HasValue;

            Assert.True(actual);
        }

        [Fact]
        public void HasValue_ContainsValidAndInvalidIds_ReturnsTrue()
        {
            this.Field.Value = string.Join("|", this.Id1, "this-is-not-valid-guid", this.Id2);

            var actual = this.FieldWrapper.HasValue;

            Assert.True(actual);
        }
    }
}
