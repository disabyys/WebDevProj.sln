using WebDevProj.Api.Models;
using Xunit;

namespace WebDevProj.Tests;

public class ValeraTests
{
    //Start state

    [Fact]
    public void NewValera_HasDefaultStats()
    {
        var valera = new Valera();

        Assert.Equal(100, valera.Health);
        Assert.Equal(0, valera.Alcohol);
        Assert.Equal(5, valera.Cheer);
        Assert.Equal(0, valera.Fatigue);
        Assert.Equal(100, valera.Money);
    }

    //Clamping stats

    [Theory]
    [InlineData(-50, 0)]      
    [InlineData(0, 0)]        
    [InlineData(50, 50)]     
    [InlineData(100, 100)]    
    [InlineData(500, 100)]    
    public void Health_ClampsToRange(int input, int expected)
    {
        var valera = new Valera();
        valera.Health = input;
        Assert.Equal(expected, valera.Health);
    }

    [Theory]
    [InlineData(-100, -10)]
    [InlineData(0, 0)]
    [InlineData(10, 10)]
    [InlineData(100, 10)]
    public void Cheer_ClampsToRange(int input, int expected)
    {
        var valera = new Valera();
        valera.Cheer = input;
        Assert.Equal(expected, valera.Cheer);
    }

    [Fact]
    public void Money_CannotGoNegative()
    {
        var valera = new Valera();
        valera.Money = -50;
        Assert.Equal(0, valera.Money);
    }


    //GoToWork 

    [Fact]
    public void GoToWork_WhenSoberAndRested_ChangesStats()
    {
        var valera = new Valera { Alcohol = 0, Fatigue = 0 };

        valera.GoToWork();

        Assert.Equal(0, valera.Cheer);   
        Assert.Equal(0, valera.Alcohol); 
        Assert.Equal(200, valera.Money);
        Assert.Equal(70, valera.Fatigue);
    }

    [Fact]
    public void GoToWork_WhenDrunk_Throws()
    {
        var valera = new Valera { Alcohol = 60, Fatigue = 0 };
        Assert.Throws<InvalidOperationException>(() => valera.GoToWork());
    }

    [Fact]
    public void GoToWork_WhenTired_Throws()
    {
        var valera = new Valera { Alcohol = 0, Fatigue = 15 };
        Assert.Throws<InvalidOperationException>(() => valera.GoToWork());
    }

    [Fact]
    public void GoToWork_WhenDrunk_DoesNotChangeStats()
    {
        var valera = new Valera { Alcohol = 60, Money = 100 };

        try { valera.GoToWork(); } catch (InvalidOperationException) { }

        Assert.Equal(60, valera.Alcohol);
        Assert.Equal(100, valera.Money);
    }

    //EnjoyNature

    [Fact]
    public void EnjoyNature_ChangesStats()
    {
        var valera = new Valera { Alcohol = 50, Cheer = 0, Fatigue = 0 };

        valera.EnjoyNature();

        Assert.Equal(1, valera.Cheer);
        Assert.Equal(40, valera.Alcohol);
        Assert.Equal(10, valera.Fatigue);
    }

    [Fact]
    public void EnjoyNature_WhenAlcoholZero_ClampsToZero()
    {
        var valera = new Valera { Alcohol = 0 };

        valera.EnjoyNature();

        Assert.Equal(0, valera.Alcohol);  
    }

    //DrinkVineWatchTv

    [Fact]
    public void DrinkVineWatchTv_ChangesStats()
    {
        var valera = new Valera { Money = 100, Health = 100, Cheer = 5 };

        valera.DrinkVineWatchTv();

        Assert.Equal(4, valera.Cheer);
        Assert.Equal(30, valera.Alcohol);
        Assert.Equal(10, valera.Fatigue);
        Assert.Equal(80, valera.Money);
        Assert.Equal(95, valera.Health);
    }

    //GoToBar

    [Fact]
    public void GoToBar_ChangesStats()
    {
        var valera = new Valera { Money = 200 };

        valera.GoToBar();

        Assert.Equal(6, valera.Cheer);
        Assert.Equal(60, valera.Alcohol);
        Assert.Equal(40, valera.Fatigue);
        Assert.Equal(90, valera.Health);
        Assert.Equal(100, valera.Money);
    }

    [Fact]
    public void GoToBar_WhenNoMoney_ClampsMoneyToZero()
    {
        var valera = new Valera { Money = 50 };

        valera.GoToBar();

        Assert.Equal(0, valera.Money);  
    }

    //DrinkWithMarginals

    [Fact]
    public void DrinkWithMarginals_ChangesStats()
    {
        var valera = new Valera { Money = 200, Health = 100 };

        valera.DrinkWithMarginals();

        Assert.Equal(10, valera.Cheer);   
        Assert.Equal(20, valera.Health); 
        Assert.Equal(90, valera.Alcohol);
        Assert.Equal(80, valera.Fatigue);
        Assert.Equal(50, valera.Money);  
    }

    //SignInSubway

    [Theory]
    [InlineData(0, 20)]   
    [InlineData(39, 20)]   
    [InlineData(50, 70)]   
    [InlineData(69, 70)]  
    [InlineData(70, 20)]  
    public void SignInSubway_MoneyBonusDependsOnAlcohol(int startAlcohol, int expectedMoney)
    {
        var valera = new Valera { Alcohol = startAlcohol, Money = 10 };

        valera.SignInSubway();

        Assert.Equal(expectedMoney, valera.Money);
    }

    [Fact]
    public void SignInSubway_AlwaysChangesCheerAndFatigue()
    {
        var valera = new Valera { Cheer = 5, Fatigue = 0 };

        valera.SignInSubway();

        Assert.Equal(6, valera.Cheer);
        Assert.Equal(20, valera.Fatigue);
    }

    //Sleep

    [Fact]
    public void Sleep_WhenSober_HealsHealth()
    {
        var valera = new Valera { Alcohol = 0, Health = 10, Fatigue = 80, Cheer = 5 };

        valera.Sleep();

        Assert.Equal(100, valera.Health);  
        Assert.Equal(0, valera.Alcohol);   
        Assert.Equal(10, valera.Fatigue); 
        Assert.Equal(5, valera.Cheer);    
    }

    [Fact]
    public void Sleep_WhenVeryDrunk_LosesCheerButNoHeal()
    {
        var valera = new Valera { Alcohol = 90, Health = 50, Fatigue = 80, Cheer = 5 };

        valera.Sleep();

        Assert.Equal(50, valera.Health);   
        Assert.Equal(2, valera.Cheer);     
        Assert.Equal(40, valera.Alcohol); 
        Assert.Equal(10, valera.Fatigue);  
    }

    [Fact]
    public void Sleep_WhenModeratelyDrunk_NoHealNoCheerLoss()
    {
        var valera = new Valera { Alcohol = 50, Health = 50, Cheer = 5 };

        valera.Sleep();

        Assert.Equal(50, valera.Health); 
        Assert.Equal(5, valera.Cheer);   
        Assert.Equal(0, valera.Alcohol);   
    }

    [Fact]
    public void Sleep_WhenExhaustedButDrunk_FatigueClampsToZero()
    {
        var valera = new Valera { Alcohol = 90, Fatigue = 30 };

        valera.Sleep();

        Assert.Equal(0, valera.Fatigue);  
    }
}
