﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            List<string> osvenyek = OsvenyeketBeolvas("osvenyek.txt");
            List<int> dobasok = DobasokatBeolvas("dobasok.txt");

            Console.WriteLine("2. feladat");
            Console.WriteLine($"A dobások száma: {dobasok.Count}");
            Console.WriteLine($"Az ösvények száma: {osvenyek.Count}");
            Console.WriteLine();

            Console.WriteLine("3. feladat");
            int leghosszabbIndex = LeghosszabbOsvenyIndex(osvenyek);
            Console.WriteLine($"Az egyik leghosszabb az {leghosszabbIndex + 1}. ösvény, hossza: {osvenyek[leghosszabbIndex].Length}");
            Console.WriteLine();

            Console.WriteLine("4. feladat");
            Console.Write("Adja meg egy ösvény sorszámát: ");
            int osvenySzam = int.Parse(Console.ReadLine());
            Console.Write("Adja meg a játékosok számát: ");
            int jatekosSzam = int.Parse(Console.ReadLine());
            string kivalasztottOsveny = osvenyek[osvenySzam - 1];
            Console.WriteLine();

            Console.WriteLine("5. feladat");
            BetuGyakorisagKiirasa(kivalasztottOsveny);

            KivalasztottMezokIrasaFajlba(kivalasztottOsveny, "kulonleges.txt");

            Console.WriteLine();
            Console.WriteLine("6. feladat");
            Console.WriteLine("A fájlbaírás sikeres volt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt: {ex.Message}");
        }
    }

    static List<string> OsvenyeketBeolvas(string fajlUtvonal)
    {
        List<string> osvenyek = new List<string>();
        using (StreamReader sr = new StreamReader(fajlUtvonal))
        {
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                osvenyek.Add(sor);
            }
        }
        return osvenyek;
    }

    static List<int> DobasokatBeolvas(string fajlUtvonal)
    {
        string dobasokSor;
        using (StreamReader sr = new StreamReader(fajlUtvonal))
        {
            dobasokSor = sr.ReadLine();
        }
        return dobasokSor.Split().Select(int.Parse).ToList();
    }

    static int LeghosszabbOsvenyIndex(List<string> osvenyek)
    {
        int leghosszabbIndex = 0;
        for (int i = 1; i < osvenyek.Count; i++)
        {
            if (osvenyek[i].Length > osvenyek[leghosszabbIndex].Length)
            {
                leghosszabbIndex = i;
            }
        }
        return leghosszabbIndex;
    }

    static void BetuGyakorisagKiirasa(string osveny)
    {
        Dictionary<char, int> statisztika = new Dictionary<char, int>();
        foreach (char betu in osveny)
        {
            if (!statisztika.ContainsKey(betu))
            {
                statisztika[betu] = 0;
            }
            statisztika[betu]++;
        }

        foreach (var bejegyzes in statisztika)
        {
            Console.WriteLine($"{bejegyzes.Key}: {bejegyzes.Value} darab");
        }
    }

    static void KivalasztottMezokIrasaFajlba(string osveny, string fajlUtvonal)
    {
        using (StreamWriter sw = new StreamWriter(fajlUtvonal))
        {
            int pozicio = 0;
            foreach (char mezo in osveny)
            {
                pozicio++;
                if (mezo != 'M')
                {
                    sw.WriteLine($"{pozicio}\t{mezo}");
                }
            }
        }
    }
}
