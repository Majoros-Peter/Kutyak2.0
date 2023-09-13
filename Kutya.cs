using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Kutyak;

internal class Kutya
{
    private Nevek Nev;
    private Fajtak Fajta;

    public int Id { get; private set; }
    public int EletKor { get; private set; }
    public DateTime UtolsoOrvosiEllenorzes { get; private set; }
    public Nevek GetNev { get => Nev; }
    public Fajtak GetFajta { get => Fajta; }

    private void SetName(int NevId, string Nevek)
    {
        Nev.Id = NevId;
        Nev.KutyaNev = File.ReadAllLines(Nevek).Where(G => G.StartsWith($"{NevId}")).Take(1).ToArray()[0].Split(';')[1];
    }
    private void SetFajta(int FajtaId, string Fajtak)
    {
        string[] temp = File.ReadAllLines(Fajtak).Where(G => G.StartsWith($"{FajtaId}")).Take(1).ToArray()[0].Split(';');

        Fajta.Id = FajtaId;
        Fajta.Nev = temp[1];
        Fajta.EredetiNev = temp[2];
    }

    public Kutya(string[] adatok, string Nevek, string Fajtak)
    {
        Id = Convert.ToInt32(adatok[0]);
        EletKor = Convert.ToInt32(adatok[3]);
        UtolsoOrvosiEllenorzes = DateTime.Parse(adatok[4]);
        
        SetFajta(Convert.ToInt32(adatok[1]), Fajtak);
        SetName(Convert.ToInt32(adatok[2]), Nevek);
    }

    public override string ToString()
    {
        return $"Id: {Id}; Eletkor: {EletKor}; UtolsoOrvosiEllenorzes: {UtolsoOrvosiEllenorzes}";
    }
}
