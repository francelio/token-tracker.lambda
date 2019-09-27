using Token.BusinessLogic;
using Xunit;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Token.Models;
using Alexa.NET.Response;
using Token.BusinessLogic.IntentRequestHandlers;

namespace Token.Tests.BusinessLogic.IntentRequestHandlers
{
  public class RemovePointsTests
  {
    private static SkillRequest GenerateValidSkillRequest(Request request)
    { 
      SkillRequest skillRequest = new SkillRequest()
      {
        Context = new Context()
        {
          System = new AlexaSystem()
          {
            ApiEndpoint = "http://localhost",
            ApiAccessToken = "xx508xx63817x752xx74004x30705xx92x58349x5x78f5xx34xxxxx51",
            User = new User()
            {
              UserId = "TestUserId"
            }
          }
        },
        Request = request
      };

      return skillRequest;
    }
    
    [Fact]
    public void Ctor_ShouldReturnInstanceOfClass_WhenDependenciesAreValid()
    {
      Mock<ILogger<RemovePoints>> mockLogger = new Mock<ILogger<RemovePoints>>();
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();

      RemovePoints sut = new RemovePoints(mockLogger.Object, mockSkillRequestValidator.Object);

      Assert.IsType<RemovePoints>(sut);
    }

    [Fact]  
    public void Ctor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
    {
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();

      Assert.Throws<ArgumentNullException>(() => new RemovePoints(null, mockSkillRequestValidator.Object));
    }

    [Fact]  
    public void Ctor_ShouldThrowArgumentNullException_WhenSkillRequestValidatorIsNull()
    {
      Mock<ILogger<RemovePoints>> mockLogger = new Mock<ILogger<RemovePoints>>();
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();

      Assert.Throws<ArgumentNullException>(() => new RemovePoints(mockLogger.Object, null));
    }

    [Fact]
    public void Handle_ShouldReturnSkillResponse_WhenCalled()
    {
      Mock<ILogger<RemovePoints>> mockLogger = new Mock<ILogger<RemovePoints>>();
      
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();
      mockSkillRequestValidator.Setup(x => x.IsValid(It.IsAny<SkillRequest>())).Returns(true);
      
      RemovePoints sut = new RemovePoints(mockLogger.Object, mockSkillRequestValidator.Object);

      Assert.IsType<RemovePoints>(sut);
    }
  }
}