using Algorithms.Common;
using Algorithms.RSASignature;
using Algorithms.XorEncryption;
using Modelling.Models;

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
    new Voter(1, "Jasper Lambert", 17, true, rsaKeysGenerator),
    new Voter(2, "Jonty Levine", 22, false, rsaKeysGenerator),
    new Voter(3, "Nathaniel Middleton", 35, true, rsaKeysGenerator),
    new Voter(4, "Nathan Bass", 43, true, rsaKeysGenerator),
    new Voter(5, "Aran Doyle", 21, true, rsaKeysGenerator),
    new Voter(6, "Julian Harper", 19, true, rsaKeysGenerator),
    new Voter(7, "Lucian Gross", 20, true, rsaKeysGenerator),
};
var commission = new CentralElectionCommission(candidates, voters, xorKeyGenerator);

foreach (var voter in voters)
{
    var ballot = voter.GenerateBallot(random.Next(1, candidates.Count + 1), commission.BallotEncryptionKey, rsaSignatureProvider, xorEncryptionProvider, objectToByteArrayTransformer);
    var result = commission.AcceptBallot(ballot, rsaSignatureProvider, xorEncryptionProvider, objectToByteArrayTransformer);

    if (result.IsSuccess)
    {
        Console.WriteLine($"{voter.FullName} voted successfully.");
    }
    else
    {
        Console.WriteLine($"Error: {string.Join(Environment.NewLine, result.Errors)}");
    }
}

Console.WriteLine();
