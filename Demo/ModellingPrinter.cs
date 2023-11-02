using Algorithms.Abstractions;
using FluentResults;
using Modelling.Extensions;
using Modelling.Models;
using Modelling.Models.Ballots;
using static Demo.UtilityMethods;

namespace Demo;
public sealed class ModellingPrinter
{
    private readonly IEncryptionProvider _encryptionProvider;

    private readonly IAsymmetricKeysGenerator _asymmetricKeysGenerator;
    private readonly ISignatureProvider _signatureProvider;

    private readonly IObjectToByteArrayTransformer _objectToByteArrayTransformer;

    private readonly IRandomProvider _randomProvider;

    public ModellingPrinter(IEncryptionProvider encryptionProvider, IAsymmetricKeysGenerator asymmetricKeysGenerator, ISignatureProvider signatureProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer, IRandomProvider randomProvider)
    {
        _encryptionProvider = encryptionProvider;
        _asymmetricKeysGenerator = asymmetricKeysGenerator;
        _signatureProvider = signatureProvider;
        _objectToByteArrayTransformer = objectToByteArrayTransformer;
        _randomProvider = randomProvider;
    }

    public void PrintUsualVoting(CentralElectionCommission commission, IReadOnlyList<Candidate> candidates, IReadOnlyList<Voter> voters)
    {
        EncryptedSignedBallot ballot;
        Result result;
        foreach (var voter in voters)
        {
            var candidateId = _randomProvider.NextItem(candidates).Id;
            ballot = voter.GenerateBallot(candidateId, commission.BallotEncryptionKey, _signatureProvider, _encryptionProvider, _objectToByteArrayTransformer);
            result = commission.AcceptBallot(ballot, _signatureProvider, _encryptionProvider, _objectToByteArrayTransformer);

            if (result.IsSuccess)
            {
                Console.WriteLine($"{voter.FullName} voted successfully.");
            }
            else
            {
                PrintError(result);
            }
        }
    }

    public void PrintVotingWithIncorrectBallot(CentralElectionCommission commission)
    {
        var ballot = new EncryptedSignedBallot(new byte[] { 4, 6, 8, 0 });
        var result = commission.AcceptBallot(ballot, _signatureProvider, _encryptionProvider, _objectToByteArrayTransformer);

        if (!result.IsSuccess)
        {
            PrintError(result);
        }
    }

    public void PrintVotingWithBallotSignedByThirdParty(CentralElectionCommission commission, int candidateId, int voterId)
    {
        var keys = _asymmetricKeysGenerator.GenerateKeys();

        var ballot = new Ballot(voterId, candidateId);

        var ballotAsByteArray = _objectToByteArrayTransformer.Transform(ballot);
        var signedBallot = new SignedBallot(ballot, _signatureProvider.Sign(ballotAsByteArray, keys.PrivateKey), keys.PublicKey);

        var signedBallotAsByteArray = _objectToByteArrayTransformer.Transform(signedBallot);
        var encryptedSignedBallot = new EncryptedSignedBallot(_encryptionProvider.Encrypt(signedBallotAsByteArray, commission.BallotEncryptionKey));

        var result = commission.AcceptBallot(encryptedSignedBallot, _signatureProvider, _encryptionProvider, _objectToByteArrayTransformer);

        if (!result.IsSuccess)
        {
            PrintError(result);
        }
    }

    public void PrintVotingWithDoubleBallot(CentralElectionCommission commission, int candidateId, Voter voter)
    {
        var ballot = voter.GenerateBallot(candidateId, commission.BallotEncryptionKey, _signatureProvider, _encryptionProvider, _objectToByteArrayTransformer);

        var result = commission.AcceptBallot(ballot, _signatureProvider, _encryptionProvider, _objectToByteArrayTransformer);

        if (!result.IsSuccess)
        {
            PrintError(result);
        }
    }

    public void PrintVotingResults(CentralElectionCommission commission)
    {
        Console.WriteLine("Results:");
        var results = commission.VotingResults.CandidatesResults.OrderByVotes().ToList();
        foreach (var candidate in results)
        {
            Console.WriteLine($"{candidate.Candidate.FullName} (id: {candidate.Candidate.Id}): {candidate.Votes} votes");
        }
    }
}
