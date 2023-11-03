using Algorithms.Abstractions;
using Modelling.Models;

namespace Demo;
public sealed class DemoDataFactory
{
    private readonly ISymmetricKeyGenerator _symmetricKeyGenerator;
    private readonly IAsymmetricKeysGenerator _asymmetricKeysGenerator;

    public DemoDataFactory(ISymmetricKeyGenerator symmetricKeyGenerator, IAsymmetricKeysGenerator asymmetricKeysGenerator)
    {
        _symmetricKeyGenerator = symmetricKeyGenerator;
        _asymmetricKeysGenerator = asymmetricKeysGenerator;
    }

    public IReadOnlyList<Candidate> CreateCandidates()
    {
        return new List<Candidate>
        {
            new Candidate(1, "Ishaan Allison"),
            new Candidate(2, "Oliver Mendez"),
            new Candidate(3, "Naomi Winter"),
        };
    }

    public IReadOnlyList<Voter> CreateVoters()
    {
        return new List<Voter>
        {
            new Voter(1, "Jasper Lambert", 17, true, _asymmetricKeysGenerator),     // Too young.
            new Voter(2, "Jonty Levine", 22, false, _asymmetricKeysGenerator),      // Not capable.
            new Voter(3, "Nathaniel Middleton", 35, true, _asymmetricKeysGenerator),
            new Voter(4, "Nathan Bass", 43, true, _asymmetricKeysGenerator),
            new Voter(5, "Aran Doyle", 21, true, _asymmetricKeysGenerator),
            new Voter(6, "Julian Harper", 19, true, _asymmetricKeysGenerator),
            new Voter(7, "Lucian Gross", 20, true, _asymmetricKeysGenerator),
        };
    }

    public CentralElectionCommission CreateCentralElectionCommission(IReadOnlyList<Candidate> candidates, IReadOnlyList<Voter> voters)
    {
        return new CentralElectionCommission(candidates, voters, _symmetricKeyGenerator);
    }

    public Dictionary<Voter, int> CreateVotersWithCandidateIds(IReadOnlyList<Voter> voters)
    {
        var dictionary = new Dictionary<Voter, int>();
        foreach (var voter in voters)
        {
            var candidateId = (voter.Id % 8) switch
            {
                1 => 1,
                2 => 1,

                3 => 2,
                4 => 1,
                5 => 3,
                6 => 3,
                7 => 3,

                _ => throw new InvalidOperationException("Negative and zero voters' ids are not supported in this method.")
            };
            dictionary.Add(voter, candidateId);
        }
        return dictionary;
    }
}
