namespace Core;

public class Seria
{
    private Seria(){}
    public Seria(uint num, string idFile){
        Num = num;
        IdFile = idFile;

        if(string.IsNullOrEmpty(IdFile))
            throw new FieldIsNullOrEmptyException(nameof(IdFile), nameof(Seria));
    }

    public uint Num { get; set; }

    public string IdFile {  get; set; } = null!;
}
