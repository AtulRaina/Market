using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecFlow
{
    [Binding]
    public class SpecFlowFeature1Steps:Steps
    {
        public class UserData
        {
            public string  MyProperty { get; set; }
            public int Property { get; set; }
            public void SetMyProperty(string myProperty) { MyProperty = myProperty; }
        }
        readonly UserData testData = new UserData();
        public SpecFlowFeature1Steps(UserData userdata)
        {
            testData = userdata;
        }
        [Given(@"I have entered (.*) into the calculator")]
        [Given(@"i have entered a number which is negative (.*)")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            testData.Property = p0;
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            ScenarioContext.Current["number2"] = p0;   
        }



        [Given(@"I have entered two numbers (.*) and (.*)")]
        [Scope(Feature = "SpecFlowFeature1")]
        [Scope(Tag ="mytag", Feature = "SpecFlowFeature1")]
        public void GivenIHaveEnteredTwoNumbersAnd(int p0, int p1)
        {
            When(string.Format("I have entered {0} into the calculator", p0));
            When(string.Format("i have entered a number which is negative {0}",p1));

        }




    }
}
