using StockKangourou;
using Microsoft.EntityFrameworkCore;
using Telerik.JustMock;

namespace TestProject1
{
    public class UnitTest1
    {
        DbContextOptions<MyContext> _options = new DbContextOptionsBuilder<MyContext>()
            .UseInMemoryDatabase(databaseName: "KangourouListDatabase")
            .Options;

        public UnitTest1()
        {
            using (var context = new MyContext(_options))
            {
                var mockDataService = Mock.Create<IDataService>();
                Mock.Arrange(() => mockDataService.DisplayMessage("")).IgnoreArguments()
                    .DoNothing();

                context.Kangourous.RemoveRange(context.Kangourous);
                context.SaveChanges();

                context.Kangourous.Add(new Kangourou { codeKangourou = "000A1", nomKangourou = "Laurent", poidsKangourou = 177, tailleKangourou = 2.4, raceKangourou = "Clara" });
                context.Kangourous.Add(new Kangourou { codeKangourou = "000A2", nomKangourou = "Paolo", poidsKangourou = 2, tailleKangourou = 71, raceKangourou = "Pétrogale" });
                context.Kangourous.Add(new Kangourou { codeKangourou = "000A3", nomKangourou = "Débile", poidsKangourou = 14.8, tailleKangourou = 87, raceKangourou = "Rat Kangourou" });
                context.SaveChanges();
            }
        }

        [WpfFact]
        public void Test_ShouldCountVoyages()
        {
            //Arrange
            var context = new MyContext(_options);
            var _sut = new StockKangourou.MainWindow(new DataServiceEF(null,context));

            //Act

            //Assert
            Assert.Equal(3, _sut.Kangourous.Count);
        }

        [WpfFact]
        public void Test_SouldValidateNumeroAdd()
        {
            //Arrange
            var context = new MyContext(_options);
            var _sut = new StockKangourou.MainWindow(new DataServiceEF(null, context));

            //Act
            _sut.TheKangourou.codeKangourou = "000A1";
            _sut.TheKangourou.nomKangourou = "Laurent";
            _sut.TheKangourou.raceKangourou = "Pétrogale";

            _sut.Stocker_Click(null, null);

            //Assert
            Assert.Equal("TTT0001", _sut.Kangourous.ElementAt(3).codeKangourou);
            Assert.Equal(4, _sut.Kangourous.Count);
        }

        [WpfFact]
        public void Test_SouldNotValidateNumeroAdd()
        {
            //Arrange
            var context = new MyContext(_options);
            var _sut = new StockKangourou.MainWindow(new DataServiceEF(null, context));

            //Act
            _sut.TheKangourou.codeKangourou = "TTT01";
            _sut.TheKangourou.nomKangourou = "Laurent";
            _sut.TheKangourou.raceKangourou = "Pétrogale";

            _sut.Stocker_Click(null, null);

            //Assert
            Assert.Equal(3, _sut.Kangourous.Count);
        }
    }
}