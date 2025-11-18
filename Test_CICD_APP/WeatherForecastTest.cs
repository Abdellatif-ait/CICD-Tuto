namespace Test_CICD_APP
{
    public class WeatherForecastTest
    {
        [Fact]
        public void Test_Sucess()
        {
            Assert.Equal(5, 5);
        }

        [Fact]
        public void Test_Failed()
        {
            Assert.Equal(5, 4);
        }
    }
}