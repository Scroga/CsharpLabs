using System;

namespace MultipleJustification;

public interface ITokenProcessor
{
    void ProcessToken(Token token);
    void WriteResult();
}
