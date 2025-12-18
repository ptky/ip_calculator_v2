using Calculate_Data;
namespace Ip_Calculator;

class Get_info
{
    public static void Main()
    {
        int network_c = network_count();
        var (ip, hosts, network_index) = ip_and_hosts(network_c);
        Calculate.sort_lists(ip, hosts, network_index);
    }

    public static (string[] ip, List<int> hostok, List<int> netw_index) ip_and_hosts(int netw)
    {
        List<int> ip_address = new List<int>();
        Console.Clear();
        Console.Write("Enter IP Address: ");
        string ip = Console.ReadLine();
        string[] ip_splitted = ip.Split(".");
        while (ip_splitted.Length < 4)
        {
            Console.WriteLine("Invalid IP\n");
            Console.Write("Enter IP Address: ");
            ip = Console.ReadLine();
            ip_splitted = new string[0];
            ip_splitted = ip.Split(".");
        }
        List<int> hostok = new List<int>(netw);
        List<int> networkIndex = new List<int>();
        for (int i = 0; i < netw; i++)
        {
            Console.Clear();
            Console.Write($"Enter the amount of hosts you want to fit in in network {i}.: ");
            int mennyiseg = int.Parse(Console.ReadLine());
            hostok.Add(mennyiseg);
            networkIndex.Add(i);
        }
        for (int i = 0, y = hostok.Count; i < y - 1; i++)
        {
            for (int j = i + 1, w = hostok.Count; j < w; j++)
            {
                if (hostok[i] < hostok[j])
                {
                    (hostok[i], hostok[j]) = (hostok[j], hostok[i]);
                    (networkIndex[i], networkIndex[j]) = (networkIndex[j], networkIndex[i]);
                }
            }
        }
        return (ip_splitted, hostok, networkIndex);
    }


    public static int network_count()
    {
        Console.Write("Enter the amount of networks: ");
        try
        {
            int amount = int.Parse(Console.ReadLine());
            return amount;
        }
        catch
        {
            int amount = 0;
            while (amount <= 0)
            {
                Console.Clear();
                Console.WriteLine("Invalid number \n");
                Console.Write("Enter the amount of networks: ");
                try
                {
                    amount = int.Parse(Console.ReadLine());
                    return amount;
                }
                catch
                {
                    continue;
                }
            }
        }
        return 0;

    }
}