using FluentResults;

namespace Domain.Entities;
public sealed class Voter
{
    public Guid Id { get; set; }

    public string FullName { get; }

    public ushort Age { get; }

    public bool IsCapable { get; }

    public byte[]? PublicKey { get; }

    public bool HasVoted { get; private set; }

    public Voter(string fullName, ushort age, bool isCapable)
    {
        FullName = fullName;
        Age = age;
        IsCapable = isCapable;
    }

    public Result IsAbleToVote()
    {
        if (Age < 18)
        {
            return Result.Fail($"Voter {FullName} must be 18 years old. Currently, they are {Age} years old.");
        }

        if (!IsCapable)
        {
            return Result.Fail($"Voter {FullName} is not capable.");
        }

        if (HasVoted)
        {
            return Result.Fail($"Voter {FullName} has already casted their vote.");
        }

        return Result.Ok();
    }

    public void MarkAsVoted()
    {
        HasVoted = true;
    }
}
