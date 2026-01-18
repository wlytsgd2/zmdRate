using System.IO;

namespace zmdRate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] P = new double[80];
            for (int i = 0; i < 65; i++)
            {
                P[i] = 0.008;
            }
            for (int i = 65; i < 79; i++)
            {
                P[i] = P[i - 1] + 0.05;
            }
            P[79] = 1.0;

            int n;//每个版本的抽数
            Console.WriteLine("请输入每个池子可获得的抽数：");
            n = int.Parse(Console.ReadLine());

            double current = 0;//当前六星数量
            int dianchou = 0;//当前单抽数
            int allchou = 0;//总抽数
            int dangqichou = 0;//当前版本抽数
            int shoutouchou = 50;//当前剩余抽数,初始值不为零是因为开服多点
            double banben = 0;//版本数


            bool ifnot_exceed30;
            bool ifnot_exceed60 = true;
            bool if_chudangqi = false;
            Random rand = new Random();
            while (banben < 10000000)
            {
                banben++;
                //Thread.Sleep(10);
                dangqichou = 0;
                shoutouchou += n;
                if (ifnot_exceed60 == false)
                {
                    ifnot_exceed60 = true;
                    if (shoutouchou + 10 < 120)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            dianchou++;
                            if (rand.NextDouble() <= P[dianchou - 1])
                            {
                                dianchou = 0;
                                if (rand.NextDouble() <= 0.5)
                                {
                                    current++;
                                }
                            }
                        }
                        //Console.WriteLine($"{10} {shoutouchou} {current}");
                        continue;
                    }
                    else shoutouchou += 10;
                }
                else if (shoutouchou < 120)
                {
                    //Console.WriteLine($"{0} {shoutouchou} {current}");
                    continue;
                }

                ifnot_exceed30 = true;
                if_chudangqi = false;
                while (!if_chudangqi)
                {
                    allchou++;//总抽数加一
                    dianchou++;
                    dangqichou++;
                    shoutouchou--;
                    if (dangqichou == 120)
                    {

                        dianchou = 0;
                        current++;
                        break;
                    }

                    if (rand.NextDouble() <= P[dianchou - 1])//判断是否抽到
                    {
                        dianchou = 0;
                        if (rand.NextDouble() <= 0.5)
                        {
                            current++;
                            if_chudangqi = true;
                        }
                    }

                    if (ifnot_exceed30 && dangqichou == 30)
                    {
                        ifnot_exceed30 = false;
                        for (int i = 0; i < 10; i++)
                        {
                            if (rand.NextDouble() <= 0.004)
                            {
                                current++;
                                if_chudangqi = true;
                            }
                        }
                    }

                    if (ifnot_exceed60 && dangqichou == 60)
                    {
                        ifnot_exceed60 = false;
                    }
                }
                //Console.WriteLine($"{dangqichou} {shoutouchou} {current}");
            }
            Console.WriteLine(current / banben);
            Console.WriteLine(shoutouchou);
        }
    }
}
