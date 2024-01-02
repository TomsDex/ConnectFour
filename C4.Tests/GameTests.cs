using C4_Game;

namespace TW_D2_Tests
{
    public class GameTests
    {
        Game testGame = new();

        [Fact]
        public void SpaceBelowIsEmpty_Returns_True_If_The_Space_Below_The_Parameters_Is_Empty()
        {
            //Arrange
            testGame.Board[0, 0] = 't';

            //Act

            //Assert

            
        }
    }
}
