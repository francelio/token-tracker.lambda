using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Token.BusinessLogic;
using Token.BusinessLogic.Interfaces;
using Token.Data.Interfaces;
using Xunit;
using System.Linq;
using System;

namespace Token.Tests.BusinessLogic
{
  public class RequestBusinessLogicTests
  {
    [Fact]
    public void Ctor_ShouldReturnInstanceOfClass_WhenInputIsValid()
    {
      Mock<ILogger<RequestBusinessLogic>> mockLogger = new Mock<ILogger<RequestBusinessLogic>>();
      List<Mock<IRequestRouter>> mockRequestRouters = new List<Mock<IRequestRouter>>();
      mockRequestRouters.Add(new Mock<IRequestRouter>());

      Mock<ITokenUserData> mockTokenUserData = new Mock<ITokenUserData>();

      RequestBusinessLogic sut = new RequestBusinessLogic(mockLogger.Object, mockRequestRouters.Select(x => x.Object), mockTokenUserData.Object);
      Assert.IsType<RequestBusinessLogic>(sut);
    }

    [Fact]
    public void Ctor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
    {
      List<Mock<IRequestRouter>> mockRequestRouters = new List<Mock<IRequestRouter>>();
      mockRequestRouters.Add(new Mock<IRequestRouter>());

      Mock<ITokenUserData> mockTokenUserData = new Mock<ITokenUserData>();      
      Assert.Throws<ArgumentNullException>(() => new RequestBusinessLogic(null, mockRequestRouters.Select(x => x.Object), mockTokenUserData.Object));
    }

    [Fact]
    public void Ctor_ShouldThrowArgumentNullException_WhenInputRequestRoutersIsNull()
    {
      Mock<ILogger<RequestBusinessLogic>> mockLogger = new Mock<ILogger<RequestBusinessLogic>>();
      Mock<ITokenUserData> mockTokenUserData = new Mock<ITokenUserData>();

      Assert.Throws<ArgumentNullException>(() => new RequestBusinessLogic(mockLogger.Object, null, mockTokenUserData.Object));
    }

    [Fact]
    public void Ctor_ShouldThrowArgumentNullException_WhenRequestRoutersIsEmpty()
    {
      Mock<ILogger<RequestBusinessLogic>> mockLogger = new Mock<ILogger<RequestBusinessLogic>>();
      List<Mock<IRequestRouter>> mockRequestRouters = new List<Mock<IRequestRouter>>();

      Mock<ITokenUserData> mockTokenUserData = new Mock<ITokenUserData>();

      Assert.Throws<ArgumentNullException>(() => new RequestBusinessLogic(mockLogger.Object, mockRequestRouters.Select(x => x.Object), mockTokenUserData.Object));
    }

    [Fact]
    public void Ctor_ShouldThrowArgumentNullException_WhenTokenDataIsNull()
    {
      Mock<ILogger<RequestBusinessLogic>> mockLogger = new Mock<ILogger<RequestBusinessLogic>>();
      List<Mock<IRequestRouter>> mockRequestRouters = new List<Mock<IRequestRouter>>();
      mockRequestRouters.Add(new Mock<IRequestRouter>());
      Assert.Throws<ArgumentNullException>(() => new RequestBusinessLogic(mockLogger.Object, mockRequestRouters.Select(x => x.Object), null));
    }
  }
}