namespace Core;

public class Season
{
    private Season(){}
    public Season(string banner, uint num){
        Banner = banner;
        Num = num;

        if(string.IsNullOrEmpty(Banner))
            throw new FieldIsNullOrEmptyException(nameof(Banner), nameof(Season));
    }

    public uint Num { get; set; }

    public List<Seria> Serias { get; set; } = new List<Seria>();

    public string Banner { get; set; } = null!;
}
