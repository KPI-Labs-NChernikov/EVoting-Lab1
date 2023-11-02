using Application.Abstractions.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities;
public class DemoDataGenerator
{
    private readonly IVotingRepository _votingRepository;
    private readonly IVoterRepository _voterRepository;

    public DemoDataGenerator(IVotingRepository votingRepository, IVoterRepository voterRepository)
    {
        _votingRepository = votingRepository;
        _voterRepository = voterRepository;
    }

    public void Generate()
    {
        var voters = new List<Voter>
        {
            new Voter("Ivan Ivanov", 17, true),
            new Voter("Petro Petrenko", 21, false),
            new Voter("Sydir Sydorovych", 25, true),
            new Voter("Yurii Gural", 30, true),
            new Voter("Illia Berezhko", 40, true),
            new Voter("Yana Blazhkevych", 33, true),
            new Voter("Anna Chernenko", 19, true),
            new Voter("Yuliia Trehub", 21, true)
        };
        foreach (var voter in voters)
        {
            _voterRepository.Add(voter);
        }

        var candidates = new List<Candidate>()
        {
            new Candidate("Makar Kushnirov"),
            new Candidate("Iryna Losko"),
            new Candidate("Emil Herasymiv")
        };
    }
}
