namespace Ip_Calculator;
class Get_info{
    public static void Main()
    {
        int network_c = network_count();
        var (ip, hosts, network_index) = ip_and_hosts(network_c);
        Calculate.Calculate_IP(ip,hosts,network_index);
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
            ip_splitted = [];
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
            for (int j = i + 1,w = hostok.Count; j < w; j++)
            {
                if (hostok[i] < hostok[j])
                {
                    (hostok[i], hostok[j]) = (hostok[j], hostok[i]);
                    (networkIndex[i], networkIndex[j]) = (networkIndex[j], networkIndex[i]);
                }
            }
        }
        return (ip_splitted,hostok,networkIndex);
    }
    

    public static int network_count()
    {
        Console.Write("Enter the amount of networks: ");
        try { 
            int amount = int.Parse(Console.ReadLine());
            return amount;
        } 
        catch {
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
    class Calculate
    {
        public static List<int> szamok = new List<int> { 4, 8, 16, 32, 64, 128, 256, 512 };
        public static List<int> closest_num = new List<int>();
        public static List<int> ip_fourth_int = new List<int>();
        public static List<int> masks = new List<int>();
        public static void Calculate_IP(string[] ip, List<int> hosts, List<int> netw_index)
        {
            
            for (int i = 0, w = hosts.Count; i < w; i++)
            {
                foreach (int j in szamok) {
                    if (j >= hosts[i] + 2) {
                        closest_num.Add(j);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            calculate_mask();
            calculate_ip_address(ip);

        }
        
        private static void calculate_mask()
        {
            int[] masks_list = { 0, 128, 192, 224, 240, 248, 252, 254, 255 };
            for (int i = 0, w = closest_num.Count; i < w; i++)
            {
                int hostbits = (int)Math.Round(Math.Log2(closest_num[i]));
                int prefix_l = 32 - hostbits;
                int index = prefix_l - 24;
                int choosen = masks_list[index];
                masks.Add(choosen);
            }
                
        }
        public static void calculate_ip_address(string[] ip)
        {
            ip_fourth_int.Add(int.Parse(ip[3]));
            for (int i = 0, w = closest_num.Count; i < w; i++)
            {
                ip_fourth_int.Add(ip_fourth_int[i] + closest_num[i]); 
            }
        }

    }
}