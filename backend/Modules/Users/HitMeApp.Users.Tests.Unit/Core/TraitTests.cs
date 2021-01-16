using HitMeApp.Users.Core;
using HitMeApp.Users.Core.Exceptions;
using Shouldly;
using Xunit;

namespace HitMeApp.Users.Tests.Unit.Core
{
    public class TraitTests
    {
        [Fact]
        public void given_empty_trait_value_trait_should_not_be_created()
        {
            var exception = Record.Exception(() => Trait.New(""));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidTraitException>();
        }

        [Fact]
        public void given_too_long_trait_value_trait_should_not_be_created()
        {
            var traitValue = "A really really long trait value";

            var exception = Record.Exception(() => Trait.New(traitValue));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidTraitException>();
        }

        [Fact]
        public void given_valid_trait_value_trait_should_be_created()
        {
            var traitValue = "test";

            var trait = Trait.New(traitValue);

            trait.ShouldNotBeNull();
            trait.Value.ShouldBe(traitValue);
        }

        [Fact]
        public void given_valid_not_formatted_trait_value_trait_should_be_created_with_lowered_and_trimmed_value()
        {
            var traitValue = "   Test ";

            var trait = Trait.New(traitValue);

            trait.ShouldNotBeNull();
            trait.Value.ShouldBe(traitValue.Trim().ToLowerInvariant());
        }
    }
}
