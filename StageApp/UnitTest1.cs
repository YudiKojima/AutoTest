using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace StageApp
{
    public class Tests
    {
        public IWebDriver? driver;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSignUp()
        {
            driver = new ChromeDriver();
            // gerar e armazenar email
            driver.Navigate().GoToUrl("https://www.invertexto.com/gerador-email-temporario");
            driver.Manage().Window.Maximize();
            IWebElement emailElement = driver.FindElement(By.Id("email-input"));
            string email = emailElement.GetAttribute("value");

            // gerar e armazenar cnpj
            driver.Navigate().GoToUrl("https://www.invertexto.com/gerador-de-cnpj");
            driver.FindElement(By.Id("gerar")).Click();
            IWebElement cnpjElement = driver.FindElement(By.Id("cnpj"));
            var cnpj = cnpjElement.GetAttribute("value");

            // criar uma conta no stageApp
            driver.Navigate().GoToUrl("https://stage-app.spedy.com.br/signup");
            driver.FindElement(By.Name("name")).SendKeys("teste auto");
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phoneNumber")).SendKeys("66666666666");
            driver.FindElement(By.Name("password")).SendKeys("teste123");
            driver.FindElement(By.Name("checkbox")).Click();

            driver.FindElement(By.XPath("//*[@id=\'root\']/div/form/button")).Click();

            // esperar botão aparecer
            WebDriverWait waitCompanyButton = new(driver, TimeSpan.FromSeconds(10));
            IWebElement companyButton = waitCompanyButton.Until(ExpectedConditions.ElementIsVisible(By.XPath
                ("/html/body/div[3]/div/div[2]/div/div[2]/div/div/div/div[2]/button[2]")));
            companyButton.Click();

            // preencher campo empresa
            driver.FindElement(By.Id("federalTaxNumber")).SendKeys(cnpj);
            Thread.Sleep(1000);
            driver.FindElement(By.Id("legalName")).SendKeys("teste");
            driver.FindElement(By.Id("name")).SendKeys("teste");
            driver.FindElement(By.Id("address_postalCode")).SendKeys("78068670");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("address_number")).SendKeys("123");

            driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div[2]/div/div[2]/div/button[2]")).Click();

            WebDriverWait waitSubscriptionButton = new(driver, TimeSpan.FromSeconds(10));
            IWebElement subscriptionButton = waitSubscriptionButton.Until(ExpectedConditions.ElementIsVisible(By.XPath
                ("/html/body/div[2]/div/div[2]/div/div[2]/div/div[2]/div[2]/div[1]/div/div/div[2]/button")));
            subscriptionButton.Click();

            WebDriverWait waitDashboardButton = new(driver, TimeSpan.FromSeconds(10));
            IWebElement dashboardButton = waitDashboardButton.Until(ExpectedConditions.ElementIsVisible(By.XPath
                ("/html/body/div[3]/div/div[2]/div/div[2]/div/div/div[2]/button")));
            dashboardButton.Click();
        }

        [Test]
        public void TestLogin()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.invertexto.com/gerador-de-cnpj");
            driver.Manage().Window.Maximize();

            // gerar e armazenar cnpj
            driver.FindElement(By.Id("gerar")).Click();
            IWebElement cnpjElement = driver.FindElement(By.Id("cnpj"));
            var cnpj = cnpjElement.GetAttribute("value");

            // login
            driver.Navigate().GoToUrl("https://stage-app.spedy.com.br/signin");
            driver.FindElement(By.Name("email")).SendKeys("fanuta8601@uorak.com");
            driver.FindElement(By.Name("password")).SendKeys("teste123");

            driver.FindElement(By.XPath("//*[@id=\'root\']/div/form/button")).Click();

            // esperar botão aparecer
            WebDriverWait waitCompanyButton = new(driver, TimeSpan.FromSeconds(10));
            IWebElement companyButton = waitCompanyButton.Until(ExpectedConditions.ElementIsVisible(By.XPath
                ("/html/body/div[2]/div/div[2]/div/div[2]/div/div/div/div[2]/button[2]")));
            companyButton.Click();

            // preencher campo empresa
            driver.FindElement(By.Id("federalTaxNumber")).SendKeys(cnpj);
            Thread.Sleep(2000);
            driver.FindElement(By.Id("legalName")).SendKeys("teste");
            driver.FindElement(By.Id("name")).SendKeys("teste");
            driver.FindElement(By.Id("address_postalCode")).SendKeys("78068670");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("address_number")).SendKeys("123");

            driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div[2]/div/div[2]/div/button[2]")).Click();

            WebDriverWait waitSubscriptionButton = new(driver, TimeSpan.FromSeconds(10));
            IWebElement subscriptionButton= waitSubscriptionButton.Until(ExpectedConditions.ElementIsVisible(By.XPath
                ("/html/body/div[2]/div/div[2]/div/div[2]/div/div[2]/div[2]/div[1]/div/div/div[2]/button")));
            subscriptionButton.Click();
            
            WebDriverWait waitDashboardButton = new(driver, TimeSpan.FromSeconds(10));
            IWebElement dashboardButton= waitDashboardButton.Until(ExpectedConditions.ElementIsVisible(By.XPath
                ("/html/body/div[3]/div/div[2]/div/div[2]/div/div/div[2]/button")));
            dashboardButton.Click();
        }
    }
}