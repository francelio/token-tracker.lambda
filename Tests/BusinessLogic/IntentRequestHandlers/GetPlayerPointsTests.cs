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
  public class GetPlayerPointsTests
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
      Mock<ILogger<GetPlayerPoints>> mockLogger = new Mock<ILogger<GetPlayerPoints>>();
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();

      GetPlayerPoints sut = new GetPlayerPoints(mockLogger.Object, mockSkillRequestValidator.Object);

      Assert.IsType<GetPlayerPoints>(sut);
    }

    [Fact]  
    public void Ctor_ShouldThrowArgumentNullException_WhenLoggerIsNull()
    {
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();

      Assert.Throws<ArgumentNullException>(() => new GetPlayerPoints(null, mockSkillRequestValidator.Object));
    }

    [Fact]  
    public void Ctor_ShouldThrowArgumentNullException_WhenSkillRequestValidatorIsNull()
    {
      Mock<ILogger<GetPlayerPoints>> mockLogger = new Mock<ILogger<GetPlayerPoints>>();
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();

      Assert.Throws<ArgumentNullException>(() => new GetPlayerPoints(mockLogger.Object, null));
    }

    [Fact]
    public void Handle_ShouldReturnSkillResponse_WhenCalled()
    {
      Mock<ILogger<GetPlayerPoints>> mockLogger = new Mock<ILogger<GetPlayerPoints>>();
      
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();
      mockSkillRequestValidator.Setup(x => x.IsValid(It.IsAny<SkillRequest>())).Returns(true);
      
      GetPlayerPoints sut = new GetPlayerPoints(mockLogger.Object, mockSkillRequestValidator.Object);

      Assert.IsType<GetPlayerPoints>(sut);
    }

    [Fact]
    public void Handle_ShouldReturnSkillResponse_WhenCalledWithValidInputs()
    {
      Mock<ILogger<GetPlayerPoints>> mockLogger = new Mock<ILogger<GetPlayerPoints>>();
      
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();
      mockSkillRequestValidator.Setup(x => x.IsValid(It.IsAny<SkillRequest>())).Returns(true);
      
      GetPlayerPoints sut = new GetPlayerPoints(mockLogger.Object, mockSkillRequestValidator.Object);

      SkillRequest skillRequest = GenerateValidSkillRequest(new IntentRequest()
      { 
        RequestId ="TestRequestId", 
        Locale = "en-US", 
        Type = "IntentRequest", 
        Intent = new Intent() 
        { 
          ConfirmationStatus = "CONFIRMED",
          Name = "GetPlayerPoints",
          Slots = new Dictionary<string, Slot>()
          {
            {
              "player", 
              new Slot()
              {
                Name = "player",
                Value = "blue",
                ConfirmationStatus = "NONE"
              }
            }
          }
        } 
      });

      TokenUser tokenUser = new TokenUser() { Players = new List<Player>() };

      SkillResponse skillResponse = sut.Handle(skillRequest, tokenUser);

      Assert.IsType<SkillResponse>(skillResponse);
    }

    [Fact]
    public void Handle_ShouldThrowArgumentNullException_WhenCalledWithInvalidSkillRequest()
    {
      Mock<ILogger<GetPlayerPoints>> mockLogger = new Mock<ILogger<GetPlayerPoints>>();
      
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();
      mockSkillRequestValidator.Setup(x => x.IsValid(It.IsAny<SkillRequest>())).Returns(false);
      
      GetPlayerPoints sut = new GetPlayerPoints(mockLogger.Object, mockSkillRequestValidator.Object);

      TokenUser tokenUser = new TokenUser() { Players = new List<Player>() };

      SkillRequest skillRequest = new SkillRequest();

      Assert.Throws<ArgumentNullException>(() => sut.Handle(skillRequest, tokenUser));
    }

    [Fact]
    public void Handle_ShouldThrowArgumentNullException_WhenTokenUserIsNull()
    {
      Mock<ILogger<GetPlayerPoints>> mockLogger = new Mock<ILogger<GetPlayerPoints>>();
      
      Mock<ISkillRequestValidator> mockSkillRequestValidator = new Mock<ISkillRequestValidator>();
      mockSkillRequestValidator.Setup(x => x.IsValid(It.IsAny<SkillRequest>())).Returns(true);
      
      GetPlayerPoints sut = new GetPlayerPoints(mockLogger.Object, mockSkillRequestValidator.Object);

      SkillRequest skillRequest = new SkillRequest();

      Assert.Throws<ArgumentNullException>(() => sut.Handle(skillRequest, null));
    }
  }
}