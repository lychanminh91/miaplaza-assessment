using System.Text.RegularExpressions;
using Microsoft.Playwright.MSTest;

namespace PlaywrightTests;

[TestClass]
public class ExampleTest : PageTest
{

    public async Task SelectCustomDropdownOption(string dropdownSelector, string attributeValue)
    {
        await Page.WaitForSelectorAsync(dropdownSelector);
        await Page.ClickAsync(dropdownSelector);
        await Page.ClickAsync($"//li[@data-dial-code='{attributeValue}']");
    }

    [TestMethod]
    public async Task FillParentInformation()
    {
        await Page.GotoAsync("https://miacademy.co/#/");

        await Page.Locator("//a[contains(text(),\"Online High School\")]").IsVisibleAsync();
        await Page.GetByText("Online High School").ClickAsync();
        await Page.Locator("//a[contains(text(),\"Apply to Our School\")]").IsVisibleAsync();
        var applySchoolBtn = Page.Locator("//a[contains(text(),\"Apply to Our School\")]");
        await applySchoolBtn.ClickAsync();
        var nextBtn = Page.Locator("//button[@elname=\"next\"]").First;
        await nextBtn.ClickAsync();

        var firstNameField = Page.Locator("//input[@elname=\"First\" and @name=\"Name\"]");
        var lastNameField = Page.Locator("//input[@elname=\"Last\" and @name=\"Name\"]");
        var emailField = Page.Locator("#Email-arialabel");

        await firstNameField.FillAsync("Minh");
        await lastNameField.FillAsync("Minh");
        await emailField.FillAsync("Minh@minh.com");
        await Page.WaitForSelectorAsync("//div[@class=\"selected-flag\"]");

        await SelectCustomDropdownOption("//div[@class=\"flag-container\"]", "84");
        await Page.FillAsync("//input[@name=\"PhoneNumber\"]", "353333288");
        var secondParentDropdown = Page.Locator("//span[@class=\"select2-selection select2-selection--single select2FormCont\"]").First;
        await secondParentDropdown.ClickAsync();
        await Page.FillAsync("//input[@class=\"select2-search__field\"]", "No");
        await Page.Keyboard.DownAsync("Enter");
        await Page.ClickAsync("//span[@class=\"multiAttType cusChoiceSpan\"]");
        await Page.ClickAsync("//input[@id=\"Date-date\"]");
        await Page.Keyboard.PressAsync("Enter");
        await Page.WaitForTimeoutAsync(3000);
        await Page.ClickAsync("//ul[@page_no=\"2\"]//button[@elname=\"next\"]");

    }

}