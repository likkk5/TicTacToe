namespace BlazorTicTacToe.Shared;

public class GameRoom(string roomId, string roomName)
{
    public string RoomId { get; set; } = roomId;
    public string RoomName { get; set; } = roomName;
    public List<Player> PlayerList { get; set; } = new();
    public TicTacToeGame Game { get; set; } = new();

    public bool TryAddPlayer(Player newPlayer)
    {
        if (PlayerList.Count < 2 && !PlayerList.Any(p => p.ConnectionId == newPlayer.ConnectionId))
        { 
            PlayerList.Add(newPlayer);

            if (PlayerList.Count == 1)
            { 
                Game.PlayerXId = newPlayer.ConnectionId;
            }
            else if (PlayerList.Count == 2)
            {
                Game.PlayerOId = newPlayer.ConnectionId;
            }

            return true;
        }

        return false;
    }
}
