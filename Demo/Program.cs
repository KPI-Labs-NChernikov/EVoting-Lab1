using Algorithms.Abstractions;
using Algorithms.Common;
using Algorithms.RSASignature;
using Algorithms.XorEncryption;
using FluentResults;
using Modelling.Extensions;
using Modelling.Models;
using Modelling.Models.Ballots;

var xorKeyGenerator = new XorEncryptionKeyGenerator();
var xorEncryptionProvider = new XorEncryptionProvider();
var rsaKeysGenerator = new RSASignatureKeysGenerator();
var rsaSignatureProvider = new RSASignatureProvider();
var objectToByteArrayTransformer = new ObjectToByteArrayTransformer();
var random = new Random();
var candidates = new List<Candidate>
{
    new Candidate(1, "Ishaan Allison"),
    new Candidate(2, "Oliver Mendez"),
    new Candidate(3, "Naomi Winter"),
};
var voters = new List<Voter>
{
    new Voter(1, "Jasper Lambert", 17, true, rsaKeysGenerator),     // Too young.
    new Voter(2, "Jonty Levine", 22, false, rsaKeysGenerator),      // Not capable.
    new Voter(3, "Nathaniel Middleton", 35, true, rsaKeysGenerator),
    new Voter(4, "Nathan Bass", 43, true, rsaKeysGenerator),
    new Voter(5, "Aran Doyle", 21, true, rsaKeysGenerator),
    new Voter(6, "Julian Harper", 19, true, rsaKeysGenerator),
    new Voter(7, "Lucian Gross", 20, true, rsaKeysGenerator),
};
var commission = new CentralElectionCommission(candidates, voters, xorKeyGenerator);

EncryptedSignedBallot ballot;
Result result;
foreach (var voter in voters)
{
    var candidateId = candidates[random.Next(0, candidates.Count)].Id;
    ballot = voter.GenerateBallot(candidateId, commission.BallotEncryptionKey, rsaSignatureProvider, xorEncryptionProvider, objectToByteArrayTransformer);
    result = commission.AcceptBallot(ballot, rsaSignatureProvider, xorEncryptionProvider, objectToByteArrayTransformer);

    if (result.IsSuccess)
    {
        Console.WriteLine($"{voter.FullName} voted successfully.");
    }
    else
    {
        PrintError(result);
    }
}
SendIncorrectBallot(commission, rsaSignatureProvider, xorEncryptionProvider, objectToByteArrayTransformer);
SendBallotSignedByThirdParty(
    voters[random.Next(0, voters.Count)].Id, 
    candidates[random.Next(0, candidates.Count)].Id,
    commission,
    rsaKeysGenerator,
    rsaSignatureProvider, 
    xorEncryptionProvider, 
    objectToByteArrayTransformer);
SendDoubleBallot(
    voters[3],
    candidates[random.Next(0, candidates.Count)].Id,
    commission,
    rsaSignatureProvider,
    xorEncryptionProvider,
    objectToByteArrayTransformer);

Console.WriteLine();

Console.WriteLine("Results:");
var results = commission.VotingResults.CandidatesResults.OrderByVotes();
foreach (var candidate in results)
{
    Console.WriteLine($"{candidate.Candidate.FullName} (id: {candidate.Candidate.Id}): {candidate.Votes} votes");
}

Console.WriteLine();

static void SendIncorrectBallot(CentralElectionCommission commission, ISignatureProvider signatureProvider, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
{
    var ballot = new EncryptedSignedBallot(new byte[] {4,6,8,0});
    var result = commission.AcceptBallot(ballot, signatureProvider, encryptionProvider, objectToByteArrayTransformer);

    if (!result.IsSuccess)
    {
        PrintError(result);
    }
}

static void SendBallotSignedByThirdParty(int voterId, int candidateId, CentralElectionCommission commission, IAsymmetricKeysGenerator keysGenerator, ISignatureProvider signatureProvider, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
{
    var keys = keysGenerator.GenerateKeys();

    var ballot = new Ballot(voterId, candidateId);

    var ballotAsByteArray = objectToByteArrayTransformer.Transform(ballot);
    var signedBallot = new SignedBallot(ballot, signatureProvider.Sign(ballotAsByteArray, keys.PrivateKey), keys.PublicKey);

    var signedBallotAsByteArray = objectToByteArrayTransformer.Transform(signedBallot);
    var encryptedSignedBallot = new EncryptedSignedBallot(encryptionProvider.Encrypt(signedBallotAsByteArray, commission.BallotEncryptionKey));

    var result = commission.AcceptBallot(encryptedSignedBallot, signatureProvider, encryptionProvider, objectToByteArrayTransformer);

    if (!result.IsSuccess)
    {
        PrintError(result);
    }
}

static void SendDoubleBallot(Voter voter, int candidateId, CentralElectionCommission commission, ISignatureProvider signatureProvider, IEncryptionProvider encryptionProvider, IObjectToByteArrayTransformer objectToByteArrayTransformer)
{
    var ballot = voter.GenerateBallot(candidateId, commission.BallotEncryptionKey, signatureProvider, encryptionProvider, objectToByteArrayTransformer); ;

    var result = commission.AcceptBallot(ballot, signatureProvider, encryptionProvider, objectToByteArrayTransformer);

    if (!result.IsSuccess)
    {
        PrintError(result);
    }
}

static void PrintError(Result result)
{
    Console.WriteLine($"Error: {string.Join(" ", result.Errors.Select(e => e.Message))}");
}
