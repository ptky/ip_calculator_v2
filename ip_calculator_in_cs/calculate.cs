using System;
using System.Collections.Generic;
using System.Text;

namespace Calculate_Data
{
    class Calculate
    {
        public static List<int> szamok = new List<int> { 4, 8, 16, 32, 64, 128, 256, 512 };
        public static List<int> closest_num = new List<int>();
        public static List<int> masks = new List<int>();
        public static void sort_lists(string[] ip, List<int> hosts, List<int> netw_index)
        {
            closest_num.Clear();
            masks.Clear();
            for (int i = 0, w = hosts.Count; i < w; i++)
            {
                foreach (int j in szamok)
                {
                    if (j >= hosts[i] + 2)
                    {
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
            calculate_everything(ip, netw_index);

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
        public static void calculate_everything(string[] ip, List<int> netw_index)
        {
            int start_ip = int.Parse(ip[3]);
            int third_ip = int.Parse(ip[2]);
            string design = "----------------------------------------";
            Console.WriteLine();
            File.WriteAllText("ipcalculation.txt", "Made by Pataky\n\n");
            for (int i = 0, w = closest_num.Count; i < w; i++)
            {
                string data = $"{design}\n{netw_index[i]}. network\nClosest Number to hosts: {closest_num[i]}\nIP Address = 192.168.{third_ip}.{start_ip}\n\nMask = 255.255.255.{masks[i]}\nFirst Usable = 192.168.{third_ip}.{start_ip + 1} \nGateway = 192.168.{third_ip}.{start_ip + closest_num[i] - 2} \nBroadcast = 192.168.{third_ip}.{start_ip + closest_num[i] - 1} \n";
                File.AppendAllText("ipcalculation.txt", data);
                start_ip += closest_num[i];
            }
            File.AppendAllText("ipcalculation.txt", design);
            Console.Clear();
            Console.WriteLine("All calculations have been saved to 'ipcalculation.txt'.\nPress any key to exit.");
            Console.ReadKey();
        }

    }
}

