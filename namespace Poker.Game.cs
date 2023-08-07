namespace Poker.Game
{
public abstract class PokerTable {

    /*
     * Represents first bet size.
     * It is a positive number.
     */ 
    protected readonly Decimal SmallBlind;

    /*
     * Represents second bet size.
     * Is atleast two times bigger than smallBlind.
     * And it is positive number.
     */
    protected readonly Decimal BigBlind;

    /*
     * Maximum amount of players at table.
     */ 
    private readonly uint MaxPlayers;

    /*
     * Contains all players who are in current hand(not folded, not waiting).
     * If player folds/leaves then he is removed from this list.
     */
    protected CircularList<Player> playersInHand;

    /*
     * Contains all players who are sitting at table either waiting or playing a hand.
     * key is seat at which player is seated,
     * value is Player who is sitting there.
     */
    private Dictionary<int, Player> playersAtTable = new Dictionary<int, Player>();

    /*
     * Creates basic game instance with setting blind and max player count.
     */
    public PokerTable(Decimal smallBlind, Decimal bigBlind, uint maxPlayers)
    {
        if (bigBlind / 2 < smallBlind) throw new ArgumentException("Big blind must be twice as big as small blind");
        if (smallBlind <= 0) throw new ArgumentException("Small blind must be positive");
        if (bigBlind <= 0) throw new ArgumentException("Big blind must be positive");

        SmallBlind = smallBlind;
        BigBlind = bigBlind;
        MaxPlayers = maxPlayers;
    }

    /*
     * Seats player at table, it doesn't mean he will be in the hand.
     * He might be in the next hand.
     */ 
    public void addPlayer(int seat, Player player)
    {
        if(seat < 0 || seat >= MaxPlayers) throw new ArgumentException("Player can sit only at [0;" + (MaxPlayers - 1) + "]");

        playersAtTable[seat] = player;
    }

    /*
     * This method starts Game: Deals cards and players
     * can make their bets/folds etc.
     * 
     * If there is less then 2 players the exception will be thrown
     * since one player can't play this game.
     */ 
    public void StartHand()
    {
        if(playersAtTable.Count < 2) throw new LackOfPlayersException();

        //Moves players sitting at table to list of players who are playing a hand
        List<Player> playersAtTable0 = new List<Player>();
        foreach (var entry in playersAtTable)
        {
            playersAtTable0.Add(entry.Value);
        }
        playersInHand = new CircularList<Player>(playersAtTable0);

        StartAction();
    }

    /*
     * This method is called when game can start and playersInHand are set.
     */ 
    protected abstract void StartAction();


    public string GetTableState()
    {
        string state = "";
        foreach (var entry in playersAtTable)
        {
            state += entry.Value.ToString() + "\n";
        }
        return state;
    }
}

public class LackOfPlayersException : Exception { }
}