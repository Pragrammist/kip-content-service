namespace Core;

public class Seria
{
    private Seria(){}
    public Seria(uint num, string idFile){
        Num = num;
        IdFile = idFile;
    }

    public uint Num { get; set; }

    public string IdFile {  get; set; } = null!;
}
