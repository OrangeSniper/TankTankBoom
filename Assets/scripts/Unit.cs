public enum tankTeam { blue, red };

[System.Serializable]
public class Unit
{
    public tankTeam team;

    public int HP;

    public int speed;
}