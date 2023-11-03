using Algorithms.Common;
using Algorithms.RSASignature;
using Algorithms.XorEncryption;
using Demo;

var xorKeyGenerator = new XorEncryptionKeyGenerator();
var xorEncryptionProvider = new XorEncryptionProvider();
var rsaKeysGenerator = new RSASignatureKeysGenerator();
var rsaSignatureProvider = new RSASignatureProvider();
var objectToByteArrayTransformer = new ObjectToByteArrayTransformer();
var randomProvider = new RandomProvider();

var factory = new DemoDataFactory(xorKeyGenerator, rsaKeysGenerator);
var candidates = factory.CreateCandidates();
var voters = factory.CreateVoters();
var commission = factory.CreateCentralElectionCommission(candidates, voters);

var printer = new ModellingPrinter(xorEncryptionProvider, rsaKeysGenerator, rsaSignatureProvider, objectToByteArrayTransformer);

printer.PrintUsualVoting(commission, factory.CreateVotersWithCandidateIds(voters));
printer.PrintVotingWithIncorrectBallot(commission);
printer.PrintVotingWithBallotSignedByThirdParty(commission, randomProvider.NextItem(candidates).Id, randomProvider.NextItem(voters.Skip(2)).Id);
printer.PrintVotingWithDoubleBallot(commission, randomProvider.NextItem(candidates).Id, randomProvider.NextItem(voters.Skip(2)));
Console.WriteLine();

Console.WriteLine("Results:");
printer.PrintVotingResults(commission);

Console.WriteLine();
printer.PrintVotingAfterCompletion(commission, randomProvider.NextItem(candidates).Id, randomProvider.NextItem(voters));

Console.WriteLine();
