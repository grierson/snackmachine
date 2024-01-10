namespace SnackMachine.Domain;

public class Slot : Entity
{
    public int Position { get; }
    public SnackPile SnackPile { get; set; }

    public Slot(int position)
    {
        Position = position;
        SnackPile = SnackPile.Empty;
    }
}

