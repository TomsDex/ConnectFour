using C4_Game;

namespace TW_D2_Tests
{
    public class PlayerTests
    {
        Player playerOne = new(true);
        Player playerTwo = new(false);

        [Fact]
        public void DetermineIfPlayerOne_Returns_Same_Value_As_Is_Passed_In()
        {
            //Act and assert
            Assert.True(playerOne.IsPlayerOne);
            Assert.False(playerTwo.IsPlayerOne);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(6)]
        public void Checks_A_Valid_Column_Number_Returns_As_A_Valid_User_Input(byte columnNo)
        {
            //Assert
            Assert.InRange(columnNo, 0, 6);
        }
    }
}