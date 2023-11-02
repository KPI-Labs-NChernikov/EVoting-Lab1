using Modelling.Models;

namespace Domain.Extensions;
public static class LinqExtensions
{
    public static IOrderedEnumerable<Candidate> OrderByVotes(this IEnumerable<Candidate> candidatesVotingResults)
        => candidatesVotingResults.OrderByDescending(c => c.Votes);
}
