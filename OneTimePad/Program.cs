using System;
using System.Data;
using System.Globalization;
using System.IO;

class Program
{
    static char Caesar(char plain, int shift)
    {
        int asci = (int)plain;
        if ((asci >= 65 && asci <= 90) || (asci >= 97 && asci <= 122))
        {
            if (shift < 0)
            {
                if ((asci >= 65 && asci <= 64-shift) || (asci >= 97 && asci <= 98-shift))
                {
                    return (char)(asci+26+shift);
                }
                return (char)(asci+shift);
            }
            if (shift > 0)
            {
                if ((asci <= 90 && asci >= 91-shift) || (asci >= 123-shift && asci <= 122))
                {
                    return (char)(asci-26+shift);
                }
                return (char)(asci+shift);
            }
            return plain;
        }
        return plain;
    }
    static void Main(string[] args)
    {
        Random random = new Random();
        Console.Write("Encode or decode (e or d): ");
        bool encrypt = Console.ReadLine() == "e";

        string plain;
            if (encrypt)
            {
                Console.Write("Enter the plaintext: ");
                plain = Console.ReadLine();
            }
            else
            {
                Console.Write("Enter the ciphertext: ");
                plain = Console.ReadLine();
            }

            List<int> key = new List<int>();

            bool own = false;
            if (encrypt)
            {
                Console.Write("Enter your own key or a random key (o or r): ");
                own = Console.ReadLine() == "o";
            }

            if (own || !encrypt)
            {
                bool flag = false;
                while (!flag)
                {
                    key.Clear();
                    flag = true;
                    Console.Write("Enter the key (numbers separated by commas): ");
                    try
                    {
                        string[] temp = Console.ReadLine().Split(',');
                        int i = -1;
                        while(flag && i < temp.Length-1)
                        {
                            i++;
                            try
                            {
                                key.Add(Convert.ToInt16(temp[i]));
                                if (key[i] < 0 || key[i] > 25)
                                {
                                    flag = false;
                                }
                            }
                            catch
                            {
                                flag = false;
                            }
                        }
                    }
                    catch
                    {
                        flag = false;
                    }

                    if (!flag)
                    {
                        Console.WriteLine("(numbers 0 to 25)");
                    }
                }
            }
            else
            {
                for (int i = 0; i < plain.Length; i++)
                {
                    key.Add(random.Next(0, 26));
                }
            }

            if (!encrypt)
            {
                for(int i = 0; i < key.Count; i++)
                {
                    key[i] = -key[i];
                }
            }
            
            if (encrypt)
                Console.Write("The ciphertext is: ");
            else
                Console.Write("The plaintext is: ");

            for (int i = 0; i < plain.Length; i++)
            {
                char c = plain[i];
                if (((int)c >= 65 && (int)c <= 90) || ((int)c >= 97 && (int)c <= 122))
                {
                    Console.Write(Caesar(c, key[i%key.Count]));
                }
                else
                {
                    Console.Write(c);
                }
            }

            Console.WriteLine();

            if (!own && encrypt)
            {
                Console.Write("The key is: ");
                for (int i = 0; i < key.Count-1; i++)
                {
                    Console.Write(key[i] + ",");
                }
                Console.Write(key[key.Count-1] + "\n");
            }
    }
}