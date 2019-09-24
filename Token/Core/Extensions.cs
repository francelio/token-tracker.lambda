using System;
using Alexa.NET;
using Alexa.NET.Response;
using Token.Models;

namespace Token.Core
{
  public static class Extensions
  {
    public static SkillResponse Tell(this string phrase)
    {
      SsmlOutputSpeech speech = new SsmlOutputSpeech();
      speech.Ssml = string.Format("<speak>{0}</speak>", phrase);
      SkillResponse speechResponse = ResponseBuilder.Tell(speech);
      return speechResponse;
    }

    public static string Mask(this string source)
    {
      return source.Mask('*');
    }

    public static string Mask(this string source, char maskCharacter)
    {
      int sourceLength = source.Length;
      int start = (int)Math.Ceiling((sourceLength / 2d) / 2d);
      int maskLength = sourceLength - start;

      if (start > source.Length - 1)
      {
        throw new ArgumentException("Start position is greater than string length");
      }

      if (maskLength > source.Length)
      {
        throw new ArgumentException("Mask length is greater than string length");
      }

      if (start + maskLength > source.Length)
      {
        throw new ArgumentException("Start position and mask length imply more characters than are present");
      }


      string mask = new string(maskCharacter, maskLength);
      string unMaskStart = source.Substring(0, start);

      return unMaskStart + mask;
    }

    public static TokenUser Clean(this TokenUser tokenUser)
    {
      return new TokenUser()
      {
        Id = tokenUser.Id,
        CreateDate = tokenUser.CreateDate,
        UpdateDate = tokenUser.UpdateDate,
        Players = tokenUser.Players,
        PasswordHash = tokenUser.PasswordHash != null ? tokenUser.PasswordHash.Mask() : null,
        HasPointsPersistence = tokenUser.HasPointsPersistence,
        TTL = tokenUser.TTL
      };
    }
  }
}