# EVotingLab1
This project is **a model of the simple e-voting protocol** that utilizes **RSA for signing** and **XOR cipher for encryption**. You can experiment with it just by forking and changing the source code.  
The simple e-voting protocol has the next characteristics:
- Only those who have the right to vote can vote;
- Each voter can vote only once;
- Strangers cannot find out who voted for which candidate;
- Central Election Commission knows who voted for whom and should be trusted;
- No one can change the vote;
- Voter cannot see or check their vote, they will see only the final results.
  
The flow of the simple e-voting protocol is the next:
- Lists of candidates and voters are formed;
- Each voter chooses the candidate, votes and signs the ballot with their private RSA key;
- Each voter encrypts their ballot with the XOR key of the Central Election Commission;
- Central Election Commission decrypts the ballots, checks signatures and publishes the results of voting.

**Warning**  
XOR cipher is used here only for demonstration purposes and it should not be used in real e-voting systems. This algorithm is symmetric and all voters have access to the key, so, ballots can be easily decrypted and read in case of interception.
