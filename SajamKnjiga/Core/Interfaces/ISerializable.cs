namespace Core.Interfaces;

//Source: OISISI exercises 3
public interface ISerializable
{
    string[] ToCSV();

    void FromCSV(string[] values);
}