using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace CsvConverter.Tests;

public class MockBase<T> where T : class
{
    protected AutoMocker Mocker { get; private set; } = null!;
    
    protected T Subject { get; private set; } = null!;

    [SetUp]
    public void BaseSetup()
    {
        Mocker = new AutoMocker();
        
        Setup();
        
        Subject = Mocker.CreateInstance<T>();
    }

    protected virtual void Setup()
    {
    }

    protected Mock<TMock> GetMock<TMock>() where TMock : class
    {
        return Mocker.GetMock<TMock>();
    }
}