using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace StageApp
{
    public class Tests
    {
        public IWebDriver driver;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            driver = new ChromeDriver();
            // gerar email
            driver.Navigate().GoToUrl("https://www.invertexto.com/gerador-email-temporario");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Id("copiar")).Click();

            // criar uma conta no stageApp
            driver.Navigate().GoToUrl("https://stage-app.spedy.com.br/signup");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Name("name")).SendKeys("teste auto");
            driver.FindElement(By.Name("email")).SendKeys(Keys.Control + "v");
            driver.FindElement(By.Name("phoneNumber")).SendKeys("66666666666");
            driver.FindElement(By.Name("password")).SendKeys("teste123");
            driver.FindElement(By.Name("checkbox")).Click();

        }
    }
}